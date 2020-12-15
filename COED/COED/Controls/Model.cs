using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COED.Controls
{
    //
    // Summary:
    //     PATATES.
    public static class Model
    {
        public static int ContentID { get; set; } // public static Content Identity

        public static string Connection { get; set; }
        public static bool Baglanti { get; set; }

        public static string User { get; set; }

        public static List<int> ContentList { get; set; } // Content Identity List

        public static ProcessType Process { get; set; }


        public static int cab { get; set; } // Content Count
        public static int pab { get; set; } // Panel Number
        public static int tab { get; set; } // Tabs

        public static List<string> donus_tipler { get; set; }
        public static List<string> donus_bolumler { get; set; }
    }

    public class NewSDetailM
    {
        public int status { get; set; }
        // 0 silinmiş, 1 aktif, 2 taslak

        public int maincateg { get; set; }
        public int subcateg { get; set; }

        public int owner { get; set; }
        public string title { get; set; }
        public string picture { get; set; }
        public int likes { get; set; }

        public DateTime datec { get; set; }
        public DateTime datem { get; set; }
    }


}
