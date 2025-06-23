namespace MyPhoneBook.Models;

public class PhoneNumber
{
    public Person Person { get; set; }
    public string Number { get; set; }

    public PhoneNumber(Person person, string number)
    {
        this.Person = person;
        this.Number = number;
    }

    public override string ToString()
    {
        return $"{this.Person} {this.Number}";
    }
}
