using Exmark.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exmark.Entity
{
    internal class ECategory : XEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class CategoryAbout
    {
        private static Envoy e;
        internal List<ECategory> s { get; set; }

        public CategoryAbout()
        {
            s = new List<ECategory>();
            e = new Envoy();
        }

        internal void Add(string ExternalLocation, string categoryName)
        {
            DEntity d = new DEntity();
            d.TargetLocation = ExternalLocation;
            d = e.Before(new ECategory(), d);
            e.CreateIfNotThere(d);
            UpdateList(ExternalLocation);

            if (!IsThereAny(categoryName))
            {
                ECategory c = new ECategory();
                c.Name = categoryName;
                if (s.Count > 0)
                    c.Id = s[s.Count - 1].Id + 1;
                else
                    c.Id = 1;
                DataAccess.Insert insert = new DataAccess.Insert
                    (c, d);
            }
        }

        internal void UpdateList(string ExternalLocation)
        {
            s.Clear();
            DEntity d = new DEntity();
            d.TargetLocation = ExternalLocation;
            d = e.Before(new ECategory(), d);
            s = e.DataToList<ECategory>(d);
        }

        internal bool IsThereAny(string name)
        {
            bool value = false;
            foreach (var item in s)
            {
                if (item.Name == name)
                {
                    value = true;
                    break;
                }
            }
            return value;
        }
    }
}
