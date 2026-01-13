using System;
namespace ConsoleShop.Payments
class CashPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("Заказ будет оплачен наличными: " + amount);
    }
}