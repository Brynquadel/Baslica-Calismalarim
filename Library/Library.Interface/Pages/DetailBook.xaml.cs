using Library.Entity.Concrete;
using System.Windows;
using System.Windows.Controls;
using static Library.Interface.Information;

namespace Library.Interface.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Business.Concrete;
    using Library.Interface.Controls;

    public partial class DetailBook : UserControl
    {
        private Frame frame;
        private DetailType detailType;
        private List<Book> Books;
        private Book book;
        private BookManager bookManager = new BookManager();
        private StudentManager studentManager = new StudentManager();
        bool IsExcelData = false;

        public DetailBook()
        {
            InitializeComponent();
            detailType = DetailType.Add;
            Loaded += DetailBook_Loaded;
            IsExcelData = false;
        }

        public DetailBook(Book book)
        {
            InitializeComponent();
            this.book = book;
            detailType = DetailType.Edit;
            Loaded += DetailBook_Loaded;
            IsExcelData = false;
        }

        public DetailBook(bool IsExcelData, List<Book> books)
        {

            Books = books;
            detailType = DetailType.Edit;
            this.IsExcelData = IsExcelData;
            InitializeComponent();

            Loaded += DetailBook_Loaded;
            book = Books.FirstOrDefault(i => i.Identity == rowNumber);
        }

        private void DetailBook_Loaded(object sender, RoutedEventArgs e)
        {
            frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            ChangesDetection();

            List<Student> listStudent = studentManager.GetAll();

            books_persons.Children.Clear();
            Style style = App.Current.FindResource("ButtonStyle") as Style;
            foreach (var item in listStudent)
            {

                Button btn = new Button();
                btn.Style = style;
                btn.Height = 30;
                btn.Content = item.StudentName;
                btn.Click += Btn_Click;
                books_persons.Children.Add(btn);
            }

            ShowData();

            txtSendBook.Text = studentManager.Get(book.BookLocation).StudentName;


          




        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            txtSendBook.Text = ((Button)sender).Content.ToString();
        }

        private void ShowData()
        {
            if (detailType == DetailType.Edit)
            {

                try
                {
                    lbldetail.Content = DateDifference.RemoveHour(book.DateOfIssue) + " Kitap şurada: ";
                    lbldetail.Content += studentManager.Get(book.BookLocation).StudentName;
                }
                catch (Exception)
                {
                }
                lblDetailTitle.Content = book.BookName;
                txt_isim.Text = book.BookName;
                txt_tur.Text = book.BookType;
                try
                {
                    txt_raf.Text = book.BookBarcode.ToString();
                }
                catch (Exception)
                {

            
                }
                txt_yazar.Text = book.BookAuthor;
                txt_yayinci.Text = book.BookPublisher;
                txt_yayintarihi.Text = DateDifference.RemoveHour(book.BookReleaseDate);
                txtSendDate.Text = DateDifference.RemoveHour(book.DateOfCommitment);
            }

            if (KeySet.Yetki == false)
            {
                btnDelete.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Hidden;

                foreach (var item in anapanel.Children)
                {
                    if (item is TextBox)
                    {
                        TextBox ann = item as TextBox;
                        ann.IsHitTestVisible = true;
                        ann.IsReadOnly = true;
                    }
                }
            }
        }

        private void THome_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Sequence(new Book()));
        }
        int rowNumber = 0;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (ControlProblems())
            {
                DiagnosisDetails();
                if (detailType == DetailType.Add)
                {
                    bookManager.Add(book);
                    notification.shownot("Ekleme işlemi başarılı");
                }
                else if (detailType == DetailType.Edit && IsExcelData == true)
                {
                    bookManager.Add(book);
                    notification.shownot("Ekleme işlemi başarılı");
                    ContinueBooks();
                }
                else if (detailType == DetailType.Edit)
                {
                    bookManager.Update(book);
                    notification.shownot(book.BookName+" güncelleme işlemi başarılı");
                }
                detailType = DetailType.Edit;
                book.Identity = bookManager.LastId;
                ChangesDetection();
            }
        }

        private void ContinueBooks()
        {
            if (rowNumber + 1 < Books.Count)
            {
                rowNumber++;
                ReLoad();
            }
            else
            {
                frame.Navigate(new Pages.Sequence(new Book()));
            }

        }

        private void ReLoad()
        {
            ChangesDetection();
            book = Books[rowNumber];
            ShowData();
        }

        private void ChangesDetection()
        {
            if (detailType == DetailType.Add)
            {
                book = new Book();
                btnSaveLbl.Content = "Ekle";
                btnDeleteLbl.Content = "Çık";
            }
            else if (detailType == DetailType.Edit)
            {
                btnSaveLbl.Content = "Kaydet";
                btnDeleteLbl.Content = "Sil";
            }
        }

        private bool ControlProblems()
        {
            bool state = true;


            try
            {
                Student student = studentManager.GetStudentByName(txtSendBook.Text);
            }
            catch (Exception)
            {
                Student student = studentManager.Get(0);
                if (detailType == DetailType.Add)
                {
                    book.BookLocation = 0;
                }
                else
                {
                    notification.shownot("Kitabı aktarmak istediğiniz kişi bulunamadı");
                }
            }
            book.DateOfIssue = DateTime.Now;


            if (txtSendBook.Text != "Kütüphane")
            {

                try
                {


                    DateTime date = DateTime.ParseExact(txtSendDate.Text, "dd/MM/yyyy", null);
                    var d = date.Day;
                    string path="";
                    if(d< 10)
                    {
                        path = "0" + txtSendDate.Text;
                        book.DateOfCommitment = DateTime.ParseExact(path, "dd/MM/yyyy", null);
                    }
                    else
                        book.DateOfCommitment = date;


                }
                catch (Exception)
                {

                    state = false;
                    notification.shownot("Bir öğrenciye kitap veriyorsunuz. Lütfen geçerli bir alım tarihi giriniz.");
                }

            }
            else
            {

            }

            try
            {
                Student student = studentManager.GetStudentByName(txtSendBook.Text);
                book.BookLocation = student.Identity;
            }
            catch (Exception)
            {
                notification.shownot("Alıcı kişi tanınmadı");
                state = false;
            }

            try
            {
                book.BookName = txt_isim.Text;
                book.BookType = txt_tur.Text;
                book.BookBarcode = txt_raf.Text;
                book.BookAuthor = txt_yazar.Text;
                book.BookPublisher = txt_yayinci.Text;
                book.BookReleaseDate = Convert.ToDateTime(txt_yayintarihi.Text);
            }
            catch (Exception)
            {
                notification.shownot("Hata var. Boş yer bırakmayınız");
                state = false;
            }
           
            return state;
        }
        Student student;
        private Book DiagnosisDetails()
        {
            book.BookName = txt_isim.Text;
            book.BookType = txt_tur.Text;
            book.BookBarcode = txt_raf.Text;
            book.BookAuthor = txt_yazar.Text;
            book.BookPublisher = txt_yayinci.Text;
            book.BookReleaseDate = Convert.ToDateTime(txt_yayintarihi.Text);


            // Eğer kitap kütüphanede değil başkasındaysa o kişinin ktap sayısı bir azaltılır.
            if (book.BookLocation != 0)
            {
                student = studentManager.Get(book.BookLocation);
                student.StudentBookCount--;
                studentManager.Update(student);
            }

            try
            {
                student = studentManager.GetStudentByName(txtSendBook.Text);
                if (student.StudentName != "Kütüphane")
                {
                    try
                    {
                        student.StudentBookCount++;
                        studentManager.Update(student);
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            catch (Exception)
            {
                if (IsExcelData == true)
                {
                    student = studentManager.Get(0);
                }
                try
                {
                    student = studentManager.GetStudentByName(txtSendBook.Text);
                }
                catch (Exception)
                {
                    if (student.StudentName != "Kütüphane")
                    {
                        try
                        {
                            student.StudentBookCount++;
                            studentManager.Update(student);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
               

            }
            finally
            {

                if (txtSendBook.Text == "Kütüphane")
                {
                   
                }
                else
                {
                    book.DateOfCommitment = Convert.ToDateTime(txtSendDate.Text);
                }

            }
            book.BookLocation = student.Identity;

            return book;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (detailType == DetailType.Add)
            {
                frame.Navigate(new Sequence(new Book()));
            }
            else if (detailType == DetailType.Edit)
            {
                if (MessageBox.Show("Silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    bookManager.Delete(book);
                   notification.shownot("Silme işlemi başarılı");
                    frame.Navigate(new Sequence(new Book()));
                }
            }
        }

        private void txtSendBook_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Student> students = studentManager.Search(txtSendBook.Text);
            try
            {
                books_persons.Children.Clear();
            }
            catch (Exception)
            {

            }
            Style style = App.Current.FindResource("ButtonStyle") as Style;
            foreach (var item in students)
            {
                Button btn = new Button();
                btn.Style = style;
                btn.Height = 30;
                btn.Content = item.StudentName;
                btn.Click += Btn_Click;
                try
                {
                    books_persons.Children.Add(btn);
                }
                catch (Exception)
                {


                }
            }
        }

        private void DatePicker_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void datePickerX_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSendDate.Text = datePickerX.Text;
        }

        private void dataPickerDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_yayintarihi.Text = dataPickerDate.Text;
        }

        private void btn7gun_Click(object sender, RoutedEventArgs e)
        {
            DateTime test = DateTime.Now;
            test = test.AddDays(7);
            txtSendDate.Text = DateDifference.RemoveHour(test);
        }

        private void btn14gun_Click(object sender, RoutedEventArgs e)
        {
            DateTime test = DateTime.Now;
            test = test.AddDays(14);
            txtSendDate.Text = DateDifference.RemoveHour(test);
        }
    }
}
