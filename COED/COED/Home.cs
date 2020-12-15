using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace COED
{

    public enum ExpectType
    {
        database
    }

    public enum DefineType
    {
        user,
        database
    }

    public enum MessageType
    {
        warn,
        error
    }

    public enum ProcessType
    {
        add,
        edit,
        free,
        xml,
        remove
    }

    public static class Home
    {
        public static MainWindow MainWindowS { get; set; }
        public static Frame ContentFrame { get; set; }
        public static Window Window
        {
            get
            {
                Window window = new Window()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    WindowStyle = WindowStyle.None,
                    ResizeMode = ResizeMode.NoResize,
                    Width = 500,
                    Height = 230
                };

                return window;
            }
            set { Window = value; }

        }

        public static Window BBWindow
        {
            get
            {
                Window window = new Window()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    WindowStyle = WindowStyle.None,
                    ResizeMode = ResizeMode.NoResize,
                    Width = 1100,
                    Height = 600
                };

                return window;
            }
            set { Window = value; }

        }


    }



}
