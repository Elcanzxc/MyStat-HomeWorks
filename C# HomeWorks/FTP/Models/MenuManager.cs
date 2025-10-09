using FTP.Models;

namespace FTP.Models
{
    public class MenuManager
    {
        public static async Task DisplayMenu(List<MenuItem> items, string currentPath)
        {
            int selectedIndex = 0;

      
            var goUpIndex = items.FindIndex(item => item.Text == "..");
            if (goUpIndex != -1)
            {
                selectedIndex = goUpIndex;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($" FTP Навигатор | Нынешний Path: {currentPath}");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("Используйте ↑/↓ - Enter - Esc ");
                Console.WriteLine("-----------------------------------------------------");


                for (int i = 0; i < items.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($" {items[i].Text}");
                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % items.Count;
                        break;
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + items.Count) % items.Count;
                        break;
                    case ConsoleKey.Escape:
                        var goUpItem = items.FirstOrDefault(x => x.Text == "..");
                        if (goUpItem != null)
                        {
                            await goUpItem.Func();
                        }
                        return;
                    case ConsoleKey.Enter:
                        var result = await items[selectedIndex].Func();

            
                        if (result != null)
                        {
                            Console.Clear();
                            Console.WriteLine(result);
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                        }
                        return;
                }
            }
        }
    }
}