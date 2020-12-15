using Exmark;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konsol
{
    class Student:XEntity
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }













    }
}
