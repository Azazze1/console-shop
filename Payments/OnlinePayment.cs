using System;
namespace ConsoleShop.Payments

class OnlinePayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("Заказ будет оплачен онлайн: " + amount);
    }
}