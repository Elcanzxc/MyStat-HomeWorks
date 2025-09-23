using Threads.Models;

public class Menu
{
    private  UserTracker UserTracker { get; set; }

    public Menu(UserTracker userTracker)
    {
        UserTracker = userTracker;
    }

    public void Run()
    {
        while (true)
        {
            UserTracker.TrackPageEntry("Главное меню");

            Console.Clear();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1) Заказать еду");
            Console.WriteLine("2) Заказать продукты из магазина");
            Console.WriteLine("3) Выйти");

            int choseMenu = ChooseMenu();

            switch (choseMenu)
            {
                case 1:
                    UserTracker.TrackButtonClick("Кнопка 'Заказать еду'");
                    OrderFood();
                    break;
                case 2:
                    UserTracker.TrackButtonClick("Кнопка 'Заказать продукты'");
                    OrderGroceries();
                    break;
                case 3:
                    UserTracker.TrackButtonClick("Кнопка 'Выйти'");
                    UserTracker.TrackPageExit("Главное меню");
                    return;
                default:
                    Console.WriteLine("Значение должно быть от 1 до 3 включительно! Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
            }
            UserTracker.TrackPageExit("Главное меню");
        }
    }

    private void OrderFood()
    {
        UserTracker.TrackPageEntry("Заказ еды");

        Console.Clear();
        Console.WriteLine("Выбрано меню заказа еды.");
        Console.Write("Введите название блюда: ");
        Console.ReadLine();
        Console.Write("Введите количество: ");
        Console.ReadLine();
        Console.WriteLine("\nВаш заказ оформлен. Курьер уже в пути!");
        Console.WriteLine("Нажмите Enter, чтобы вернуться в главное меню.");
        Console.ReadLine();

        UserTracker.TrackButtonClick("Кнопка 'Заказать'");

        UserTracker.TrackPageExit("Заказ еды");
    }

    private void OrderGroceries()
    {
        UserTracker.TrackPageEntry("Заказ продуктов");

        Console.Clear();
        Console.WriteLine("Выбрано меню заказа продуктов.");
        Console.Write("Введите название продукта: ");
        Console.ReadLine();
        Console.Write("Введите количество: ");
        Console.ReadLine();
        Console.WriteLine("\nВаш заказ оформлен. Курьер уже в пути!");
        Console.WriteLine("Нажмите Enter, чтобы вернуться в главное меню.");
        Console.ReadLine();

        UserTracker.TrackButtonClick("Кнопка 'Заказать продукты'");

        UserTracker.TrackPageExit("Заказ продуктов");
    }

    private int ChooseMenu()
    {
        Console.Write("\nВаш выбор: ");
        string? inputChooseStr = Console.ReadLine() ?? "";
        bool isParsed = int.TryParse(inputChooseStr, out int inputChoose);
        return isParsed ? inputChoose : 0;
    }
}