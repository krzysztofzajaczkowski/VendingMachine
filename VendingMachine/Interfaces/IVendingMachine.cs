using System.Collections.Generic;

namespace VendingMachine
{
    public interface IVendingMachine
    {
        double Credit { get; set; }
        ICreditCard CreditCard { get; set; }
        double MaxPrice { get; }
        List<Product> Products { get; set; }
        bool UsingCreditCard { get; set; }

        bool BuyProduct(int code);
        void CheckFunds();
        bool InsertMoney(double amount);
        bool PayWithCard(ICreditCard creditCard);
        void SeeProducts();
        double Withdraw();
    }
}