using Exmark.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exmark.Entity
{
    class EMap : XEntity
    {
        [Key]
        public int Id { get; set; }
        public string Entity { get; set; }
        public bool Single { get; set; }
        public string Name { get; set; }
        public string Root { get; set; }
        // Bu seçenek güncel olarak takip edilecek zaten.
        //public bool WorkOnTheMap { get; set; }
        public string ExternalLocation { get; set; }
        public bool AutomaticId { get; set; }
        public bool CheckBeforeAdding { get; set; }
        //public bool CategoryMapOption { get; set; }
        public string CategoryProperty { get; set; }
    }

    class MapAbout
    {
        internal List<EMap> l;

        public MapAbout()
        {
            l = new List<EMap>();
        }

        private bool IsThereAny(string entityName)
        {
            bool value = false;
            foreach (var item in l)
            {
                if (item.Entity == entityName)
                {
                    value = true;
                    break;
                }
            }
            return value;
        }

        internal EMap Get(string externalLocation, string entityName)
        {
            UpdateList(externalLocation);

            EMap map = new EMap();
            map = null;

            foreach (var item in l)
            {
                if (item.Entity == entityName)
                {
                    map = item;
                    break;
                }
            }

            return map;
        }

        internal void Create(EMap m)
        {
            // Name ve Root bilgileri Before üzerinden yükleniyor.
            // Güncelleme bu bilgiler Instruction kısmı için düzenleniyor.
            // Kullandığımız entity'in Name ve Root bilgileri alınmalı.

            DEntity d = new DEntity();
            Envoy e = new Envoy();

            d.TargetLocation = m.ExternalLocation;

            d = e.Before(new EMap(), d);
            e.CreateIfNotThere(d);
            UpdateList(d.TargetLocation);

            if (!IsThereAny(m.Entity))
            {
                if (l.Count > 0)
                    m.Id = l[l.Count - 1].Id + 1;
                else
                    m.Id = 1;

                DataAccess.Insert insert = new DataAccess.Insert
                    (m, d);
            }
        }

        private void UpdateList(string ExternalLocation)
        {
            l.Clear();
            DEntity d = new DEntity();
            Envoy e = new Envoy();
            d.TargetLocation = ExternalLocation;
            d = e.Before(new EMap(), d);
            l = e.DataToList<EMap>(d);
        }

        internal void Delete(EMap map)
        {
            // Name ve Root bilgileri Before üzerinden yükleniyor.
            // Güncelleme bu bilgiler Instruction kısmı için düzenleniyor.
            // Kullandığımız entity'in Name ve Root bilgileri alınmalı.

            DEntity d = new DEntity();
            Envoy e = new Envoy();

            d.TargetLocation = map.ExternalLocation;

            d = e.Before(new EMap(), d);
            UpdateList(d.TargetLocation);

            EMap m = new EMap();

            if (IsThereAny(map.Entity))
            {
                m = l.FirstOrDefault(x => x.Entity == map.Entity);
            }

            DataAccess.Reduce delete = new DataAccess.Reduce(m, d);
        }
    }
}
