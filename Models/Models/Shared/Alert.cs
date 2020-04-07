using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models.Shared
{
    public class Alert : CommonsProperty
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
