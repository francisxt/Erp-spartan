using Models.Enums.HiAccounting;
using Models.Models.HiAccounting.Debs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models.HiAccounting
{
    /// <summary>
    /// Prestamo
    /// </summary>
    public class Loan : CommonsProperty
    {
        //User to Create loans
        public string UserId { get; set; }
        public User User { get; set; }
        public decimal InitialCapital { get; set; }
        public AmortitationType AmortitationType { get; set; }
        public PaymentModality PaymentModality { get; set; }
        public int Interest { get; set; }
        public Guid ClientUserId { get; set; }
        public ClientUser ClientUser { get; set; }
        public virtual IEnumerable<Deb> Debs { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}
