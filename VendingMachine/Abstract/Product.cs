using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public abstract class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }

        public Product()
        {
            IsAvailable = true;
        }
        public Product(string name, double price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
            if (stock > 0)
            {
                IsAvailable = true;
            }
            else
            {
                IsAvailable = false;
            }
        }

        public Product(string name, double price, int stock, bool isAvailable)
        {
            Name = name;
            Price = price;
            Stock = stock;
            IsAvailable = isAvailable;
        }

        public void PrintDetails()
        {
            Console.Write("{0}  | {1} | {2} $",Name, Stock, Price);
        }
        public abstract void Consume();
    }
}
