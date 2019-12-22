using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class ClientUser : CommonsProperty
    {
        public string EnterpriseName { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Client> SubClients { get; set; }
    }
}
