using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models.Shared
{
    public class Alert : CommonsProperty
    {
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Mensaje")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Usuario")]
        public string UserId { get; set; }
        public User User { get; set; }
        
        [NotMapped]
        public string StateStr => (State == Enums.State.Active) ? "Enviada" : "Vista";
    }
}
