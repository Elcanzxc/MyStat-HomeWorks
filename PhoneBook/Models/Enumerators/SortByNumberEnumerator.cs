namespace MyPhoneBook.Models.Enumerators;

public class SortByNumberEnumerator : IEnumerator<PhoneNumber>
{
    private List<PhoneNumber> sorted;
    private int position = -1;

    public SortByNumberEnumerator(PhoneBook phoneBook)
    {
        sorted = new List<PhoneNumber>(phoneBook.GetAll());

        for (int i = 0; i < sorted.Count - 1; i++)
        {
            for (int j = 0; j < sorted.Count - i - 1; j++)
            {
                if (sorted[j].Number.CompareTo(sorted[j + 1].Number) > 0)
                {
                    var temp = sorted[j];
                    sorted[j] = sorted[j + 1];
                    sorted[j + 1] = temp;
                }
            }
        }
    }

    public PhoneNumber Current => sorted[position];

    object System.Collections.IEnumerator.Current => Current;

    public bool MoveNext()
    {
        position++;
        return position < sorted.Count;
    }

    public void Reset()
    {
        position = -1;
    }

    public void Dispose() { }
}
