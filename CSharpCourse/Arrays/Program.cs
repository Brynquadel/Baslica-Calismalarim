using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            // 3 verilik bir array alanı ayrılır
            string[] students = new string[3]{ "Engin", "Derin", "Salih" };
            // farklı bir atama yaptığınızda referansı değişeceğinden yeni değeri alır
            students = new string[4] {"", "", "", ""};
            // new 'lenmeden kullanılabilir
            string[] students2 = {"Engin","Derin","Salih"};
            //students2[3] = "Ahmet";

            //foreach (var student in students2)
            //{
            //    Console.WriteLine(student);
            //}

            // çok uzaylı string array'i, 5 satır ve 3 sütun ayrılmıştır
            string[,] regions=new string[5,3]
            {
                {"İstanbul","İzmit","Balıkesir" },
                {"Ankara","Konya","Kırıkkale" },
                {"Antalya","Adana","Mersin" },
                {"Rize","Trabzon","Samsun" },
                {"İzmir","Muğla","Manisa" }
            };

            // okumak için GetUpperbound kullanılır, ilgili sekmeye ulaştığında kolonları okumaya başlar.
            // <= kullanmamızın sebebi dizin aramasına 0 'dan başladığı içindir olmadığı takdirde son kolonu okumaz
            for (int i = 0; i <= regions.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= regions.GetUpperBound(1); j++)
                {
                    Console.WriteLine(regions[i,j]);
                }
                Console.WriteLine("*********");
            }

            Console.ReadLine();

        }
    }
}
