using Dapper;
using Npgsql;
using Quotes.Application.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Seeds
{
    public class DefaultUsers
    {
        public static string ConnectionString { get; set; }

        public static async Task AddDefaultUsers()
        {
            List<CreateUserRequest> users = new List<CreateUserRequest>
            {
                new CreateUserRequest { UserId = "wqer1234-qwer-1234-qwerty123456", SecretWord = "Dota 2" },
                new CreateUserRequest { UserId = "1234qwer-1234-wqer-123456qwerty", SecretWord = "Alif pro" },
            };
            using var connection = new NpgsqlConnection(ConnectionString);
            await connection.ExecuteAsync("INSERT INTO users(userid,secretword) values(@UserId,@SecretWord) ON CONFLICT DO NOTHING;", users[0]);
            await connection.ExecuteAsync("INSERT INTO users(userid,secretword) values(@UserId,@SecretWord) ON CONFLICT DO NOTHING;", users[1]);
        }
    }
}
