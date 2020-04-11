using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models.HiAccounting.Debs
{
    public class Deb : CommonsProperty
    {
        public DateTime DateOfPayment { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Interest { get; set; }
        [Required]
        public double ToPay { get; set; }
        public int Share { get; set; }
        public double Amortitation { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal EndBalance { get; set; }

        public Guid LoanId { get; set; }
        public Loan Loan { get; set; }

    }
}
