using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    interface ITransferDao
    {
        // As an authenticated user of the system, I need to be able to send a transfer of a specific amount of TE Bucks to a registered user.
        //      I should be able to choose from a list of users to send TE Bucks to.
        List<User> GetListOfUsers();
        //      A transfer should include the User IDs of the from and to users and the amount of TE Bucks.
        Transfer GetSpecificTransfer();
        //      A Sending Transfer has an initial status of Approved.
        Transfer UpdateSendingTransferStatus();
        //      The receiver's account balance is increased by the amount of the transfer.
        Transfer UpdateBalanceForReceiveingAccount();
        //      The sender's account balance is decreased by the amount of the transfer.
        Transfer UpdateBalanceForSendingAccount();
        //      I must not be allowed to send money to myself.
        //      I can't send more TE Bucks than I have in my account.
        //      I can't send a zero or negative amount.
        // As an authenticated user of the system, I need to be able to see transfers I have sent or received.
        List<Transfer> GetTransfers(User user);
        // As an authenticated user of the system, I need to be able to retrieve the details of any transfer based upon the transfer ID.
        Transfer GetSpecificTransfer(User user, int transferId);
    }
}
