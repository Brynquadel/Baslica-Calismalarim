using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TURGWEB.Models
{
    public class news
    {

        public int id { get; set; }
        public int main_category { get; set; }
        public int sub_category { get; set; }
        public int owner { get; set; }
        public string header { get; set; }
        public string image { get; set; }
        public string detail { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_modified { get; set; }

    }
}