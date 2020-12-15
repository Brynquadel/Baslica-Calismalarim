using Library.Entity.Concrete;
using Library.Interface.Pages;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library.Interface.Controls
{
    internal class SeqBook
    {

        static BrushConverter bc = new BrushConverter();
        private static void EntityPanel_Click(object sender, RoutedEventArgs e)
        {
            if (KeySet.Pressing == false)
            {
                KeySet.bookList.Clear();
                Button button = ((Button)sender);
                BrushConverter bc = new BrushConverter();


                Business.Concrete.BookManager bookManager = new Business.Concrete.BookManager();
                Book book = bookManager.Get(Convert.ToInt32(button.Tag));

                Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;

                frame.Navigate(new Pages.DetailBook(book));
            }


        }
        private static Button _button = new Button();

        static Button staticButon
        {
            get
            {
                return _button;
            }
            set
            {
                _button = value;
            }
        }

        private static Image imagetest = new Image();
        static Image image
        {
            get
            {
                return imagetest;
            }
            set { }
        }


        private static Button _buton33 = new Button();

        static Button btn
        {
            get
            {
                return _buton33;
            }
            set
            {
                _buton33 = value;
            }
        }


        internal static Button Get(Book book)
        {
            Button _button = new Button();
            Style style = App.Current.FindResource("ButtonStyle") as Style;
            _button.Style = style;
            _button.Click += EntityPanel_Click;
            _button.Height = 70;
            _button.Width = 540;
            _button.Margin = new Thickness(20, 0, 20, 0);
            _button.Tag = book.Identity;

            DockPanel dp1 = new DockPanel();

            DockPanel dp2 = new DockPanel();
            dp2.Width = 30;

            Image _image = new Image();
            _image.Source = new BitmapImage(new Uri("../Images/books.png", UriKind.Relative));
            RenderOptions.SetBitmapScalingMode(_image, BitmapScalingMode.HighQuality);
            dp2.Children.Add(_image);

            Label label = new Label();
            label.Padding = new Thickness(30, 5, 30, 5);
            label.FontSize = 16;
            label.Width = 240;
            label.Content = string.Format(book.BookName, Encoding.UTF8);
            label.FontWeight = FontWeights.SemiBold;

            Label label2 = new Label();
            label2.Padding = new Thickness(30, 5, 30, 5);
            label2.FontSize = 16;
            label2.Width = 150;
            label2.Content = book.BookBarcode;
            label2.FontWeight = FontWeights.SemiBold;


            Border _border = new Border();
            _border.Width = 15;
            _border.Height = 15;
            _border.CornerRadius = new CornerRadius(20);
            _border.Padding = new Thickness(10, 0, 0, 0);
            BrushConverter bc = new BrushConverter();
            if (book.BookLocation == 0)
                _border.Background = (Brush)bc.ConvertFrom("green");
            else
                _border.Background = (Brush)bc.ConvertFrom("red");

            Button btn = new Button();

            btn.Tag = book.Identity;
            btn.Click += Btn_Click;
            btn.Width = 20;
            btn.Height = 20;
            btn.BorderThickness = new Thickness(0);
            btn.Background = (Brush)bc.ConvertFrom("white");
            btn.BorderThickness = new Thickness(1);
            btn.Margin = new Thickness(10, 0, 0, 0);
            btn.MouseDown += Btn_MouseDown;
            btn.MouseUp += Btn_MouseUp;
            btn.MouseEnter += Btn_MouseEnter;
            btn.MouseLeave += Btn_MouseLeave;

            dp1.Children.Add(dp2);
            dp1.Children.Add(label);
            dp1.Children.Add(label2);
            dp1.Children.Add(_border);
            dp1.Children.Add(btn);

            _button.Content = dp1;

            return _button;
        }

        private static void Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            KeySet.Pressing = false;
        }

        private static void Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            KeySet.Pressing = true;
        }

        private static void Btn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            KeySet.Pressing = false;
        }

        private static void Btn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            KeySet.Pressing = true;
        }


        private static void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Focusable = false;

            Business.Concrete.BookManager bookManager = new Business.Concrete.BookManager();
            Book book = bookManager.Get(Convert.ToInt32(button.Tag));

            bool varmi = false;

            foreach (var item in KeySet.bookList)
            {
                if (item.Identity == Convert.ToInt32(button.Tag))
                    varmi = true;
            }

            BrushConverter bc = new BrushConverter();

            if (varmi == false)
            {
                button.Background = (Brush)bc.ConvertFrom("#bfbfbf");
                KeySet.bookList.Add(book);
            }
            else if (varmi == true)
            {
                button.Background = (Brush)bc.ConvertFrom("white");
                try
                {
                    foreach (var item in KeySet.bookList)
                    {
                        if (item.Identity == Convert.ToInt32(button.Tag))
                        {
                            KeySet.bookList.Remove(item);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
