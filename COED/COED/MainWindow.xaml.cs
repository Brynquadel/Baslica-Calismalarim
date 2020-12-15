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
using System.Windows.Threading;

using COED.Controls;

namespace COED
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer SubInfo;

        public MainWindow()
        {
            InitializeComponent();
            Setup();
           
        }

        void Setup()
        {
            btnExit.Click += BtnExit_Click;
            btnHome.Click += BtnHome_Click;
            TitleBar.MouseDown += TitleBar_MouseDown;
            SubInfo = new DispatcherTimer() { Interval = TimeSpan.FromTicks(500) };
            Home.ContentFrame = ContentFrame;
            Home.MainWindowS = this;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Model.ContentID = -1;
            Model.Process = ProcessType.free;

            SubInfo.Tick += SubInfo_Tick;
            SubInfo.Start();

            Home.ContentFrame.Navigate(new Pages.Definition());
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public void BtnHome_Click(object sender, RoutedEventArgs e)
        {
           
            Data.SystemRefresh();

            ContentFrame.Navigate(new Pages.Welcome());
            Model.Process = ProcessType.free;
            Model.ContentID = -1;
            Model.cab = 0;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SubInfo_Tick(object sender, EventArgs e)
        {
            LDB.Content = "DB " + Model.Baglanti.ToString();
            LPS.Content = "PS " + Model.Process.ToString().ToUpper();
            LTB.Content = "TB " + Model.tab.ToString();
            LSP.Content = "SP " + Model.pab.ToString();
            LCC.Content = "CC " + Model.cab.ToString();
            if (Model.ContentID == 0)
                LID.Content = "ID Plus";
            else
                LID.Content = "ID " + Model.ContentID;
        }

    }
}
