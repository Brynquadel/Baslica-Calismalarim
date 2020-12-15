using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keywords
{
    class Program
    {
        static void Main(string[] args)
        {
            // var keyword
            // Değişkene atılan değer esnasında tipini alır sonradan değiştirilemez. Değer verilmeden oluşturulamaz.

            // ref keyword
            // bir değişkene, objeye, nesneye farklı bir noktadan erişilmesini sağlarız. gönderilen yer ile gönderildiği yer aynı alana müdahale eder. <> DoItMethod(ref variableName);

            // out keyword
            // ref'in aynısı ancak değişkenlerde değeri olmayanlar da gönderilebilir, ref'te bu durum hata verir. Gönderildiği methotta set etme zorunluluğu vardır.

            // params keyword // params int[] numbers
            // Params ile methoda istenildiği kadar aynı tipte değer gönderilebilir. <> private int Total(params int[] numbers) { }

            // operator
            // implicit ve explicit için geçerli, metot şeklinde tanımlama yapıp casting işlemlerini kontrol etmemizi sağlar.

        }
    }
}
