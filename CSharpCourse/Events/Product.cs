using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{

    // Event'in kullanabilmesi için bir oluşturulur
    public delegate void StockControl();

    // Bir Product nesnesi var
    public class Product
    {
        private int _stock;
        // burada test için bir stok sayısı alınıyor.
        public Product(int stock)
        {
            _stock = stock;
        }

        // bu event. delegate kullanıyor.
        public event StockControl StockControlEvent;
        // Her product örneği için özel olarak oluşturuluyor

        public string ProductName { get; set; } // isim

        // 
        public int Stock // Sell metodunun çalıştırılması sonrası
        {
            get { return _stock; } // istenildiğinde stock adedi döndürüyor
            set // bir değer verildiğinde 
            {
                _stock = value; // değer kaydediliyor // duruma göre bunu red edebilirsin
                // eğer Stock'a değer atılsaydı bu tehlikeli olabilirdi. O zaman satılan miktar 15'in altıydaysa şart sağlanırdı. Ancak,
                // Stock -= amount (=>) şu anlama gelir: Stock = Stock - amount. Satılan adedin stock sayısından çıkarılan sonuç Stock'a eşitleniyor. 
                // Böylece biz Stock'un güncel değerini görebiliyoruz.
                if (value<=15 && StockControlEvent!=null) // eğer stock adedi 15'in altına düşerse ve StockControlEvent üyeliği varsa => StockControlEvent!=null
                {
                    StockControlEvent(); // nesnenin kendisinin üye olduğu metotu tetikle(r)
                }
            }
        }

        // Dışarıdan ürünün şu kadar satıldığına dair talimat geliyor.
        public void Sell(int amount)
        {
            Stock -= amount; // adet Stock 'tan çıkartılıyor // Stock'un değişikliğinde get set çalışıyor
            Console.WriteLine("{1} Stock amount : {0}",Stock,ProductName); // şu kadar satıldı diye mesaj gösteriyor
        }
    }
}
