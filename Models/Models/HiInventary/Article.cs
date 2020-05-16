using Commons.Helpers;
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
        [Display(Name = "Nombre o Titulo")]
        public string Name { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public string Code { get; set; } = $"{nameof(Code)}{StringHelper.GetRandomCode(5)}";
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PriceForShop { get; set; } = 0;
        [Required(ErrorMessage = "La cantidad es requerida")]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        [Display(Name = "Comentario o Descripción")]
        public string Description { get; set; }
    }
}
