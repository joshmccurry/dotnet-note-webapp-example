using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_note_webjob_example {
    public class PurgeClient {
        SqliteConnection _connection;
        ILogger<PurgeClient> _logger;
        public PurgeClient(ILogger<PurgeClient> logger) {
            _logger = logger;

            // Ensure the database file exists and create the table if it doesn't
            if (!File.Exists("/home/site/wwwroot/Data/SQlLiteDatabase.db")) {
                Directory.CreateDirectory("/home/site/wwwroot/Data");
                File.Create("/home/site/wwwroot/Data/SQlLiteDatabase.db").Close();
                _logger.LogInformation("Database file created.");
            }
            _connection = new SqliteConnection("Data Source=/home/site/wwwroot/Data/SQlLiteDatabase.db");
            _connection.Open();
            _logger.LogInformation("Database opened.");

            var command = _connection.CreateCommand();
            command.CommandText =
            @"CREATE TABLE IF NOT EXISTS notes (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            content TEXT NOT NULL,
            created DATETIME NOT NULL);";

            command.ExecuteNonQuery();
        }
        public void Purge() {
            var command = _connection.CreateCommand();
            command.CommandText = "DELETE FROM notes;";
            command.ExecuteNonQuery();
            _logger.LogInformation("Purge Completed.");
            AddNote("Purge Completed", $"Purge completed on: {DateTime.Now.ToString()}");
        }
        public void AddNote(string title, string content) {
            var command = _connection.CreateCommand();
            command.CommandText =
            @"INSERT INTO notes (title, content, created)
              VALUES ($title, $content, $created);";
            command.Parameters.AddWithValue("$title", title);
            command.Parameters.AddWithValue("$content", content);
            command.Parameters.AddWithValue("$created", DateTime.UtcNow);
            command.ExecuteNonQuery();
            _logger.LogInformation("Added Note.");
        }

        public void Dispose() {
            _connection.Close();
            _connection.Dispose();
            _logger.LogInformation("Disposing Client.");
        }
    }
}
