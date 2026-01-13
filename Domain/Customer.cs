using System;
namespace ConsoleShop.Domain

class Customer
{
    public string Name { get; private set; }
    public int Money { get; private set; }

    public Customer(string name, int money)
    {
        Name = name;
        Money = money;
    }
}
