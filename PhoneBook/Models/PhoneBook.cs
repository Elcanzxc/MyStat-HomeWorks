using MyPhoneBook.Models;
using System.Collections;

public class PhoneBook : IEnumerable<PhoneNumber>
{
    private readonly List<PhoneNumber> numbers = new();

    public void Add(PhoneNumber phoneNumber) => this.numbers.Add(phoneNumber);

    public IEnumerator<PhoneNumber> GetEnumerator()
    {
        return new PhoneBookEnumerator(numbers, SortMode.ByNumber);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<PhoneNumber> GetSortedByName()
    {
        return new PhoneBookEnumerable(numbers, SortMode.ByName);
    }
}
