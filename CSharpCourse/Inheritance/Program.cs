using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Program
    {
        // Inherit etmek, Inheritance
        // Sınıflar arası hiyerarşik yapı kurabilmek, oluşturmak için kullanılır.
        // Bir sınıf, başka bir sınıftan türeyerek(kalıtım alma), o sınıfın public ve protected tanımlı yapılarını devralır.
        // Kalıtım alan sınıfa : Derived Class, kalıtım veren sınıfa : Base class denir.

        static void Main(string[] args)
        {
            Person[] persons=new Person[3]
            {
                new Customer
                {
                    FirstName = "Engin"
                }, new Student
                {
                    FirstName = "Derin"
                }, new Person
                {
                    FirstName = "Salih"
                }
            };
            foreach (var person in persons)
            {
                Console.WriteLine(person.FirstName);
            }

            Console.ReadLine();
            
        }
    }

    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Customer:Person
    {
        public string City { get; set; }
    }

    class Student : Person
    {
        public string Department { get; set; }
    }
}
