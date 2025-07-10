




// Использовал string? Чтобы не надоедал знак предупреждения 
// Не использовал switch ( мало ли скажете не прошли еще не используйте)
// Не использовал рекурсию в факториале по той же причине что и сверху
// Есть TryParse с out частью ( Чуть чуть обьяснили эту тему по этому )


void MenuOutput()
{
    Console.WriteLine("\nChoose which action you want to do");
    Console.WriteLine("divide (press 1)");
    Console.WriteLine("add (press 2)");
    Console.WriteLine("subtract (press 3)");
    Console.WriteLine("multiply (press 4)");
    Console.WriteLine("square (press 5)");
    Console.WriteLine("calculate factorial (press 6)");
    Console.WriteLine("exit (press 7)");
    Console.Write("Your choice: ");
}

int InputAction()
{

    while (true) { 
    string? InputActionChoiceStr = Console.ReadLine();
    bool isInputNumber = int.TryParse(InputActionChoiceStr, out int input);

        if (!isInputNumber || input < 1 || input > 7)
        {
            Console.WriteLine("Enter a number from 1 to 7.");
            Console.Write("Your choice: ");
        }
        else return input;

    }
}
double InputNumber(string text)
{
    while (true)
    {
        Console.Write(text);
        string? inputStr = Console.ReadLine();
        bool check = double.TryParse(inputStr, out double input);

        if (check == true) return input;
        else
        {
            Console.WriteLine("You entered the data incorrectly.");
            Console.Write("Number: ");
        }

    }
}
double Divide()
{
 
    double firstNumber = InputNumber("Enter the first number: ");
    double secondNumber;

    while (true)
    {
        secondNumber = InputNumber("Enter the second number: ");
        if (secondNumber == 0)  Console.WriteLine("You can't divide by 0.");
        else break;
    }
    return firstNumber / secondNumber;
}

double Add()
{

    double firstNumber = InputNumber("Enter the first number: ");
    double secondNumber = InputNumber("Enter the second number: ");

    return firstNumber + secondNumber;

}

double Subtract()
{
    double firstNumber = InputNumber("Enter the first number: ");
    double secondNumber = InputNumber("Enter the second number: ");

    return firstNumber - secondNumber;
}

double Multiply()
{
    double firstNumber = InputNumber("Enter the first number: ");
    double secondNumber = InputNumber("Enter the second number: ");

    return firstNumber * secondNumber;
}

double Square()
{
    double firstNumber = InputNumber("Enter the  number: ");
    return firstNumber * firstNumber;
}

long Factorial()
{
    while (true) {

    double firstNumber = InputNumber("Enter a non-negative integer: ");


        if (firstNumber < 0 || firstNumber % 1 != 0)
        {
            Console.WriteLine("The number must be a non-negative integer.");
            continue;
        }

        long result = 1;
        for (int i = 2; i <= firstNumber; i++)
            result *= i;
        return result;

    }
}
do
{

    MenuOutput();


    int InputActionChoice = InputAction();

    if (InputActionChoice == 1) Console.WriteLine($"Result:{Divide()}");
    else if (InputActionChoice == 2) Console.WriteLine($"Result:{Add()}");
    else if (InputActionChoice == 3) Console.WriteLine($"Result:{Subtract()}");
    else if (InputActionChoice == 4) Console.WriteLine($"Result:{Multiply()}");
    else if (InputActionChoice == 5) Console.WriteLine($"Result:{Square()}");
    else if (InputActionChoice == 6) Console.WriteLine($"Result:{Factorial()}");
    else if (InputActionChoice == 7) return;




}
while (true);