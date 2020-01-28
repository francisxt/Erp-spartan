using BusinesLogic.Repository.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces
{
    public interface IMovementService : IBaseRepository<Movement>
    {
        decimal TotalDebs(Guid clientUserId);
        int CountOfDebs(Guid clientUserId);
        int CountOfPayment(Guid clientUserId);
        Task<bool> SoftDelete(Guid id);
        /// <summary>
        /// Payment all debs
        /// </summary>
        /// <param name="id">is a ClientUserId</param>
        /// <returns></returns>
        Task<bool> PayAll(Guid clientUserId);
    }
}
