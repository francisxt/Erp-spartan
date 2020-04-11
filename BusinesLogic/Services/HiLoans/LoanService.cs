using BusinesLogic.Interfaces.HiLoans;
using BusinesLogic.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Enums.HiAccounting;
using Models.Enums.HiLoans;
using Models.Models.HiAccounting;
using Models.Models.HiAccounting.Debs;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var debs = GenerateAllDebs(model);
                _dbContext.Debs.AddRange(debs);
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<IEnumerable<Loan>> GetAllWithRelationShip(string userId)
            => await Filter(x => x.UserId == userId)
            .Include(x => x.Debs)
            .Include(x => x.Payments).Include(x => x.ClientUser)
            .ThenInclude(x => x.User).OrderByDescending(x => x.CreateAt).ToListAsync();

        public async Task<Loan> GetByIdWithRelationships(Guid id)
        {
            return await GetAll().Include(x => x.Debs)
                .Include(x => x.Payments)
                .Include(x => x.ClientUser)
                .ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SoftRemove(Guid id)
        {
            var model = await GetById(id);
            model.State = Models.Enums.State.Removed;
            model.UpdateAt = DateTime.Now;
            return await Update(model);
        }

        private IEnumerable<Deb> GenerateAllDebs(Loan loan)
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

        private DateTime GetDateOfPayment(PaymentModality modality,DateTime dateNow)
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
