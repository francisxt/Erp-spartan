using Models.Enums.HiAccounting;
using Models.Models.HiAccounting.Debs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public decimal InitialCapital { get; set; }
        public AmortitationType AmortitationType { get; set; }
        public PaymentModality PaymentModality { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Interest { get; set; }
        public Guid ClientUserId { get; set; }
        public ClientUser ClientUser { get; set; }
        public virtual IEnumerable<Deb> Debs { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}
