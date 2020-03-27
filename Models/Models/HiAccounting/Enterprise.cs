using System.ComponentModel.DataAnnotations;

namespace Models.Models.Accounting
{
    public class Enterprise : CommonsProperty
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Name { get; set; }
        
        [Display(Name = "Numero Telefonico")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Dirreción")]
        public string Address { get; set; }
        public string UserId { get; set; }
        public virtual User  User { get; set; }
    }
}
