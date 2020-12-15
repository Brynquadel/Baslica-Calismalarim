using COED.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace COED.Interfaces
{
    /// <summary>
    /// Interaction logic for InterXml.xaml
    /// </summary>
    public partial class InterXml : UserControl
    {

        public InterXml()
        {
            InitializeComponent();
            Setup();
        }

        List<string> bolumler = new List<string>();
        List<string> tipler = new List<string>();

        bool dosyavarmi = false;

        void Setup()
        {
            KontrolEt();
            BtnXmlExit.Click += BtnXmlExit_Click;

            BtnXmlDefine.Click += BtnXmlDefine_Click;

            BtnXmlEditId.Click += BtnProcess;
            BtnXmlCreateId.Click += BtnProcess;
        }

        private void BtnProcess(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            if (tag == "1")
            {
                Model.Process = ProcessType.add;
               
                Home.ContentFrame.Navigate(new Pages.Detail(bolumler,tipler,true));
            }
            else if (tag == "2")
            {
                Model.Process = ProcessType.edit;
                Home.ContentFrame.Navigate(new Pages.Listcontent(bolumler,tipler,true));
            }
        }

        private void BtnXmlDefine_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog filedialog = new Microsoft.Win32.OpenFileDialog();
            string dosya = null;
            if (filedialog.ShowDialog() == true)
            {
                dosya = filedialog.FileName;
                dosya = System.IO.Path.GetFullPath(dosya);
                {

                    bolumler.Clear(); tipler.Clear();
                    {
                        XmlTextReader oku = new XmlTextReader(dosya);
                        while (oku.Read())
                        {
                            switch (oku.Name)
                            {
                                case "type":
                                    {
                                        tipler.Add(oku.ReadString());
                                    }
                                    break;
                                case "part":
                                    {
                                        bolumler.Add(oku.ReadString());
                                    }
                                    break;
                            }
                        }
                        oku.Close();

                    }
                }
            }
            else { }

            dosyavarmi = true;
            KontrolEt();

        }

        private void BtnXmlExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        void KontrolEt()
        {
            if(dosyavarmi)
            {
                BtnXmlCreateId.IsEnabled = true;
                BtnXmlEditId.IsEnabled = true;
            }
            else
            {
                BtnXmlCreateId.IsEnabled = false;
                BtnXmlEditId.IsEnabled = false;
            }
        }


    }
}
