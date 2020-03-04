using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace VendingMachine
{
    public class VendingMachine : IVendingMachine
    {
        private IConfiguration _config { get; set; }
        public double Credit { get; set; }
        public List<Product> Products { get; set; }
        public bool UsingCreditCard { get; set; } = false;
        public ICreditCard CreditCard { get; set; }
        public double MaxPrice
        {
            get
            {
                return Products.Max(p => p.Price);
            }
        }

        public VendingMachine(IConfiguration config)
        {
            _config = config;
            var snacks = config.GetSection("Products").GetSection("Snacks").Get<List<Snack>>();
            var drinks = config.GetSection("Products").GetSection("Drinks").Get<List<Drink>>();
            Products = new List<Product>();
            Products.AddRange(snacks);
            Products.AddRange(drinks);
            Credit = 0;
        }

        public bool InsertMoney(double amount)
        {
            if (UsingCreditCard)
            {
                Withdraw();
            }
            Random random = new Random();
            if (random.Next(100) < 101)
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
                if (Products[code].IsAvailable)
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
                        if (Products[code].Price <= Credit)
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
    }
}
