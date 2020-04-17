using Commons.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
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

        public decimal ExtraMount { get; set; }
        public bool IsExtraMount { get; set; } = false;
        public bool AllowPayInterest { get; set; } = false;
        public Guid LoanId { get; set; }
        public Loan Loan { get; set; }

        [NotMapped]
        public string StateStr => State == Enums.State.Payment ? "Pagado" : "Pendiente";
        [NotMapped]
        public string ExtraMountStr => ExtraMount == 0 ? "N/A" : ExtraMount.ToString();
        //Formated property
        [NotMapped]
        public string DateOfPaymentFormated => StringHelper.FormatDate(this.DateOfPayment);

        [NotMapped]
        public string AmountFormated => StringHelper.FormatMoney(this.Amount);

        [NotMapped]
        public string InterestFormated => StringHelper.FormatMoney(this.Interest);

        [NotMapped]
        public string ToPayFormated => StringHelper.FormatMoney((decimal)this.ToPay);

        [NotMapped]
        public string AmortitationFormated => StringHelper.FormatMoney((decimal)this.Amortitation);

        [NotMapped]
        public string EndBalanceFormated => StringHelper.FormatMoney( this.EndBalance);

        [NotMapped]
        public string ExtraMountFormated => StringHelper.FormatMoney(this.ExtraMount);
    }

}
