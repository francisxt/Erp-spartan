using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Enums
{
    public enum TypeOfMovement
    {
        [Display(Name = "Pago")]
        Payment,
        [Display(Name = "Deuda")]
        Deb
    }
}
