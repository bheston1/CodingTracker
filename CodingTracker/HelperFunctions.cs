using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class HelperFunctions
    {
        internal static void PressEnter()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.Enter);
        }

        internal static void Startup()
        {
            string databasePath = ConfigurationManager.AppSettings.Get("DatabasePath");
            bool databaseExists = File.Exists(databasePath);
            if (!databaseExists)
            {
                DatabaseFunctions.CreateDB();
            }
            else
            {
                Menu.ShowMenu();
            }
        }
    }
}
