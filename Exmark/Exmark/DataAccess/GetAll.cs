
namespace Exmark.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    internal class GetAll<TEntity> where TEntity : class, XEntity, new()
    {
        private List<TEntity> _entities { get; set; }

        DEntity _ { get; set; }

        public GetAll(DEntity d)
        {
            _ = d;
            _entities = new List<TEntity>();
        }

        internal List<TEntity> ToList()
        {
            _entities.Clear();
            
            Load();
            return _entities;
        }

        private void Load()
        {
            if (_.Continuity)
            {
                XDocument xDoc;

                using (StreamReader oReader = new StreamReader(_.MainLocation, Encoding.Default))
                {
                    xDoc = XDocument.Load(oReader);
                }

                XElement rootElement = xDoc.Root;

                foreach (XElement elements in rootElement.Elements())
                {
                    TEntity entity = new TEntity();

                    foreach (var item in entity.GetType().GetProperties())
                    {
                        if (item.Name == _.Key)
                        {
                            entity.GetType().GetProperty(item.Name).SetValue(
                                entity, Convert.ChangeType(
                                    elements.Attribute("id").Value, item.PropertyType
                                    ));
                        }
                        else
                        {
                            entity.GetType().GetProperty(item.Name).SetValue(
                         entity, Convert.ChangeType(
                             elements.Element(item.Name).Value, item.PropertyType
                             ));
                        }
                    }
                    _entities.Add(entity);
                } 
            }
        }
    }

}
