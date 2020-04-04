using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models.HiAccounting.Debs
{
    public class Deb : CommonsProperty
    {
        public DateTime DateOfPayment { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal PaymentCapital { get; set; }
        [Required]
        public decimal Interest { get; set; }
        [Required]
        public decimal ToPay { get; set; }
        public Guid LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}
