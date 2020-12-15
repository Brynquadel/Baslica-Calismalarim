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

namespace COED.Interfaces
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {

        string content; MessageType type;

        public Message(string content, MessageType type)
        {
            InitializeComponent();
            this.content = content;
            this.type = type;

            Setup();
        }

        public void Setup()
        {
            Loaded += Message_Loaded;
            messexitbtn.Click += Messexitbtn_Click;
            messokbtn.Click += Messexitbtn_Click;
        }

        private void Messexitbtn_Click(object sender, RoutedEventArgs e)
        {
            var wind = Window.GetWindow(this); wind.Close();
        }

        private void Message_Loaded(object sender, RoutedEventArgs e)
        {
            messtext.Content = content;
            if (type == MessageType.error)
            {
                messimage.Source = new BitmapImage(new Uri(@"/Images/n_error.png", UriKind.Relative));
                messlabel.Content = "Hata";
            }
            else if (type == MessageType.warn)
            {
                messimage.Source = new BitmapImage(new Uri(@"/Images/n_warning.png", UriKind.Relative));
                messlabel.Content = "Uyarı";
            }
        }


    }
}
