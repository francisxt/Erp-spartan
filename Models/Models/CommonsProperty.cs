using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models
{
    public class CommonsProperty
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public State State { get; set; } = State.Active;

        [NotMapped]
        public string CreatedAtStr => CreateAt.ToShortDateString();
        [NotMapped]
        public string UpdatedAtStr => UpdateAt.ToShortDateString();

    }
}
