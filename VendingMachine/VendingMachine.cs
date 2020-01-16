using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VendingMachine
{
    public class VendingMachine : BoardObject
    {
        public char Symbol { get; set; }
        public Point Position { get; set; }
        public double Credit { get; set; }
        public List<Product> Products { get; set; }
        public bool UsingCreditCard { get; set; } = false;
        public ICreditCard CreditCard { get; set; }
        public string[] Interface { get; set; }

        public double MaxPrice { 
            get
            {
                return Products.Max(p => p.Price);
            }
        }

        public VendingMachine(int x, int y) : base(x, y, 'V')
        {
            Credit = 0;
            Products = new List<Product>()
            {
                new Drink("Fuzzy Drink", 2.5, 10),
                new Drink("Shea Tea", 3.5, 5),
                new Snack("Snack-y", 1.75, 10),
                new Snack("Delicious Bar", 2.30, 8),
                new Drink("Fiddly", 1.80, 5, false),
                new Snack("Too-doo", 1, 1)

            };
            Interface = new string[] {
                "Change card",
                "See products",
                "Check funds in machine",
                "Insert coins",
                "Use card",
                "Withdraw",
                "Select products",
                "EXIT"
            };
        }
        public VendingMachine(int x, int y, List<Product> products) : base(x, y, 'V')
        {
            Credit = 0;
            Products = products;
            Interface = new string[] {
                "Change card",
                "See products",
                "Check funds in machine",
                "Insert coins",
                "Use card",
                "Withdraw",
                "Select products",
                "EXIT"
            };
        }

        public bool InsertMoney(double amount)
        {
            if (UsingCreditCard)
            {
                Withdraw();
            }
            Random random = new Random();
            if(random.Next(100) < 101)
            {
                Credit += amount;
                CheckFunds();
                return true;
            }
            Console.WriteLine("Sadly, the coin haven't been accepted");
            return false;
        }

        public void SeeProducts()
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Stock > 0)
                {
                    Console.Write("{0}. ", i);
                    Products[i].PrintDetails();
                    Console.Write("\n");
                }
                
            }
        }

        public bool BuyProduct(int code)
        {
            if (code < Products.Count)
            {
                if(Products[code].IsAvailable)
                {
                    if (UsingCreditCard)
                    {
                        Credit -= Products[code].Price;
                        Withdraw();
                        Products[code].Consume();
                        return true;
                    }
                    else
                    {
                        if(Products[code].Price <= Credit)
                        {
                            Credit -= Products[code].Price;
                            Withdraw();
                            Products[code].Consume();
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Not enough funds");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("This product is unavailable");
                }
                
            }
            else
            {
                Console.WriteLine("There's no product with this code");
            }
            return false;
        }

        public void CheckFunds()
        {
            Console.WriteLine("Current funds: {0}", Credit);
        }

        public bool PayWithCard(ICreditCard creditCard)
        {
            if (Credit > 0)
            {
                Console.WriteLine("There are funds in machine");
                return false;
            }
            else
            {
                if (creditCard.IsValid)
                {
                    if (creditCard.Balance >= MaxPrice)
                    {
                        creditCard.Balance -= MaxPrice;
                        Credit += MaxPrice;
                        CreditCard = creditCard;
                        UsingCreditCard = true;
                        Console.WriteLine("Using {0} card", CreditCard.BankName);
                        CheckFunds();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds");
                    }
                }
                else
                {
                    Console.WriteLine("Your credit card has expired");
                }
                return false;
            }
        }

        public double Withdraw()
        {
            if (UsingCreditCard)
            {
                Console.WriteLine("{0} $ returned to credit card balance", Credit);
                CreditCard.Balance += Credit;
                Credit = 0;
                CreditCard = null;
                UsingCreditCard = false;
                return 0;
            }
            var amountWithdrawn = Credit;
            Credit = 0;
            Console.WriteLine("{0} $ in coins withdrawn", amountWithdrawn);
            return amountWithdrawn;
        }
        
        public void Menu(Player player)
        {
            string option;
            int code;
            double amountToInsert;
            double amountWithdrawn;
            while (true)
            {
                Console.WriteLine(" ");
                player.Details();
                for (int i = 0; i < Interface.Length; i++)
                {
                    Console.WriteLine("{0}. " + Interface[i], i);
                }
                Console.Write("Select option: ");
                option = Console.ReadLine();
                Console.WriteLine(" ");
                switch (option)
                {
                    case "0":
                        player.SwitchSelectedCards();
                        break;
                    case "1":
                        SeeProducts();
                        break;
                    case "2":
                        CheckFunds();
                        break;
                    case "3":
                        Console.WriteLine("Enter amount to insert");
                        double.TryParse(Console.ReadLine(), out amountToInsert);
                        if(amountToInsert <= player.Money)
                        {
                            InsertMoney(amountToInsert);
                            player.Money -= amountToInsert;
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money");
                        }
                        break;
                    case "4":
                        PayWithCard(player.SelectedCreditCard);
                        break;
                    case "5":
                        amountWithdrawn = Withdraw();
                        player.Money += amountWithdrawn;
                        break;
                    case "6":
                        Console.WriteLine("Enter product code");
                        int.TryParse(Console.ReadLine(), out code);
                        BuyProduct(code);
                        break;
                    case "7":
                        return;
                    default:
                        break;
                }
                Console.WriteLine(" ");
            }
        }

        public override void Interact(BoardObject boardObject)
        {
            if (boardObject is Player)
            {
                Menu((Player)boardObject);
            }
        }
    }
}
