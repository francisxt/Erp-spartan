using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Enums
{
    public enum RolsAuthorization
    {
        Admin,
        #region HiAccounting
        ClientsUser,
        Client,
        #endregion
        #region HIInventory
        HiInventory
        #endregion
    }
}
