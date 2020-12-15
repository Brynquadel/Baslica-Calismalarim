using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class ShallowCopy
    {
        // Shallow Copy

        // Bir sınıfın kopyalanmasında primitive özelliklerin aktarılmasını sağlar.
        // Ancak custom tiplerin referans tiplerini alır.Bir bakıma yüzeysel kopyalama yapar.

        // <> var yeniNesne = eskiNesne.ShallowCopy(); // kullanılarak nesnenin kopyası oluşturulur.
    }
    public class Nesne1 : ICloneable
    {
        public int id { get; set; }
        public int name { get; set; }

        public object Clone()
        {
            // Farklı bir yol olarak sınıf için ICloneable 'dan inherit alabiliriz. Clone() adında bir metot uyguluyor.
            /* Bu özelliğe*/
            return this.MemberwiseClone(); // ekleyerek kullanılabilir hale getirebiliriz.
        }

        public Nesne1 ShallowCopy()
        {
            return (Nesne1)this.MemberwiseClone();
        }
    }
}
