using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Enums.HiAccounting
{
    public enum AmortitationType
    {
        [Display(Name = "CUOTA FIJA")]
        Fixedfee,
        [Display(Name = "INTERES FIJO")]
        FixedInterest,
        [Display(Name = "CAPITAL FINAL")]
        CapitalEnd
    }
}
