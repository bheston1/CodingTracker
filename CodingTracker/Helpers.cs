using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class Helpers
    {
        internal static TimeSpan CalculateDuration(string sessionStart, string sessionEnd)
        {
            return DateTime.Parse(sessionEnd).Subtract(DateTime.Parse(sessionStart));
        }

        internal static string GetDateInput()
        {
            Console.Write("Enter the date (format mm/dd/yyyy): ");
            string dateInput = Console.ReadLine();
            while (!DateTime.TryParseExact(dateInput, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid date. (format mm/dd/yyyy) | Try again");
                dateInput = Console.ReadLine();
            }
            return dateInput;
        }

        internal static object GetIdInput(string message)
        {
            Console.Write(message);
            string idInput = Console.ReadLine();
            while (!Int32.TryParse(idInput, out _) || Convert.ToInt32(idInput) < 0)
            {
                Console.WriteLine("Invalid Id, try again");
                idInput = Console.ReadLine();
            }
            int finalInput = Convert.ToInt32(idInput);
            return finalInput;
        }

        internal static string GetSessionTimes(string message)
        {
            Console.Write(message);
            string timeInput = Console.ReadLine();
            while (!DateTime.TryParseExact(timeInput, "hh:mm tt", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid time entry (format: hh:mm (format hh:mm AM/PM)) | Try again");
                timeInput = Console.ReadLine();
            }
            return timeInput;
        }

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
