using Files.Models.Base;
using Files.Models.Interfaces;

class Program
{
    static void Main()
    {
        ILogger logger = new Logger();
        Calculator calc = new Calculator(logger);


        // код будет работать до первой ошибки , нашел ошибку, записал в лог и закончил свою работу 
        // думаю что вы так и хотели чтобы код работал
        try
        {
            var sum = calc.SumResult(3, 4);
            var div = calc.DivResult(10, 0);
            var fact = calc.FactorialResult(5);
        }
        catch (Exception) {  }
    }
}
