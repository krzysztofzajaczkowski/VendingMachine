using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class ABCCreditCard : ICreditCard
    {
        public string BankName { get; set; }
        public double Balance { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool IsValid {
            get
            {
                if(DateTime.Compare(ExpirationDate, DateTime.Today) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ABCCreditCard()
        {
            Balance = 0;
            ExpirationDate = DateTime.Now.AddMonths(15);
        }

        public ABCCreditCard(double balance, DateTime expirationDate)
        {
            Balance = balance;
            ExpirationDate = expirationDate;
        }
        public bool Charge(double amount)
        {
            if (Balance > amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }
}
