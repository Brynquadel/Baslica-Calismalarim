using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ftp
{
    class Program
    {
        public static string command; public static string swfolder; public static string website; public static string yol; public static bool Alt = true;
        public static bool FileorFolder; // file=false; directory=true;
        // her şey iyi geliştirilebilir ama tek sorun klasörde dosyalar varsa klasörü silemiyorsun
        public static void learn()
        {
            if (command.IndexOf(".") != -1) { FileorFolder = false; } else { FileorFolder = true; }
            if (command.IndexOf(" -swf") != -1)
            {
                if (command.IndexOf(" in") != -1)
                {
                    swfolder = command.Substring(0, command.IndexOf(" -swf ")); swfolder = command.Replace(swfolder, null); swfolder = swfolder.Replace(" -swf ", null); swfolder = swfolder.Replace(" in ", "/");
                    yol = "ftp://" + website + "/" + swfolder;
                }
                else
                {
                    swfolder = command.Substring(0, command.IndexOf(" -swf ")); swfolder = command.Replace(swfolder, null); swfolder = swfolder.Replace(" -swf ", null);
                    yol = "ftp://" + website + "/" + swfolder;

                }
            }
            else
            {
                command += " in";
                swfolder = command.Substring(0, command.IndexOf(" in"));
                swfolder = command.Replace(swfolder, null); swfolder = swfolder.Replace(" in", "/");
                yol = "ftp://" + website + "/" + swfolder;
            }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string sifre = null; string kullanici = null;
            string[] files = Directory.GetFiles(@"files\");

            basadon: Console.Write("Get Command > ");
            command = Console.ReadLine();



            if (command.IndexOf("view server") == 0)
            {
                Console.WriteLine("");
                string[] Dosyalar = null;
                StringBuilder result = new StringBuilder();

                FtpWebRequest FTP;
                //  yol = "ftp://" + website + "/" + command.Replace(0,) + "/"; 
                try
                {

                    learn();

                    FTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(yol));

                    FTP.UseBinary = true;

                    FTP.Credentials = new NetworkCredential(kullanici, sifre);

                    FTP.Method = WebRequestMethods.Ftp.ListDirectory;

                    WebResponse response = FTP.GetResponse();

                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string line = reader.ReadLine(); while (line != null)

                    {

                        result.Append(line);

                        result.Append("\n");

                        line = reader.ReadLine();

                    }

                    result.Remove(result.ToString().LastIndexOf('\n'), 1);

                    reader.Close();

                    response.Close(); Dosyalar = result.ToString().Split('\n');


                    foreach (string dosya in Dosyalar)
                    {
                        if (dosya.IndexOf(".") != -1)
                        {
                            Console.WriteLine(" [F] " + dosya.ToString());
                            Console.WriteLine("");
                        }
                        else
                        {
                            Console.WriteLine(" [D] " + dosya.ToString());
                            string[] Dosyalar2;
                            StringBuilder result2 = new StringBuilder();

                            FtpWebRequest FTP2;
                            try
                            {

                                FTP2 = (FtpWebRequest)FtpWebRequest.Create(new Uri(yol + @"\" + dosya));

                                FTP2.UseBinary = true;

                                FTP2.Credentials = new NetworkCredential(kullanici, sifre);

                                FTP2.Method = WebRequestMethods.Ftp.ListDirectory;

                                WebResponse response2 = FTP2.GetResponse();

                                StreamReader reader2 = new StreamReader(response2.GetResponseStream());

                                string line2 = reader2.ReadLine(); while (line2 != null)

                                {

                                    result2.Append(line2);

                                    result2.Append("\n");

                                    line2 = reader2.ReadLine();

                                }

                                result2.Remove(result2.ToString().LastIndexOf('\n'), 1);

                                reader2.Close();

                                response2.Close(); Dosyalar2 = result2.ToString().Split('\n');

                                foreach (string dosya2 in Dosyalar2)
                                {
                                    if (dosya2.IndexOf(".") != -1)
                                    {
                                        Console.WriteLine("    [F] " + dosya2.ToString());

                                    }
                                    else
                                    {
                                        Console.WriteLine("    [D] " + dosya2.ToString());
                                    }
                                }
                            }
                            catch { }
                            Console.WriteLine("");


                        }
                    }


                }

                catch

                {
                    Console.WriteLine("[System] > Folder empty");
                }

            }
            else if (command == "view folder")
            {
                foreach (string file in files)
                {
                    Console.WriteLine("[All] " + file);
                }

            }
            else if (command.IndexOf("upload to server") == 0)
            {

                learn();
                foreach (string file in files)
                {
                    FileInfo FI = new FileInfo(file);
                    // Dosyanın gönderileceği ftp yolunu belirliyoruz
                    string uri = yol + "/" + FI.Name;
                    // Ftp işlemlerini yapacağımız sınıfımızı tanımlıyoruz
                    FtpWebRequest FTP;
                    // Oluşturduğumuz değişkene hedef yolumuzu gösteriyoruz
                    FTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                    // Ftp bağlantısı için gerekli bilgileri belirliyoruz
                    FTP.Credentials = new NetworkCredential(kullanici, sifre);
                    // Default olarak true geliyor false 'a çeviriyoruz. Amacımız bağlantı açıksa hataya düşmemesi
                    FTP.KeepAlive = false;
                    // Bu kısımda hangi işlemi yapacağımızı belirtiyoruz, dosya göndereceğimiz için UploadFile methodunu seçiyoruz
                    FTP.Method = WebRequestMethods.Ftp.UploadFile;
                    // Dosya tranferinin Binary türden yapılacağını belirtiyoruz
                    FTP.UseBinary = true;
                    // Gönderdiğimiz dosyanın boyutunu belirtiyoruz
                    FTP.ContentLength = FI.Length;
                    // Buffer büyüklüğünü 2KB olarak belirtiyoruz ve değişkenimizi tanımlıoyruz
                    int buffLength = 2048;
                    byte[] buff = new byte[buffLength];
                    int contentLen;
                    // Bu kısımda dosyayı binary'e çevirip ftp'ye gönderiyoruz
                    FileStream FS = FI.OpenRead();
                    try
                    {
                        Stream strm = FTP.GetRequestStream();
                        contentLen = FS.Read(buff, 0, buffLength);
                        while (contentLen != 0)//dosya bitene kadar gönderme işlemi
                        {
                            strm.Write(buff, 0, contentLen);
                            contentLen = FS.Read(buff, 0, buffLength);
                        }
                        strm.Close();
                        FS.Close();
                        Console.WriteLine("[System] > Operation is completed");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message, "Hata");
                    }
                }


            }
            else if (command.IndexOf("remove on server") == 0)
            {
                learn();

                if (FileorFolder == false)
                {

                    WebRequest FTP;
                    try
                    {
                        FTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(yol));
                        FTP.Credentials = new NetworkCredential(kullanici, sifre);
                        FTP.Method = WebRequestMethods.Ftp.DeleteFile;
                        FtpWebResponse response = (FtpWebResponse)FTP.GetResponse();
                        Console.WriteLine(response.StatusDescription);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                else
                {

                    FtpWebRequest FTP;
                    try
                    {
Console.WriteLine(yol);
                        FTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(yol + "/"));
                        FTP.UseBinary = true; FTP.Credentials = new NetworkCredential(kullanici, sifre);
                        FTP.UsePassive = true;
                        FTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                        FtpWebResponse response = (FtpWebResponse)FTP.GetResponse();
                        Console.WriteLine(response.StatusDescription);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }



            }
            else if (command.IndexOf("create on server") == 0)
            {

                learn();

                FtpWebRequest FTP;
                try
                {
                    Console.Write("Get Name > ");
                    swfolder = Console.ReadLine();
                    FTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(yol + "/" + swfolder));
                    FTP.UseBinary = true;
                    FTP.Credentials = new NetworkCredential(kullanici, sifre);
                    FTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    FtpWebResponse response = (FtpWebResponse)FTP.GetResponse();
                    Console.WriteLine(response.StatusDescription);
                }

                catch (Exception ex)

                {
                    Console.WriteLine(ex.Message);
                }


            }
            else if (command.IndexOf("rename on server") == 0)
            {
                learn();

                Console.Write("Get new label > ");
                swfolder = Console.ReadLine();

                FtpWebRequest FTP;
                try
                {
                    FTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(yol));
                    FTP.UseBinary = true;
                    FTP.RenameTo = swfolder;
                    FTP.Credentials = new NetworkCredential(kullanici, sifre);
                    FTP.Method = WebRequestMethods.Ftp.Rename;
                    FtpWebResponse response = (FtpWebResponse)FTP.GetResponse();
                    Console.WriteLine(response.StatusDescription);

                }
                catch (Exception ex)

                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (command == "con")
            {
                website = "gauncher.com";
                kullanici = "gauncher";
                sifre = "cryxleaderDatabase7";
                Console.WriteLine("[System] > Saved");
            }
            else if (command == "userpass")
            {
                Console.Write("Website > "); website = Console.ReadLine();
                Console.Write("Username > "); kullanici = Console.ReadLine();
                Console.Write("Password > "); sifre = Console.ReadLine();
                Console.WriteLine("[System] > Saved");
            }
            else if (command == "exit" || command == "/q") { Environment.Exit(0); }
            else if (command == "console clear" || command=="/cc") { Console.Clear(); }
            else if (command == "options")
            {
                Console.WriteLine("[System] > This content null");
            }
            else if (command == "help")
            {
                Console.WriteLine("[Command] > view server : Server dosyalarını görüntüle");
                Console.WriteLine("[Command] > view folder : Klasördeki dosyaları görüntüle");
                Console.WriteLine("[Command] > upload to server : Klasördeki dosyaları servera yükle");
                Console.WriteLine("[Command] > remove on server : Server üzerinde dosya sil");
                Console.WriteLine("[Command] > create on server : Server üzerinde dosya oluştur");
                Console.WriteLine("[Command] > rename on server : Server üzerinde dosyaya yeni isim ver");
                Console.WriteLine("[Command] > con : Oto bağlan");
                Console.WriteLine("[Command] > userpass : Bilgileri değiştir");
                Console.WriteLine("[Command] > exit & /q : Çıkış");
                Console.WriteLine("[Command] > console clear & /cc : Konsol ekranını temizle");
                Console.WriteLine("[Command] > options : Ayarlar");
            }

            else { Console.Write("[System] > Command unresolved, try again"); Console.WriteLine(); }

            goto basadon;
        }




    }
}
