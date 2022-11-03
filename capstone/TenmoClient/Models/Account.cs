using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
        public class Account
        {
            public int AccountId { get; set; } // properties from account table
            public int UserId { get; set; }
            public decimal Balance { get; set; }
        }
    
}
