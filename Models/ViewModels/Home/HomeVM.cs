using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.Home
{
    public class HomeVM
    {
        #region HIACCOUNTING
        public int Clients { get; set; }
        public int Articles { get; set; }
        public int Enterprices { get; set; }
        public decimal TotalOfDebs { get; set; }
        #endregion

    }
}
