using Models.Enums;
using Models.Models;
using Models.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels.ClientUsers
{
    public class CreateUserViewModel : User
    {
        [Required]
        public RolsAuthorization Rol { get; set; }
        [Required]
        public Guid EnterpriseId { get; set; }
        public IEnumerable<Enterprise> Enterprises { get; set; }
    }
}
