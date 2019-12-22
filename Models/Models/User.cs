using Microsoft.AspNetCore.Identity;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class User : IdentityUser
    {
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
    }
}
