using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        // TODO: do we need to require any other fields? do we need descriptions for type & status?
        public int TransferId { get; set; }
        public int TransferTypeId { get; set; } = 2;
        public int TransferStatusId { get; set; } = 2;
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        [Range(0.01, 1000000000000, ErrorMessage = "User must transfer a positive amount.")] // TODO: also can't transfer more than what is in sender's account
        // TODO: can we cast this to a decimal? Quick fix was setting the max to one trillion
        public decimal Amount { get; set; } = 0.0M; // amount to transfer, default would be 0. add amount to default that we would transfer.
    }
}
