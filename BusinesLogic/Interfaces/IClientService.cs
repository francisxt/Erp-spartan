using BusinesLogic.Repository.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinesLogic.Interfaces
{
    public interface IClientService : IBaseRepository<Client>,IQuerableRepository<Client>
    {
    }
}
