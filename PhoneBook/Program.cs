using System;
using MyPhoneBook.Models;
using MyPhoneBook.Models.Enumerators;

class Program
{
    static void Main()
    {
        var phoneBook = new PhoneBook();

       
        phoneBook.AddNumber(new PhoneNumber(new Person("Ann", "Brown", Gender.Female), "122435"));
        phoneBook.AddNumber(new PhoneNumber(new Person("John", "White", Gender.Male), "434223"));
        phoneBook.AddNumber(new PhoneNumber(new Person("Bob", "Marley", Gender.Male), "212365"));


        phoneBook.SetEnumerator(new SortByNumberEnumerator(phoneBook));

        Console.WriteLine("Sorted By Number:");
        foreach (var phone in phoneBook)
        {
            Console.WriteLine($"{phone.Person.Gender} {phone.Person.Name} {phone.Person.Surname} {phone.Number}");
        }



        Console.WriteLine("\nSorted By Name:");
        phoneBook.SetEnumerator(new SortByNameEnumerator(phoneBook));
        foreach (var phone in phoneBook)
        {
            Console.WriteLine($"{phone.Person.Gender} {phone.Person.Name} {phone.Person.Surname} {phone.Number}");
        }
    }
}