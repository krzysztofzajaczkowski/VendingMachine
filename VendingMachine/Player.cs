using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class Player : BoardObject
    {
        public double Money { get; set; }
        public ICreditCard BestBankCreditCard { get; set; }
        public ICreditCard JavaFanCreditCard { get; set; }
        public ICreditCard SelectedCreditCard { get; set; }



        public Player() : base(0, 0, 'P')
        {
            Money = 20;
            JavaFanCreditCard = new JavaFanCreditCard(15,DateTime.Today.AddDays(-2));
            BestBankCreditCard = new BestBankCreditCard(15,DateTime.Today.AddMonths(15));
            SelectedCreditCard = BestBankCreditCard;
        }

        public void SwitchSelectedCards()
        {
            if (SelectedCreditCard.Equals(JavaFanCreditCard))
            {
                SelectedCreditCard = BestBankCreditCard;
            }
            else
            {
                SelectedCreditCard = JavaFanCreditCard;
            }
        }

        public void Details()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Cash: {0} $", Money);
            Console.WriteLine("Card: {0}, Balance: {1} $", JavaFanCreditCard.BankName, JavaFanCreditCard.Balance);
            Console.WriteLine("Card: {0}, Balance: {1} $", BestBankCreditCard.BankName, BestBankCreditCard.Balance);
            Console.WriteLine("Selected card: {0}", SelectedCreditCard.BankName);
            Console.WriteLine("----------------------------");
        }

        public void Move(Point newPosition)
        {
            Position = new Point(newPosition.X, newPosition.Y);
        }

        public override void Interact(BoardObject boardObject)
        {
            Console.WriteLine("How did You manage to interact with yourself?");
        }
    }
}
