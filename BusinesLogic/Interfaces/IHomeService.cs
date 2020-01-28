using Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interfaces
{
    public interface IHomeService
    {
        /// <summary>
        /// GET INFO TO HOME BY USERID
        /// </summary>
        /// <param name="id">USER ID</param>
        /// <returns></returns>
        HomeVM Get(string id);
    }
}
