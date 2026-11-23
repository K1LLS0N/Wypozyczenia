using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
namespace Wypozyczenia
{
    public static class Database
    {

        private const string ConnectionString = "Data Source=bazaDanych.db";

        public static List<Sprzet> GetSprzet()
        {
            var lista = new List<Sprzet>();
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqliteCommand(
                "SELECT ID, Nazwa, [Numer seryjny], Status FROM Sprzet ORDER BY Nazwa;", conn);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                lista.Add(new Sprzet
                {
                    Id = r.GetInt32(0),
                    Nazwa = r.GetString(1),
                    NumerSeryjny = r.GetString(2),
                    Status = r.GetString(3)
                });
            }
            return lista;
        }

        public static bool NumerSeryjnyIstnieje(string numer)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            using var cmd = new SqliteCommand(
                "SELECT COUNT(1) FROM Sprzet WHERE [Numer seryjny] = $n;", conn);
            cmd.Parameters.AddWithValue("$n", numer);
            return (long)cmd.ExecuteScalar()! > 0;
        }

        public static int AddSprzet(Sprzet s)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            using var cmd = new SqliteCommand(
                @"INSERT INTO Sprzet (Nazwa, [Numer seryjny], Status)
                  VALUES ($nazwa, $num, $status);
                  SELECT last_insert_rowid();", conn);
            cmd.Parameters.AddWithValue("$nazwa", s.Nazwa);
            cmd.Parameters.AddWithValue("$num", s.NumerSeryjny);
            cmd.Parameters.AddWithValue("$status", s.Status);
            return (int)(long)cmd.ExecuteScalar()!;
        }
    }
}
