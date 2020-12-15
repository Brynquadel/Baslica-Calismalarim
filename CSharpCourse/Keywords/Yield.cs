using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keywords
{
    class Yield
    {
        // Yield Keyword

        // Alandaki değere göre iterasyon hazırlar.Foreach ile kullanılabilir.
        // Döndürülen değerin IEnumerable, IEnumerator, IEnumerable<T> veya IEnumerator<T>
        // olması veya türetilmiş olması gerekmektedir.
        // Return işlemi değere göre belirlenir, tamamlanınca döndürülür.

        //IEnumerable<string> veriler = Work.VerileriGetir2();
    }

    class Work
    {
        static public IEnumerable<string> VerileriGetir2()
        {
            string[] Gunler = { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };
            foreach (var Gun in Gunler)
                yield return Gun;
        }

        static public IEnumerable<string> VerileriGetir3()
        {
            yield return "Pazartesi";
            yield return "Salı";
            yield return "Çarşamba";
            yield return "Perşembe";
            yield return "Cuma";
            yield return "Cumartesi";
            yield return "Pazar";
        }
    }
}
