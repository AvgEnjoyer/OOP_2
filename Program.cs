using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace OOP_2
{





    static class Program
    {
        static void Transform()

        {

            //// Завантаження стилів

            //XslCompiledTransform xslt = new XslCompiledTransform();

            //string f1 = getFilePath("output1.xsl");

            //xslt.Load(f1);

            //// Виконання перетворення і виведення результатів у файл.

            //string f2 = getFilePath("books.xml");

            //string f3 = getFilePath("books.html");

            //xslt.Transform(f2, f3);

        }

        //Шлях на робочий стіл

        private static string getFilePath(string fileName)

        {

            return Path.Combine(Environment.GetFolderPath(

            Environment.SpecialFolder.Desktop), fileName);

        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            Transform();
        }
    }
}
