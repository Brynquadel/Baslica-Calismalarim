using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressions
{
    class Program
    {

        static void Main(string[] args)
        {
            //Basic();
            Advanced();

            // <!--
            Console.ReadLine();
        }

        private static void Advanced()
        {
            // Burada değer alan, aldığı değeri kontrolleyen ve geri döndüren bir yöntem var.
            // Bunlar Runtime zamanında oluşturuluyor.

            // .Parameter: "value" adında (bu isim metot içinde kullanılır) int tipinde değer isteyen bir kalıp . typeof(int) => "int tipi, int"
            ParameterExpression value = Expression.Parameter(typeof(int), "value");

            // .Label: Bool dönüş değeri kalıbı
            LabelTarget returnValue = Expression.Label(typeof(bool));

            // Metodun içerisini burada yazıyoruz. Olanı açıklamaya çalışacağım sadece.
            // Yukarıda oluşturulan nesneler .Block içerisinde kullanılacaktır.

            // .Block: metodun içeriği, IfThenElse; bir bakıma içeriği için kontrol mekanizması sağlar. İf kontrolüdür ve dönüşleri ayarlayabiliriz.
            BlockExpression methodBody = Expression.Block(
            Expression.IfThenElse(  // .Block içerisine .IfThenElse açılır.
               Expression.Equal(    // .Equal: Şart belirlenmek üzere,
                                    // .Modula mod almak için. 4%2=0 gibi. "value" üzerinden verilen değerin üzerinden mod'2' alınır. Ardından '0' ile eşitliği gözlenir.
                                    // .Constant tanım için.

                    Expression.Modulo(value, Expression.Constant(2)), Expression.Constant(0)),
                    Expression.Return(returnValue, Expression.Constant(true)), // Doğruysa; "returnValue" üzerinden true döndürür. return true;
                    Expression.Return(returnValue, Expression.Constant(false))), // Doğruysa; "returnValue" üzerinden false döndürür, return false;
                    Expression.Label(returnValue, Expression.Constant(false)) // Emin değilim
            );

            // Burada; yukarıda oluşturduğumuz "methodBody" adındaki bloğu ve döndüreceği değer "value" olaylarını .Compile() ile derleyerek, kullanılabilir tek hale getiriyoruz.
            // Dönüş olarak belirlediğimiz "returnValue" olayını göstermemize gerek yok, çünkü block içerisinde işlenip değer döndürüyor zaten.
            // Ayrıca Func, int tipinde aldığı değeri bool tipinde dönüş yapıyor. Değer verilmeli, sonucu ise farklı şekilde alıyoruz >  var result = 
            Func<int, bool> IsEven = Expression.Lambda<Func<int, bool>>(methodBody, value).Compile();

            var result = IsEven(6); // IsEven metot adresine 6 değeri gönderiliyor. Bunların hepsi çalışma zamanında üretiliyor.
                                    // Yapılacak işlem ise şu şekilde: 6 % 2 == 0

            Console.WriteLine("Sonuç: {0}", result.ToString());
        }

        private static void Basic()
        {
            // Expression, çalışma zamanında (runtime) birimler oluşturmamızı sağlar.
            // Sonradan bir metot üretebilir ve değişiklikler gerçekleştirebiliriz.
            // Basit bir örnek

            // Func, verilen bir ifadeyi kontrol etmemizi sağlar. Aşağıda verilen string değer(name) ile => Arda değeri karşılaştırılıyor.
            Expression<Func<string, bool>> lambdaExpression = name => name == "Arda"; // Ardından bu Expression'a yerleştirilmiş
            Func<string, bool> quessMyName = lambdaExpression.Compile(); // Yukarıdaki işlem derlenerek farklı bir fonksiyon haline daha getiriliyor.

            // Oluşturulan quessMyName fonksiyonuna "Arda" değeri veriliyor, beklenen dönüş; "True"
            bool result = quessMyName("Arda");

            Console.WriteLine("Sonuç: {0}", result.ToString());
        }
    }
}
