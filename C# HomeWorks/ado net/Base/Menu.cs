
using ado_net.Models;
using System.Linq;
namespace ado_net.Base;



using System;
using System.Collections.Generic;
using System.Linq;

enum Entity
{
    Customers,
    Dishes,
    Ingredients,
    Orders,
    OrderDishes,
    DishIngredients,
    Exit
}

enum Operation
{
    ShowAll,
    GetById,
    Create,
    Update,
    Delete,
    Back
}
static class Menu
{
    private static readonly Dictionary<Entity, Dictionary<Operation, Action>> actions
        = new()
        {
            [Entity.Customers] = new()
            {
                [Operation.ShowAll] = CustomersRepository.GetAllInteractive,
                [Operation.GetById] = CustomersRepository.GetByIdInteractive,
                [Operation.Create] = CustomersRepository.CreateInteractive,
                [Operation.Update] = CustomersRepository.UpdateInteractive,
                [Operation.Delete] = CustomersRepository.DeleteInteractive
            },
            [Entity.Dishes] = new()
            {
                [Operation.ShowAll] = DishesRepository.GetAllInteractive,
                [Operation.GetById] = DishesRepository.GetByIdInteractive,
                [Operation.Create] = DishesRepository.CreateInteractive,
                [Operation.Update] = DishesRepository.UpdateInteractive,
                [Operation.Delete] = DishesRepository.DeleteInteractive
            },
            [Entity.Ingredients] = new()
            {
                [Operation.ShowAll] = IngredientsRepository.GetAllInteractive,
                [Operation.GetById] = IngredientsRepository.GetByIdInteractive,
                [Operation.Create] = IngredientsRepository.CreateInteractive,
                [Operation.Update] = IngredientsRepository.UpdateInteractive,
                [Operation.Delete] = IngredientsRepository.DeleteInteractive
            },
            [Entity.Orders] = new()
            {
                [Operation.ShowAll] = OrdersDetailedRepository.GetAllInteractive,
                [Operation.GetById] = OrdersRepository.GetByIdInteractive,
                [Operation.Create] = OrdersRepository.CreateInteractive,
                [Operation.Update] = OrdersRepository.UpdateInteractive,
                [Operation.Delete] = OrdersRepository.DeleteInteractive
            },
            [Entity.OrderDishes] = new()
            {
                [Operation.ShowAll] = OrderDishesDetailedRepository.GetAllInteractive,
                [Operation.GetById] = OrderDishesRepository.GetByIdInteractive,
                [Operation.Create] = OrderDishesRepository.CreateInteractive,
                [Operation.Update] = OrderDishesRepository.UpdateInteractive,
                [Operation.Delete] = OrderDishesRepository.DeleteInteractive
            },
            [Entity.DishIngredients] = new()
            {
                [Operation.ShowAll] = DishIngredientsDetailedRepository.GetAllInteractive,
                [Operation.GetById] = DishIngredientsRepository.GetByIdInteractive,
                [Operation.Create] = DishIngredientsRepository.CreateInteractive,
                [Operation.Update] = DishIngredientsRepository.UpdateInteractive,
                [Operation.Delete] = DishIngredientsRepository.DeleteInteractive
            }
        };
    private static readonly Dictionary<Entity, string> entityIcons = new()
    {
        [Entity.Customers] = "👤",
        [Entity.Dishes] = "🍲",
        [Entity.Ingredients] = "🥕",
        [Entity.Orders] = "📦",
        [Entity.OrderDishes] = "📝",
        [Entity.DishIngredients] = "🥘",
        [Entity.Exit] = "🚪"
    };

    private static readonly Dictionary<Operation, string> operationIcons = new()
    {
        [Operation.ShowAll] = "📄",
        [Operation.GetById] = "🔍",
        [Operation.Create] = "➕",
        [Operation.Update] = "✏️",
        [Operation.Delete] = "🗑️",
        [Operation.Back] = "↩️"
    };
    public static void Run()
    {
        var entities = Enum.GetValues<Entity>().Cast<Entity>().ToArray();

        while (true)
        {
            var selectedEntity = ShowMenu("Главное меню - выберите сущность", entities);
            if (selectedEntity == Entity.Exit) break;

            var operations = Enum.GetValues<Operation>().Cast<Operation>().ToArray();

            while (true)
            {
                var selectedOp = ShowMenu($"Операции для {selectedEntity}", operations);
                if (selectedOp == Operation.Back) break;

                if (actions.TryGetValue(selectedEntity, out var ops) && ops.TryGetValue(selectedOp, out var action))
                {
                    Console.Clear();
                    action.Invoke();
                }
                else
                {
                    Console.WriteLine("❌ Действие не реализовано.");
                }

                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться к операциям...");
                Console.ReadKey(true);
              
            }
        }

        Console.WriteLine("Выход из программы...");
    }

    private static T ShowMenu<T>(string title, T[] items)
    {
        int selectedIndex = 0;
        ConsoleKey key;


        int width = Math.Max(title.Length, items.Max(i => i.ToString().Length + 2)) + 6;

        do
        {
            Console.Clear();

            // Заголовок
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔" + new string('═', width+2) + "╗");
            Console.WriteLine($"║ {title.PadRight(width)} ║");
            Console.WriteLine("╚" + new string('═', width+2) + "╝\n");
            Console.ResetColor();

            // Меню
            Console.WriteLine("╔" + new string('═', width + 2) + "╗");
            for (int i = 0; i < items.Length; i++)
            {
                string display = items[i].ToString();

                if (typeof(T) == typeof(Entity) && entityIcons.ContainsKey((Entity)(object)items[i]))
                    display = entityIcons[(Entity)(object)items[i]] + " " + display;
                else if (typeof(T) == typeof(Operation) && operationIcons.ContainsKey((Operation)(object)items[i]))
                    display = operationIcons[(Operation)(object)items[i]] + " " + display;

                string prefix = i == selectedIndex ? "> " : "  ";
                string line = prefix + display;

                if (line.Length > width) line = line.Substring(0, width); 

                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"║ {line.PadRight(width)} ║");
                Console.ResetColor();
            }
            Console.WriteLine("╚" + new string('═', width + 2) + "╝\n");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("↑/↓ – навигация | Enter – выбрать | Esc – назад/выход");
            Console.ResetColor();

            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = items.Length - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex++;
                if (selectedIndex >= items.Length) selectedIndex = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                return items[selectedIndex];
            }
            else if (key == ConsoleKey.Escape)
            {
                return default;
            }

        } while (true);
    }

}