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

        public void ViewPastTransfers()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Transfers");
            Console.WriteLine("ID From/ To                 Amount");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("23          From: Bernice          $ 903.14");
            Console.WriteLine("79          To: Larry           $  12.55");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Please enter transfer ID to view details(0 to cancel): ");
        }

    }
}


//Console.WriteLine("");
//Console.WriteLine("| --------------Users-------------- |");
//Console.WriteLine("|    Id | Username                  |");
//Console.WriteLine("| -------+---------------------------|");
//Console.WriteLine($"|  {} | Bernice                   |");
//Console.WriteLine("|  1003 | Deandre                   |");
//            | -----------------------------------|
//            Id of the user you are sending to[0]: 1003
//            Enter amount to send: 75.74