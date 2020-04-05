using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models.HiAccounting
{
    public class Payment : CommonsProperty
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Charges { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Descount { get; set; }
        public string Comments { get; set; }

    }
}
