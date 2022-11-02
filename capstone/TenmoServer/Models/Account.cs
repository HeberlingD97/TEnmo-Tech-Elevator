using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Account
    {
        public int AccountId { get; set; } // properties from account table
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
