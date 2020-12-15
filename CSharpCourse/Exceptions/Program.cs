using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exceptions
{
    class Program
    {
        // Genel olarak Action, Func, Delegate metotlar üzerinde kontrol sahibi olmaya yarar.
        // Action ile metot gönderebilirsiniz, Func ile dönüş alabilirsiniz. // Func; kontrol edip dönüş almamızı sağlıyor.


        // Delegate ile kendi türünüzü oluşturup metotları yönetebilirsiniz.

        static void Main(string[] args)
        {
            //ExceptionIntro();

            //TryCatch();

            //ActionDemo();

            // Action ile Func farkı. Func, verilen metodu yerine getirip değer döndürür.

            Func<int, int, int> func = Topla;
            // ilk ve ikinci parametre int, dönüş tipi int'tir. bu parametrelere uyan bir metot olan Topla'yı veririz.

            var result = func(3, 5); // şeklinde parametreler verilerek sonuç alınabilir

            Console.WriteLine();

            // tek int > sadece int tip dönüş yapan örnek
            // delegate()'e blok verilmiş blokta bir random sayı üretici var ve bu blok func'a eşitlemiş
            // normalde biz delegasyon oluşturup metotlara uygulatırdık burada ikisini kullanmış olduk
            Func<int> getRandomNumber = delegate()
            {
                Random random=new Random();
                return random.Next(1, 100);
            };

            // Yukarıda yazan örnek ayrıca bu şekilde yazılabilir
            // örnekler içerisine bakıldığında nasıl çalıştığı anlaşılıyor zaten
            // yukarıda return şeklinde dönüş yaparken burada new'lenip verilmiş.
            Func<int> getRandomNumber2=()=>new Random().Next(1,100);

            Console.WriteLine(getRandomNumber());
            Thread.Sleep(1000);
            Console.WriteLine(getRandomNumber2());

            //Console.WriteLine(Topla(2,3));

            Console.ReadLine();
        }

        static int Topla(int x, int y)
        {
            return x + y;
        }

        private static void ActionDemo()
        {
            // parametre olarak method göndermek // isteğe göre içerisine sadece kodlar yazılabilir
            HandleException(() => { Find(); }); // Action - kod bloğu gönderme * önemli
        }

        private static void TryCatch()
        {
            try
            {
                Find();
            }
            catch (RecordNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
                // daha detaylı bilgi
                Console.WriteLine(exception.InnerException);
            }
            catch (Exception exception)
            {
            }
        }

        // hata kontrolü için genel
    
        private static void HandleException(Action action) // method parametre şeklinde alınır * önemli
        {
            try
            {
                // alonan method try bloğu içerisinde çalıştırılır
                action.Invoke();
            }
            catch (Exception exception)
            {
                // hata olması durumunda yakalanır
                Console.WriteLine(exception.Message);
            }

        }

        private static void Find()
        {
            List<string> students = new List<string> {"Engin", "Derin", "Salih"};

            if (!students.Contains("Ahmet"))
            {
                // Özel hatamız ve base gönderilen Mesajımız
                throw new RecordNotFoundException("Record Not Found!");
            }
            else
            {
                Console.WriteLine("Record Found!");
            }
        }

        private static void ExceptionIntro()
        {
            try
            {
                string[] students = new string[3] {"Engin", "Derin", "Salih"};
                students[3] = "Ahmet";
            }
            //hatanın türüne göre kontroller
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (DivideByZeroException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
