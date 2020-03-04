using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class BestBankCreditCard : ABCCreditCard
    {
        public BestBankCreditCard() : base()
        {
            BankName = "Best Bank";
        }

        public BestBankCreditCard(double balance, DateTime expirationDate) : base(balance, expirationDate)
        {
            BankName = "Best Bank";
        }
    }
}
