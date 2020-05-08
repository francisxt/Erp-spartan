using BusinesLogic.Repository.Interfaces;
using Models.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces.Alerts
{
    public interface IAlertService : IBaseRepository<Alert>, IHelperRepository<Alert>
    {
        Task<int> Quantity(string userId);
        Task<bool> MaskAsRead(string userId);

        Task<bool> SendMasive(Alert alert, string id);
        Task<bool> RemoveAll();

    }
}
