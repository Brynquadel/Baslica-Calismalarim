using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generally
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }


    class RulesTactic
    {
        /*
            RULES AND BEST PRACTİS

            Genel olarak aldığım notlar benle ilgili olur. Gördüğüm her şeyi not etmekten çekinirim.
            Sadece önemli gördüğüm ve kullanacağım metot ve bilgileri yedeklerim.
            Bu yüzden sadece daha önce görmediğim ve uygulamadığım şeyleri görmektesiniz.
            Bilgilerin artık son evreye geçmesi yani daha araştırılması gereken bir şey olmadığı anlamındadır.
            Zaten araştırılması gereken bir şey var ise kullanım esnasında araştırılır,

            **

            <>
            var seattleCustomers = from customer in customers
            where customer.City == "Seattle"
            select customer.Name;
            <>
            şeklinde aratmalar kod düzenini koruyacaktır.

            public Form2()
            {
                this.Click += (s, e) =>
                {
                    MessageBox.Show(
                        ((MouseEventArgs)e).Location.ToString());
                };
            }

            public Form1()
            {
              this.Click += new EventHandler(Form1_Click);
            }

            void Form1_Click(object sender, EventArgs e)
            {
               MessageBox.Show(((MouseEventArgs)e).Location.ToString());
            }



         */
    }


    class Hints
    {
        /*

            ! Referans Kontrol - Referansların aynı olup olmadığını kontrol eder <> object.ReferenceEquals(a, b);

            ! Iterator
            Iterator dediğimiz yapılar, diziler yahut koleksiyonlar üzerinde çözüme gidecek olan adımları oluşturmamızı sağlayan yapılardır.
            Örneğin foreach döngüsü arka planda bir iterator yapısını kullanmaktadır. 
            Verilen koleksiyon veya dizi içerisindeki elemanları iterator sayesinde tek tek elde etmektedir.

            ! Encapsulation - Sınıf içerisinde bulunan propertyleri get, set kullanarak kontrol etmektir.

            ! System.Diagnostics.Debug & System.Diagnostics.Trace
            Output-Debug ekranına müdahale etmemizi sağlar. Örneğin bilgi çıktısı sağlayabiliriz.
            Debug ile Trace aynı işi yapıyor gibi görünse de farkları olabilir.()

            Casting etmek - çevirim yapmaktır
            sayi1 = (int)sayi2;  

            Convert - belirlenen tipe çevirmek
            sayi1 = Convert.ToInt32(sayi2);


            Readonly tanımlı değişkeni salt okunur moduna getirmektedir. Yani readonly olarak tanımlanan bir değişken sadece okunabilmektedir.
            Setleme işlemi değişkenin oluşturulduğu anda ya da oluşturulan sınıfın constructor metodu içerisinde yapılmaktadır.

            const : Türkçe’ye Sabit olarak çevrilebilir. Class seviyesinde tanımlanır ve tanımlanma anında değeri verilmek zorundadır. 
            Sonradan değeri değiştirilemez.

            readonly : Sadece-Okunabilir anlamına gelir. Class seviyesinde tanımlanır. 
            Tanımlandığı anda değeri verilebilir veya Class Constructor’ında değeri verilebilir. Sonradan değeri değiştirilemez.

            sealed class 'tan alıntı yapılamaz.
            sealed metotların görünürlüğü kaldırılır.


            object metoduna her tür atanamaz. Atanan türler üzerinde yapılan işlemlerde hata ihtimali vardır.
            Örneğin sayı verdiğimizde 
            <> object obj = 10;
            obj = obj + 10; yapamayız, hata verir. Bu hata derlenme aşamasında sağlanır.
            obj = (int)obj + 10; şeklinde(unboxing) kullanabiliriz. Bu da zaman kaybettirmekle beraber, bu gibi genellikle hatalara açıktır.

            dynamic 'le ilgili işlemler önceden belirlenebilir ancak hatalar çalışma zamanında verilir.




         */
    }
}