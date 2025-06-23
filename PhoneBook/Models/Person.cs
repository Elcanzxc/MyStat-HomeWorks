
namespace MyPhoneBook.Models;

    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }

        public Person(string name, string surname, Gender gender)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"{Name} {Surname} ({Gender})";
        }
    }

