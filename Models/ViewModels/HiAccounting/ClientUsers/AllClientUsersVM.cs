using Models.Models;
using Models.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.HiAccounting.ClientUsers
{
    public class AllClientUsersVM
    {
        public IEnumerable<ClientUser> Clients { get; set; }
        public IEnumerable<Enterprise> Enterprises { get; set; }
    }
}
