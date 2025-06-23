using MyPhoneBook.Models;

public class Program
{
    public static void Main()
    {
        var book = new PhoneBook();

        book.Add(new PhoneNumber(new Person("Elcan", "Aliyev", Gender.Male), "112365"));
        book.Add(new PhoneNumber(new Person("Vusale", "Abbasova", Gender.Female), "122435"));
        book.Add(new PhoneNumber(new Person("Fuad", "Mirzayev", Gender.Male), "434223"));

        Console.WriteLine("Sorted by number:");
        foreach (var data in book)
        {
            Console.WriteLine(data);
        }
            

        Console.WriteLine("\nSorted by person name:");
        foreach (var data in book.GetSortedByName())
        {
            Console.WriteLine(data);
        }
            
    }
}
