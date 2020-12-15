using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMethods
{
    class Program
    {
        // Virtual Methods

        // Ezerek yerine veya üzerine farklı kodlar yazmamızı sağlayan belirli metotlardır.
        // Örneğin inherit edilen iki farklı classlardan biri base sınıftaki bir metodun üzerine farklı bir şey ekleyeceği zaman,
        // Base class da virtual olan metot, inherit alan class içerisine override edilir.Override 'ın türkçe anlamı,
        // "geçersiz kılma, çiğneme" bu durumu özetliyor.Bir nevi siz o metoda müdahale etmiş oluyorsunuz.
        // Inherit edilme zorunlulukları yoktur.Override edilmeden bile kullanılabilir haldedirler.

        static void Main(string[] args)
        {
            SqlServer sqlServer=new SqlServer();
            sqlServer.Add();
            MySql mySql=new MySql();
            mySql.Add();

            Console.ReadLine();
        }
    }

    class Database
    {
        public virtual void Add()
        {
            Console.WriteLine("Added by default");
        }

        public virtual void Delete()
        {
            Console.WriteLine("Deleted by default");
        }
    }

    class SqlServer:Database
    {
        // override yazdıktan sonra virtual methotlar görünmektedir.
        public override void Add()
        {
            // Müdahale kodlar
            Console.WriteLine("Added by Sql Code");

            // inherit alınan class'taki Add() metodunu da çağırabiliriz.
            // yani tamamen engellemedik, müdahale etmiş oluyoruz.
            //base.Add();
        }
    }

    class MySql:Database
    {
        
    }
}
