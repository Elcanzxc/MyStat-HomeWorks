namespace Files.Models.Base
{
    using Files.Models.Enum;
    using Files.Models.Interfaces;
    using System;

    public class Calculator
    {
        private readonly ILogger logger;

        public Calculator(ILogger logger)
        {
            this.logger = logger;
        }

        public int SumResult(int a, int b)
        {
            logger.Write(new Log{Type = LOG_TYPE.Info,Title = "Sum Started",Description = $"Calculating {a} + {b}"});

            int result = a + b;

            logger.Write(new Log{Type = LOG_TYPE.Info,Title = "Sum Result",Description = $"Result = {result}"});

            return result;
        }

        public double DivResult(int a, int b)
        {
            logger.Write(new Log {Type = LOG_TYPE.Info,Title = "Divide Started",Description = $"Calculating {a} / {b}"});

            if (b == 0)
            {
                var ex = new DivideByZeroException("Нельзя делить на ноль.");
                logger.Write(new Log{Type = LOG_TYPE.Error,Title = "Divide Error",Description = "Ошибка при делении",Exception = ex});
                throw ex;
            }

            double result = (double)a / b;

            logger.Write(new Log{Type = LOG_TYPE.Info,Title = "Div Result",Description = $"Result = {result}" });

            return result;
        }

        public long FactorialResult(int n)
        {
            logger.Write(new Log{Type = LOG_TYPE.Info,Title = "Factorial Started",Description = $"Calculating factorial({n})"});

            if (n < 0)
            {
                var ex = new ArgumentException("Факториал от отрицательного числа не определён.");
                logger.Write(new Log{Type = LOG_TYPE.Error,Title = "Factorial Error",Description = "Отрицательное значение",Exception = ex});
                throw ex;
            }

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }

            logger.Write(new Log{Type = LOG_TYPE.Info,Title = "Factorial Result",Description = $"Result = {result}"});

            return result;
        }
    }

}
