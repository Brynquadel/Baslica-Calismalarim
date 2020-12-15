using Library.Business.Concrete;
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
using static Library.Interface.Information;

namespace Library.Interface.Pages
{
    /// <summary>
    /// Interaction logic for DetailStudent.xaml
    /// </summary>
    public partial class DetailStudent : UserControl
    {
        private Frame frame;
        private DetailType detailType;
        private Student student;
        private StudentManager studentManager = new StudentManager();
        private BookManager bookManager = new BookManager();
        bool IsExcelData;
        List<Student> students;
        public DetailStudent()
        {
            InitializeComponent();
            detailType = DetailType.Add;
            Loaded += DetailStudent_Loaded;
        }


        private void ChangesDetection()
        {
            if (detailType == DetailType.Add)
            {
                student = new Student();
                btnSaveLbl.Content = "Ekle";
                btnDeleteLbl.Content = "Çık";
            }
            else if (detailType == DetailType.Edit)
            {
                btnSaveLbl.Content = "Kaydet";
                btnDeleteLbl.Content = "Sil";
            }
        }

        public DetailStudent(Student student)
        {
            InitializeComponent();
            detailType = DetailType.Edit;
            this.student = student;
            Loaded += DetailStudent_Loaded;
        }

        public DetailStudent(bool IsExcelData, List<Student> students)
        {
            this.IsExcelData = IsExcelData;
            InitializeComponent();
            detailType = DetailType.Edit;
            this.students = students;
            Loaded += DetailStudent_Loaded;
            student = students.FirstOrDefault(i => i.Identity == rowNumber);
        }

        private void DetailStudent_Loaded(object sender, RoutedEventArgs e)
        {
            frame = ((MainWindow)Application.Current.MainWindow).ConFrame;
            ChangesDetection();

            if (detailType == DetailType.Add)
            {

            }

            if (detailType == DetailType.Edit)
            {
                List<Book> list = new List<Book>();
                try
                {
                    if (IsExcelData != true)
                    {
                        list = bookManager.GetBooksOfStudent(student.Identity);
                        lblBookCount.Content = "Öğrenci kütüphaneden toplam " + student.StudentBookCount + " adet kitap almış.";

                    }
                }
                catch (Exception)
                {

                }


                booksOfStudent.Children.Clear();

                foreach (var item in list)
                {
                    Label label = new Label()
                    {
                        Height = 40,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(0, 0, 0, 10),
                        FontSize = 16,
                    };
                    label.Content = DateDifference.RemoveHour(item.DateOfIssue)
                        + " tarihinde > \"" + item.BookName + "\" adlı kitap";
                    booksOfStudent.Children.Add(label);
                }

                LoadDatas();

            }
        }

        private void LoadDatas()
        {
            lblDetailTitle.Content = student.StudentName;
            txt_numara.Text = student.StudentNumber;
            txt_isim.Text = student.StudentName;
            txt_okul.Text = student.StudentSchool;
            txt_sehir.Text = student.StudentCity;
            txt_bolum.Text = student.StudentSchoolPart;
            txt_sinif.Text = student.StudentPartClass;
        }

        private void THome_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Sequence(new Student()));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            DiagnosisDetails();
            if (ControlProblems())
            {
                if (detailType == DetailType.Add)
                {
                    studentManager.Add(student);
                    notification.shownot("Ekleme işlemi başarılı");
                }
                else if (detailType == DetailType.Edit && IsExcelData == true)
                {
                    studentManager.Add(student);
                    notification.shownot("Ekleme işlemi başarılı");
                    ContinueLoads();
                }
                else if (detailType == DetailType.Edit)
                {
                    studentManager.Update(student);
                    notification.shownot(student.StudentName+"güncelleme işlemi başarılı");
                }
                detailType = DetailType.Edit;
                student.Identity = studentManager.LastId;
                ChangesDetection();
            }
        }
        int rowNumber;
        private void ContinueLoads()
        {
            if (rowNumber + 1 < students.Count)
            {
                rowNumber++;
                ReLoad();
            }
            else
            {
                frame.Navigate(new Pages.Sequence(new Student()));
            }
        }

        private void ReLoad()
        {
            ChangesDetection();
            student = students[rowNumber];
            LoadDatas();
        }

        private bool ControlProblems()
        {
            if (string.IsNullOrWhiteSpace(txt_isim.Text))
            {
                notification.shownot("Boş bırakmanız önerilmez");
            }

            if (student.StudentBookCount >= 3)
            {
                notification.shownot("Öğrenci üç adet kitap almış, daha fazlasını alamaz.");
                return false;
            }
            else
                return true;

          
        }

        private void DiagnosisDetails()
        {
            student.StudentName = txt_isim.Text;
            student.StudentCity = txt_sehir.Text;
            student.StudentNumber = txt_numara.Text;
            student.StudentSchool = txt_okul.Text;
            student.StudentSchoolPart = txt_bolum.Text;
            student.StudentPartClass = txt_sinif.Text;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (detailType == DetailType.Add)
            {
                frame.Navigate(new Sequence(new Student()));
            }
            else if (detailType == DetailType.Edit)
            {
                if (MessageBox.Show("Silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    studentManager.Delete(student);
                    notification.shownot("Silme işlemi başarılı");
                    frame.Navigate(new Sequence(new Student()));
                }
            }
        }
    }
}
