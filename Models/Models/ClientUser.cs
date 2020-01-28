using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    /// <summary>
    /// A clientUser i was created by another clientUser , how by a admin
    /// </summary>
    public class ClientUser : CommonsProperty
    {
        public string EnterpriseName { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public virtual IEnumerable<Movement> Movements { get; set; }
    }
}
