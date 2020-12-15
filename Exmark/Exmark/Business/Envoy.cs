
namespace Exmark.Business
{
    using Exmark.Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Xml;

    internal class Envoy
    {

        internal DEntity Before(XEntity entity, DEntity d)
        {
            StringBuilder s = new StringBuilder();

            Type t = entity.GetType();

            if (t == new ECategory().GetType())
            {
                d.Name = "Category";
                d.Root = "Storage";
            }
            else if (t == new EStatus().GetType())
            {
                d.Name = "State";
                d.Root = "Status";
            }
            else if (t == new EMap().GetType())
            {
                d.Root = "Instruction";
                d.Name = "Map";
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(d.Name) && !String.IsNullOrWhiteSpace(d.Root))
                { }
                else
                {
                    if (d.Name is null)
                    {
                        d.Name = entity.GetType().Name.ToString();
                    }

                    //

                    string ca = d.Name.Substring((d.Name.Length - 2), 2);

                    string unsuzler = "bcdfghjklmnprsştvyzxwq";

                    if (ca.ToLower()[1] == 'y' && unsuzler.Contains(ca.ToLower()[0].ToString()))
                        s.Append(d.Name.Remove(d.Name.Length - 1) + "ies");
                    else
                        s.Append(d.Name + "s");

                    d.Root = s.ToString();
                }
            }

            d.Key = FindKey(entity);

            {
                if (String.IsNullOrWhiteSpace(d.CategorizeName))
                    d.MainLocation = d.Root + ".xml";
                else
                {
                    d.MainLocation = d.CategorizeName + @"\" + d.Root + ".xml";
                    d.IsCategorized = true;
                }

                if (!String.IsNullOrWhiteSpace(d.TargetLocation))
                    d.MainLocation = d.TargetLocation + @"\" + d.MainLocation;

                d.Continuity = CheckFileLocation(d.MainLocation);
            }

            return d;
        }

        internal void DataUpdate(XEntity entity, DEntity info)
        {
            DataAccess.Change change = new DataAccess.Change(entity, info);
        }

        internal void DataDelete(XEntity entity, DEntity info)
        {
            DataAccess.Reduce delete = new DataAccess.Reduce(entity, info);
        }

        internal void DataInsert(XEntity entity, DEntity info)
        {
            DataAccess.Insert insert = new DataAccess.Insert(entity, info);
        }

        internal List<T> DataToList<T>(DEntity d) where T : class, XEntity, new()
        {
            DataAccess.GetAll<T> getAll = new DataAccess.GetAll<T>(d);
            return getAll.ToList();
        }

        private bool CheckFileLocation(string fileName)
        {
            if (File.Exists(fileName))
                return true;
            else
                return false;
        }

        internal string FindKey(XEntity entity)
        {
            string value = null;
            foreach (var item in entity.GetType().GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(item, typeof(KeyAttribute));
                if (attribute != null)
                    value = item.Name;
            }
            return value;
        }

        internal void CreateIfNotThere(DEntity d)
        {
            string path;

            if (String.IsNullOrWhiteSpace(d.TargetLocation))
                path = d.CategorizeName;
            else
                path = d.TargetLocation + @"\" + d.CategorizeName;

            if (d.IsCategorized == true)
                Directory.CreateDirectory(path);

            if (!File.Exists(d.MainLocation))
            {
                XmlTextWriter writer = new XmlTextWriter(d.MainLocation, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement(d.Root);

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }

            Debug.WriteLine(d.MainLocation);
            Debug.WriteLineIf(true, d.Name);
        }



    }
}
