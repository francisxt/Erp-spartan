using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Enums
{
    public enum State
    {
        Active,
        Removed,
        Blocked,

        #region For Movements
        Payment,
        #endregion
        #region FOR ALERTS
        View
        #endregion
    }
}
