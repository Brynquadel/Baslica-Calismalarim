using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Abstract
{
    interface IStudentService
    {
        List<Student> GetAll();
        Student Get(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
    }
}
