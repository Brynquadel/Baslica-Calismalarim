using Library.Business.Concrete;
using Library.Entity.Abstract;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        SettingManager settingManager = new SettingManager();

        IEntity entityOn;
        StudentManager studentManager = new StudentManager();
        BookManager bookManager = new BookManager();
        bool stateOfData,stateOfSearch;

        Setting setting1 = new Setting();
        Setting setting2 = new Setting();

        List<Book> listBugun = new List<Book>();
        List<Book> listYarin = new List<Book>();
        List<Book> listZamaniVar = new List<Book>();
        List<Book> listDolanlar = new List<Book>();

        public Settings(IEntity entity)
        {
            entityOn = entity;
            InitializeComponent();
            settingManager = new SettingManager();
     
            setting1 = settingManager.Get(1);
            stateOfData= setting1.State;

            setting2 = settingManager.Get(2);
            stateOfSearch = setting2.State;

            DurumlariKontrolET();
           
            Loaded += Settings_Loaded;


        }

        private void DurumlariKontrolET()
        {
            if (stateOfSearch == true)
            {
                brdCheck2.Margin = new Thickness(15, 0, 0, 0);
                durum2.Visibility = Visibility.Hidden;
            }
            else
            {
                durum2.Visibility = Visibility.Visible;
            }

            if (stateOfData == true)
            {
                brdCheck.Margin = new Thickness(15, 0, 0, 0);
                durum1.Visibility = Visibility.Hidden;
            }
            else
            {
                durum1.Visibility = Visibility.Visible;
            }

        }

        bool sureGectiMi = false;
        bool bugunMu = false;
        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            List<Book> books = bookManager.EscrowBooks();
            stkStatesOfBooks.Children.Clear();

            foreach (var item in books)
            {
                Style style = this.FindResource("ButtonStyle") as Style;
                Student student = studentManager.Get(item.BookLocation);

                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;


                Button button1 = new Button();
                button1.Click += Button1_Click;
                button1.Content = "Kitap: " + item.BookName;
                button1.Style = style;
                button1.Width = 250;

                panel.Children.Add(button1);


                Button button2 = new Button();
                button2.Click += Button2_Click;
                button2.Content = "Öğrenci: " + student.StudentName;
                button2.Style = style;
                button2.Width = 250;

                panel.Children.Add(button2);

                Label label3 = new Label();
                label3.FontSize = 16;

                string detay = DateDifference.WhatDifference(DateTime.Now, item.DateOfCommitment).ToString();

                BrushConverter bc = new BrushConverter();

                int detayNum = Temizle(detay);



                label3.Content = " - " + DateDifference.RemoveHour(item.DateOfIssue) + " aldı - "
                    + DateDifference.RemoveHour(item.DateOfCommitment) +
                    " vermeli - ";

                if (detayNum == 0)
                {
                    label3.Foreground = (Brush)bc.ConvertFrom("orange");
                    label3.Content += "bugün vermeli";
                    listBugun.Add(item);
                }
                else if (detayNum < 0)
                {
                    label3.Foreground = (Brush)bc.ConvertFrom("red");
                    label3.Content += detay.Replace("-", null) + " süresi geçti";
                    listDolanlar.Add(item);
                }
                else if (detayNum > 1)
                {
                    label3.Foreground = (Brush)bc.ConvertFrom("green");
                    label3.Content += detay + " var";
                    listZamaniVar.Add(item);
                }
                else if (detayNum == 1)
                {
                    label3.Foreground = (Brush)bc.ConvertFrom("steelblue");
                    label3.Content += " yarın vermeli";
                    listYarin.Add(item);
                }


                panel.Children.Add(label3);

                stkStatesOfBooks.Children.Add(panel);
            }
        }

        private void manuelDoldur(List<Book> books)
        {
            stkStatesOfBooks.Children.Clear();

            foreach (var item in books.ToList())
            {
                Style style = this.FindResource("ButtonStyle") as Style;
                Student student = studentManager.Get(item.BookLocation);

                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;


                Button button1 = new Button();
                button1.Click += Button1_Click;
                button1.Content = "Kitap: " + item.BookName;
                button1.Style = style;
                button1.Width = 250;

                panel.Children.Add(button1);


                Button button2 = new Button();
                button2.Click += Button2_Click;
                button2.Content = "Öğrenci: " + student.StudentName;
                button2.Style = style;
                button2.Width = 250;

                panel.Children.Add(button2);

                Label label3 = new Label();
                label3.FontSize = 16;

                string detay = DateDifference.WhatDifference(DateTime.Now, item.DateOfCommitment).ToString();

                BrushConverter bc = new BrushConverter();

                int detayNum = Temizle(detay);



                label3.Content = " - " + DateDifference.RemoveHour(item.DateOfIssue) + " aldı - "
                    + DateDifference.RemoveHour(item.DateOfCommitment) +
                    " vermeli - ";

                if (detayNum == 0)
                {
                    label3.Foreground = (Brush)bc.ConvertFrom("orange");
                    label3.Content += "bugün vermeli";
                }
                else if (detayNum < 0)
                {
                    label3.Foreground = (Brush)bc.ConvertFrom("red");
                    label3.Content += detay.Replace("-", null) + " süresi geçti";
                }
                else if (detayNum > 1)
                {
                    label3.Foreground = (Brush)bc.ConvertFrom("green");
                    label3.Content += detay + " var";
                }
                else if (detayNum == 1)
                {
                    label3.Foreground = (Brush)bc.ConvertFrom("steelblue");
                    label3.Content += " yarın vermeli";
                }


                panel.Children.Add(label3);

                stkStatesOfBooks.Children.Add(panel);
            }
        }

        private int Temizle(string detay)
        {
            detay = detay.Replace(" gün", null);
            detay = detay.Replace(" ay", null);
            detay = detay.Replace(" yıl", null);

            return Convert.ToInt32(detay.Trim());
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string studentName = button.Content.ToString().Replace("Öğrenci: ", null);
            Student student = studentManager.GetStudentByName(studentName.ToString());
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(new Pages.DetailStudent(student));
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string bookName = button.Content.ToString().Replace("Kitap: ", null);
            Book book = bookManager.GetBookByName(bookName.ToString());

            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(new Pages.DetailBook(book));
        }

      
        private void btnDarkMode_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton.Motion(stateOfData, ref brdCheck);
            stateOfData= !stateOfData;
            setting1.State = stateOfData;
            settingManager.ToggleMove(setting1);
            DurumlariKontrolET();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;

            if (entityOn is Book)
                frame.Navigate(new Pages.Sequence(new Book()));
            else if (entityOn is Student)
                frame.Navigate(new Pages.Sequence(new Student()));

        }

        private void btnSuresiDolanlar_Click(object sender, RoutedEventArgs e)
        {
            manuelDoldur(listDolanlar);
        }

        private void btnBugunVerecekler_Click(object sender, RoutedEventArgs e)
        {
            manuelDoldur(listBugun);
        }

        private void btnYarinVerecekler_Click(object sender, RoutedEventArgs e)
        {
            manuelDoldur(listYarin);
        }

        private void btnSuresiVar_Click(object sender, RoutedEventArgs e)
        {
            manuelDoldur(listZamaniVar);
        }

        private void btnHepsi_Click(object sender, RoutedEventArgs e)
        {
            List<Book> books = bookManager.EscrowBooks();
            manuelDoldur(books);
        }

        private void btnSearchTog_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton.Motion(stateOfSearch, ref brdCheck2);
            stateOfSearch = !stateOfSearch;
            setting2.State = stateOfSearch;
            settingManager.ToggleMove(setting2);
            DurumlariKontrolET();
        }
    }
}
