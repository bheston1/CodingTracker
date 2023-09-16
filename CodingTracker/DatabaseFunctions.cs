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
        internal static void CreateDB()
        {
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = "CREATE TABLE IF NOT EXISTS Sessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Start TEXT, End TEXT, Duration TEXT)";
                tableCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
