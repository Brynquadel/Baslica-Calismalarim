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

using COED.Controls;

namespace COED.Interfaces
{
    /// <summary>
    /// Interaction logic for Define.xaml
    /// </summary>
    public partial class Define : UserControl
    {
        Model model; DefineType type;
        public Define(Model model, DefineType type)
        {
            InitializeComponent();
            this.model = model;
            this.type = type;

            Setup();
        }

        void Setup()
        {
            Loaded += Define_Loaded;
            defexitbtn.Click += Defexitbtn_Click;
            defokbtn.Click += Defokbtn_Click;
            deftxtbox.KeyUp += Deftxtbox_KeyUp;
        }

        private void Deftxtbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            e.Handled = true; // yazdır yazdırma
            Defokbtn_Click(sender, e);
        }

        private void Defokbtn_Click(object sender, RoutedEventArgs e)
        {
            if (type == DefineType.database)
            {
                model.Connection = deftxtbox.Text;
            }

            else if (type == DefineType.user)
                model.User = deftxtbox.Text;

            Defexitbtn_Click(sender, e);
        }

        private void Define_Loaded(object sender, RoutedEventArgs e)
        {
            if (type == DefineType.database)
            {
                defimage.Source = new BitmapImage(new Uri(@"/Images/db.png", UriKind.Relative));
                deflabel.Content = "Database";
                deftxtbox.Text = model.Connection;
            }
            else if (type == DefineType.user)
            {
                defimage.Source = new BitmapImage(new Uri(@"/Images/user.png", UriKind.Relative));
                deflabel.Content = "User";
                deftxtbox.Text = model.User;
            }

        }

        private void Defexitbtn_Click(object sender, RoutedEventArgs e)
        {
            var thisWindow = Window.GetWindow(this);
            thisWindow.Close();
        }

    }
}
