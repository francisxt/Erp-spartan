using BusinesLogic.Interfaces;
using BusinesLogic.Repository.Services;
using Models.Contexts;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinesLogic.Services
{
    public class ClientService : BaseRepository<Client>, IClientService
    {
        private readonly ApplicationDbContext _context;
        public ClientService(ApplicationDbContext context) : base(context) => _context = context;
    }
}
