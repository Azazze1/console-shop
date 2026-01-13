using System;
namespace ConsoleShop.Payments
class CardPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("Заказ будет оплачен картой: " + amount);
    }
}