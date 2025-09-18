using Attribute.Attributes;

namespace Attribute.Entities;

public class User
{
    [BackgroundColor(ConsoleColor.Red)]
    public string Name { get; set; }

    [ForegroundColor(ConsoleColor.Green)]
    public int Age;

    [Case(true)]
    public string Country { get; set; }

    [Padding(5,PaddingSide.Left)]

    public double Salary;

    public User()
    {
        this.Name = string.Empty;
        this.Age = 0;
        this.Country = string.Empty;
        this.Salary = 0;
    }

    public User(string name, int age,string country,double salary)
    {
        this.Name = name;
        this.Age = age;
        this.Country = country;
        this.Salary = salary;
    }

}
