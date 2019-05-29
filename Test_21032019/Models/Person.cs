using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_21032019.Models
{
    public class Person
    {


        [Required(ErrorMessage = "Wymagane jest podanie imienia")]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }

        public int Id { get; set; }

        public string FullName {get { return Name + " " + Surname; } }

        public Person(string name, string surname,  int age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }

        public Person()
        {

        }

        public static List<Person> GetPersonList()
        {
            testowaEntities ent = new testowaEntities();
            return FromUzytkownicy(ent.uzytkownicies.ToList());
        }

        public static List<Person> FromUzytkownicy(List<uzytkownicy> uList)
        {
            //List<Person> pList = new List<Person>();
            //foreach(uzytkownicy u in uList)
            //{
            //    Person p = new Person(u.Imie, u.Nazwisko, u.Wiek);
            //    pList.Add(p);
            //}

            List<Person> personList = uList.Select(x => new Person() { Age = x.Wiek, Name = x.Imie, Surname = x.Nazwisko, Id = x.id }).ToList();
            return personList;
        }

        public Person(uzytkownicy user)
        {
            Name = user.Imie;
            Surname = user.Nazwisko;
            Age = user.Wiek;
            Id = user.id;
        }

        public static uzytkownicy ConvertToUzytkownicy(Person p)
        {
            uzytkownicy u = new uzytkownicy();
            u.Imie = p.Name;
            u.Nazwisko = p.Surname;
            u.Wiek = p.Age;
            u.id = p.Id;
            return u;
        }

    }
}