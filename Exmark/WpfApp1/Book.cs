using Exmark;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Book : XEntity
    {

        [Key]
        public int Id { get; set; }
     
        public string Name { get; internal set; }

        public string Category { get; set; }


    }
}
