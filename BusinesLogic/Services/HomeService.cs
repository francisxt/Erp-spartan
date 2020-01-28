using BusinesLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _dbContext;
        public HomeService(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public HomeVM Get(string id)
        {
            return new HomeVM { Clients = _dbContext.ClientUsers.Count(x => x.CreatedBy == id), Articles = _dbContext.Articles.Count(x => x.UserId == id) };
        }
    }
}
