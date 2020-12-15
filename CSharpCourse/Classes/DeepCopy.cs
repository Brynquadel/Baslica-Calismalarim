using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class DeepCopy
    {
        // Deep Copy

        // Bir nesnenin tamamen farklı bir kopyasını oluşturmak için kullanılır.

        // <> var yeniNesne = eskiNesne.DeepCopy(); // kullanılarak farklı bir tam kopya oluşturulur.
    }

    class Nesne2
    {
        public Nesne2 DeepCopy()
        {
            using (var ms = new MemoryStream()) // System.IO;
            {
                var formatter = new BinaryFormatter(); // System.Runtime.Serialization.Formatters.Binary;
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (Nesne2)formatter.Deserialize(ms);
            }

        }
    }
}
