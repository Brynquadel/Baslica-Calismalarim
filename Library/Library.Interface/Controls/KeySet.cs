using Library.Entity.Abstract;
using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interface.Controls
{

    static class KeySet
    {
        internal static bool Yetki { get; set; }
        internal static bool Pressing { get; set; }
        internal static IEntity entity { get; set; }
        internal static List<Book> bookList = new List<Book>();
        internal static List<Student> studentList = new List<Student>();
    }
}
