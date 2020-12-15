using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesAndVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            //Value Types
            //Console.WriteLine("Hello World!");

            // sbyte // 1 byte // -128 ile 127

            // short - 16 bit // 2 byte // -32.768 ile 32.767

            // int - 32 bit // 4 byte // -2.147.483.648 ile 2.147.483.647

            // long - 64 bit // 8 byte // -9.223.372.039.854.775.808 ile 9.223.372.036.854.775.807

            // byte // 1 byte // 0 ile 225

            // ushort // 2 byte 0 ile 65.535

            // uint // 4 byte // 0 ile 4.294.967.295

            // ulong // 8 byte // 0 ile 18.446.744.073.709.551.615

            // float - double - decimal

            // boolean - bool // 1 bit

            // char - string - object

            double number5 = 10.4;
            decimal number6 = 10;
            char character = 'A';
            bool condition = false;
            byte number4 = 255;
            short number3 = 32767;
            int number1 = 2147483647;
            long number2 = 9223372036854775807;
            var number7 = 10;
            number7 = 'A';
            //number7 = "A";
            
            Console.WriteLine("Number1 is {0}", number1);
            Console.WriteLine("Number2 is {0}", number2);
            Console.WriteLine("Number3 is {0}", number3);
            Console.WriteLine("Number4 is {0}", number4);
            Console.WriteLine("Number5 is {0}", number5);
            Console.WriteLine("Number7 is {0}", number7);
            Console.WriteLine("Character is : {0}",(int)character);
            Console.WriteLine((int)Days.Friday);
            Console.ReadLine();

            //Primitive Type 

            //Değişkenin temeli veya en yalın hali denilebilir.
            //Örneğin string, char'dan türetilerek oluşturulmuştur.
            //Buna göre char primitive tiptir.
            // <> typeof(double).IsPrimitive kullanılarak olup olmadığı öğrenilebilir.

            // -- -- -- 

            //Anonymouse Type

            //Tek seferli işlemler için veya benzeri durumlarda sınıf yerine kullanılabilir.
            //Daha az maliyet sunar.
            //<> var Kisi = new { Ad = "Mustafa", Soyad = "Demirel" };
        }

    }

    enum Days
    {
        Monday=10,Tuesday=20,Wednesday=30,Thursday,Friday,Saturday,Sunday
    }
}
