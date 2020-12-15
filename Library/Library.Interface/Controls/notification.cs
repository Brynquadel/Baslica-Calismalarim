using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Library.Interface.Controls
{
    static class notification
    {
        static DispatcherTimer timer = new DispatcherTimer();
        static Border border;

       internal static void shownot(string message)
        {

            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;

            border = ((MainWindow)App.Current.MainWindow).mainNotBorder;
            Label label = ((MainWindow)App.Current.MainWindow).mainNotlbl;

            border.Height = 40;
            label.Content = message;

            timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            border.Height = 0;
        }
    }
}
