using BusinesLogic.Interfaces;
using BusinesLogic.Repository.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBaseRepository<Client> ClientService { get; }
        IClientUserService ClientUserService { get; }

        Task Commit();
    }
}
