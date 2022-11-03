using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    //public class TransferSqlDao : ITransferDao
    //{
    //    private readonly string connectionString;
    //    const decimal startingBalance = 1000;

    //    public TransferSqlDao(string dbConnectionString)
    //    {
    //        connectionString = dbConnectionString;
    //    }

    //    // As an authenticated user of the system, I need to be able to send a transfer of a specific amount of TE Bucks to a registered user.
    //    //      I should be able to choose from a list of users to send TE Bucks to.
    //    List<User> GetListOfUsers()
    //    //      A transfer should include the User IDs of the from and to users and the amount of TE Bucks.
    //    Transfer GetSpecificTransfer()
    //    //      A Sending Transfer has an initial status of Approved.
    //    Transfer UpdateSendingTransferStatus()
    //    //      The receiver's account balance is increased by the amount of the transfer.
    //    Transfer UpdateBalanceForReceiveingAccount()
    //    //      The sender's account balance is decreased by the amount of the transfer.
    //    Transfer UpdateBalanceForSendingAccount()
    //    //      I must not be allowed to send money to myself.
    //    //      I can't send more TE Bucks than I have in my account.
    //    //      I can't send a zero or negative amount.
    //    // As an authenticated user of the system, I need to be able to see transfers I have sent or received.
    //    List<Transfer> GetTransfers(User user)
    //    // As an authenticated user of the system, I need to be able to retrieve the details of any transfer based upon the transfer ID.
    //    Transfer GetSpecificTransfer(User user, int transferId)
    //    {
    //        Account returnAccount = null; // set up initial account

    //        try // try reading from SQL all data where we have given uder id
    //        {
    //            using (SqlConnection conn = new SqlConnection(connectionString))
    //            {
    //                conn.Open();

    //                SqlCommand cmd = new SqlCommand("SELECT user_id, account_id, balance FROM account WHERE user_id = @user_id", conn);
    //                cmd.Parameters.AddWithValue("@user_id", userId);
    //                SqlDataReader reader = cmd.ExecuteReader();

    //                if (reader.Read()) // should only read 1 row of table
    //                {
    //                    returnAccount = GetAccountFromReader(reader);
    //                }
    //            }
    //        }
    //        catch (SqlException)
    //        {
    //            throw;
    //        }
    //        return returnAccount;
    //    }
            


    //    private Transfer GetTransferFromReader(SqlDataReader reader) // privately build POCO based on sql row
    //    {
    //        Transfer t = new Transfer()
    //        { 
    //            TransferId = Convert.ToInt32(reader["transfer_id"]),
    //            TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
    //            TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
    //            AccountFrom = Convert.ToInt32(reader["account_from"]),
    //            AccountTo = Convert.ToInt32(reader["account_to"]),
    //            Amount = Convert.ToDecimal(reader["amount"])
    //        };

    //        return t;
    //    }
    //}
}
