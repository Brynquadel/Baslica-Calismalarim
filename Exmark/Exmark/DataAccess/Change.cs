
namespace Exmark.DataAccess
{
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    internal class Change
    {
        public Change(XEntity entity, DEntity _)
        {
            if (_.Continuity)
            {
                XDocument xdoc;

                using (StreamReader oreader = new StreamReader(_.MainLocation, Encoding.UTF8))
                {
                    xdoc = XDocument.Load(oreader);
                }

                XElement rootelement = xdoc.Root;entity.GetType().GetProperty(_.Key).GetValue(entity).ToString();

                foreach (XElement item1 in rootelement.Elements())
                {
                    if (item1.Attribute("id").Value == entity.GetType().GetProperty(_.Key).GetValue(entity).ToString())
                    {
                        foreach (var item2 in entity.GetType().GetProperties())
                        {
                            if (item2.Name == _.Key) continue;
                            item1.Element(item2.Name).Value = item2.GetValue(entity).ToString();
                        }
                        break;
                    }
                }
                xdoc.Save(_.MainLocation);
            }
        }
    }
}
