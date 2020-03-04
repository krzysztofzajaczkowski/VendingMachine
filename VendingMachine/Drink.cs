using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class Drink : Product
    {
        public Drink()
        {

        }
        public Drink(string name, double price, int stock) : base(name, price, stock)
        {

        }
        public Drink(string name, double price, int stock, bool isAvailable) : base(name, price, stock, isAvailable)
        {

        }
        public override void Consume()
        {
            Console.WriteLine("You've drank {0}", Name);
            Stock -= 1;
        }
    }
}
