using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Enums.HiLoans
{
    public enum RateType
    {
        [Display(Name = "Mensual")]
        Month,
        [Display(Name = "Anual")]
        Anual

    }
}
