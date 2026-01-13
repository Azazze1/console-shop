using System;
namespase ConsoleShop.Discounts

class NoDiscount : IDiscountPromo
{
    public int Promo(int totalprice)
    {
        Console.WriteLine("Промокод неверный");
        return totalprice;
    }
}
