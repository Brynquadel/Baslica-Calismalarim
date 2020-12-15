using Library.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entity.Concrete
{
    public class Student : IEntity
    {
        public int Identity { get; set; }
        public string StudentName { get; set; }
        public string StudentCity { get; set; }
        public string StudentNumber { get; set; }
        public string StudentSchool { get; set; }
        public string StudentSchoolPart { get; set; }
        public string StudentPartClass { get; set; }
        public int StudentBookCount { get; set; }
        public List<string> CityList()
        {
            return new List<string>()
            {
               "İstanbul",
               "Ankara",
               "Erzurum",
               "İzmir",
               "Trabzon"
            };
        }

    }
}
