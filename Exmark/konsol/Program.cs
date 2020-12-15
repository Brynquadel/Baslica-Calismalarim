using Exmark;
using Exmark.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konsol
{
    class Program
    {
        static void Main(string[] args)
        {
            ExmarkManager<Book> bookManager = new ExmarkManager<Book>();

            Book book = new Book();
            book.Id = 1000;
            book.Name = "Kübra";
            book.Category = "BP2";


           // bookManager.Add(book);

         

            //bookManager.CategoryProperty = "Category";

            //bookManager.CheckBeforeAdding = false;

            //bookManager.ExternalLocation = @"C:\Users\Mustafa Demirel\Desktop";

            //bookManager.AutomaticIdOnAdd = false;

            //bookManager.Add(book);

            ExmarkManager<Category> exmarkManager = new ExmarkManager<Category>();



            //var list = exmarkManager.GetAll();

            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.Name);
            //}

            Category cat = new Category();
            cat.Identity = 1;
            cat.Name = "Erdem";
            cat.Description = "Recep";
            cat.IconPath = "Developers";

            exmarkManager.ExternalLocation = @"C:\Users\Mustafa Demirel\Desktop";

            exmarkManager.CategoryProperty = "IconPath";
      

            exmarkManager.Add(cat);


           
        
            Console.WriteLine("bitti...");


        }

        private static void test(XEntity entt)
        {

            Console.ReadKey();
        }
    }
}
