using System;
namespase ConsoleShop.Discounts
class DiscountFactory
{

    public static IDiscountPromo Create(string promo)
    {
        string code = promo?.Trim().ToUpper();
        return code switch
        {
            "OWERTY" => new Discount(),
            _ => new NoDiscount()

        };
    }
}