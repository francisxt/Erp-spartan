using Models.Models.HiAccounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models.HiLoans
{
    public class ReclosingHistory : CommonsProperty
    {
        [Required]
        public decimal Amount { get; set; }
        public Guid IdLoan { get; set; }
        public Loan Loan { get; set; }
    }
}
