
namespace Library.DataAccess.Concrete.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Windows;
    using System.Xml.Linq;
    using Library.DataAccess.Abstract;
    using Library.Entity.Abstract;

    public class XEntityRepoBase<TEntity> : IEntityDalRepo<TEntity> where TEntity : class, IEntity, new()
    {
        string fileName;
        public XEntityRepoBase()
        {
            fileName = XConfig<TEntity>.DetectWorkFileName(new TEntity());
        }

        private static PropertyInfo Info(TEntity entity)
        {
            return entity.GetType().GetProperty(XData<TEntity>.IdentityPattern);
        }

        public void Add(TEntity entity)
        {
            XDocument xDoc;

            using (StreamReader oReader = new StreamReader(fileName, Encoding.UTF8))
            {
                xDoc = XDocument.Load(oReader);
            }

            XElement rootElement = xDoc.Root;

            XElement newElement = new XElement(XData<TEntity>.RootName(entity));

            XAttribute idAttribute = new XAttribute("id", Info(entity).GetValue(entity).ToString());

            List<XElement> elements = new List<XElement>();

            foreach (var item in entity.GetType().GetProperties())
            {
                if (item.Name == XData<TEntity>.IdentityPattern)
                    continue;
                elements.Add(new XElement(item.Name, item.GetValue(entity)));
            }

            newElement.Add(idAttribute, elements);

            rootElement.Add(newElement);

            xDoc.Save(fileName);
        }

        public void Delete(TEntity entity)
        {
            XDocument xDoc;

            using (StreamReader oReader = new StreamReader(fileName, Encoding.UTF8))
            {
                xDoc = XDocument.Load(oReader);
            }

            XElement rootElement = xDoc.Root;

            foreach (XElement rehberimiz in rootElement.Elements())
            {
                if (rehberimiz.Attribute("id").Value == Info(entity).GetValue(entity).ToString())
                {
                    rehberimiz.Remove();
                }
            }
            xDoc.Save(fileName);
        }

        public List<TEntity> GetAll()
        {
            List<TEntity> entities = new List<TEntity>();

            XDocument xDoc;

            using (StreamReader oReader = new StreamReader(fileName, Encoding.UTF8))
            {
                xDoc = XDocument.Load(oReader);
            }

            XElement rootElement = xDoc.Root;

            foreach (XElement rehberimiz in rootElement.Elements())
            {
                TEntity entity = new TEntity();

                foreach (var item in entity.GetType().GetProperties())
                {
                    if (item.Name == XData<TEntity>.IdentityPattern)
                    {
                        entity.GetType().GetProperty(item.Name).SetValue(
                            entity, Convert.ChangeType(
                                rehberimiz.Attribute("id").Value, item.PropertyType
                                ));
                    }
                    else
                    {

                        try
                        {
                            entity.GetType().GetProperty(item.Name).SetValue(
                         entity, Convert.ChangeType(
                             rehberimiz.Element(item.Name).Value, item.PropertyType
                             ));
                        }
                        catch (Exception)
                        {
                            
                        }
                   
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }

        public void Update(TEntity entity)
        {
            XDocument xDoc;

            using (StreamReader oReader = new StreamReader(fileName, Encoding.UTF8))
            {
                xDoc = XDocument.Load(oReader);
            }

            XElement rootElement = xDoc.Root;

            foreach (XElement rehberimiz in rootElement.Elements())
            {
                if (rehberimiz.Attribute("id").Value == Info(entity).GetValue(entity).ToString())
                {
                    foreach (var item in entity.GetType().GetProperties())
                    {
                        try
                        {
                            if (item.Name == XData<TEntity>.IdentityPattern) continue;
                            rehberimiz.Element(item.Name).Value = item.GetValue(entity).ToString();
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;
                }
            }
            xDoc.Save(fileName);
        }
    }
}
