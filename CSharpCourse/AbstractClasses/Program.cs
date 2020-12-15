using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClasses
{
    class Program
    {
        // Abstract Class

        // Virtual metot ve Interface'lerin bazı özelliklerinin birleştirilmiş halidir diyebiliriz.
        // Şöyle ki, implement edilen her class içerisinde geçerli olacak ve değiştirilmesi gereken*
        // özelliklerin bulunduğu alanları içerir.Interface 'ler gibi bir örneği oluşturulamaz.
        // Onun yerine implement edilen özellikler new'lenebilir.

        static void Main(string[] args)
        {
            Database database = new Oracle();
            database.Add();
            database.Delete();

            Database database2 = new SqlServer();
            database2.Add();
            database2.Delete();

            Console.ReadLine();
        }
    }

    abstract class Database
    {
        public void Add()
        {
            Console.WriteLine("Added by default");
        }

        public abstract void Delete();

        // Açıklama, Add() metodu implement edilmez*, implement edilen tüm classlar için aynı işlevi görür iken 
        // abstract void 'te ise bu iş tam tersidir. Implement zorunluluğu vardır ve implement edilen her class içerisinde yeniden tanımlanmalıdır.
        // aslında abstract özellikler tanımlayabilmemiz için class'a bu bir abstract class'tır diyoruz. Eğer metodu ezmek isteseydik,
        // o zaman virtual metot kullanırdık. Nitekim implement ettiğimiz classlar içerisinde abstract void'lerin override olarak tanımlandığını göreceksiniz.

    }

    class SqlServer : Database
    {
        // Database abstract class'ından alınan abstract void zorunlu olarak override edilir.
        public override void Delete()
        {
            Console.WriteLine("Deleted by Sql");
        }
    }

    class Oracle : Database
    {
        public override void Delete()
        {
            Console.WriteLine("Deleted by Oracle");
        }
    }
}
