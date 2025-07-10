
using MyLinq.Models.Extensions;
using System;
using System.Collections.Generic;

// У меня имя метода конфликтовал с методами из Linq , так что я отключил   <ImplicitUsings>disable</ImplicitUsings>
class Program
{
    public static string ShowNames<T>(IEnumerable<T> obj)
    {
        string names = "";
        foreach (var name in obj)
        {
            names+= name + " ";
        }
        return names;
    }  // Создал метод который выводит имена ( сделал чисто для того чтобы не портить эстетику в Main части)
    static void Main()
    {
        string[] names = { "Elcan", "Kamran", "Ilham", "Rashad" };



        Console.WriteLine($"Names: {ShowNames(names)}\n");
       
        Console.WriteLine($"Is all names contains 'a' : {names.All(name => name.Contains('a'))}\n");

        Console.WriteLine($"Is names have elements: {names.Any()}\n");

        Console.WriteLine($"Is any names contains 'h' : {names.Any(name => name.Contains('h'))}\n");

        Console.WriteLine($"First element of names: {names.First()}\n");

        Console.WriteLine($"First element contain 'm': {names.First(name => name.Contains('m'))}\n");

        Console.WriteLine($"First or Default element of names: {names.FirstOrDefault()}\n");

        Console.WriteLine($"First or Default element contain 'z': {names.FirstOrDefault(name => name.Contains('z'))}\n");

        string? result = names.FirstOrDefault(name => name.Contains('z'),"NONE");
        Console.WriteLine($"First or Default(My Default) element contain 'z': {result}\n");

        Console.WriteLine($"Names where length <= 5:{ShowNames<string>(names.Where(name => name.Length <= 5))} \n");

        Console.WriteLine($"Names count : {names.Count()}\n");

        Console.WriteLine($"Names count which contains 'm':  {names.Count(name => name.Contains('m'))}\n");
    }
}
