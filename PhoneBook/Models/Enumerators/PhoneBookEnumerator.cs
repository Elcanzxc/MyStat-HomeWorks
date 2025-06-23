using MyPhoneBook.Models;
using System.Collections;


public class PhoneBookEnumerator : IEnumerator<PhoneNumber>
{
    private readonly List<PhoneNumber> sorted;
    private int position = -1;

    public PhoneBookEnumerator(IEnumerable<PhoneNumber> source, SortMode sortMode)
    {

        sorted = new List<PhoneNumber>(source);
        Sort(sorted, sortMode);
    }


    private void Sort(List<PhoneNumber> list, SortMode mode)
    {
        for (int i = 1; i < list.Count; i++)
        {
            var key = list[i];
            int j = i - 1;

            while (j >= 0 && Compare(list[j], key, mode) > 0)
            {
                list[j + 1] = list[j];
                j--;
            }

            list[j + 1] = key;
        }
    }

    private int Compare(PhoneNumber a, PhoneNumber b, SortMode mode)
    {
        if (mode == SortMode.ByNumber)
            return a.Number.CompareTo(b.Number);
        else if (mode == SortMode.ByName)
            return a.Person.Name.CompareTo(b.Person.Name);
        else
            return 0;
    }

    public PhoneNumber Current
    {
        get
        {
            if (position < 0 || position >= sorted.Count)
                throw new InvalidOperationException();
            return sorted[position];
        }
    }

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        position++;
        return position < sorted.Count;
    }

    public void Reset() => position = -1;

    public void Dispose() { }
}
