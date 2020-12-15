using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Product harddisk=new Product(50);
            harddisk.ProductName = "Hard Disk";

            Product gsm=new Product(50);
            gsm.ProductName = "GSM";
            // Product nesnesinin içerisindeki event += şeklinde örneğe verilir.
            // Diğer tarafta şartlara göre tetiklenme durumu var.
            gsm.StockControlEvent += Gsm_StockControlEvent; // nesne eklenmiş
            // Kullanılıp kullanılmadığı nesne içinde kontrol edilebilir.
            // ve duruma göre işlem yapılabilir.


            for (int i = 0; i < 10; i++)
            {
                harddisk.Sell(10);
                gsm.Sell(10);
                Console.ReadLine();
            }

            Console.ReadLine();


        }

        private static void Gsm_StockControlEvent()
        {
            // stok'un 15'in altına düşmesi halinde tetiklenir.
            Console.WriteLine("Gsm about to finish!!!");
        }
    }
}
