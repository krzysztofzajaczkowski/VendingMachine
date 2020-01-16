using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class JavaFanCreditCard : ABCCreditCard
    {
        public JavaFanCreditCard() : base()
        {
            BankName = "Java Fan";
        }

        public JavaFanCreditCard(double balance, DateTime expirationDate) : base(balance, expirationDate)
        {
            BankName = "Java Fan";
        }
    }
}
