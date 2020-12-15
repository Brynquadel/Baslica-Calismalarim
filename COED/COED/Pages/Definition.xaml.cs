using COED.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfAnimatedGif;

namespace COED.Pages
{
    /// <summary>
    /// Interaction logic for Definition.xaml
    /// </summary>
    public partial class Definition : UserControl
    {


        public Definition()
        {
            InitializeComponent();
            Setup();
        }

        void Setup()
        {
            Loaded += Definition_Loaded;
            okeydb.Click += Okeydb_Click;
        }

        private void Okeydb_Click(object sender, RoutedEventArgs e)
        {
            Model.Connection = deftxtbox.Text;

            Thread thread2 = new Thread(() =>
            {
                Model.Baglanti = Data.ConnectionControl();
                Dispatcher.Invoke(() => { bitiskontrol(); });
            });

            kktest(2);
        }

        void kktest(int type)
        {
            if(type == 1)
            {
                beklewrp.Visibility = Visibility.Visible; content.Visibility = Visibility.Hidden;
                loadingLbl.Content = "Veritabanı bilgileri okunuyor";
                Thread thread2 = new Thread(() =>
                {
                    Data.SystemRefresh();
                    if (Model.tab == -1)
                    {
                        Window pencere = Home.Window;
                        pencere.Content = new Interfaces.Message("DB hatalı, bildirin. Tab: " + Model.tab, MessageType.error);
                        pencere.ShowDialog();
                        Environment.Exit(0);
                    }

                    Dispatcher.Invoke(() => {

                        Home.ContentFrame.Navigate(new Pages.Welcome());
                        defimage.Source = new BitmapImage(new Uri("/Images/db.png", UriKind.Relative));
                    });
                });
                thread2.Start();
            }
            else if (type == 2)
            {
                beklewrp.Visibility = Visibility.Visible; content.Visibility = Visibility.Hidden;
                loadingLbl.Content = "Veritabanı bağlantısı sağlanıyor -1";

                Thread thread2 = new Thread(() =>
                {
                    Model.Baglanti = Data.ConnectionControl();
                    Dispatcher.Invoke(() => { bitiskontrol(); });
                });
                thread2.Start();
            }
        }

        private void Definition_Loaded(object sender, RoutedEventArgs e)
        {
            Data.ConnectionOku();
            deftxtbox.Text = Model.Connection;

            kktest(2);
        }

        void bitiskontrol()
        {

            if (Model.Baglanti == false)
            {
                beklewrp.Visibility = Visibility.Hidden;
                content.Visibility = Visibility.Visible;
                defimage.Source = new BitmapImage(new Uri("/Images/db_error.png", UriKind.Relative));
            }
            else
            {
                kktest(1);
                Data.ConfigOlustur();
            }
        }

        private void Testbtn_Click(object sender, RoutedEventArgs e)
        {

            //bool kontrol = false;


            //Thread thread = new Thread(() =>
            //{
            //    kontrol = data.ConnectionControl(model);
            //    //loadingLbl.Content = "Tamamlandı, durum: " + kontrol;

            //    this.Dispatcher.Invoke(() => { loadingLbl.Content = "test"; });

            //    //Dispatcher.BeginInvoke(
            //    //    new ThreadStart(() => loadingLbl.Content = "test"));

            //});
            //thread.Start();


        }



        //var image = new BitmapImage();
        //image.BeginInit();
        //    image.UriSource = new Uri("/Images/murat2.gif", UriKind.Relative);
        //image.EndInit();
        //    ImageBehavior.SetAnimatedSource(resim, image);


    }
}
