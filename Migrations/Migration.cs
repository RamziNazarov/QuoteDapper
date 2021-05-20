using Dapper;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Quotes.Migrations
{
    public class Migration
    {
        public static string ConnectionString { get; set; }

        public static async Task MigrateAsync()
        {
            await InitCreate();
        }

        private static async Task InitCreate()
        {
            using var connection = new NpgsqlConnection(ConnectionString ?? throw new ArgumentNullException());
            await connection.ExecuteAsync
            (@"CREATE TABLE IF NOT EXISTS Authors (
               Id SERIAL PRIMARY KEY,
               FullName VARCHAR(255) NOT NULL
            );");
            await connection.ExecuteAsync
            (@"CREATE TABLE IF NOT EXISTS Categories (
               Id SERIAL PRIMARY KEY,
               Name VARCHAR(255) NOT NULL
            );");
            await connection.ExecuteAsync
            (@"CREATE TABLE IF NOT EXISTS Quotes (
               Id SERIAL PRIMARY KEY,
               QuoteText VARCHAR(255) NOT NULL,
               AuthorId INT NOT NULL,
               CategoryId INT NOT NULL,
               CreateAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
               UpdateAt TIMESTAMP NULL,
               RemoveAt TIMESTAMP NULL,
               Removed BOOLEAN NOT NULL DEFAULT false,
               FOREIGN KEY (AuthorId) REFERENCES Authors (Id),
               FOREIGN KEY (CategoryId) REFERENCES Categories (Id)
            );");
            await connection.ExecuteAsync
            (@"CREATE TABLE IF NOT EXISTS Users (
               UserId VARCHAR(255) PRIMARY KEY,
               SecretWord VARCHAR(255) NOT NULL
            );");
        }
    }
}
