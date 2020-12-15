using Exmark;
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

namespace WpfApp1
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
        ExmarkManager<Book> manager = new ExmarkManager<Book>();
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            manager.ExternalLocation = @"C:\Users\Mustafa Demirel\Desktop\";
            manager.CategoryProperty = "Category";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book();

            book.Id = 200;
            book.Category = "Merhaba";
            book.Name = new TextRange(rbxContent.Document.ContentStart, rbxContent.Document.ContentEnd).Text;

            //manager.Add(book);

           var list = manager.GetCategorized("Merhaba");

            foreach (var item in list)
            {
                string text = item.Name.ToString();
                MessageBox.Show(text);
            }
        }
    }
}
