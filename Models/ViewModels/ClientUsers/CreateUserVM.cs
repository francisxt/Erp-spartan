using Models.Enums;
using Models.Models;
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
    }
}
