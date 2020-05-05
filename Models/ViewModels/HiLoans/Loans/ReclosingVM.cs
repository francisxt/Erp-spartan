using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels.HiLoans.Loans
{
    public class ReclosingVM
    {
        public Guid Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
