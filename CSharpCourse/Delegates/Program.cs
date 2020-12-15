using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    public delegate void MyDelegate(); // void tipinde bir delege, bir tanımdır. 
    // Bu özelliklerdeki metotlar için örnek oluşturulabilir.

    // Parametreli delegate'ler de aynı şekilde çalışıyor ancak çalıştırma aşamasında sizden parametre istiyor.
    // Bu parametreler sırasıyla delege metotlarına yollanarak çalıştırılıyor (anladın işte)
    public delegate void MyDelegate2(string text);

    // Burada ise int dönüşü olan bir delege var.
    public delegate int MyDelegate3(int number1, int number2);

    class Program
    {
        // Delege ile sizin metotlarınızın özelliklerini aynı olması şartıyla;
        // bir sınıfın metotuna yol oluşturmak için kullanabilirsin.
        // Ayrıca birden fazla metot ekleyerek tek çağrışta bunların hepsi sırasıyla çalıştırılabilir.

        static void Main(string[] args)
        {
            CustomerManager customerManager=new CustomerManager();
            //customerManager.SendMessage();
            //customerManager.ShowAlert();

            MyDelegate myDelegate = customerManager.SendMessage; // MyDelegate özelliği yukarıda tanımlanmış üzere; bir örnek oluşturulur ve buna SendMessage metotu atılır.
            myDelegate += customerManager.ShowAlert; // Ayrıca ShowAlert metotu da += şeklinde eklenir.

            myDelegate -= customerManager.SendMessage; // İsteğe bağlı -= şeklinde çıkartılabilir.

            MyDelegate2 myDelegate2 = customerManager.SendMessage2; // bir örneğin içerisine string parametreli metot eklenir.
            myDelegate2+= customerManager.ShowAlert2;

            Matematik matematik=new Matematik();
            MyDelegate3 myDelegate3 = matematik.Topla;  // Dönüş tipine sahip metotların delegeye yüklenmesi halinde
            myDelegate3 += matematik.Carp;  // eğer birden fazla yükleme varsa en son hangi metot verildiyse o çalışır.
                                            // yani topla'yı yapar ama göstermez** carp'ı gösterir. 

            var sonuc = myDelegate3(2, 3); // Parametreleri gönderiyoruz ve dönüş değeri alabiliyorum.
            Console.WriteLine(sonuc); // son metot gösterilir >   myDelegate3 += matematik.Carp;

            // myDelegate2("Hello");

            //myDelegate();

            Console.ReadLine();
        }
    }

    public class CustomerManager
    {
        public void SendMessage()
        {
            Console.WriteLine("Hello!");
        }

        public void ShowAlert()
        {
            Console.WriteLine("Be careful!");
        }

        public void SendMessage2(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowAlert2(string alert)
        {
            Console.WriteLine(alert);
        }
    }

    public class Matematik
    {
        public int Topla(int sayi1, int sayi2)
        {
            Console.WriteLine("Be careful!");
            return sayi1 + sayi2;
        }

        public int Carp(int sayi1, int sayi2)
        {
            Console.WriteLine("Be careful!");
            return sayi1 * sayi2;
        }
    }
}
