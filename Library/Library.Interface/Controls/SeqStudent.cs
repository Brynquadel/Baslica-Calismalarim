using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library.Interface.Controls
{
    internal static class SeqStudent
    {

        private static void EntityPanel_Click(object sender, RoutedEventArgs e)
        {
            if (KeySet.Pressing == false)
            {
                KeySet.studentList.Clear();
                Button button = ((Button)sender);

                Frame frame = ((MainWindow)Application.Current.MainWindow).ConFrame;

                Business.Concrete.StudentManager studentManager = new Business.Concrete.StudentManager();

                Student student = studentManager.Get(Convert.ToInt32(button.Tag));

                frame.Navigate(new Pages.DetailStudent(student));
            }
        }

        internal static Button Get(Student student)
        {
            Button button = new Button();
            Style style = App.Current.FindResource("ButtonStyle") as Style;
            button.Style = style;
            button.Tag = student.Identity;
            button.Click += EntityPanel_Click;
            button.Height = 70;
            button.Width = 540;
            button.Tag = student.Identity;

            DockPanel dp1 = new DockPanel();

            DockPanel dp2 = new DockPanel();
            dp2.Width = 30;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri("../Images/students.png", UriKind.Relative));
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);

            dp2.Children.Add(image);

            Label label = new Label();
            label.Padding = new Thickness(30, 5, 30, 5);
            label.FontSize = 16;
            label.Width = 150;
            label.Content = student.StudentName;
            label.FontWeight = FontWeights.SemiBold;

            Label label2 = new Label();
            label2.Padding = new Thickness(30, 5, 30, 5);
            label2.FontSize = 16;
            label2.Width = 300;
            string okul = student.StudentSchool.Replace(" Üniversitesi", " Ü.");
            label2.Content = okul+" "+student.StudentSchoolPart;
            label2.FontWeight = FontWeights.SemiBold;

            BrushConverter bc = new BrushConverter();
            Button btn = new Button();
            btn.Click += Btn_Click;
            btn.Width = 20;
            btn.Height = 20;
            btn.BorderThickness = new Thickness(0);
            btn.Background = (Brush)bc.ConvertFrom("white");
            btn.BorderThickness = new Thickness(1);
            btn.Margin = new Thickness(10, 0, 0, 0);
            btn.Tag = student.Identity;
            btn.MouseDown += Btn_MouseDown;
            btn.MouseUp += Btn_MouseUp;
            btn.MouseEnter += Btn_MouseEnter;
            btn.MouseLeave += Btn_MouseLeave;

            dp1.Children.Add(dp2);
            dp1.Children.Add(label);
            dp1.Children.Add(label2);
            dp1.Children.Add(btn);

            button.Content = dp1;

            return button;
        }

        private static void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            KeySet.Pressing = false;
        }

        private static void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            KeySet.Pressing = true;
        }

        private static void Btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KeySet.Pressing = false;
        }

        private static void Btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            KeySet.Pressing = true;
        }

        private static void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Focusable = false;

            Business.Concrete.StudentManager studentManager = new Business.Concrete.StudentManager();
            Student student = studentManager.Get(Convert.ToInt32(button.Tag));

            bool varmi = false;

            foreach (var item in KeySet.studentList)
            {
                if (item.Identity == Convert.ToInt32(button.Tag))
                    varmi = true;
            }

            BrushConverter bc = new BrushConverter();

            if (varmi == false)
            {
                button.Background = (Brush)bc.ConvertFrom("black");
                KeySet.studentList.Add(student);
            }
            else if (varmi == true)
            {
                button.Background = (Brush)bc.ConvertFrom("white");
                try
                {
                    foreach (var item in KeySet.studentList)
                    {
                        if (item.Identity == Convert.ToInt32(button.Tag))
                        {
                            KeySet.studentList.Remove(item);
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
