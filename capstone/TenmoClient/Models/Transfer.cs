using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TenmoClient.Models
{
    public class Transfer
    {

        // TODO: do we need to require any other fields? do we need descriptions for type & status?
        public int TransferId { get; set; }
        public int TransferTypeId { get; set; } = 2; // default = ID for Sending
        public int TransferStatusId { get; set; } = 2; // default = ID for Approved
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        //[Range(0.01, 1000000000000, ErrorMessage = "User must transfer a positive amount.")] // TODO: also can't transfer more than what is in sender's account
        // TODO: can we cast this to a decimal? quick fix was setting the max to 1 trillion
        public decimal Amount { get; set; } = 0.0M; // amount to transfer, default would be 0. add amount to default that we would transfer.

        public Transfer() { }

        public Transfer(int transferId, int accountFrom, int accountTo, decimal amount)
        {
            this.TransferId = transferId;
            this.AccountFrom = accountFrom;
            this.AccountTo = accountTo;
            this.Amount = amount;
        }

        public Transfer(int transferId, int transferTypeId, int transferStatusId, int accountFrom, int accountTo, decimal amount)
        {
            this.TransferId = transferId;
            this.TransferStatusId = transferStatusId;
            this.TransferTypeId = transferTypeId;
            this.AccountFrom = accountFrom;
            this.AccountTo = accountTo;
            this.Amount = amount;
        }
    }

    public class TransferHistory
    {
        public int TransferId { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
    }
}
   
