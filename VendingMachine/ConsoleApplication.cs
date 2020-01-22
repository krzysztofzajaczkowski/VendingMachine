using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class ConsoleApplication
    {
        public void Run()
        {
            string[] options = {
                "Change card",
                "See products",
                "Check funds in machine",
                "Insert coins",
                "Use card",
                "Withdraw",
                "Select products",
                "EXIT"
            };
            
            double money = 10;
            List<Product> products = new List<Product>()
            {
                new Drink("Fuzzy Drink", 2.5, 10),
                new Drink("Shea Tea", 3.5, 5),
                new Snack("Snack-y", 1.75, 10),
                new Snack("Delicious Bar", 2.30, 8),
                new Drink("Fiddly", 1.80, 5, false),
                new Snack("Too-doo", 1, 1)

            };
            VendingMachine vendingMachine = new VendingMachine(products);
            var javaFanCard = new JavaFanCreditCard(15,DateTime.Today.AddDays(-2));
            var bestBankCard = new BestBankCreditCard(15,DateTime.Today.AddMonths(15));
            ICreditCard selectedCard = bestBankCard;
            string option;
            int code;
            double amountToInsert;
            double amountWithdrawn;
            while (true)
            {
                Console.WriteLine(" ");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Cash: {0} $", money);
                Console.WriteLine("Card: {0}, Balance: {1} $", javaFanCard.BankName, javaFanCard.Balance);
                Console.WriteLine("Card: {0}, Balance: {1} $", bestBankCard.BankName, bestBankCard.Balance);
                Console.WriteLine("Selected card: {0}", selectedCard.BankName);
                Console.WriteLine("----------------------------");
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine("{0}. " + options[i], i);
                }
                Console.Write("Select option: ");
                option = Console.ReadLine();
                Console.WriteLine(" ");
                switch (option)
                {
                    case "0":
                        if (selectedCard.Equals(javaFanCard))
                        {
                            selectedCard = bestBankCard;
                        }
                        else
                        {
                            selectedCard = javaFanCard;
                        }
                        break;
                    case "1":
                        vendingMachine.SeeProducts();
                        break;
                    case "2":
                        vendingMachine.CheckFunds();
                        break;
                    case "3":
                        Console.WriteLine("Enter amount to insert");
                        double.TryParse(Console.ReadLine(), out amountToInsert);
                        if(amountToInsert <= money)
                        {
                            vendingMachine.InsertMoney(amountToInsert);
                            money -= amountToInsert;
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money");
                        }
                        break;
                    case "4":
                        vendingMachine.PayWithCard(selectedCard);
                        break;
                    case "5":
                        amountWithdrawn = vendingMachine.Withdraw();
                        money += amountWithdrawn;
                        break;
                    case "6":
                        Console.WriteLine("Enter product code");
                        int.TryParse(Console.ReadLine(), out code);
                        vendingMachine.BuyProduct(code);
                        break;
                    case "7":
                        return;
                    default:
                        break;
                }
            }
        }
    }
}
