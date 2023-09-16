using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using System.Globalization;
using ConsoleTableExt;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Sessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Start TEXT, End TEXT, Duration TEXT)";
                command.ExecuteNonQuery();
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

        internal static void UpdateSession()
        {
            ViewSessions();
            var recordId = Helpers.GetIdInput("Enter Id of session to update or type 'r' to return to main menu: ");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM Sessions WHERE Id = {recordId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (checkQuery == 0)
                {
                    Console.WriteLine("Enter valid Id");
                    connection.Close();
                    UpdateSession();
                }
                else if (Convert.ToString(checkQuery).Trim().ToLower() == "r")
                {
                    Menu.ShowMenu();
                }
                string sessionStart = Helpers.GetSessionTimes("Enter session start time (format hh:mm AM/PM): ");
                string sessionEnd = Helpers.GetSessionTimes("Enter session end time (format hh:mm AM/PM): ");
                TimeSpan sessionDuration = Helpers.CalculateDuration(sessionStart, sessionEnd);
                var command = connection.CreateCommand();
                command.CommandText = $"UPDATE Sessions SET Start = '{sessionStart}', End = '{sessionEnd}', Duration = '{sessionDuration}' WHERE Id = {recordId}";
                command.ExecuteNonQuery();
                connection.Close();
                Menu.ShowMenu();
            }
        }

        internal static void ViewSessions()
        {
            Console.Clear();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Sessions ORDER BY Date DESC";

                List<CodingSession> tableData = new List<CodingSession>();

                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(new CodingSession
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "MM/dd/yyyy", new CultureInfo("en-US")),
                            Start = DateTime.ParseExact(reader.GetString(2), "hh:mm tt", new CultureInfo("en-US")),
                            End = DateTime.ParseExact(reader.GetString(3), "hh:mm tt", new CultureInfo("en-US")),
                            Duration = TimeSpan.Parse(reader.GetString(4))
                        });
                    }
                }
                else
                {
                    Console.WriteLine("No sessions found");
                }
                connection.Close();
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
                foreach (var session in tableData)
                {
                    Console.WriteLine($"{session.Id}. {session.Date.ToString("MM/dd/yyyy")} - Start time: {session.Start.ToString("hh:mm tt")} | End time: {session.End.ToString("hh:mm tt")} | Duration: {session.Duration.ToString(@"hh\:mm")}");
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------\n");
            }
        }
    }
}
