using Library.DataAccess.Abstract;
using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Concrete.Xml
{
    public class XStudentDal : XEntityRepoBase<Student>, IStudentDal
    {

    }
}
