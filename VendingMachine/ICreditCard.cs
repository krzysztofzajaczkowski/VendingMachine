using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public interface ICreditCard
    {
        public string BankName { get; set; }
        public double Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsValid { get; }

        public bool Charge(double amount);

    }
}
