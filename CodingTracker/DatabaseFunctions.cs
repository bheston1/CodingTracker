using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;

namespace CodingTracker
{
    internal class DatabaseFunctions
    {
        static readonly string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        internal static void CreateDB()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = "CREATE TABLE IF NOT EXISTS Sessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Start TEXT, End TEXT, Duration TEXT)";
                tableCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        internal static void AddSession()
        {
            Console.Clear();
            string date = Helpers.GetDateInput();
            string sessionStart = Helpers.GetSessionTimes("Enter session start time (format hh:mm AM/PM): ");
            string sessionEnd = Helpers.GetSessionTimes("Enter session end time (format hh:mm AM/PM): ");
            TimeSpan sessionDuration = Helpers.CalculateDuration(sessionStart, sessionEnd);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO Sessions (Date, Start, End, Duration) VALUES ('{date}', '{sessionStart}', '{sessionEnd}', '{sessionDuration}')";
                command.ExecuteNonQuery();
                connection.Close();
            }
            Menu.ShowMenu();
        }
    }
}
