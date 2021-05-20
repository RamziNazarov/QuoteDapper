using Dapper;
using Npgsql;
using System.Threading.Tasks;

namespace Quotes.Seeds
{
    public class DefaultAuthors
    {
        public static string ConnectionString { get; set; }

        public static async Task AddDefaultAuthors()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            await connection.ExecuteAsync("INSERT INTO authors(id,fullname) values(@Id,@FullName) ON CONFLICT (id) DO UPDATE SET fullname = @FullName;", new {Id = 1, FullName = "А. С. Пушкин"});
            await connection.ExecuteAsync("INSERT INTO authors(id,fullname) values(@Id,@FullName) ON CONFLICT (id) DO UPDATE SET fullname = @FullName;", new {Id = 2, FullName = "Л. Н. Толстой"});
        }
    }
}
