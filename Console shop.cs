using ConsoleApp6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowInterface Show = new ShowInterface();
            Show.Menu();

        }
    }



    class ShowInterface
    {
        public List<Product> products;
        public void Menu()
        {
            Console.WriteLine("Добро пожаловать в магазин!\nВыбирайте: ");
            ShowProd();
            MakeOrder();

        }

        public void ShowProd()
        {
            products = new List<Product>
            {
            new Product("Молоко", 89),
            new Product("Сыр", 110),
            new Product("Майонез", 119),
            new Product("Пиво", 69),
            new Product("Чипсы", 149),
            new Product("Сникерс", 79),
            new Product("Хлеб", 56),
            new Product("Сметана", 99),
            new Product("Авокадо", 140),
            };
        }

        private void MakeOrder()
        {
            bool shopping = true;
            List<Buy> currentBuys = new List<Buy>();
            Customer cust = new Customer("Джинкс", 1200);

            Order order = new Order(1, cust, currentBuys);
            while (shopping)
            {
                int deliver = 5;
                deliver++;

                Console.WriteLine("Список продуктов");
                for (int i = 0; i < products.Count; i++)
                {

                    Console.WriteLine(i + 1 + ". " + products[i].Name + " - " + products[i].Price);
                }


                Console.WriteLine($"{products.Count + 1}. Применить скидку");
                Console.WriteLine($"{products.Count + 2}. Перейти к оплате");
                Console.WriteLine($"{products.Count + 3}. Посмотреть статус доставки");
                Console.WriteLine($"{products.Count + 4}. Завершить заказ");
                int choice = ReadInt("Выбор продукта: ", 1, products.Count + 3);
                if (choice == products.Count + 1)
                {
                    Console.WriteLine("Введите промокод");
                    string promocode = Console.ReadLine();
                    int total = order.TotalPrice;
                    IDiscountPromo discount = DiscountFactory.Create(promocode);
                    discount.Promo(total);



                }
                if (choice == products.Count + 2)
                {
                    Console.WriteLine("Как будете оплачивать?\nCard, Cash, Online");
                    string payment = Console.ReadLine();
                    IPayment IPay = PaymentFactory.Create(payment);
                    IPay.Pay(order.TotalPrice);
                    Pause();
                    Console.Clear();

                }
                if (choice == products.Count + 3)
                {
                    order.OnDelivery += () => Console.WriteLine("Заказ доставлен!");
                    order.Delivery(deliver);
                    Pause();
                    Console.Clear();

                }
                if (choice == products.Count + 4)
                {
                    shopping = false;
                }
                if (choice <= products.Count + 1)
                {

                    Product selected = products[choice - 1];
                    currentBuys.Add(new Buy(selected));

                    Console.WriteLine($"Добавлено: {selected.Name}");
                    Pause();
                    Console.Clear();
                }
                if (choice > products.Count + 3)
                {
                    Console.WriteLine("Ошибка.Попробуйте снова");
                }
            }
        }
        private int ReadInt(string prompt, int min, int max)
        {
            int result;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max);
            return result;
        }

        private void Pause()
        {

            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();
        }

    }





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

    interface IPayment
    {
        public void Pay(decimal amount);

    }

    class CardPayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("Заказ будет оплачен картой: " + amount);
        }
    }
    class CashPayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("Заказ будет оплачен наличными: " + amount);
        }
    }
    class OnlinePayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("Заказ будет оплачен онлайн: " + amount);
        }
    }


    class PaymentFactory
    {
        public static IPayment Create(string type)
        {
            return type.ToLower() switch
            {
                "card" => new CardPayment(),
                "cash" => new CashPayment(),
                "online" => new OnlinePayment(),
                _ => throw new ArgumentException("Неизвестный способ оплаты")

            };
        }
    }

    class Buy
    {
        public Product Prod { get; private set; }

        public Buy(Product prod)
        {
            this.Prod = prod;
        }
    }
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

    interface IDiscountPromo
    {
        int Promo(int totalprice);

    }

    class NoDiscount : IDiscountPromo
    {
        public int Promo(int totalprice)
        {
            Console.WriteLine("Промокод неверный");
            return totalprice;
        }
    }

    class Discount : IDiscountPromo
    {
        public int Promo(int totalprice)
        {
            Console.WriteLine("Прокод успешно работает");
            return totalprice * 80 / 100;

        }
    }


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

}


