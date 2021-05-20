using Dapper;
using Npgsql;
using System.Threading.Tasks;

namespace Quotes.Seeds
{
    public class DefaultCategories
    {
        public static string ConnectionString { get; set; }

        public static async Task AddDefaultCategories()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            await connection.ExecuteAsync("INSERT INTO categories(id,name) values(@Id,@Name) ON CONFLICT (id) DO UPDATE SET name = @Name;", new { Id = 1, Name = "Любовные" });
            await connection.ExecuteAsync("INSERT INTO categories(id,name) values(@Id,@Name) ON CONFLICT (id) DO UPDATE SET name = @Name;", new { Id = 2, Name = "Научные" });
        }
    }
}