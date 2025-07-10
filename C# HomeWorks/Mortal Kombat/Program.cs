using Mortal_Kombat.Models.Base;



// Советую сразу запускать консоль в полноэкранном режиме


// у меня случались проблемы с графикой, символы были UNICODE по этому в коде есть 
//Console.OutputEncoding = Encoding.UTF8;
//Console.OutputEncoding = System.Text.Encoding.UTF8;

// Во время вывода графики на консоль случалась ошибка ( с размером буфера вывода консоли) по этому в коде есть
//Console.SetBufferSize(Console.BufferWidth, 100);


// вычисление заполнений квадратиков в X-Ray всегда получались неправильными , по этому я нашел такое решение проблемы 
//int filledHP = (int)Math.Round((double)fighter.CurrentHP / fighter.MaxHealth * hpBarLength);

class Program
{


    public static void Main()
    {

        Game game = new Game();
        game.Run();
    }
}