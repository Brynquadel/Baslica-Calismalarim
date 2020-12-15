
namespace Exmark.DataAccess
{
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    internal class Reduce
    {
        public Reduce(XEntity entity, DEntity _)
        {
            if (_.Continuity)
            {
                XDocument xDoc;

                using (StreamReader oReader = new StreamReader(_.MainLocation, Encoding.UTF8))
                {
                    xDoc = XDocument.Load(oReader);
                }

                XElement rootElement = xDoc.Root;

                foreach (XElement item in rootElement.Elements())
                {
                    if (item.Attribute("id").Value == entity.GetType().GetProperty(_.Key).GetValue(entity).ToString())
                    {
                        item.Remove();
                    }
                }
                xDoc.Save(_.MainLocation);
            }
        }
    }
}
