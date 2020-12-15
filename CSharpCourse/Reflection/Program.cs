using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            //DortIslem dortIslem=new DortIslem(2,3);
            //Console.WriteLine(dortIslem.Topla2());
            //Console.WriteLine(dortIslem.Topla(3, 4));


            //DortIslem dortIslem = (DortIslem)Activator.CreateInstance(tip,6,7);                        **
            //Console.WriteLine(dortIslem.Topla(4, 5));
            //Console.WriteLine(dortIslem.Topla2());

            // Çalışma zamanında kurulması - Reflection ile çalışma

            var tip = typeof(DortIslem); // DorIslem'in tipi alınır

            var instance = Activator.CreateInstance(tip, 6, 5); // Activator ile alınan tipin bir örneği oluşturulur, gerekli parametreler verilir.

            MethodInfo methodInfo =  instance.GetType().GetMethod("Topla2"); // oluşturulan instance içerisinde Topla2 adında bir metot aratılıyor.

            Console.WriteLine(methodInfo.Invoke(instance,null)); // bilgisine ulaşılan metot çalıştırılıyor. null = parametre
                                                                 // instance' yazılmasının sebebi oluşturulan örneğin kaybolmasıdır. "Topla2" metotunu Invoke et, şunun üzerinden "instance"

            Console.WriteLine("------------------");
            var metodlar = tip.GetMethods(); // tipin metotları alınmış

            foreach (var info in metodlar) 
            {
                Console.WriteLine("Metod adı : {0}",info.Name);

                foreach (var parameterInfo in info.GetParameters())
                {
                    Console.WriteLine("Parametre : {0}",parameterInfo.Name);
                }

                foreach (var attribute in info.GetCustomAttributes()) // metotların attributeları alınıyor
                {
                    Console.WriteLine("Attribute Name : {0}",attribute.GetType().Name); // attribute'a ulaşarak adını çekiyor
                }
            }

            Console.ReadLine();
        }
    }

    public class DortIslem
    {
        private int _sayi1;
        private int _sayi2;

        public DortIslem(int sayi1, int sayi2)
        {
            _sayi1 = sayi1;
            _sayi2 = sayi2;
        }

        public DortIslem()
        {
            
        }
        public int Topla(int sayi1, int sayi2)
        {
            return sayi1 + sayi2;
        }

        public int Carp(int sayi1, int sayi2)
        {
            return sayi1 * sayi2;
        }

        public int Topla2() //
        {
            return _sayi1 + _sayi2;
        }

        [MetodName("Carpma")]
        public int Carp2()
        {
            return _sayi1 * _sayi2;
        }
    }

    public class MetodNameAttribute:Attribute
    {
        public MetodNameAttribute(string name)
        {
            
        }
    }
}
