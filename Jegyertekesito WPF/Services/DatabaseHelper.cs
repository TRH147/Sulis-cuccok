// Services/DatabaseHelper.cs – TELJES FÁJL
using Jegyertekesito.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace Jegyertekesito.Services
{
    public static class DatabaseHelper
    {
        private static readonly string DbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "JegyManager",
                "jegyek.db"
            );

        private static readonly string ConnectionString = $"Data Source={DbPath}";

        // 1. ADATBÁZIS INICIALIZÁLÁSA
        public static void InitializeDatabase()
        {
            string? folder = Path.GetDirectoryName(DbPath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder!);
            }

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Jegyek
                (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nev TEXT NOT NULL,
                    Ar REAL NOT NULL,
                    Darabszam INTEGER NOT NULL
                );";

            using var command = new SqliteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        // 2. ÚJ JEGY HOZZÁADÁSA (CREATE)
        public static void AddTicket(Ticket ticket)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string query = @"
                INSERT INTO Jegyek (Nev, Ar, Darabszam)
                VALUES (@nev, @ar, @darabszam)";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nev", ticket.Nev);
            command.Parameters.AddWithValue("@ar", ticket.Ar);
            command.Parameters.AddWithValue("@darabszam", ticket.Darabszam);
            command.ExecuteNonQuery();
        }

        // 3. ÖSSZES JEGY LEKÉRÉSE (READ)
        public static List<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = new();
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string query = "SELECT * FROM Jegyek ORDER BY Id";
            using var command = new SqliteCommand(query, connection);
            using SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                tickets.Add(new Ticket
                {
                    Id = reader.GetInt32(0),
                    Nev = reader.GetString(1),
                    Ar = reader.GetDecimal(2),
                    Darabszam = reader.GetInt32(3)
                });
            }

            return tickets;
        }

        // 4. JEGY MÓDOSÍTÁSA (UPDATE) – EZT ADD HOZZÁ, HA HIÁNYZIK
        public static void UpdateTicket(Ticket ticket)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string query = @"
                UPDATE Jegyek 
                SET Nev = @nev, Ar = @ar, Darabszam = @darabszam 
                WHERE Id = @id";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nev", ticket.Nev);
            command.Parameters.AddWithValue("@ar", ticket.Ar);
            command.Parameters.AddWithValue("@darabszam", ticket.Darabszam);
            command.Parameters.AddWithValue("@id", ticket.Id);
            command.ExecuteNonQuery();
        }

        // 5. JEGY TÖRLÉSE (DELETE) – EZT ADD HOZZÁ, HA HIÁNYZIK
        public static void DeleteTicket(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string query = "DELETE FROM Jegyek WHERE Id = @id";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}