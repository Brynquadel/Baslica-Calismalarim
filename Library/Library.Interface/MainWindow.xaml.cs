using Library.Business.Concrete;
using Library.Entity.Abstract;
using Library.Entity.Concrete;
using Library.Interface.Controls;
using Library.Interface.Pages;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library.Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            menuExt.Visibility = Visibility.Collapsed;
            ConFrame.Navigate(new Pages.Login());
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl && e.Key == Key.Delete)
            {

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(new Pages.DetailBook());
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(new Pages.DetailStudent());
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (KeySet.entity is Book)
            {
                if (KeySet.bookList.Count > 0)
                {
                    string liste = "SEÇİLİ OLAN KİTAPLAR \n";
                    int sirasi = 1;
                    foreach (var item in KeySet.bookList)
                    {
                        liste += "\n" + sirasi + " " + item.BookName;
                        sirasi++;
                    }
                    MessageBox.Show(liste);
                }
                else
                {
                    notification.shownot("Seçili kitap yok!");
                }
            }
            else if (KeySet.entity is Student)
            {
                if (KeySet.studentList.Count > 0)
                {
                    string liste = "SEÇİLİ OLAN ÖĞRENCİLER \n";
                    int sirasi = 1;
                    foreach (var item in KeySet.studentList)
                    {
                        liste += "\n" + sirasi + " " + item.StudentName;
                        sirasi++;
                    }
                    MessageBox.Show(liste);
                }
                else
                {
                    notification.shownot("Seçili öğrenci yok!");
                }
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(null);

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
                frame.Navigate(new Pages.Sequence(new Book()));
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

                frame.Navigate(new Pages.Sequence(new Student()));

            }
            notification.shownot("Seçili olanlar silindi");

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            IEntity entity = new Book();
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(new Pages.Settings(entity));
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(new Pages.Sequence(new Student()));
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(new Pages.Sequence(new Book()));
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            if (KeySet.entity is Book)
            {
                if (KeySet.bookList.Count > 0)
                {
                    string liste = "SEÇİLİ OLAN KİTAPLAR \n";
                    int sirasi = 1;
                    foreach (var item in KeySet.bookList)
                    {
                        liste += "\n" + sirasi + " " + item.BookName;
                        sirasi++;
                    }
                    MessageBox.Show(liste);
                }
                else
                {
                    notification.shownot("Seçili kitap yok!");
                }
            }
            else if (KeySet.entity is Student)
            {
                if (KeySet.studentList.Count > 0)
                {
                    string liste = "SEÇİLİ OLAN ÖĞRENCİLER \n";
                    int sirasi = 1;
                    foreach (var item in KeySet.studentList)
                    {
                        liste += "\n" + sirasi + " " + item.StudentName;
                        sirasi++;
                    }
                    MessageBox.Show(liste);
                }
                else
                {
                    notification.shownot("Seçili öğrenci yok!");
                }
            }
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            frame.Navigate(null);

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

                frame.Navigate(new Pages.Sequence(new Book()));
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

                    frame.Navigate(new Pages.Sequence(new Student()));
                }


            }
            notification.shownot("Seçili olanlar silindi");

         
         

        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            menuExt.Visibility = Visibility.Collapsed;
            ConFrame.Navigate(new Pages.Login());
        }
    }
}
