using System;
namespace ConsoleShop.Payments

class PaymentFactory
{
    public static IPayment Create(string type)
    {
        return type.ToLower() switch
        {
            "card" => new CardPayment(),
            "cash" => new CashPayment(),
            "online" => new OnlinePayment(),
            _ => throw new ArgumentException("Неизвестный способ оплаты")

        };
    }
}