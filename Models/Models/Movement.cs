using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models
{
    public class Movement : CommonsProperty
    {
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Required]
        public Guid ClientUserId { get; set; }
        public ClientUser Client { get; set; }
        [Required(ErrorMessage = "EL CAMPO Monto ES REQUERIDO")]
        [Display(Name = "Monto")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public TypeOfMovement Type { get; set; } = TypeOfMovement.Deb;

    }
}
