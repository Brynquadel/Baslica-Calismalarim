using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
using COED.Controls; //
using Panel = COED.Controls.Panel; //

namespace COED.Pages
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : UserControl
    {
        public WrapPanel wrpanel;

        public List<string> bolumler = new List<string>();
        public List<string> tipler = new List<string>();

        BrushConverter bc = new BrushConverter();

        Panel panel = new Panel();

        NewSDetailM NewSModel;

        bool Xmlmi = false;

        public Detail()
        {
            InitializeComponent();
            Setup();
        }

        bool ElaDonus = false;
        public Detail(NewSDetailM newSModelP, bool ElaDonus)
        {
            // ContentFrame değişince içerisindeki tüm bilgiler nedense kayboluyor veya bozuluyor.
            // Bunun önüne geçmek için elimdeki verileri işlem sayfasına gönderiyorum, kullanılan veya kullanılmayan verileri
            // geri dönüyor. Buraya kadar sorun yok, asıl olay bundan sonra başlıyor. Birden fazla geri dönüşü yöneten Detail sayfası,
            // bunu yönetebilmesi için bir sistemi olması gerekiyor. Xmlmi de bu kolaydı çünkü tek bir dönüş tipinden işlem yaptırıyorum.
            // Bu sefer de tek bir dönüş tipi görünüyor ancak dönen verilerin sorunsuzca yerleştirilmesi gerekiyor.
            // Yerleşecek verilerin 

            InitializeComponent();
            NewSModel = newSModelP;
            /*********************/
            bolumler = Model.donus_bolumler;
            tipler = Model.donus_tipler;
            /*************** burayı unutma :D ****////


            this.ElaDonus = true;
            Setup();
        }

        public Detail(List<string> bolumler, List<string> tipler, bool Xmlmi)
        {
            InitializeComponent();

            this.bolumler = bolumler;
            this.tipler = tipler;

            this.Xmlmi = Xmlmi;

            Setup();
        }

        void Setup()
        {
            Loaded += Detail_Loaded;

            TAddBtn.Click += TAddBtn_Click;
            TUppBtn.Click += TUppBtn_Click;
            TDownBtn.Click += TDownBtn_Click;
            TRemoveBtn.Click += TRemoveBtn_Click;
            TPanelBtn.Click += TPanelBtn_Click;
            TClearBtn.Click += TClearBtn_Click;
            TSaveBtn.Click += TSaveBtn_Click;
            TXmlBtn.Click += TXmlBtn_Click;
            TUploadBtn.Click += TUploadBtn_Click;
            TDeleteBtn.Click += TDeleteBtn_Click;
            TDetailBtn.Click += TDetailBtn_Click;
            TRefreshBtn.Click += TRefreshBtn_Click;

            xd.KeyUp += Xd_KeyUp;
        }

        private void TRefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            OtoUnRG();
            ResetBGColors();
            upanel.Children.Clear();
            wrpanel = null; Model.pab = 0; Model.cab = 0;
            bolumler.Clear(); tipler.Clear();
            upanel.Children.Clear(); dpanel.Children.Clear();

            NewSModel = Data.GetNews(Model.Connection, Model.ContentID);
            bolumler = Data.GetTypePart("parts", Model.ContentID, Model.Connection);
            tipler = Data.GetTypePart("types", Model.ContentID, Model.Connection);

            edit();
        }

        private void TDetailBtn_Click(object sender, RoutedEventArgs e)
        {

            Model.donus_bolumler = Data.itCorrect("bolumler", upanel);
            Model.donus_tipler = Data.itCorrect("tipler", upanel);

            Home.ContentFrame.Navigate(new Pages.Elaboration(NewSModel));

            /*Window pencere = Home.BBWindow;
            pencere.Content = new Pages.Elaboration(NewSModel);
            pencere.ShowDialog();*/
        }

        private void TDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Her şeyiyle beraber içeriği veritabanından silmek üzeresiniz", "Emin misiniz?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Data.dbRemoveUpp("types");
                Data.dbRemoveUpp("parts");
                Data.dbRemoveUpp("details");

                Home.MainWindowS.BtnHome_Click(null, null);
            }
        }

        private void TUploadBtn_Click(object sender, RoutedEventArgs e)
        {
            bolumler = Data.itCorrect("bolumler", upanel);
            tipler = Data.itCorrect("tipler", upanel);

            if (Model.Process == ProcessType.add)
                Data.dbAddUpp(tipler, bolumler, NewSModel);
            else if (Model.Process == ProcessType.edit)
                Data.dbEditUpp(tipler, bolumler, NewSModel);
            else if (Model.Process == ProcessType.free)
            {
                Window pencere = Home.Window;
                Content = new Interfaces.Message("Detail.cs -> TUploadBtn_Click", MessageType.error);
                pencere.ShowDialog();
            }


            if (Model.Process == ProcessType.add)
            {
                Model.ContentID = Data.LastIdentity("types", Model.Connection);
                Model.Process = ProcessType.edit;
            }


        }

        private void TXmlBtn_Click(object sender, RoutedEventArgs e)
        {

            bolumler = Data.itCorrect("bolumler", upanel);
            tipler = Data.itCorrect("tipler", upanel);

            Data.XmlFilePrint(tipler, bolumler);

        }

        private void Detail_Loaded(object sender, RoutedEventArgs e)
        {

            //foreach (PropertyInfo propertyInfo in NewSModel.GetType().GetProperties())
            //{
            //    try
            //    {
            //         MessageBox.Show(propertyInfo.GetValue(NewSModel).ToString());
            //    }
            //    catch (Exception)
            //    {

            //        MessageBox.Show(propertyInfo.GetValue(NewSModel)+"null");
            //    }

            //}

            if (Xmlmi == false && ElaDonus == false) // Add işlemi geçerli olsa da verileri yüklemiyor anlamadım
            {
                NewSModel = Data.GetNews(Model.Connection, Model.ContentID);
                bolumler = Data.GetTypePart("parts", Model.ContentID, Model.Connection);
                tipler = Data.GetTypePart("types", Model.ContentID, Model.Connection);
            }

            edit();

        }

        private void TSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //int dek = Convert.ToInt32(((WrapPanel)sender).Name.ToString().Replace("Yakala", null));
            WrapPanel wrp = (WrapPanel)upanel.FindName("panel" + Model.pab);

            bool oncelik = true; string tp = null, cp = null;
            foreach (RichTextBox item in dpanel.Children)
            {

                if (oncelik == true)
                {
                    oncelik = false;
                    tp = new TextRange(item.Document.ContentStart, item.Document.ContentEnd).Text.TrimEnd();
                }

                else if (oncelik == false)
                {
                    oncelik = true;
                    cp = new TextRange(item.Document.ContentStart, item.Document.ContentEnd).Text.TrimEnd();
                }

            }

            oncelik = true;
            foreach (var icerik in wrp.Children)
            {
                if (icerik is WrapPanel) continue;
                RichTextBox item = (RichTextBox)icerik;

                if (oncelik == true)
                {
                    item.Document.Blocks.Clear();
                    item.Document.Blocks.Add(
                        new Paragraph(new Run(
                            tp.ToString()
                            ))
                        );
                    oncelik = false;
                }
                else if (oncelik == false)
                {
                    item.Document.Blocks.Clear();
                    item.Document.Blocks.Add(
                        new Paragraph(new Run(
                            cp.ToString()
                            ))
                        );
                    oncelik = true;
                }



            }


        }

        private void TClearBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ekrandaki veriler temizlenecek. Daha sonra bunlar geri getirilemez.", "Emin misiniz?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                OtoUnRG();
                ResetBGColors();
                upanel.Children.Clear();
                wrpanel = null; Model.pab = 0; Model.cab = 0;
                bolumler.Clear(); tipler.Clear();
                upanel.Children.Clear(); dpanel.Children.Clear();
            }

        }

        private void TPanelBtn_Click(object sender, RoutedEventArgs e)
        {
            Model.pab = 0; wrpanel = null;
            dpanel.Children.Clear(); ResetBGColors();
        }

        private void TRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Model.cab != 0 && wrpanel != null)
            {

                if (wrpanel.Name == "panel" + (Model.cab))
                {
                    panel.PanelRemove(this);
                    Ana_MouseUp(wrpanel, null);
                }
                else
                {
                    Window pencere = Home.Window;
                    pencere.Content = new Interfaces.Message("Silmek için en alta getirin", MessageType.warn);
                    pencere.ShowDialog();
                }

            }
            else
            {
                Window pencere = Home.Window;
                pencere.Content = new Interfaces.Message("wrpanel null", MessageType.warn);
                pencere.ShowDialog();
            }

        }

        private void Xd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                TUppBtn_Click(null, null);
            }
        }

        private void TDownBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Model.pab != 0 && Model.pab != Model.cab)
                panel.PanelDown(this);
        }

        private void TUppBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Model.pab != 0 && Model.pab != 1)
                panel.PanelUpp(this);
        }

        private void TAddBtn_Click(object sender, RoutedEventArgs e)
        {

            if (Model.tab != Model.cab)
            {
                WrapPanel pull = panel.PanelAdd(this);
                wrpanel = pull;
                Model.pab = Model.cab;

                upanel.Children.Add(pull);

                Ana_MouseUp(pull, null);
            }
            else
            {
                Window pencere = Home.Window;
                pencere.Content = new Interfaces.Message("İzin verilenden daha fazlası eklenemiyor", MessageType.warn);
                pencere.ShowDialog();
            }

        }

        void edit()
        {

            // Ela dönüşüne göre doldurma yapıyordum ancak zaten sıfırdan yapıyor. Bu yüzden kaldırdım.

            if (ElaDonus == false)
            { wrpanel = null; Model.pab = 0; }

            upanel.Children.Clear();
            Model.cab = 0; // en sonda değişiyor ama sen bilirsin
            OtoUnRG();

            if (bolumler.Count() != tipler.Count())
            {

                Window pencere = Home.Window;
                pencere.Content = new Interfaces.Message("Parts ve Types içerik sayısı birbiriyle uyumsuz, verileri kontrol edin.", MessageType.error);
                pencere.ShowDialog();

            }

            else
            {
                int intager = 1;
                foreach (string tap in bolumler)
                {

                    if ((WrapPanel)upanel.FindName("panel0") == null)
                    {
                        WrapPanel wrpbaslik = new WrapPanel();
                        wrpbaslik.Name = "panel0";
                        RegisterName(wrpbaslik.Name, wrpbaslik);
                        upanel.Children.Add(wrpbaslik);
                    }

                    WrapPanel ana = new WrapPanel();
                    ana.Name = $"panel{intager}";
                    RegisterName(ana.Name, ana);
                    ana.MouseUp += Ana_MouseUp;
                    ana.MouseMove += Ana_MouseMove;
                    ana.MouseLeave += Ana_MouseLeave;
                    ana.Cursor = Cursors.Hand;
                    ana.Margin = new Thickness(0, 0, 0, 10);
                    ana.Cursor = Cursors.Hand;
                    ana.Background = (Brush)bc.ConvertFrom("#e6e6e6");



                    RichTextBox rctyp = new RichTextBox();
                    rctyp.Padding = new Thickness(3, 6, 3, 6);
                    rctyp.BorderThickness = new Thickness(0);
                    rctyp.HorizontalContentAlignment = HorizontalAlignment.Center;
                    rctyp.Background = (Brush)bc.ConvertFrom("transparent");
                    rctyp.Width = 70;
                    rctyp.Height = 40;
                    rctyp.FontSize = 16;
                    rctyp.Document.Blocks.Clear();
                    rctyp.Document.Blocks.Add(
                        new Paragraph(new Run(
                            tipler[intager - 1].ToString()
                            ))
                        );
                    ana.Children.Add(rctyp);

                    RichTextBox rcprt = new RichTextBox();
                    rcprt.BorderThickness = new Thickness(0);
                    rcprt.Background = (Brush)bc.ConvertFrom("transparent");
                    rcprt.Width = 340;
                    rcprt.Height = 40;
                    rcprt.VerticalContentAlignment = VerticalAlignment.Center;
                    rcprt.FontSize = 16;

                    rcprt.Document.Blocks.Clear();
                    rcprt.Document.Blocks.Add(
                        new Paragraph(new Run(
                            bolumler[intager - 1].ToString()
                            ))
                        );
                    //rcprt.MouseLeave += Rcprt_MouseLeave;
                    ana.Children.Add(rcprt);




                    WrapPanel yakala = new WrapPanel();
                    yakala.Height = 40;
                    yakala.Width = 40;
                    yakala.Name = "Yakala" + intager;
                    yakala.Background = (Brush)bc.ConvertFrom("transparent");
                    yakala.MouseUp += Btn_MouseUp;
                    yakala.MouseMove += Btn_MouseMove;
                    yakala.MouseLeave += Btn_MouseLeave;

                    ana.Children.Add(yakala);



                    upanel.Children.Add(ana);

                    intager++;

                }

                Model.cab = bolumler.Count();
            }



        }

        public void Btn_MouseLeave(object sender, MouseEventArgs e)
            => ((WrapPanel)sender).Background = (Brush)bc.ConvertFrom("transparent");

        public void Btn_MouseMove(object sender, MouseEventArgs e)
           => ((WrapPanel)sender).Background = (Brush)bc.ConvertFrom("#bfbfbf");

        public void Btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

                int dek = Convert.ToInt32(((WrapPanel)sender).Name.ToString().Replace("Yakala", null));
                Model.pab = dek;

                wrpanel = (WrapPanel)upanel.FindName("panel" + dek);

                Ana_MouseUp(wrpanel, null);

            }
        }

        public void Ana_MouseLeave(object sender, MouseEventArgs e)
        {
            int dek = Convert.ToInt32(((WrapPanel)sender).Name.ToString().Replace("panel", null));
            if (Model.pab != dek) ((WrapPanel)sender).Background = (Brush)bc.ConvertFrom("#e6e6e6");
            else ((WrapPanel)sender).Background = (Brush)bc.ConvertFrom("#bfbfbf");
        }

        public void Ana_MouseMove(object sender, MouseEventArgs e)
        {
            WrapPanel wrp = ((WrapPanel)sender);

            int gelen = Convert.ToInt32(wrp.Name.Replace("panel", null));
            if (gelen != Model.pab)
                wrp.Background = (Brush)bc.ConvertFrom("#d3d3d3");
        }

        public void Ana_MouseUp(object sender, MouseButtonEventArgs e)
        {

            wrpanel = ((WrapPanel)sender);
            Model.pab = Convert.ToInt32(wrpanel.Name.Replace("panel", null).Trim());

            ResetBGColors();
            wrpanel.Background = (Brush)bc.ConvertFrom("#bfbfbf");

            PanelSelect((WrapPanel)sender);

        }

        void PanelSelect(WrapPanel wrp)
        {
            bool oncelik = true;
            dpanel.Children.Clear();
            foreach (var item2 in ((WrapPanel)wrp).Children)
            {
                if (item2 is WrapPanel) continue;

                RichTextBox item = (RichTextBox)item2;

                if (oncelik == true)
                {
                    oncelik = false;
                    RichTextBox rcdet = new RichTextBox();
                    rcdet.Padding = new Thickness(3, 6, 3, 6);
                    rcdet.BorderThickness = new Thickness(0);
                    rcdet.HorizontalContentAlignment = HorizontalAlignment.Center;
                    rcdet.VerticalContentAlignment = VerticalAlignment.Center;
                    rcdet.Background = (Brush)bc.ConvertFrom("#e6e6e6");
                    rcdet.Width = 380;
                    rcdet.Height = 40;
                    rcdet.FontWeight = FontWeights.Bold;
                    rcdet.FontSize = 16;
                    rcdet.Document.Blocks.Clear();
                    rcdet.Document.Blocks.Add(
                        new Paragraph(new Run(
                           new TextRange(item.Document.ContentStart, item.Document.ContentEnd).Text.TrimEnd()
                            ))
                        );
                    rcdet.TextChanged += Rcdet1_TextChanged;
                    dpanel.Children.Add(rcdet);
                }
                else
                {
                    oncelik = true;
                    RichTextBox rcdet = new RichTextBox();
                    rcdet.BorderThickness = new Thickness(0);
                    rcdet.Background = (Brush)bc.ConvertFrom("#e6e6e6");
                    rcdet.Width = 380;
                    rcdet.FontSize = 16;
                    rcdet.ScrollToHome();
                    rcdet.Document.Blocks.Clear();
                    rcdet.Document.Blocks.Add(
                        new Paragraph(new Run(
                           new TextRange(item.Document.ContentStart, item.Document.ContentEnd).Text.TrimEnd()
                            ))
                        );
                    rcdet.TextChanged += Rcdet2_TextChanged;
                    dpanel.Children.Add(rcdet);
                }

            }
        }

        private void Rcdet1_TextChanged(object sender, TextChangedEventArgs e)
        {
            panel.TextChange1(dpanel, wrpanel);
        }

        private void Rcdet2_TextChanged(object sender, TextChangedEventArgs e)
        {
            panel.TextChange2(dpanel, wrpanel);
        }

        private void ResetBGColors()
        {
            int gecisi = 0;
            while (true)
            {
                WrapPanel wrp = (WrapPanel)upanel.FindName("panel" + gecisi);
                try { wrp.Background = (Brush)bc.ConvertFrom("#e6e6e6"); } catch { }

                if (gecisi > Model.cab)
                {
                    break;
                }
                gecisi++;
            }

        }

        void OtoUnRG()
        {
            int gecisi = 0;
            while (true)
            {

                try { UnregisterName("panel" + (gecisi)); UnregisterName("Yakala" + (gecisi + 1)); } catch { }
                if (gecisi > Model.cab)
                {
                    break;
                }
                gecisi++;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WrapPanel pnl = (WrapPanel)upanel.FindName("panel6");
            try { pnl.Background = (Brush)bc.ConvertFrom("red"); } catch { }
        }
    }
}
