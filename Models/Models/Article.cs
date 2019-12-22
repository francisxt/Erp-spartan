using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models
{
    public class Article : CommonsProperty
    {
        [Required(ErrorMessage = "EL CAMPO {0} ES REQUERIDO")]
        [Display(Name = "Nombre o Descripción")]
        public string Description { get; set; }
        [Required]
        public Guid ClientUserId { get; set; }
        public ClientUser ClientUser { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PriceForShop { get; set; } = 0;
    }
}
