using System;
using System.Collections.Generic;
using TenmoClient.Models;
using System.Linq;

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

        //public void viewpasttransfers(list<transfer> transfers, list<apiuser> users)
        //{
        //    console.writeline("-------------------------------------------");
        //    console.writeline("transfers");
        //    console.writeline("id           from/ to                 amount");
        //    console.writeline("------------------------------------------");

        //    foreach (transfer transfer in transfers) // get list of users to call for the username
        //    {
        //        if (transfer.transfertypeid == 1)
        //        {
        //            console.writeline($"{transfer.transferid}          from: {user.username}          ${transfer.amount}");
        //        }
        //        else                
        //        {
        //            console.writeline($"{transfer.transferid}          to: {user.username}              ${transfer.amount}");
        //        }
        //    }
        //    console.writeline("-----------------------------------------");
        //    console.writeline("please enter transfer id to view details(0 to cancel): ");
        //}

        public void SendBucks(List<Transfer> transfers, int transferId, ApiUser user, decimal amount) // print
        {

            Console.WriteLine("");
            Console.WriteLine("| --------------Users-------------- |");
            Console.WriteLine("|    Id | Username                  |");
            Console.WriteLine("| -------+---------------------------|");
            foreach (Transfer fransfer in transfers)
                {
                Console.WriteLine($"|  {transferId} | {user.Username}  |");
                }   
            Console.WriteLine("| -----------------------------------|");
            Console.WriteLine($"Sending TE Bucks to {user.Username}");
            Console.WriteLine($"Amount sending: {amount}");
        }

        internal int PromptForTransferToUser(List<ApiUser> users)
        {
            Console.WriteLine("");
            Console.WriteLine("| --------------Users-------------- |");
            Console.WriteLine("|    Id | Username                  |");
            Console.WriteLine("| -------+---------------------------|");
            foreach (ApiUser u in users)
            {
                Console.WriteLine($"| {u.UserId} | {u.Username} |");
            }
            Console.WriteLine("| -------+---------------------------|");
            bool isValdUserId = false;
            int targetUserId = 0;
            while (!isValdUserId)
            {
                targetUserId = PromptForInteger("Id of the user you are sending to", 0);
                if (users.Select(u => u.UserId).Contains(targetUserId))//
                {
                    isValdUserId = true;
                }
                else
                { Console.WriteLine("Please try again."); }
            }


            return targetUserId;
        }

        internal decimal PromptForTransferAmount(decimal sendersBalance)
        {
            bool isValidAmount = false;
            decimal amountToSend = 0;
            while(!isValidAmount)
            {
                amountToSend = PromptForDecimal("Enter amount to send: ");
                if (amountToSend <= sendersBalance)
                {
                    isValidAmount = true;
                }
                else
                {
                    Console.WriteLine("please enter valid amount: ");
                }
            }
            return amountToSend;
        }

        //internal // output: transfer ids, other usernames, amounts 
            // input list transfers & list of users, account list?

        //public void ViewSpecificTransfer()
        //{
        //    Console.WriteLine("--------------------------------------------");
        //    Console.WriteLine("Transfer Details");
        //    Console.WriteLine("--------------------------------------------");
        //    Console.WriteLine($"Id: {transfer.TransferId}");
        //    Console.WriteLine($"From: {/*ApiUser Account ID*/}");
        //    Console.WriteLine($"To: Me Myselfandi");
        //    Console.WriteLine($"Type: Send"); // read from sql?
        //    Console.WriteLine($"Status: Approved");
        //    Console.WriteLine($"Amount: {transfer.amount}");
        //}

    }
}

