using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoConsoleService : ConsoleService
    {
        /************************************************************
            Print methods
        ************************************************************/
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

        public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hello, {username}!");
            Console.WriteLine("1: View your current balance");
            Console.WriteLine("2: View your past transfers");
            Console.WriteLine("3: View your pending requests");
            Console.WriteLine("4: Send TE bucks");
            Console.WriteLine("5: Request TE bucks");
            Console.WriteLine("6: Log out");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }
        public LoginUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        // Add application-specific UI methods here...

        public void GetBalance(decimal balance)
        {
            Console.WriteLine($"Your current account balance is: ${balance}");
        }

        public void ViewPastTransfers(List<Transfer> transfers, int transferID, int accountFrom, int accountTo, List<ApiUser> users)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Transfers");
            Console.WriteLine("ID           From/ To                 Amount");
            Console.WriteLine("------------------------------------------");

            foreach (Transfer transfer in transfers) // Get list of users to call for the username
            {
                if (transfers.Count > 0 )
                {
                    Console.WriteLine($"{transfer.TransferId}          From: {accountFrom}          ${transfer.Amount}");
                    Console.WriteLine($"{transfer.TransferId}          To: {accountTo}              ${transfer.Amount}");
                }
                else if (transfers.Count <= 0)                  
                {
                    Console.WriteLine("There are no transfers for this ID: " + transferID);
                }
            }
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Please enter transfer ID to view details(0 to cancel): ");
        }

        public void SendBucks() // print
        {

            Console.WriteLine("");
            Console.WriteLine("| --------------Users-------------- |");
            Console.WriteLine("|    Id | Username                  |");
            Console.WriteLine("| -------+---------------------------|");

            foreach ()

                Console.WriteLine($"|  {} | Bernice                   |");
            Console.WriteLine("|  1003 | Deandre                   |");
            | -----------------------------------|
            //Id of the user you are sending to[0]: 1003
            //Enter amount to send: 75.74
        }

        public void ViewSpecificTransfer()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Transfer Details");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Id: {transfer.TransferId}");
            Console.WriteLine($"From: {/*ApiUser Account ID*/}");
            Console.WriteLine($"To: Me Myselfandi");
            Console.WriteLine($"Type: Send"); // read from sql?
            Console.WriteLine($"Status: Approved");
            Console.WriteLine($"Amount: {transfer.amount}");
        }

    }
}

