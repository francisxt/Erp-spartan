using BusinesLogic.Interfaces.Alerts;
using BusinesLogic.Repository.Services;
using Models.Contexts;
using Models.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinesLogic.Services.Alerts
{
    public class AlertService : BaseRepository<Alert> , IAlertService
    {
        public AlertService(ApplicationDbContext dbContext) :base(dbContext)
        {

        }
    }
}
