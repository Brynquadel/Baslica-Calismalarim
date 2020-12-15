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
using System.Windows.Threading;

namespace Library.Interface.Pages
{
    /// <summary>
    /// Interaction logic for Sequence.xaml
    /// </summary>
    public partial class Sequence : UserControl
    {
        private Business.Concrete.BookManager bookManager;
        private Business.Concrete.StudentManager studentManager;
        IEntity SeqEntity;
        DispatcherTimer timer = new DispatcherTimer();


        DispatcherTimer uynkTim = new DispatcherTimer();

        public Sequence()
        {
            bookManager = new Business.Concrete.BookManager();
            studentManager = new Business.Concrete.StudentManager();
            InitializeComponent();

            SeqEntity = new Book();
            WorkingType();
        }

        public Sequence(IEntity entity)
        {
            bookManager = new Business.Concrete.BookManager();
            studentManager = new Business.Concrete.StudentManager();
            InitializeComponent();

            SeqEntity = entity;
            WorkingType();
        }

        private void WorkingType()
        {
            KeySet.entity = SeqEntity;
            timer.Interval = TimeSpan.FromTicks(500);
            timer.Tick += Timer_Tick;
            timer.Start();

            if (SeqEntity is Book)
            {
                lblSeqTitle.Content = "Kitaplar";
                ImgSeqImage.Source = new BitmapImage(new Uri("../Images/books.png", UriKind.Relative));
                LoadByBooks();
            }
            else if (SeqEntity is Student)
            {
                lblSeqTitle.Content = "Öğrenciler";
                ImgSeqImage.Source = new BitmapImage(new Uri("../Images/students.png", UriKind.Relative));
                LoadByStudents();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (SeqEntity is Book)
            {
                lblSelectedCount.Content = "Seçildi: " + KeySet.bookList.Count;
            }
            else if (SeqEntity is Student)
            {
                lblSelectedCount.Content = "Seçildi: " + KeySet.studentList.Count;
            }
        }

        private void Sequence_Loaded(object sender, RoutedEventArgs e)
        {
            uynkTim.Interval = TimeSpan.FromSeconds(1);
            uynkTim.Tick += UynkTim_Tick;
            uynkTim.Start();
        }

        private void UynkTim_Tick(object sender, EventArgs e)
        {
            WorkingType();
        }

        private void LoadByStudents()
        {
            List<Student> studentList = new List<Student>();

            try
            {
                studentList = studentManager.GetAll();
            }
            catch (Exception e)
            {

            }

            SequenceContent.Children.Clear();

            foreach (var item in studentList)
            {
                if (item.Identity == 0) continue;
                Button buton = SeqStudent.Get(item);
                SequenceContent.Children.Add(buton);
            }
            lblAllCount.Content = "Kayıtlı Toplam Öğrenci: " + (studentList.Count - 1);
            lblLibraryCount.Visibility = Visibility.Hidden;
        }

        private void LoadByBooks()
        {
            List<Book> bookList = bookManager.GetAll();
            SequenceContent.Children.Clear();
            foreach (var item in bookList)
            {
                Button buton = SeqBook.Get(item);
                SequenceContent.Children.Add(buton);
            }
            lblAllCount.Content = "Kayıtlı Toplam Kitap: " + bookList.Count;
            lblLibraryCount.Visibility = Visibility.Visible;
            lblLibraryCount.Content = "Kütüphanedeki Kitap: " + bookManager.GetAll().Where(i => i.BookLocation == 0).Count().ToString();
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                SettingManager settingManager = new SettingManager();
                Setting setting = settingManager.Get(1);
                bool readOption = setting.State;

                if (SeqEntity is Book)
                {
                    Business.Concrete.ExcelOptions<Book> excelOptions = new Business.Concrete.ExcelOptions<Book>();
                     List<Book> books = excelOptions.ReadExcel(files[0]);

                    if (readOption == true)
                    {
                        Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
                        frame.Navigate(new Pages.DetailBook(true, books));
                    }
                    else
                    {
                        foreach (var item in books)
                        {

                            bookManager.Add(item);
                        }
                        WorkingType();
                    }



                }
                else if (SeqEntity is Student)
                {
                    Business.Concrete.ExcelOptions<Student> excelOptions = new Business.Concrete.ExcelOptions<Student>();
                    List<Student> students = excelOptions.ReadExcel(files[0]);

                    if (readOption == true)
                    {
                        Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
                        frame.Navigate(new Pages.DetailStudent(true, students));
                    }
                    else
                    {
                        foreach (var item in students)
                        {
                            studentManager.Add(item);
                        }
                        WorkingType();
                    }

                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;

            if (SeqEntity is Book)
                frame.Navigate(new Pages.DetailBook());
            else if (SeqEntity is Student)
                frame.Navigate(new Pages.DetailStudent());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            SettingManager settingManager = new SettingManager();
            Setting setting = settingManager.Get(2);
            bool durum = setting.State;

            if (durum == true)
            {
                SequenceContent.Children.Clear();
                if (SeqEntity is Book)
                {
                    List<Book> books = bookManager.Search(textBox.Text);

                    foreach (Book item in books)
                    {
                        Button button = SeqBook.Get(item);
                        SequenceContent.Children.Add(button);
                    }
                }
                else if (SeqEntity is Student)
                {
                    List<Student> books = studentManager.Search(textBox.Text);
                    foreach (Student item in books)
                    {
                        if (item.Identity == 0)
                            continue;
                        Button button = SeqStudent.Get(item);
                        SequenceContent.Children.Add(button);
                    }
                }
            }



        }

        private void TypeChange_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            if (SeqEntity is Book)
            {
                SeqEntity = new Student();
            }
            else if (SeqEntity is Student)
            {
                SeqEntity = new Book();
            }
            WorkingType();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in KeySet.bookList)
            {
                MessageBox.Show(item.BookName);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SeciliKaldir();
        }

        void SeciliKaldir()
        {
            if (KeySet.entity is Book)
            {
                if (MessageBox.Show("Geri dönüşü yoktur.", "Silmek istediğinize emin misiniz?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    BookManager bookM = new BookManager();
                    StudentManager studentM = new StudentManager();
                    try
                    {
                        foreach (var item in KeySet.bookList)
                        {
                            if (item.BookLocation != 0)
                            {
                                Student student = studentM.Get(item.BookLocation);
                                student.StudentBookCount--;
                                studentM.Update(student);
                            }
                            bookM.Delete(item);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    KeySet.bookList.Clear();
                }
            }
            else if (KeySet.entity is Student)
            {
                if (MessageBox.Show("Öğrencilerde olan kitaplar kütüphaneye eklenecektir, geri dönüşü yoktur.", "Silmek istediğinize emin misiniz?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    StudentManager studentM = new StudentManager();
                    BookManager bookM = new BookManager();
                    try
                    {
                        foreach (var item in KeySet.studentList)
                        {
                            if (item.Identity != 0)
                            {
                                List<Book> getBooks = bookM.GetBooksOfStudent(item.Identity);
                                foreach (var item2 in getBooks)
                                {
                                    item2.BookLocation = 0;
                                    bookM.Update(item2);
                                }
                            }
                            studentM.Delete(item);

                        }
                    }
                    catch (Exception)
                    {

                    }
                    KeySet.studentList.Clear();
                }


            }
            notification.shownot("Seçili olanlar silindi");

            WorkingType();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;

            frame.Navigate(new Pages.Settings(SeqEntity));
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            SettingManager settingManager = new SettingManager();
            Setting setting = settingManager.Get(2);
            bool durum = setting.State;

            if (e.Key == Key.Enter)
            {
                if (durum == false)
                {
                    SequenceContent.Children.Clear();
                    if (SeqEntity is Book)
                    {
                        List<Book> books = bookManager.Search(textBox.Text);
                        foreach (Book item in books)
                        {
                            Button button = SeqBook.Get(item);
                            SequenceContent.Children.Add(button);
                        }
                    }
                    else if (SeqEntity is Student)
                    {
                        List<Student> books = studentManager.Search(textBox.Text);
                        foreach (Student item in books)
                        {
                            if (item.Identity == 0)
                                continue;
                            Button button = SeqStudent.Get(item);
                            SequenceContent.Children.Add(button);
                        }
                    }
                }
            }


        }

        private void btnselected_Click(object sender, RoutedEventArgs e)
        {
        
        }
    }
}
