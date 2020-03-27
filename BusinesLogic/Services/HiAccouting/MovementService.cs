using BusinesLogic.Interfaces;
using BusinesLogic.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Models.Contexts;
using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class MovementService : BaseRepository<Movement>, IMovementService
    {
        private readonly ApplicationDbContext _dbContext;
        public MovementService(ApplicationDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

        public int CountOfDebs(Guid clientUserId) => GetAll().Count(x => x.ClientUserId == clientUserId && x.Type == TypeOfMovement.Deb);

        public int CountOfPayment(Guid clientUserId) => GetAll().Count(x => x.ClientUserId == clientUserId && x.Type == TypeOfMovement.Payment);
        public decimal TotalDebs(Guid clientUserId) => GetAll().Where(x => x.ClientUserId == clientUserId).Sum(x => x.Amount);

        public async Task<bool> SoftDelete(Guid id)
        {
            var model = await GetById(id);
            if (model != null)
            {
                model.UpdateAt = DateTime.Now;
                model.State = State.Removed;

                return await Update(model);
            }
            return false;
        }

        public async Task<bool> PayAll(Guid id)
        {
            var model = await GetAll().Where(x => x.ClientUserId == id && x.State == State.Active).ToListAsync();
            model.ForEach(x =>
            {
                x.UpdateAt = DateTime.Now;
                x.State = State.Payment;
            });
            _dbContext.UpdateRange(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
