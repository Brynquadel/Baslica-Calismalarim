using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Abstract
{
    interface ISettingService
    {
        void Update(Setting setting);
        Setting Get(int settingId);
        void ToggleMove(Setting setting);
    }
}
