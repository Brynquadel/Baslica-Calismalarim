using COED.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;

namespace COED.Controls
{
    public static class Data
    {

        public static void ConfigOlustur()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CED/Connection.txt");


            if (File.Exists(path) == true)
                File.Delete(path);

            StreamWriter write = new StreamWriter(path);
            write.Write(Model.Connection);
            write.Close();


        }

        public static void ConnectionOku()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CED/Connection.txt");
            StreamReader wrt = new StreamReader(path);

            Model.Connection = wrt.ReadLine(); wrt.Close();
        }

        public static void SystemRefresh()
        {
            Model.tab = SystemOperation("tab", Model.Connection);
            if (SystemOperation("dbe", Model.Connection) == 0)
                Environment.Exit(0);
        }

        public static bool ConnectionControl()
        {
            bool boolean = true;
            try
            {
                SqlConnection test = new SqlConnection(Model.Connection);
                test.Open();
            }
            catch
            {
                boolean = false;
            }
            return boolean;
        }

        public static void dbRemoveUpp(string tableName)
        {
            #region update
            using (SqlConnection con = new SqlConnection(Model.Connection))
            {
                con.Open();
                SqlCommand com = new SqlCommand($"DELETE FROM {tableName} WHERE id=@id", con);
                com.Parameters.AddWithValue("id", Model.ContentID);
                com.ExecuteNonQuery();
                con.Close();
            }
            #endregion
        }

        public static void dbEditUpp(List<string> tipler, List<string> bolumler, NewSDetailM newsModel)
        {
            string typeString, partString;
            #region CreatingCommands
            {
                string komut = "UPDATE types SET";
                for (int i = 0; i < Model.tab; i++)
                {
                    string temp = ", t{0}=@t{0}";
                    temp = string.Format(temp, (i + 1));
                    komut = komut + temp;
                }
                komut = komut + " WHERE id=@id"; komut = komut.Replace("SET,", "SET");
                typeString = komut;
            }

            {
                string komut = "UPDATE parts SET";
                for (int i = 0; i < Model.tab; i++)
                {
                    string temp = ", p{0}=@p{0}";
                    temp = string.Format(temp, (i + 1));
                    komut = komut + temp;
                }
                komut = komut + " WHERE id=@id"; komut = komut.Replace("SET,", "SET");
                partString = komut;

            }
            #endregion
            #region update
            using (SqlConnection con = new SqlConnection(Model.Connection))
            {
                con.Open();
                SqlCommand command1 = new SqlCommand(typeString, con);
                SqlCommand command2 = new SqlCommand(partString, con);
                command1.Parameters.AddWithValue("@id", Model.ContentID);
                command2.Parameters.AddWithValue("@id", Model.ContentID);

                for (int i = 0; i < Model.tab; i++)
                {
                    try
                    {
                        command1.Parameters.AddWithValue("t" + (i + 1), tipler[i].ToString());
                        command2.Parameters.AddWithValue("p" + (i + 1), bolumler[i].ToString());
                    }
                    catch
                    {
                        command1.Parameters.AddWithValue("t" + (i + 1), "");
                        command2.Parameters.AddWithValue("p" + (i + 1), "");
                    }
                }
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();

                con.Close();


                con.Open();
                SqlCommand com = new SqlCommand("UPDATE details SET status=@status,category_main=@category_main,category_sub=@category_sub,owner=@owner,title=@title,picture=@picture,likes=@likes,date_created=@date_created,date_modified=@date_modified WHERE id=@id", con);
                com.Parameters.AddWithValue("id", Model.ContentID);
                com.Parameters.AddWithValue("status",newsModel.status);
                com.Parameters.AddWithValue("category_main", newsModel.maincateg);
                com.Parameters.AddWithValue("category_sub", newsModel.subcateg);
                com.Parameters.AddWithValue("owner", newsModel.owner);
                com.Parameters.AddWithValue("title", newsModel.title);
                com.Parameters.AddWithValue("picture", newsModel.picture);
                com.Parameters.AddWithValue("likes", newsModel.likes);
                com.Parameters.AddWithValue("date_created", newsModel.datec);
                com.Parameters.AddWithValue("date_modified", newsModel.datem);

                com.ExecuteNonQuery();
                con.Close();






            }
            #endregion
        }

        public static void dbAddUpp(List<string> tipler, List<string> bolumler, NewSDetailM newsModel)
        {
            string typeString, partString;
            #region Commands
            {
                {
                    string komut = "INSERT INTO types(id";
                    for (int i = 0; i < Model.tab; i++)
                    {
                        string temp = ",t{0}";
                        temp = string.Format(temp, (i + 1));
                        komut = komut + temp;
                    }
                    komut = komut + ") values (@id";
                    for (int i = 0; i < Model.tab; i++)
                    {
                        string temp = ",@t{0}";
                        temp = string.Format(temp, (i + 1));
                        komut = komut + temp;
                    }
                    typeString = komut + ")";
                    typeString = typeString.Replace("id,", null);
                    typeString = typeString.Replace("@id,", null);
                    typeString = typeString.Replace("@@t", "@t");
                }

                {
                    string komut = "INSERT INTO parts(id";
                    for (int i = 0; i < Model.tab; i++)
                    {
                        string temp = ",p{0}";
                        temp = string.Format(temp, (i + 1));
                        komut = komut + temp;
                    }
                    komut = komut + ") values (@id";
                    for (int i = 0; i < Model.tab; i++)
                    {
                        string temp = ",@p{0}";
                        temp = string.Format(temp, (i + 1));
                        komut = komut + temp;
                    }
                    partString = komut + ")";
                    partString = partString.Replace("id,", null);
                    partString = partString.Replace("@id,", null);
                    partString = partString.Replace("@@p", "@p");
                }
            }

            #endregion
            #region update

            using (SqlConnection con = new SqlConnection(Model.Connection))
            {
                con.Open();
                SqlCommand com1 = new SqlCommand(typeString, con);
                SqlCommand com2 = new SqlCommand(partString, con);

                for (int i = 0; i < Model.tab; i++)
                {
                    try
                    {
                        com1.Parameters.AddWithValue("t" + (i + 1), tipler[i].ToString());
                        com2.Parameters.AddWithValue("p" + (i + 1), bolumler[i].ToString());
                    }
                    catch
                    {
                        com1.Parameters.AddWithValue("t" + (i + 1), "");
                        com2.Parameters.AddWithValue("p" + (i + 1), "");
                    }

                }
                com1.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                com1.ExecuteNonQuery();
                com2.ExecuteNonQuery();

                con.Close();


                con.Open();
                SqlCommand com = new SqlCommand("INSERT INTO details(status,category_main,category_sub,owner,title,picture,likes,date_created,date_modified) values (@status,@category_main,@category_sub,@owner,@title,@picture,@likes,@date_created,@date_modified)", con);
                com.Parameters.AddWithValue("status", newsModel.status);
                com.Parameters.AddWithValue("category_main", newsModel.maincateg);
                com.Parameters.AddWithValue("category_sub", newsModel.subcateg);
                com.Parameters.AddWithValue("owner", newsModel.owner);
                com.Parameters.AddWithValue("title", newsModel.owner);
                com.Parameters.AddWithValue("picture", newsModel.picture);
                com.Parameters.AddWithValue("likes", newsModel.likes);
                com.Parameters.AddWithValue("date_created", newsModel.datec);
                com.Parameters.AddWithValue("date_modified", newsModel.datem);

                com.ExecuteNonQuery();
                con.Close();


            }
            #endregion
        }

        public static void XmlFilePrint(List<string> tipler, List<string> bolumler)
        {
            string dpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            {
                int intager = 0; string filename = "Çalışma Sayfası";
                XmlTextWriter xmlolustur = new XmlTextWriter(dpath + @"\" + filename + ".xml", UTF8Encoding.UTF8);//Dosyanın oluşturulacağı dizin,Kodlaması
                xmlolustur.WriteStartDocument();//Xml içine Element Oluşturma işlemine başlanıyor.
                xmlolustur.WriteStartElement("data");//item Etiketi ekledik.
                xmlolustur.WriteEndDocument();//Element Oluşturma işlemi bitti.
                xmlolustur.Close();//Dosya oluşturuldu ve bağlantı kapatıldı.
                XmlDocument doc = new XmlDocument();
                doc.Load(dpath + @"\" + filename + ".xml");

                foreach (string salla in tipler)
                {

                    XmlElement UserElement = doc.CreateElement("content");//Element Ekledik.

                    XmlElement kullaniciadi = doc.CreateElement("type");//Kullanicilar elementi içine bir kayıt ekledik
                    kullaniciadi.InnerText = tipler[intager].ToString();//kayıt için değer atadık
                    UserElement.AppendChild(kullaniciadi);//kayıt için parent atadık (UserElemet parenti)

                    XmlElement kullanimlimit = doc.CreateElement("part");//Kullanicilar elementi içine bir kayıt ekledik
                    kullanimlimit.InnerText = bolumler[intager].ToString();//kayıt için değer atadık
                    UserElement.AppendChild(kullanimlimit);//kayıt için parent atadık (Kullanicilar parenti)

                    doc.DocumentElement.AppendChild(UserElement);//xml dosyamıza element ve kayıtları ekledik

                    intager++;
                }
                XmlTextWriter xmleEkle = new XmlTextWriter(dpath + @"\" + filename + ".xml", null);//xml dosyamıza fiziksel olarak kayıtları yazıyoruz
                xmleEkle.Formatting = Formatting.Indented;
                doc.WriteContentTo(xmleEkle);//kayıtlar eklendi
                xmleEkle.Close();//dosya kapatıldı
            }
        }

        public static List<string> itCorrect(string ContentName, WrapPanel upanel)
        {
            string name = ContentName;
            if (ContentName != "tipler" && ContentName != "bolumler")
                return null;

            List<string> cs = new List<string>();

            bool oncelik = true;
            foreach (var item in upanel.Children)
            {
                WrapPanel wrp = (WrapPanel)item;
                foreach (var item2 in wrp.Children)
                {
                    if (item2 is WrapPanel) continue;
                    RichTextBox rich = (RichTextBox)item2;

                    if (oncelik == true)
                    {
                        oncelik = false;
                        if (name == "tipler")
                            cs.Add(new TextRange(rich.Document.ContentStart, rich.Document.ContentEnd).Text.TrimEnd());
                    }

                    else if (oncelik == false)
                    {
                        oncelik = true;
                        if (name == "bolumler")
                            cs.Add(new TextRange(rich.Document.ContentStart, rich.Document.ContentEnd).Text.TrimEnd());
                    }



                }

            }

            return cs;
        }

        public static int SystemOperation(string DBOId, string Connection) // Database Operation Identity
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM properties WHERE id=@id", con);
                com.Parameters.AddWithValue("id", DBOId);
                SqlDataReader read = com.ExecuteReader();

                if (read.Read())
                    return Convert.ToInt32(read["value"]);
                else
                    return -1;
            }
        }

        public static List<int> CollectContent(string TableName, string Connection)
        {
            List<int> cs = new List<int>();

            using (SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                SqlCommand com = new SqlCommand($"SELECT * FROM {TableName}", con);
                using (var dataReader = com.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        foreach (var item in dataReader)
                        {
                            cs.Add(
                                Convert.ToInt32(dataReader["id"])
                                );
                        }
                    }
                }
            }
            return cs;
        }

        public static int LastIdentity(string TableName, string Connection)
        {
            List<int> cs = new List<int>();

            using (SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                SqlCommand com = new SqlCommand($"SELECT * FROM {TableName}", con);
                using (var dataReader = com.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        foreach (var item in dataReader)
                        {
                            cs.Add(
                                Convert.ToInt32(dataReader["id"])
                                );
                        }
                    }
                }
            }

            return cs[cs.Count - 1];
        }

        public static List<string> GetTypePart(string TableName, int Id, string Connection)
        {
            List<string> cs = new List<string>();

            using (SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                SqlCommand com = new SqlCommand($"SELECT * FROM {TableName} WHERE id=@id", con);
                com.Parameters.AddWithValue("id", Id);
                SqlDataReader dr = com.ExecuteReader();

                TableName = TableName.Substring(0, TableName.Length - 4);
                // Bunu da unutma :D parts'ı part olarak aldığın için sorun olmuş. 
                // Aslında son güncellemede 'p' olmuştu. Neyse 1 idi 4 yaptık.

                if (dr.Read())
                {
                    int i = 0;
                    while (true)
                    {
                        i++;
                        try
                        {
                            if (dr[TableName + i].ToString() != "" && dr[TableName + i].ToString() != null && dr[TableName + i].ToString() != " ")
                                cs.Add(dr[TableName + i].ToString());
                        }
                        catch { break; }
                    }
                }
                con.Close();
            }
            return cs;
        }

        public static NewSDetailM newsDoldur()
        {
            NewSDetailM news = new NewSDetailM();

            news.status = 2;
            news.maincateg = 0;
            news.subcateg = 0;

            news.owner = 0;
            news.title = "title";
            news.picture = "picture link www";
            news.likes = 0;

            news.datec = Convert.ToDateTime("16.03.2019");
            news.datem = Convert.ToDateTime("16.03.2019");

            return news;
        }

        public static NewSDetailM GetNews(string Connection, int id)
        {
            NewSDetailM value = new NewSDetailM();
            using (SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM details WHERE id=@id", con);
                com.Parameters.AddWithValue("id", id);
                SqlDataReader rd = com.ExecuteReader();

                if (rd.Read())
                {

                    value.status = Convert.ToInt32(rd["status"]);
                    value.maincateg = Convert.ToInt32(rd["category_main"]);
                    value.subcateg = Convert.ToInt32(rd["category_sub"]);

                    value.owner = 1;
                    value.title = rd["title"].ToString();
                    value.picture = rd["picture"].ToString();
                    value.likes = Convert.ToInt32(rd["likes"]);

                    value.datec = Convert.ToDateTime(rd["date_created"]);
                    value.datem = Convert.ToDateTime(rd["date_modified"]);
                }

            }

            return value;


        }



    }
}
