using Commons.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class Client : CommonsProperty
    {
        public string UserId { get; set; }

        public User User { get; set; }
        [Display(Name = "Cedula")]
        public string IdentificationCard { get; set; }
        [Required(ErrorMessage = "EL CAMPO {0} ES REQUERIDO")]
        [Display(Name = "CODIGO CLIENTE")]
        public string Code { get; set; } = StringHelper.GetRandomCode(5);
        /// <summary>
        /// User Created
        /// </summary>
        [Required]
        public Guid ClientUserId { get; set; }

        public ClientUser ClientUser { get; set; }
        public IEnumerable<Movement> Movements { get; set; }
    }
}
