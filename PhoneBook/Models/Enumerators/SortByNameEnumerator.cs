
namespace MyPhoneBook.Models.Enumerators;

public class SortByNameEnumerator : IEnumerator<PhoneNumber>
{
    private List<PhoneNumber> sorted;
    private int position = -1;

    public SortByNameEnumerator(PhoneBook phoneBook)
    {
        sorted = new List<PhoneNumber>(phoneBook.GetAll());

        for (int i = 0; i < sorted.Count - 1; i++)
        {
            for (int j = 0; j < sorted.Count - i - 1; j++)
            {
                if (sorted[j].Person.Name.CompareTo(sorted[j + 1].Person.Name) > 0)
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
