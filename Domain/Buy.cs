using System;
namespace ConsoleShop.Domain

class Buy
{
    public Product Prod { get; private set; }

    public Buy(Product prod)
    {
        this.Prod = prod;
    }
}