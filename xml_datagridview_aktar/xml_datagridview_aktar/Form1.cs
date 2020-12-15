using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace xml_datagridview_aktar
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        void yukle()
        {
            XmlDocument i = new XmlDocument();
            DataSet ds = new DataSet();
            //xml dosyamızı okumak için bir reader oluşturuyoruz.
            XmlReader xmlFile;
            //readerin içine pathini verdiğimiz dosyayı dolduruyoruz.burada önemli olan bir nokta var.ya path imizin başına @ yazacağız ya da çift  kullanacağız.
            xmlFile = XmlReader.Create(@"veri.xml", new XmlReaderSettings());
            //içeriği Dataset e aktarıyoruz.
            ds.ReadXml(xmlFile);
            //gridviewin kaynağı olarak dataseti gösteriyoruz.
            dataGridView1.DataSource = ds.Tables[0];
            xmlFile.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            yukle();
            label2.Font = new Font(fontyukle(@"Open_Sans.ttf"), 11);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument x = XDocument.Load(@"veri.xml");

            x.Element("data").Add(
            new XElement("game",
            new XAttribute("id", textBox1.Text),
            new XElement("exp", textBox2.Text),
            new XElement("eva", textBox3.Text)
            ));

            x.Save(@"veri.xml");
            yukle();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            XDocument x = XDocument.Load(@"veri.xml");

            //ürün id= textbox'a girilen numaralı öğrenciyi sil
            x.Root.Elements().Where(a => a.Attribute("id").Value == textBox1.Text).Remove();
            x.Save(@"veri.xml");
            yukle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XDocument x = XDocument.Load(@"veri.xml");

            if (checkBox1.Checked == false)
            {
                XElement node = x.Element("data").Elements("game").FirstOrDefault(a => a.Attribute("id").Value.Trim() == textBox1.Text);
                if (node != null)
                {
                    node.SetElementValue("exp", textBox2.Text);
                    node.SetElementValue("eva", textBox3.Text);

                    x.Save(@"veri.xml");
                    yukle();
                    MessageBox.Show(node.Element("exp").ToString());

                }
            }

            else if (checkBox1.Checked == true)
            {
                XElement node2 = x.Element("data").Elements("game").FirstOrDefault(a => a.Element("exp").Value.Trim() == textBox2.Text);
                XElement node3 = x.Element("data").Elements("game").FirstOrDefault(a => a.Element("eva").Value.Trim() == textBox3.Text);

                if (node2 == node3) try { node2.SetAttributeValue("id", textBox1.Text); x.Save(@"veri.xml"); yukle(); checkBox1.Checked = false; } catch { MessageBox.Show("'İd değiştir' işaretliyken sadece 'id' kutusunda değişiklik yapınız."); }

            }


        }

        private void button4_Click(object sender, EventArgs e)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("veri.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList kayitlar = root.SelectNodes("/data/game");


            foreach (XmlNode secilen in kayitlar)
            {
                string sirasi = secilen.Attributes["id"].InnerXml;

                if (sirasi == textBox1.Text)
                {

                    MessageBox.Show(secilen["exp"].InnerText);
                    MessageBox.Show(secilen["eva"].InnerText);
                }
            }



        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows[index].Selected = true;
            }
            catch { }


            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                string a = Convert.ToString(selectedRow.Cells["id"].Value);
                string b = Convert.ToString(selectedRow.Cells["exp"].Value);
                string c = Convert.ToString(selectedRow.Cells["eva"].Value);
                textBox1.Text = a; textBox2.Text = b; textBox3.Text = c;

            }
        }

        FontFamily fontyukle(string dosyayolu)
        {
            PrivateFontCollection fontlar = new PrivateFontCollection(); // Fontları tutan nesne oluşturuldu
            fontlar.AddFontFile(dosyayolu); //font dosyasını verilen dosya yolundan yükleniyor.
            return fontlar.Families[0]; //yüklenen fontu gönderir.
        }


        private void button5_Click(object sender, EventArgs e)
        {

            System.IO.File.Copy("Open_Sans.ttf", @"C:\Windows\Fonts\Open_Sans.ttf");



            /*
             * 
            XDocument x = XDocument.Load(@"okul.xml");

            if (checkBox1.Checked == false)
            {
                XElement node = x.Element("kisiler");
                if (node != null)
                {
                    node.SetElementValue("a", "a2");
                    node.SetElementValue("b", "b3");
                    x.Save(@"okul.xml");
                }



                  dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                */






            /*
             *                  // SIRA SIRA ALMA - case belirtmen lazım if'e göre alırsın değerleri. 
             * */
            /*
           XmlTextReader oku = new XmlTextReader("okul.xml");

           while (oku.Read()) //Dosyadaki veriler tükenene kadar okuma işlemi devam eder.
           {
               if (oku.NodeType == XmlNodeType.Element)//Düğümlerdeki veri element türünde ise okuma gerçekleşir.
               {
                   switch (oku.Name)//Elementlerin isimlerine göre okuma işlemi gerçekleşir.
                   {
                           case "adi":
                           if(oku.ReadString() == "Mustafa")
                           {

                           }
                           break;
                   }
               }
           }

           oku.Close();*/













        }
    }
}
