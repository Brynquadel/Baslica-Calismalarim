using Library.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Concrete
{
    using DataAccess.Concrete.Excel;
    using System.IO;

    public class ExcelOptions<TEntity> where TEntity : class, IEntity, new()
    {
        public List<TEntity>  ReadExcel(string filePath)
        {
            EXEntityDal<TEntity> eXEntityDal = new EXEntityDal<TEntity>();

            if (TryFilePath(filePath) == true)
                return eXEntityDal.ReadExcel(filePath);
            else
                return null;
        }

        private bool TryFilePath(string filePath)
        {
            bool control = true;
            try
            {
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                stream.Close();
            }
            catch (Exception)
            {
                control = false;
            }
            return control;
        }
    }
}
