using ado_net.Interfaces;

namespace ado_net.Extensions;

static class PrintableExtensions
{

        public static void ShowInfo(this IPrintable obj)
    {
        string text = obj.ToString();
        string[] lines = text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0 || i == lines.Length - 1)
                Console.ForegroundColor = ConsoleColor.Yellow; 
            else
                Console.ForegroundColor = ConsoleColor.Cyan; 

            Console.WriteLine(lines[i]);
        }

        Console.ResetColor();
    }
}
