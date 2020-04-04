using BusinesLogic.Interfaces;
using BusinesLogic.Interfaces.HiAccouting;
using BusinesLogic.Interfaces.HiLoans;
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
        IInventaryService InventaryService { get; }
        IEnterpriseService EnterpriseService { get; }
        ILoanService LoanService { get; }
        Task Commit();
    }
}
