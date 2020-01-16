using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class Snack : Product
    {
        public Snack(string name, double price, int stock) : base(name, price, stock)
        {

        }
        public Snack(string name, double price, int stock, bool isAvailable) : base(name, price, stock, isAvailable)
        {

        }
        public override void Consume()
        {
            Console.WriteLine("You've eaten {0}", Name);
            Stock -= 1;
        }
    }
}
