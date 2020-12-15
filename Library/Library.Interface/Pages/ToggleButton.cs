using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Library.Interface.Pages
{
    internal static class ToggleButton
    {

        // Toggle state is false means it is close: check in the left
        // Toggle state is true means it is open: check in the right

        private static DispatcherTimer timer1;
        private static DispatcherTimer timer2;
        private static Border Check;
        private static Thickness thickness;

        internal static void Motion(bool toggle, ref Border border)
        {

            timer1 = new DispatcherTimer();
            timer2 = new DispatcherTimer();
            thickness = new Thickness();

            timer1.Interval = TimeSpan.FromTicks(1);
            timer2.Interval = TimeSpan.FromTicks(1);

            timer1.Tick += Timer1_Tick;
            timer2.Tick += Timer2_Tick;

            Check = border;

            if (toggle == false)
                ToggleOpen();
            else
                ToggleClose();
        }

        private static void Timer1_Tick(object sender, EventArgs e)
        {
            thickness.Right -= 0.1;
            Check.Margin = thickness;
            if (thickness.Right <= 0)
            {
                thickness.Left += 0.1;
                if (thickness.Left >= 15)
                    timer1.Stop();
            }
        }

        private static void Timer2_Tick(object sender, EventArgs e)
        {
            thickness.Left -= 0.1;
            Check.Margin = thickness;
            if (thickness.Left <= 0)
            {
                thickness.Right += 0.1;
                if (thickness.Right >= 15)
                    timer2.Stop();
            }
        }

        private static void ToggleOpen()
        {
            timer1.Start();
        }

        private static void ToggleClose()
        {
            timer2.Start();
        }
    }
}
