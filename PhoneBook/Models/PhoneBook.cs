namespace MyPhoneBook.Models;
public class PhoneBook : IEnumerable<PhoneNumber>
{
    private List<PhoneNumber> numbers = new List<PhoneNumber>();

    private IEnumerator<PhoneNumber> MyEnumerator;
    public void AddNumber(PhoneNumber phoneNumber)
    {
        numbers.Add(phoneNumber);
    }

    public void SetEnumerator(IEnumerator<PhoneNumber> enumerator)
    {
        MyEnumerator = enumerator;
    }

    public IEnumerator<PhoneNumber> GetEnumerator()
    {
        return MyEnumerator ?? numbers.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public List<PhoneNumber> GetAll() => numbers;



}
