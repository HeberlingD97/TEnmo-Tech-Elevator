using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDao
    {
        private readonly string connectionString;
        const decimal startingBalance = 1000;

        public TransferSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        // As an authenticated user of the system, I need to be able to send a transfer of a specific amount of TE Bucks to a registered user.
        //      I should be able to choose from a list of users to send TE Bucks to.
        List<User> GetListOfUsers(User user)
        {
            List<User> users = null; ;
            {
                try // try reading from SQL all data where we have given uder id
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT username, user_id FROM tenmo_user WHERE user_id != @user_id", conn);
                        cmd.Parameters.AddWithValue("@user_id", user.UserId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            User u = GetTransferFromReader(reader); // GetUserFromReader???
                            users.Add(u);
                        }

                    }
                }
                catch (SqlException)
                {
                    throw;
                }
                return users;
            }
        }
        //      A transfer should include the User IDs of the from and to users and the amount of TE Bucks.
        
        //      A Sending Transfer has an initial status of Approved.
        public void UpdateSendingTransferStatus(int transferId) // possibly come back to this
        {
            
            try // try reading from SQL all data where we have given uder id
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE transfer SET transfer_status_id = 2 WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_id", transferId);
                    cmd.ExecuteNonQuery();          
                }
            }
            catch (SqlException)
            {
                throw;
            }
            
        }
        //      The receiver's account balance is increased by the amount of the transfer.
        public void UpdateBalanceForReceiveingAccount(Transfer transfer) /// posibly edit transfer
        {
            try // try reading from SQL all data where we have given uder id
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE account SET balance = 2 WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_id", transferId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        //      The sender's account balance is decreased by the amount of the transfer.
        Transfer UpdateBalanceForSendingAccount();
        //      I must not be allowed to send money to myself.
        //      I can't send more TE Bucks than I have in my account.
        //      I can't send a zero or negative amount.
        // As an authenticated user of the system, I need to be able to see transfers I have sent or received.
        public List<Transfer> GetTransfers(User user)
        {
            List<Transfer> transfers = null;
            {
                try // try reading from SQL all data where we have given uder id
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT amount, user_id FROM transfer JOIN account ON account_id IN (account_from, account_to) WHERE user_id = @user_id", conn);
                        cmd.Parameters.AddWithValue("@user_id", user.UserId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Transfer t = GetTransferFromReader(reader);
                            transfers.Add(t);
                        }

                    }
                }
                catch (SqlException)
                {
                    throw;
                }
                return transfers;
            }
        }
        // As an authenticated user of the system, I need to be able to retrieve the details of any transfer based upon the transfer ID.
        public Transfer GetSpecificTransfer(User user, int transferId)
        {
            Transfer returnTransfer = null; // set up initial account

            try // try reading from SQL all data where we have given uder id
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM transfer WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_id", transferId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) // should only read 1 row of table
                    {
                        returnTransfer = GetTransferFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return returnTransfer;
        }





        private Transfer GetTransferFromReader(SqlDataReader reader) // privately build POCO based on sql row
        {
            Transfer t = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"])
            };

            return t;
        }
    }
}
