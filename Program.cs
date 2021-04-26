using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;


namespace FirstBankOfSuncoast
{
    class Program
    {
        static void DisplayGreeting()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("   Welcome to First Bank of Suncoast! ");
            Console.WriteLine(" ");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(" ");

            Console.WriteLine("Please enter name.");
            var personName = Console.ReadLine();

            Console.WriteLine();
            Console.Write($"Hello {personName} ! ");
        }


        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }


        static void Main(string[] args)
        {
            var transactions = new List<Transaction>();

            DisplayGreeting();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine();
                Console.Write("Do you want to (D)eposit or (W)ithdraw or view (B)alance or (T)ransaction list or (Q)uit . ");

                var choice = Console.ReadLine().ToUpper();

                if (choice == "Q")
                {
                    keepGoing = false;
                }
                else if (choice == "D")
                {
                    var deposit = new Transaction();

                    var answer = PromptForString("How much would you like to deposit to your (C)hecking or (S)avings today? ");

                    if (answer == "C")
                    {
                        deposit.TransactionType = ("Deposit");
                        deposit.AccountType = ("Checking");
                        deposit.Amount = PromptForInteger("How much would you like to deposit today? ");
                        transactions.Add(deposit);

                        Console.WriteLine($"You have deposited ${deposit.Amount}!");
                    }
                    else if (answer == "S")
                    {
                        deposit.TransactionType = ("Deposit");
                        deposit.AccountType = ("Savings");
                        deposit.Amount = PromptForInteger("How much would you like to deposit today? ");
                        transactions.Add(deposit);

                        Console.WriteLine($"You have deposited ${deposit.Amount}!");
                    }

                }

                else if (choice == "W")
                {
                    var withdraw = new Transaction();

                    var answer = PromptForString("Which account would you lke to withdraw from today, your (C)hecking or (S)avings? ");

                    if (answer == "C")
                    {
                        withdraw.TransactionType = ("Withdraw");
                        withdraw.AccountType = ("Checking");
                        withdraw.Amount = PromptForInteger("How much would you like to withdraw today? ");
                        transactions.Add(withdraw);

                        if (withdraw.Amount < 0)
                        {
                            Console.WriteLine($"Unable to make this withdraw.");
                        }
                        else
                        {
                            Console.WriteLine($"You have withdrawled ${withdraw.Amount}!");
                        }
                    }
                    else if (answer == "S")
                    {
                        withdraw.TransactionType = ("Withdraw");
                        withdraw.AccountType = ("Savings");
                        withdraw.Amount = PromptForInteger("How much would you like to withdraw today? ");
                        transactions.Add(withdraw);

                        if (withdraw.Amount < 0)
                        {
                            Console.WriteLine($"Unable to make this withdraw.");
                        }
                        else
                        {
                            Console.WriteLine($"You have withdrawled ${withdraw.Amount}!");
                        }

                        Console.WriteLine(" ");

                        Console.WriteLine(" ");

                    }

                }

                else if (choice == "B")
                {
                    var balance = new Transaction();

                    var answer = PromptForString("Which account balance would you like to see, your (C)hecking or (S)avings? ");

                    if (answer == "C")
                    {
                        var newDeposits = transactions.Where(checking => checking.AccountType == "Checking").Where(checking => checking.TransactionType == "Deposit").Sum(checking => checking.Amount);

                        var newWithdraw = transactions.Where(checking => checking.AccountType == "Checking").Where(checking => checking.TransactionType == "Withdraw").Sum(checking => checking.Amount);


                        var newBalance = $"{newDeposits - newWithdraw}";

                        Console.WriteLine($"You have ${newBalance} in your Checking account");
                    }
                    else if (answer == "S")
                    {
                        var oldDeposits = transactions.Where(savings => savings.AccountType == "Checking").Where(savings => savings.TransactionType == "Deposit").Sum(savings => savings.Amount);

                        var oldWithdraw = transactions.Where(savings => savings.AccountType == "Checking").Where(savings => savings.TransactionType == "Withdraw").Sum(savings => savings.Amount);


                        var oldBalance = $"{oldDeposits - oldWithdraw}";

                        Console.WriteLine($"You have ${oldBalance} in your Savings account");
                    }
                }

                else if (choice == "T")
                {
                    foreach (var tranaction in transactions)
                    {
                        Console.WriteLine(tranaction.TransactionList());
                    }
                }
            }
            var fileWriter = new StreamWriter("bank.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(transactions);
            fileWriter.Close();
        }

    }
}