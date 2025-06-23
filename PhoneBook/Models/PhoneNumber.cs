namespace MyPhoneBook.Models;

public class PhoneNumber
{
    public Person Person { get; set; }
    public string Number { get; set; }

    public PhoneNumber(Person person, string number)
    {
        Person = person;
        Number = number;
    }

    public override string ToString()
    {
        return $"{Person}: {Number}";
    }
}
