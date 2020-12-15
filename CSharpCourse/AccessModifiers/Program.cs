using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifiers
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    // sadece proje içerisinde kullanılabilir
    internal class Customer
    {
        // Customer'ın dahil ediliği sınıflarda kullanılabilir haldedir.
        protected int Id { get; set; }

        public void Save()
        {
          
            
        }

        public void Delete()
        {
            
        }
    }

    class Student:Customer
    {
        public void Save2()
        {
           
           
        }
    }

    // Dahil edilen tüm projeler içerisinde kullanılabilir haldedir
    public class Course
    {
        public string Name { get; set; }
    }
}
