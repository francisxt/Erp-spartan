using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinesLogic.Interfaces;
using BusinesLogic.Repository.Interfaces;
using BusinesLogic.Repository.Services;
using BusinesLogic.Services;
using Models.Contexts;
using Models.Models;

namespace BusinesLogic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private BaseRepository<Client> _clientService;
        private ClientUserService _clientUserService;
        private UserService _userService;

        public UnitOfWork(ApplicationDbContext context) => _context = context;
        public IBaseRepository<Client> ClientService => _clientService ?? (_clientService = new BaseRepository<Client>(_context));

        public IClientUserService ClientUserService => _clientUserService ?? (_clientUserService = new ClientUserService(_context));

        public IUserService UserService => _userService ?? (_userService = new UserService(_context));

        async Task IUnitOfWork.Commit() => await _context.SaveChangesAsync();
    }
}

