using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exmark.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
          


           

            //exmarkManager.Add(book);
            //exmarkManager.Add(student);


            Type t = typeof(Book);

            List<Student> students = exmarkManager.GetAll<Student>();

            foreach (var item in students)
            {
                System.Console.WriteLine(item.Id.ToString());
            }

            List<Book> books = exmarkManager.GetAll<Book>();

            foreach (var item in books)
            {
                
            }


            Book book2 = exmarkManager.Get<Book>(1);

            System.Console.WriteLine(book2.Name);

            System.Console.ReadLine();

    */







            ////Book book = new Book();

            ////book.Id = 50;
            ////book.Name = "Recep";



            ////exmarkManager.Add(book);


            //exmarkManager.Add(book);

            //exmarkManager.CheckFile(book);

            ////List<Student> studnets = exmarkManager.GetAll<Student>();


            ////System.Console.WriteLine(exmarkManager.ReturnName(book));
            //string tt = exmarkManager.ReturnName(new Book());

            //System.Console.WriteLine(tt);


            //ExmarkConfiguration.SetFileName(new Book(), "TEST1");
            //ExmarkConfiguration.SetFileName(new Book(), "TEST2");
            //ExmarkConfiguration.SetFileName(new Book(), "TEST3");

            //string test = ExmarkConfiguration.Getnamefromke(new Book());

            //System.Console.WriteLine(test);



            //List<Student> liste = exmarkManager.GetAll<Student>();


            ExmarkManager exmarkManager = new ExmarkManager();
            //exmarkManager.SetEntityType<Book>(new Book());

            //Book book = new Book();
            //book.Name = "Recep'in Maceraları";
            //book.Id = 10;


            //exmarkManager.Add(book);
            //exmarkManager.SetSaveMethod(new Book());

            //exmarkManager.GetAll<Book>();


            Book book = new Book();

            book.Id = 50;
            book.Name = "Hamza";
            book.MethodType = false;


          

            System.Console.ReadLine();

            
         


        }


    }

}
