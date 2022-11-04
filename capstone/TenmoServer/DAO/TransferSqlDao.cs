using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using TenmoServer.DAO;

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


        //      I should be able to choose from a list of users to send TE Bucks to.
        public List<User> GetListOfUsers(User user)
        {
            List<User> users = null;
            
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
                            User u = GetUserNamesFromReader(reader);
                            users.Add(u);
                        }
                        return users;
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
                
            
        }
        //      A transfer should include the User IDs of the from and to users and the amount of TE Bucks.

        // As an authenticated user of the system, I need to be able to see transfers I have sent or received.
        public List<Transfer> GetTransfers(User user)
        {
            List<Transfer> transfers = null;

            try // try reading from SQL all data where we have given uder id
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer_id, user_id, amount FROM transfer JOIN account ON account_id IN (account_from, account_to) WHERE user_id = @user_id;", conn);
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
        // As an authenticated user of the system, I need to be able to retrieve the details of any transfer based upon the transfer ID.
        public Transfer GetSpecificTransfer(User user, int transferId)
        {
            Transfer returnTransfer = null; // set up initial account
            if (user == null)
            {
                return null;
            }

            try // try reading from SQL all data where we have given uder id
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM transfer WHERE transfer_id = @transfer_id;", conn);
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
        // As an authenticated user of the system, I need to be able to send a transfer of a specific amount of TE Bucks to a registered user.
        public Transfer CreateTransfer(User user, Transfer transfer) //TODO
        {
            //List<User> users = GetListOfUsers(user); call list of users in view, select 1 user to assign to account_to
            int newTransferId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO transfer (transfer_status_id, transfer_type_id, account_to, account_from, amount) " +
                                                "OUTPUT INSERTED.transfer_id " +
                                                "VALUES (@transfer_status_id, @transfer_type_id, @acccount_to, @account_from, @amount);", conn);
                cmd.Parameters.AddWithValue("@transfer_status_id", transfer.TransferStatusId);
                cmd.Parameters.AddWithValue("@transfer_type_id", transfer.TransferTypeId);
                cmd.Parameters.AddWithValue("@acccount_to", transfer.AccountTo);
                cmd.Parameters.AddWithValue("@account_from", transfer.AccountFrom);
                cmd.Parameters.AddWithValue("@amount", transfer.Amount);

                newTransferId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return GetSpecificTransfer(user, newTransferId);
        }

        //      A Sending Transfer has an initial status of Approved.

        //      The receiver's account balance is increased by the amount of the transfer.
        public bool UpdateBalanceForTransferAccounts(Transfer transfer) // possibly edit transfer
        {
            try // try reading from SQL all data where we have given uder id
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    decimal balanceFrom = 0;
                    decimal balanceTo = 0;
                    decimal newBalanceFrom = 0;
                    decimal newBalanceTo = 0;
                    SqlCommand cmdForAccountFromBalance = new SqlCommand("SELECT balance FROM account JOIN transfer ON " +
                        "account_id = account_from WHERE transfer_id = @transfer_id;", conn);
                    cmdForAccountFromBalance.Parameters.AddWithValue("@transfer_id", transfer.TransferId);
                    SqlDataReader readerFrom = cmdForAccountFromBalance.ExecuteReader();
                    if (readerFrom.Read()) // should only read 1 row of table
                    {
                        balanceFrom = GetBalanceFromReader(readerFrom);
                        newBalanceFrom = balanceFrom - transfer.Amount;
                    }

                    SqlCommand cmdForAccountToBalance = new SqlCommand("SELECT balance FROM account JOIN transfer ON account_id = account_to WHERE transfer_id = @transfer_id;", conn);
                    cmdForAccountToBalance.Parameters.AddWithValue("@transfer_id", transfer.TransferId);
                    SqlDataReader readerTo = cmdForAccountFromBalance.ExecuteReader();
                    if (readerTo.Read()) // should only read 1 row of table
                    {
                        balanceTo = GetBalanceFromReader(readerTo);
                        newBalanceTo = balanceTo + transfer.Amount;
                    }


                    SqlCommand cmdForTransfer = new SqlCommand("BEGIN TRANSACTION; " +
                                                                " UPDATE account SET balance = @balance_from " +
                                                                "WHERE account_id = @account_from; " +
                                                                "UPDATE account SET balance = @balance_to " +
                                                                "WHERE account_id = @account_to; " +
                                                                "COMMIT;", conn);
                    cmdForTransfer.Parameters.AddWithValue("@balance_from", newBalanceFrom);
                    cmdForTransfer.Parameters.AddWithValue("@balance_to", newBalanceTo);
                    cmdForTransfer.Parameters.AddWithValue("@account_from", transfer.AccountFrom);
                    cmdForTransfer.Parameters.AddWithValue("@account_to", transfer.AccountTo);
                    cmdForTransfer.ExecuteNonQuery();

                    // write line successful transfer
                    return true;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        //      The sender's account balance is decreased by the amount of the transfer.
        
        //      I must not be allowed to send money to myself.
        //      I can't send more TE Bucks than I have in my account.
        //      I can't send a zero or negative amount.
        
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
        private User GetUserNamesFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
            };

            return u;
        }

        private decimal GetBalanceFromReader(SqlDataReader reader) // privately build POCO based on sql row
        {
            return Convert.ToDecimal(reader["balance"]); // decimal balance
        }
    }
}
