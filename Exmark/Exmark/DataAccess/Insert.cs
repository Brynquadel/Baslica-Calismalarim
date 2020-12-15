
namespace Exmark.DataAccess
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    internal class Insert
    {
        public Insert(XEntity entity, DEntity _)
        {
            if (true)
            {
                XDocument xDoc;

                using (StreamReader oReader = new StreamReader(_.MainLocation, Encoding.UTF8))
                {
                    xDoc = XDocument.Load(oReader);
                }

                XElement rootElement = xDoc.Root;

                XElement newElement = new XElement(_.Name);

                XAttribute idAttribute = new XAttribute("id", entity.GetType().GetProperty(_.Key).GetValue(entity).ToString());

                List<XElement> elements = new List<XElement>();

                foreach (var item in entity.GetType().GetProperties())
                {
                    if (item.Name == _.Key)
                        continue;
                    else
                        elements.Add(new XElement(item.Name, item.GetValue(entity)));
                }
                newElement.Add(idAttribute, elements);

                rootElement.Add(newElement);

                xDoc.Save(_.MainLocation);
            }
        }
    }
}
