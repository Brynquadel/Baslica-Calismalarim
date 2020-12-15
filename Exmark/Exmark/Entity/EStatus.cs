using Exmark.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exmark.Entity
{
    public class EStatus : XEntity
    {
        [Key]
        public int Id { get; set; }
        public string Entity { get; set; }
        public int Recent { get; set; }
    }

    internal class StatusAbout
    {
        private static Envoy e;

        private static List<EStatus> s { get; set; }

        public StatusAbout()
        {
            e = new Envoy();
            s = new List<EStatus>();
        }

        internal void Create(string ExternalLocation, Type t)
        {
            DEntity d = new DEntity();
            d.TargetLocation = ExternalLocation;
            //
            d = e.Before(new EStatus(), d);
            e.CreateIfNotThere(d);
            UpdateList(ExternalLocation);

            EStatus state = IsThereAny(t.Name);

            if (state is null)
            {
                EStatus status = new EStatus();
                status.Entity = t.Name;
                status.Recent = 0;
                if (s.Count > 0)
                        status.Id = s[s.Count - 1].Id + 1;
                else
                    status.Id = 1;

                DataAccess.Insert add = new DataAccess.Insert(status, d);
            }
        }

        internal void Add(string externalLocation, Type t)
        {
            DEntity d = new DEntity();
            d.TargetLocation = externalLocation;

            d = e.Before(new EStatus(), d);

            UpdateList(externalLocation);

            EStatus s = IsThereAny(t.Name);

            s.Recent += 1;

            DataAccess.Change change = new DataAccess.Change(s, d);
        }

        internal EStatus IsThereAny(string name)
        {
            EStatus state = null;
            foreach (var item in s)
            {
                if (item.Entity == name)
                {
                    state = item;
                    break;
                }
            }
            return state;
        }

        internal void UpdateList(string externalLocation)
        {
            s.Clear();
            DEntity d = new DEntity();
            d.TargetLocation = externalLocation;
            d = e.Before(new EStatus(), d);
            s = e.DataToList<EStatus>(d);
        }
    }
}
