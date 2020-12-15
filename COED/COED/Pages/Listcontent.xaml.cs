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

namespace COED.Pages
{
    /// <summary>
    /// Interaction logic for Listcontent.xaml
    /// </summary>
    public partial class Listcontent : UserControl
    {

        public Listcontent()
        {
            InitializeComponent();
            Loaded += Listcontent_Loaded;

        }
        List<string> bolumler = new List<string>();
        List<string> tipler = new List<string>();

        bool Xmlmi = false;

        public Listcontent(List<string> bolumler, List<string> tipler, bool Xmlmi)
        {
            InitializeComponent();

            this.bolumler = bolumler;
            this.tipler = tipler;
            this.Xmlmi = Xmlmi;

            Loaded += Listcontent_Loaded;
        }

        private void Listcontent_Loaded(object sender, RoutedEventArgs e)
        {

            List<int> liste = Data.CollectContent("types", Model.Connection);

            lcpanel.Children.Clear();
            Style style = this.FindResource("test") as Style;
            foreach (int sekme in liste)
            {
                Button btn = new Button
                {

                    Content = "Id " + sekme,
                    Width = 100,
                    Height = 40,
                    Padding = new Thickness(3),
                    Margin = new Thickness(5),
                    Tag = sekme,
                    Style = style,
                    Cursor = Cursors.Hand

                };
                btn.Click += Btn_Click;

                if (btn.Tag.ToString() != "0" && btn.Tag.ToString() != "1")
                {
                    lcpanel.Children.Add(btn);
                }

            }

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int tag = Convert.ToInt32(btn.Tag);


            if (tag != 0 && tag != -1)
            {
                Model.ContentID = tag;

                if(Xmlmi)
                    Home.ContentFrame.Navigate(new Pages.Detail(bolumler, tipler, true));
                else
                    Home.ContentFrame.Navigate(new Pages.Detail());


            }
            else
            {
                Window pencere = Home.Window;
                pencere.Content = new Interfaces.Message("Sabit değerler değiştirilemez", MessageType.error);
                pencere.ShowDialog();
            }
        }


    }
}
