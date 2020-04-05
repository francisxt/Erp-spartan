using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Enums.HiAccounting
{
    public enum PaymentModality
    {
        [Display(Name = "DIARIO")]
        Daily,
        [Display(Name = "SEMANAL")]
        Week,
        [Display(Name = "MENSUAL")]
        Month,
        [Display(Name = "ANUAL")]
        Years
    }
}
