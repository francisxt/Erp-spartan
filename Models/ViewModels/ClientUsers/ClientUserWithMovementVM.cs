using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.ClientUsers
{
    public class ClientUserWithMovementVM
    {
        public decimal Total { get; set; }
        public int Debs { get; set; }
        public int Payments { get; set; }
        public ClientUser Client { get; set; }
    }
}
