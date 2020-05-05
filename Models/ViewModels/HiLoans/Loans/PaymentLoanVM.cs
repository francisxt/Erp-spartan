using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.HiLoans.Loans
{
    public class PaymentLoanVM
    {
        public decimal AmortizationTotal { get; set; }
        public Guid IdLoan { get; set; }
        public Guid IdDeb { get; set; }
        public decimal Amortization { get; set; }
        public bool InterestOnly { get; set; }
        public decimal ExtraMount { get; set; }
    }
}
