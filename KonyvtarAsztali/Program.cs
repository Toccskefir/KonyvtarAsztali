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
            Statisztika stat = new Statisztika();
            try
            {
                stat.Fill();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

            if (args.Contains("--stat"))
            {
                Console.WriteLine("500 oldalnál hosszabb könyvek száma: " + stat.LongerThan500Page());
                if (stat.OlderThan1950())
                {
                    Console.WriteLine("Van 1950-nél régebbi könyv");
                }
                else
                {
                    Console.WriteLine("Nincs 1950-nél régebbi könyv");
                }
                Console.WriteLine("A leghosszabb könyv:");
                Console.WriteLine("\tSzerző: " + stat.LongestBook().Author);
                Console.WriteLine("\tCím: " + stat.LongestBook().Title);
                Console.WriteLine("\tKiadás éve: " + stat.LongestBook().Publish_year);
                Console.WriteLine("\tOldalszám: " + stat.LongestBook().Page_count);
                Console.WriteLine("A legtöbb könyvvel rendelkező szerző: " + stat.MostPopularAuthor());
            }
            else
            {
                Application app = new Application();
                app.Run(new MainWindow());
            }
        }
    }
}
