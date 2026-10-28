using cdn_api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsuwanieZal
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            string connectionString = @"Data Source = TF-SQL; Initial Catalog = ERPXL_Tuflowers; User ID = onesupport; Password=Ancr3d0$$db";

            TextWriter tw2 = new StreamWriter(@"C:\OneSupport\UsuwanieZal\log.txt",append: true);
            tw2.WriteLine();
            tw2.WriteLine();
            try
            {
                tw2.WriteLine("Hello");

                DateTime dzis = DateTime.Today;
                tw2.WriteLine("Dziś datetime: " + dzis);


                string sql = "SELECT DAO_DABId FROM cdn.DaneObiekty join cdn.DaneBinarne on DAO_DABId=DAB_ID join cdn.TwrKarty on DAO_ObiTyp = Twr_GIDTyp and DAO_ObiNumer = Twr_GIDNumer " +
                            " WHERE Twr_Archiwalny = 1 and Twr_Kod like '%TUFL%' GROUP BY DAO_DABId";
                List<int> wynik = HarDos.SelectListRowColumn(sql, "cdn.DaneObiekty", connectionString);
                //tw2.WriteLine("Wynik: " + wynik);

               foreach(int ID in wynik)
                {
                    string deleteDAO = "delete from cdn.DaneObiekty where DAO_DABId=" + ID;
                    HarDos.DeleteFrom(deleteDAO, connectionString);

                    string deleteDAB = "delete from cdn.DaneBinarne where DAB_ID=" + ID;
                    HarDos.DeleteFrom(deleteDAB, connectionString);

                    tw2.WriteLine("Usunięto DABID=" + ID);

                }
            }
            catch (Exception e)
            {
                tw2.WriteLine("Błąd aplikacji: " + e.ToString());
            }





            tw2.Close();

            //Console.ReadKey();
        }
    }
}
