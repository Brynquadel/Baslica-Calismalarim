using Library.Business.Abstract;
using Library.DataAccess.Abstract;
using Library.DataAccess.Concrete.Xml;
using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Concrete
{
    public class SettingManager : ISettingService
    {
        private ISettingDal settingDal;
        public SettingManager()
        {
            settingDal = new XSettingDal();
        }

        public Setting Get(int settingId)
        {
            return settingDal.GetAll().FirstOrDefault(i=>i.Identity== settingId);
        }

        public void ToggleMove(Setting setting)
        {
            settingDal.Update(setting);
        }

        public void Update(Setting setting)
        {
            throw new NotImplementedException();
        }
    }
}
