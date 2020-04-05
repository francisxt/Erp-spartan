using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Others
{
    public static class ControllersRol
    {
        public const string ClientUser = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser) +  "," + nameof(RolsAuthorization.HILoans);
        public const string EnterPrise = nameof(RolsAuthorization.ClientsUser) + "," + nameof(RolsAuthorization.HILoans);
    }
}
