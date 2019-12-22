using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class CommonsProperty
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public State State { get; set; } = State.Active;

    }
}
