using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models.HiAccounting
{
    public class Payment : CommonsProperty
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal Charges { get; set; }
        [Required]
        public decimal Descount { get; set; }
        public string Comments { get; set; }

    }
}
