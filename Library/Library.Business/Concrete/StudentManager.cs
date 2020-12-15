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
    public class StudentManager:IStudentService
    {
        private IStudentDal studentDal;
        public int LastId;
        public StudentManager(IStudentDal studentDal)
        {
            this.studentDal = studentDal;
        }
        public StudentManager()
        {
            studentDal = new XStudentDal();
        }

        public Student GetStudentByName(string name)
        {
            return studentDal.GetAll().FirstOrDefault(i => i.StudentName == name);
        }

        public List<Student> Search(string key)
        {
            if (key == "" || key == null)
            {
                return studentDal.GetAll();
            }
            else
                return studentDal.GetAll().Where(
                    i => i.StudentName.ToLower().Contains(
                        key.ToLower())).ToList();
        }

        public List<Student> GetAll()
        {
            return studentDal.GetAll();
        }

        public Student Get(int id)
        {
            return studentDal.GetAll().FirstOrDefault(i => i.Identity == id);
        }

        private int LastIdentity(List<Student> theList)
        {
            int number = 0;
            foreach (var item in theList)
            {
                if (item.Identity > number)
                    number = item.Identity;
            }
   
            return number + 1;
        }

        public void Add(Student student)
        {
            int lastId = LastIdentity(GetAll());
            student.Identity = lastId;
            LastId = lastId;
            studentDal.Add(student);
        }

        public void Update(Student student)
        {
            studentDal.Update(student);
        }

        public void Delete(Student student)
        {
            studentDal.Delete(student);
        }
    }
}
