using System;
namespace ConsoleShop.Domain

class Product
{
    public string Name { get; private set; }
    public int Price { get; private set; }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }
}