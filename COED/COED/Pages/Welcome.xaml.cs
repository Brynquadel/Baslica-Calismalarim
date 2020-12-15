using COED.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace COED.Pages
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : UserControl
    {
        //model.Connection = @"Data Source=DEMIRPC; initial Catalog=test; integrated Security=true;";
        //model.User = "Mustafa Demirel";

        BrushConverter bc = new BrushConverter();

        public Welcome()
        {
            InitializeComponent();
            Setup();
        }

        void Setup()
        {
            Loaded += Welcome_Loaded;

            wcreatebtn.Click += Wcreatebtn_Click;
            weditbtn.Click += Weditbtn_Click;
            wxmlbtn.Click += Wxmlbtn_Click;
        }

        private void Welcome_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Wxmlbtn_Click(object sender, RoutedEventArgs e)
        {
            Model.Process = ProcessType.xml;
            Home.ContentFrame.Navigate(new Interfaces.InterXml());
        }

        private void Weditbtn_Click(object sender, RoutedEventArgs e)
        {
            Model.Process = ProcessType.edit;
            Home.ContentFrame.Navigate(new Pages.Listcontent());
        }

        private void Wcreatebtn_Click(object sender, RoutedEventArgs e)
        {
            Model.Process = ProcessType.add; Model.ContentID = 1; // Normalde 0 alıyorduk, MG oto aldığı için 1 verdi, bizde bunu seçtik.
            Home.ContentFrame.Navigate(new Pages.Detail());
        }



    }
}
