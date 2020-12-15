using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interface.Controls
{
    public static class DateDifference
    {
        public static string RemoveHour(DateTime dateTime)
        {
            string d1 = dateTime.ToString();

            string al = d1.Substring(d1.IndexOf(' '), 9);

            d1 = d1.Replace(al, null);

            return d1;
        }

        public static bool TarihDurum(DateTime date1)
        {
            return false;
        }

        public static string WhatDifference(DateTime date1, DateTime date2)
        {
            DateTime t1 = date2;
            DateTime t2 = date1;

            string s2 = "16:00:00";

            DateTime dt2 = t2.AddMinutes(DateTime.Parse(s2).TimeOfDay.TotalMinutes);

            var fark = t1 - dt2;

            TimeSpan interval = new TimeSpan(fark.Ticks);

            int sonuc = 0;
            int yil = interval.Days;
            int ay = interval.Days;
            int gun = interval.Days;
            int saat = interval.Hours;

            while (true) // Yıl
            {
                yil = yil - 365;
                if (0 > yil) { yil = sonuc; break; }
                else { sonuc++; }
            }
            sonuc = 0;
            while (true) // Ay
            {
                ay = ay - 30;
                if (0 > ay) { ay = sonuc; break; }
                else { sonuc++; }
            }

            gun++;

            string returnValue="";

            if (yil > 0) { returnValue = yil+" yıl"; }
            else
            {
                if (ay > 0) { returnValue = ay + " ay"; }
                else
                {
                    if (interval.Days > 0) { returnValue = gun + " gün"; }
                    else
                    {
                       returnValue = gun + " gün"; 
                    }
                }
            }


            return returnValue;
        }

    }
}
