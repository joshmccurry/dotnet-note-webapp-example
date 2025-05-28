using dotnet_note_webapp_example.Models;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace dotnet_note_webapp_example.Data {
    public class NoteClient {
        SqliteConnection _connection;
        ILogger<NoteClient> _logger;
        public NoteClient(ILogger<NoteClient> logger) {
            _logger = logger;
            // Ensure the database file exists and create the table if it doesn't
            if (!File.Exists("./Data/SQlLiteDatabase.db")) {
                Directory.CreateDirectory("./Data");
                File.Create("./Data/SQlLiteDatabase.db").Close();
                _logger.LogInformation("Database file created.");
            }
            _connection = new SqliteConnection("Data Source=./Data/SQlLiteDatabase.db");
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
        public void AddNote(Note note) {
            AddNote(note.Title, note.Content);
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

        public List<Note> GetNote(string id) {
            var notes = new List<Note>();
            var command = _connection.CreateCommand();
            if (id == null) {
                command.CommandText = "SELECT ID, title, content, created FROM notes;";
            } else {
                command.CommandText = "SELECT ID, title, content, created FROM notes WHERE ID = $id;";
                command.Parameters.AddWithValue("$id", id);
            }
            
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    var note = new Note {
                        ID = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Content = reader.GetString(2),
                        Created = reader.GetDateTime(3)
                    };
                    notes.Add(note);
                }
            }
            _logger.LogInformation("Retrieved Notes.");
            return notes;
        }
        public void UpdateNote(Note note) {
            UpdateNote(note.ID.ToString(), note.Title, note.Content);
        }
        public void UpdateNote(string id, string title, string content) {
            var command = _connection.CreateCommand();
            command.CommandText =
            @"UPDATE notes
              SET title = $title, content = $content
              WHERE ID = $id;";
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$title", title);
            command.Parameters.AddWithValue("$content", content);
            command.ExecuteNonQuery();
            _logger.LogInformation("Updated Note.");
        }

        public void DeleteNote(string id) {
            var command = _connection.CreateCommand();
            if(id == null) {
                throw new ArgumentNullException(nameof(id), "ID cannot be null.");
            }else if(id == "all") {
                command.CommandText = "DELETE FROM notes;";
            }else {
                command.CommandText = "DELETE FROM notes WHERE ID = $id;";
                command.Parameters.AddWithValue("$id", id);
            }
            command.ExecuteNonQuery();
            _logger.LogInformation("Deleted Note.");
        }

        public void Dispose() {
            _connection.Close();
            _connection.Dispose();
            _logger.LogInformation("Disposing Client.");
        }
    }
}
