using ConsoleShop.Domain;
using ConsoleShop.Payments;
using ConsoleShop.Discounts;

namespace ConsoleShop.UI
class ConsoleMenu

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
}
