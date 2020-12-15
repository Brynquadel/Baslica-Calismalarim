using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversions
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class ImplicitExplicit
    {
        /*
         
        
            C#’ta bilinçli ve bilinçsiz olmak üzere iki tür veri dönüşümü söz konusudur.
            Bilinçsiz olarak yapılan tür dönüşümü Implicit Conversion şeklinde tarif edilirken, 
            bilinçli dönüşüme ise Explicit Conversion diye nitelendirilmektedir.

            ”Implicit Conversion” ve “Explicit Conversion”. 

            Implicit Conversion
            int number = 3;
            double doubleNumber = number;

            Explicit Conversion
            double average = 4;
            int avg = (int)average;

        
            //Koordinat k = new Lokasyon();
            //double i = new Lokasyon();

            //// Bir türü dönüştürmede kayıpları azaltmak için kullanılabilir.
            //// Örneğin, nesnenin içerisinden almamız gereken bir özelliği veya değeri
            //// tl.Price = dolar.Value * 6.97; şeklinde alabilirken, bunu nesnemizin içine gömebiliriz.
            //// Yani her seferinde çevirme yapmak yerine daha kolay çevirebiliriz.
            //// Ayrıca artı olarak new' leme işini de nesne içerisinde halledebiliriz.
            //// Bu bize çevirme işleminde kontroller yapmamızı sağlar..

            //Dolar dolar = new Dolar(20);

            //TL tt = new TL();
            //tt.Price = dolar.Value * 6.97;

            //TL tl = (TL)dolar;  // explicit ==>  TL tl = (TL)dolar;


         */
    }

    public class Dolar
    {
        public double Value { get; set; }
        public string Sign { get; private set; }

        public Dolar(double v = 0)
        {
            Sign = "$";
            Value = v;
        }
    }

    class TL : Currency
    {
        public double Price { get; set; }
        public string Sign { get { return "&#8378;"; } }

        public static explicit operator TL(Dolar dolar) // explicit 'de cast operatörü vardır, implicit'de yoktur -> (type)
        {
            // bu senaryoda Dolar nesnesine müdahale edemiyor iken, çeviri özelliği yazıp değerini alıp istenilen şekilde kullanabiliriz.
            // burada kafa karışıklığı olacak pek bir şey göremiyorum. Her şey gayet açık görünüyor. (Kullanımlara da bak)
            return new TL
            {
                Price = dolar.Value * 6.97
            };

        }
        //        Çevir:             Buna // Bunu
        public static implicit operator TL(Euro tl)
        {
            return new Euro { price = tl.price * 8 };
        }
        public static implicit operator Lokasyon(TL tl)
        {
            return new Lokasyon { };
        }
    }
    public abstract class Currency
    {
        // BİRAZ SÜRDÜ AMA ANLADIM. TL: CURRENCY TARAFINA BAK. ORADA NESNEMİZE YAPILAN BİR ÇEVRİMİ KONTROL ALTINA ALIYORUZ.
        // BİR NEVİ NESNEMİZE AKTARIM YAPILDIĞI VAKİT AKTARILAN NESNEYİ KONTROL EDİYORUZ.

        // LOKASYON TARAFINDA İSE NESNEMİZDEN BAŞKA BİR YERE AKTARIM YAPILDIĞI ZAMAN GEÇERLİ OLUYOR
        // LOKASYON ÖRNEĞİMİZİ BİRDEN FAZLA YERE AKTARIM VERMESİNİ -TABİ DİLEDİĞİMİZ ŞARTLARDA- SAĞLIYORUZ.

        // AYRI DURDUKLARINA BAKMA BUNLAR AYNI YERDE OLABİLİR, ONDAN VE ONA ŞEKLİNDE TANIMLAMALAR YAPILABİLİR.
    }

    public class Euro
    {
        public double price { get; set; }

    }


    class Lokasyon
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static implicit operator Koordinat(Lokasyon l)
        {
            return new Koordinat { X = (int)l.X, Y = (int)l.Y };
        }
        // >< aktarılan özelliğin içerisine müdahale ederek hangi durumlara çevrileceğini belirleyebiliriz.
        // örneğin; hazırladığımız bir nesne string*'e çevrilirken şunları yap <-

        //    belirleme aşamasına dikkat ederseniz yine içeriğini hazırlamamız gerekiyor.


        // Burada ise sahip olduğumuz nesnemiz için birden fazla çevirme işlemini görüyorsun. // Lokasyon'dan başkasına aktarım
        //                                                                                      parantez içerisindeki çevrilen kısım oluyor.

        public static implicit operator double(Lokasyon l)
        {
            return l.X;
        }
        public static implicit operator Euro(Lokasyon l)
        {
            return new Euro();
        }
    }

    class Koordinat
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
