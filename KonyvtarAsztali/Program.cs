using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KonyvtarAsztali
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Statisztika statistic = new Statisztika();
            if (args.Contains("--stat"))
            {

            }
            else
            {
                Application app = new Application();
                app.Run(new MainWindow());
            }
        }
    }
}
