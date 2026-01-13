using System;
namespase ConsoleShop.Discounts

class Discount : IDiscountPromo
{
    public int Promo(int totalprice)
    {
        Console.WriteLine("Прокод успешно работает");
        return totalprice * 80 / 100;

    }
}