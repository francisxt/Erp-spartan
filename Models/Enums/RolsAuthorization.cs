using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Enums
{
    public enum RolsAuthorization
    {
        [Display(Name = nameof(Admin))]
        Admin,
        #region HiAccounting
        [Display(Name = "HI CUENTAS")]
        ClientsUser,
        [Display(Name = "CLIENTE")]
        Client,
        #endregion

        #region HIInventory
        [Display(Name = "HI INVENTARIO")]
        HiInventory,
        #endregion

        #region HILoans
        [Display(Name = "HI PRESTAMOS")]
        HILoans
        #endregion
    }
}
