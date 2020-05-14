using Models.Models.HiAccounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models.HiLoans
{
    public class ReclosingHistory : CommonsProperty
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public Guid IdRenclosingLoan { get; set; }
        public Guid IdLoan { get; set; }
        public Loan Loan { get; set; }
    }
}
