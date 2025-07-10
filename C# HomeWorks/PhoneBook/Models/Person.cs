
namespace MyPhoneBook.Models;

    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }

        public Person(string name, string surname, Gender gender)
        {
        this.Name = name;
        this.Surname = surname;
        this.Gender = gender;
        }

        public override string ToString()
        {
            return $"{this.Name} {this.Surname} ({this.Gender})";
        }
    }

