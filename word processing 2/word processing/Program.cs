using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace word_processing
{
    class Program
    {
        static void Main(string[] args)
        {

            //
            // 29.09.2018 Projenin önceki sürümü Drive'da
            // 30.09.2018 Satır içerisinden komutların çözebilmesi için geliştiriliyor
            //

            File.Delete("tipçıktı.txt"); File.Delete("içerikçıktı.txt");
            StreamReader st = new StreamReader("data.txt", Encoding.Default, true);

            int i = 0;
            while (st.Read() > 0)
            {
                st.ReadLine();
                i++;
            }
            st.Close();

            Console.WriteLine("Satır Sayısı : " + i);

            StreamReader st2 = new StreamReader("data.txt", Encoding.Default, true);
            StreamWriter tipyaz = new StreamWriter("tipçıktı.txt");
            StreamWriter icerikyaz = new StreamWriter("içerikçıktı.txt");

            bool tipvarmi = false; string line;
            string tip = null;
            List<string> icerik = new List<string>();
            int b = -1;

            // Aktif bir döngü. Verileri kontrol etmek için geçici olarak txt'lere kaydediyorum.
            // Veri kayıt kontrol vs. döngü içerisinden kontrol ediliyor. (şuan)
            // Açıklama satırları ile bazı durumlara açıklık getirmeye çalışacağım.
            while (true)
            {
                // Satırı bir değişkene alıyorum ki genel operasyonları bunun üzerinden yönetebileyim. st2.ReadLine() kullanamam.
                line = st2.ReadLine(); b++; Console.WriteLine(b);
                if (b == i) { Console.WriteLine("tam bitiş"); break; } // ve daha önce aldığım satır sayısı tamamlandığında bitiriyorum.


                if (line.IndexOf("[") != -1 && line.IndexOf("]") != -1) // word_processing.Methods.Isthere(line) == true olarak kontrol ediliyordu daha önce.
                {

                    string value = Methods.TypeValue(line);

                    if (value == "ERROR") // Bir satırda birden fazla açılış ve kapanış varsa hata verir ve o bölüm dikkate alınmaz, geçersiz girdi kabul edilir
                    {
                        Console.WriteLine("# Burada bir hata var : " + value);
                        Console.WriteLine("Bkz. bir satırda birden fazla tip ataması (şuan ayrıştırılamıyor)");
                        Console.WriteLine("tam bitiş manuel"); break;
                    }
                    else if (value == "NOLIST") // Kayıtlarda yok anlamına geliyor. Genel text diyelim.
                    {
                        // ##########################################################################################
                        if (tipvarmi == true)
                        {
                            if (line == null || line == "" || line == " ") { icerik.Add(null); }
                            else { icerik.Add(line); }
                            Console.WriteLine(line);
                        }
                        else
                        {
                            Console.WriteLine("# Kayıtsız girdi 2 CID");
                        }
                        // ##########################################################################################
                    }
                    else
                    {

                        if (tipvarmi == true)
                        {
                            // Elde olan tipin kapatılma işlemi, son satır analizi ve eldeki verinin işlenmesi | Bir part'ın serüveni buraya kadar.
                            //***********************************
                            tipvarmi = false;

                            // BURALARDA BAYA DÜZENLEMELER OLDU. O YÜZDEN BAZI ŞEYLER KARMAŞIK GELEBİLİR.

                            string sonsatir = line;
                            string sagtaraf = sonsatir.Substring(0, sonsatir.IndexOf(tip));
                            if (sagtaraf != null && sagtaraf != " " && sagtaraf != "") { icerik.Add(sagtaraf); Console.WriteLine(sagtaraf); }
                            sonsatir = sonsatir.Replace(sagtaraf, " ").Trim();
                            sonsatir = sonsatir.Replace(tip, " ").Trim();

                            Console.WriteLine(tip);

                            Console.WriteLine("**************************");


                            Console.WriteLine(sonsatir + " dikkate alınmayacak");

                            tipyaz.WriteLine(" ");
                            tipyaz.WriteLine(tip);

                            icerikyaz.WriteLine(" ");
                            foreach (var item in icerik)
                            {
                                icerikyaz.WriteLine(item);
                            }

                            tip = null;
                            icerik.Clear();
                            continue;
                            //***********************************

                        }
                        else
                        {
                            tip = value; tipvarmi = true;


                            string sonsatir = line;
                            string sagkesit = sonsatir.Substring(sonsatir.LastIndexOf(tip));
                            sagkesit = sagkesit.Replace(tip, "").Trim();


                            string soltaraf = sonsatir.Substring(0, sonsatir.IndexOf(tip));
                            Console.WriteLine(soltaraf + " dikkate alınmayacak");



                            Console.WriteLine("**************************");
                            Console.WriteLine(tip);
                            if (sagkesit != null && sagkesit != " " && sagkesit != "") { icerik.Add(sagkesit); Console.WriteLine(sagkesit); }




                            // Elde olmayan tipin aktarılması, ilk satır analizi

                        }
                    }



                }
                else
                {
                    // Satırların kontrol edilişi parantez karakteri olmayanlar buraya geliyor. 
                    // Elde tip varsa kaydediliyor yoksa boşta olan veri görmezden geliniyor.

                    // ##########################################################################################
                    if (tipvarmi == true)
                    {
                        if (line == null || line == "" || line == " ") { icerik.Add(null); }
                        else { icerik.Add(line); }
                        Console.WriteLine(line);
                    }
                    else
                    {
                        Console.WriteLine("# Kayıtsız girdi 1");
                    }
                    // ##########################################################################################

                }



            }
            st2.Close();
            tipyaz.Close();
            icerikyaz.Close();



            string end = Console.ReadLine();
            if (end == "+") // İçerik metnini aç
            {
                System.Diagnostics.Process.Start("içerikçıktı.txt");
            }
            else if (end == "++") // Tip ve içerik metnini aç
            {
                System.Diagnostics.Process.Start("içerikçıktı.txt");
                System.Diagnostics.Process.Start("tipçıktı.txt");
            }
            else if (end == "+++") // Tüm metinleri aç
            {
                System.Diagnostics.Process.Start("içerikçıktı.txt");
                System.Diagnostics.Process.Start("tipçıktı.txt");
                System.Diagnostics.Process.Start("data.txt");
            }
            else { Console.WriteLine("Son bitiş"); }


        }
    }
}
