using Library.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entity.Concrete
{
    public class Setting:IEntity
    {
        public int Identity { get; set; }
        public bool State { get; set; }

    }
}
