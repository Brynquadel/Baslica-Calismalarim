using ExcelDataReader;
using Library.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Concrete.Excel
{
    public class EXEntityDal<TEntity> where TEntity : class, IEntity, new()
    {
        public List<TEntity> ReadExcel(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            IExcelDataReader excelReader;
            int counter = 0;

            if (Path.GetExtension(filePath).ToUpper() == ".XLS")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            List<TEntity> entityList = new List<TEntity>();

            while (excelReader.Read())
            {
                counter++;
                if (counter >1)
                {

                    TEntity entity = new TEntity();
                    int column = 0;

                    foreach (var item in entity.GetType().GetProperties())
                    {
                        if (item.Name.ToString() == XData<TEntity>.IdentityPattern)
                        {
                            continue;
                        }
                        else
                        {

                            object obj = excelReader.GetValue(column);



                            if (item.Name == "BookName")
                            {
                                try
                                {
                                    string test = obj as string;
                                    item.SetValue(entity, test.ToString());
                                }
                                catch (Exception)
                                {
                                }
                              
                            }

                            else if (item.Name=="BookBarcode")
                            {
                                try
                                {
                                    double test = (double)obj;
                                    item.SetValue(entity, test.ToString());
                                }
                                catch (Exception)
                                {
                                    string test = obj as string;
                                    item.SetValue(entity, test.ToString());
                                }
                            }
                            else if (item.Name == "BookLocation")
                            {
                                try
                                {
                                    string test = (obj) as string;
                                    item.SetValue(entity, test);
                                }
                                catch (Exception)
                                {

                                }
                            }
                            else if (item.Name == "BookReleaseDate")
                            {
                                try
                                {
                                    DateTime test = (DateTime)obj;
                                    item.SetValue(entity, test);
                                }
                                catch (Exception)
                                {
                                }
                            }
                            else if (item.Name == "DateOfIssue")
                            {
                                try
                                {
                                    DateTime test2 = (DateTime)obj;
                                    item.SetValue(entity, test2);
                                }
                                catch (Exception)
                                {

                                }
                            }
                            else if (item.Name == "DateOfCommitment")
                            {
                                try
                                {
                                    DateTime test3 = (DateTime)obj;
                                    item.SetValue(entity, test3);
                                }
                                catch (Exception)
                                {

                                }
                            }
                            else if (item.Name == "StudentNumber")
                            {
                                try
                                {
                                    double test = (double)obj;
                                    item.SetValue(entity, test.ToString());
                                }
                                catch (Exception)
                                {

                                }
                            }
                            else if (item.Name == "StudentPartClass")
                            {
                                try
                                {
                                    double test = (double)obj;
                                    item.SetValue(entity, test.ToString());
                                }
                                catch (Exception)
                                {

                                }
                            }
                            else if (item.Name == "StudentBookCount")
                            {
                                try
                                {
                                    string test = (obj) as string;
                                    item.SetValue(entity, test);
                                }
                                catch (Exception)
                                {

                                }
                            }
                            

                            else
                            {
                                try
                                {
                                    item.SetValue(entity,
                           Convert.ChangeType(
                           excelReader.GetString(column), item.PropertyType));
                                }
                                catch (Exception)
                                {

                                   
                                }
                            }

                            column++;
                        }

                     
                    }
                    entityList.Add(entity);
                }
            }
            excelReader.Close();
            return entityList;
        }
    }
}
