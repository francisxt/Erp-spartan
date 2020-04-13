using BusinesLogic.Interfaces.HiLoans;
using BusinesLogic.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Enums;
using Models.Enums.HiAccounting;
using Models.Enums.HiLoans;
using Models.Models.HiAccounting;
using Models.Models.HiAccounting.Debs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services.HiLoans
{
    public class LoanService : BaseRepository<Loan>, ILoanService
    {
        public readonly ApplicationDbContext _dbContext;
        public LoanService(ApplicationDbContext dbContext) : base(dbContext)
            => _dbContext = dbContext;

        public override async Task<bool> Add(Loan model)
        {
            _dbContext.Loans.Add(model);
            var result = await _dbContext.SaveChangesAsync() > 0;
            if (result)
            {
                IEnumerable<Deb> debs;
                if (model.AmortitationType == AmortitationType.Fixedfee)
                {
                    debs = FixedfeeDebs(model);
                }
                else if (model.AmortitationType == AmortitationType.FixedInterest)
                {
                    debs = FixedInterestDebs(model);
                }
                else debs = CapitalEndDebs(model);

                _dbContext.Debs.AddRange(debs);
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<IEnumerable<Loan>> GetAllWithRelationShip(string userId)
            => await Filter(x => x.UserId == userId)
            .Include(x => x.Debs)
            .Include(x => x.ClientUser)
            .ThenInclude(x => x.User).OrderByDescending(x => x.CreateAt).ToListAsync();

        public async Task<Loan> GetByIdWithRelationships(Guid id, State state)
        {
            var result = new Loan { };
            result = await GetAll().Include(x => x.ClientUser).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            if (state == State.All) result.Debs = await _dbContext.Debs.Where(x => x.LoanId == id).ToListAsync();
            else result.Debs = await _dbContext.Debs.Where(x => x.LoanId == id && x.State == state).ToListAsync();

            var pendingsDebs = await _dbContext.Debs.CountAsync(x => x.State == State.Active && x.LoanId == id);
            var paymentDebs = await _dbContext.Debs.CountAsync(x => x.State == State.Payment && x.LoanId == id);
            result.SharesStr = $"{paymentDebs} / {pendingsDebs}";
            return result;
        }

        public async Task<bool> PaymentDeb(Guid id)
        {
            var deb = await _dbContext.Debs.FirstOrDefaultAsync(x => x.Id == id);
            if (deb.State == State.Active)
            {
                deb.State = State.Payment;
            }
            else
            {
                deb.State = State.Active;
            }
            _dbContext.Debs.Update(deb);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftRemove(Guid id)
        {
            var model = await GetById(id);
            model.State = State.Removed;
            model.UpdateAt = DateTime.Now;
            return await Update(model);
        }

        private IEnumerable<Deb> FixedfeeDebs(Loan loan)
        {
            double interest = (double)((decimal)loan.Interest) / 100;
            double monthly = interest;
            double shares = loan.Shares;
            decimal balance = loan.InitialCapital;
            if (loan.RateType == RateType.Anual) monthly = interest / 12;
            var payment = (double)loan.InitialCapital * (monthly / (1 - Math.Pow(1 + monthly, -shares)));
            var result = new List<Deb>();
            var nextPayment = DateTime.Now;
            for (int i = 0; i < loan.Shares; i++)
            {
                var interestDeb = balance * (decimal)monthly;
                var deb = new Deb { };
                deb.Share = i + 1;
                nextPayment = GetDateOfPayment(loan.PaymentModality, nextPayment);
                deb.DateOfPayment = nextPayment;
                deb.Amount = balance;
                deb.Interest = interestDeb;
                deb.Amortitation = payment - (double)interestDeb;
                deb.EndBalance = balance - (decimal)deb.Amortitation;
                deb.ToPay = payment;
                deb.LoanId = loan.Id;
                balance -= (decimal)deb.Amortitation;
                result.Add(deb);
            }
            return result;
        }

        private IEnumerable<Deb> FixedInterestDebs(Loan loan)
        {
            double interest = (double)((decimal)loan.Interest) / 100;
            double monthly = interest;
            int shares = loan.Shares;
            decimal balanceFixed = loan.InitialCapital;
            decimal balance = loan.InitialCapital;
            if (loan.RateType == RateType.Anual) monthly = interest / 12;
            var result = new List<Deb>();
            var nextPayment = DateTime.Now;

            for (int i = 0; i < shares; i++)
            {
                var interestdeb = balanceFixed * (decimal)monthly;
                var monthlyPrincipal = balanceFixed / shares;
                nextPayment = GetDateOfPayment(loan.PaymentModality, nextPayment);
                double payment = (double)(monthlyPrincipal + interestdeb);
                var deb = new Deb
                {
                    Share = shares + 1,
                    DateOfPayment = nextPayment,
                    Amount = balance,
                    Interest = interestdeb,
                    Amortitation = (double)monthlyPrincipal,
                    ToPay = payment,
                    EndBalance = balance - monthlyPrincipal,
                    LoanId = loan.Id
                };
                balance -= monthlyPrincipal;
                result.Add(deb);
            }
            return result;
        }

        private IEnumerable<Deb> CapitalEndDebs(Loan loan)
        {
            double interest = (double)((decimal)loan.Interest) / 100;
            double monthly = interest;
            int shares = loan.Shares;
            decimal balance = loan.InitialCapital;
            if (loan.RateType == RateType.Anual) monthly = interest / 12;
            var result = new List<Deb>();
            var nextPayment = DateTime.Now;
            for (int i = 0; i < shares; i++)
            {
                var interestdeb = balance * (decimal)monthly;
                var payment = interestdeb;
                decimal monthlyPrincipal = 0;


                if (i == (shares - 1))
                {
                    monthlyPrincipal = balance;
                    payment = balance + interestdeb;
                }

                var deb = new Deb
                {
                    Share = shares + 1,
                    DateOfPayment = nextPayment,
                    Interest = interestdeb,
                    Amount = balance,
                    Amortitation = (double)monthlyPrincipal,
                    EndBalance = balance - monthlyPrincipal,
                    ToPay = (double)payment,
                    LoanId = loan.Id
                };
                result.Add(deb);
            }
            return result;
        }
        private DateTime GetDateOfPayment(PaymentModality modality, DateTime dateNow)
        {
            var date = dateNow;
            switch (modality)
            {
                case PaymentModality.Daily:
                    date = date.AddDays(1);
                    return date;
                case PaymentModality.Month:
                    date = date.AddMonths(1);
                    return date;
                case PaymentModality.Week:
                    date = date.AddDays(7);
                    return date;
                default: return DateTime.Now.AddYears(1);
            }
        }
    }
}
