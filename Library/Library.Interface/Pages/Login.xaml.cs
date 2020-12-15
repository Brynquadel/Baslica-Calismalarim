using Library.Entity.Concrete;
using Library.Interface.Controls;
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

namespace Library.Interface.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            Loaded += Login_Loaded;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (kullanici.Text.ToLower() == "Kütüphane" || kullanici.Text.ToLower() == "admin" && sifre.Password == "123456")
            {
                KeySet.Yetki = true;
                Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
                frame.Navigate(new Pages.Sequence(new Book()));

                var t = ((MainWindow)Application.Current.MainWindow).menuExt;
                t.Visibility = Visibility.Visible;

            }
            else if (kullanici.Text == "mustafa" || kullanici.Text.ToLower() == "alper" && sifre.Password == "123456")
            {
                KeySet.Yetki = false;
                Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
                frame.Navigate(new Pages.Sequence(new Book()));

                var t = ((MainWindow)Application.Current.MainWindow).menuExt;
                t.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı", "Tekrar deneyin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
