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
    /// Elaboration.xaml etkileşim mantığı
    /// </summary>
    public partial class Elaboration : UserControl
    {

        NewSDetailM NewSModel;

        public Elaboration(NewSDetailM NewSModel)
        {
            this.NewSModel = NewSModel;
            InitializeComponent();

            Loaded += Elaboration_Loaded;
            TDetailBackBtn.Click += TDetailBackBtn_Click;
        }

        private void Elaboration_Loaded(object sender, RoutedEventArgs e)
        {
            EVisibilityCmb.Items.Add("0");
            EVisibilityCmb.Items.Add("1");
            EVisibilityCmb.Items.Add("2");
            EVisibilityCmb.SelectedItem = "2";

            EMainCmb.Items.Add(NewSModel.maincateg);
            EMainCmb.SelectedItem = NewSModel.maincateg;

            ESubCmb.Items.Add(NewSModel.subcateg);
            ESubCmb.SelectedItem = NewSModel.subcateg;

            EOwnerTxt.Text = NewSModel.owner.ToString();
            EHeaderTxt.Text = NewSModel.title;
            EImageTxt.Text = NewSModel.picture;

            ECreatedTxt.Content = NewSModel.datec;
            EModifiedTxt.Content = NewSModel.datem;

            NewSModel = Data.newsDoldur();

        }

        private void TDetailBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NewSModel.status = Convert.ToInt32(EVisibilityCmb.Text);

            NewSModel.maincateg = Convert.ToInt32(EMainCmb.Text);
            NewSModel.subcateg = Convert.ToInt32(ESubCmb.Text);

            NewSModel.owner = Convert.ToInt32(EOwnerTxt.Text);
            NewSModel.title = EHeaderTxt.Text;
            NewSModel.picture = EImageTxt.Text;

            /*var ww = Window.GetWindow(this);
            ww.Close();*/
            Home.ContentFrame.Navigate(new Pages.Detail(NewSModel, ElaDonus: true));

        }
    }
}
