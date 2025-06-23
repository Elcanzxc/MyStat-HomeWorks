using MyPhoneBook.Models;
using System.Collections;

public class PhoneBookEnumerable : IEnumerable<PhoneNumber>
{
    private readonly IEnumerable<PhoneNumber> source;
    private readonly SortMode sortMode;

    public PhoneBookEnumerable(IEnumerable<PhoneNumber> source, SortMode sortMode)
    {
        this.source = source;
        this.sortMode = sortMode;
    }


    public IEnumerator<PhoneNumber> GetEnumerator()
    {
        return new PhoneBookEnumerator(source, sortMode);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
