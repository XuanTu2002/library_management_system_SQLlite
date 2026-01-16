using System;
namespace LibraryManagement.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public Person(int Id, string Name, string Phone)
        {
            this.Id = Id;
            this.Name = Name;
            this.Phone = Phone;
        }
        public abstract void ShowInfo();

    }
}