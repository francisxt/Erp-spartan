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
        IMovementService MovementsService { get; }
        IClientUserService ClientUserService { get; }
        IUserService UserService { get; }
        IHomeService HomeService { get; }
        Task Commit();
    }
}
