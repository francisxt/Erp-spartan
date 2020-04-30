using BusinesLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _dbContext;
        public HomeService(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<HomeVM> Get(string id)
        {
            var clients = await _dbContext.ClientUsers.Include(x => x.Movements)
                .Where(x => x.CreatedBy == id && x.State == Models.Enums.State.Active).ToListAsync();

            var loans = await _dbContext.Loans.Include(x => x.Debs)
                .Where(x => x.UserId == id && x.State == Models.Enums.State.Active).ToListAsync();
          
            decimal totalOfDebs = 0;
            decimal totalLoansDebs = 0;
            foreach (var item in clients) totalOfDebs += item.Movements.Sum(x => x.Amount);
            foreach (var item in loans) totalLoansDebs += item.ActualCapital;


            return new HomeVM
            {
                Clients = _dbContext.ClientUsers.Count(x => x.CreatedBy == id),
                Articles = _dbContext.Articles.Count(x => x.UserId == id),
                Enterprices = _dbContext.Enterprises.Count(x => x.UserId == id),
                TotalOfDebs = totalOfDebs,
                TotalOfLoansDebs = Math.Round(totalLoansDebs,2),
                TotalOfLoans = loans.Count()
            };
        }
    }
}
