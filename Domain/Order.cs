using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Domain

class Order
{
    public int ID { get; private set; }
    public Customer Cust { get; private set; }

    public List<Buy> PBuy { get; private set; }
    public int TotalPrice => PBuy.Sum(b => b.Prod.Price);
    public int FinalPrice { get; private set; }
    public Order(int id, Customer cust, List<Buy> pbuy)
    {
        ID = id;
        Cust = cust;
        PBuy = pbuy;
    }
    public void ApplyDiscount(IDiscountPromo promo)
    {
        FinalPrice = promo.Promo(TotalPrice);

        Console.WriteLine("К оплате: " + FinalPrice + "вместо " + TotalPrice);
    }

    public event Action OnDelivery;

    public void Delivery(int chrona)
    {
        if (chrona >= 5)
        {
            OnDelivery?.Invoke();
        }
    }
}