using Dapper;
using Npgsql;
using Quotes.Application.Interfaces.Repositories;
using Quotes.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Quotes.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> GetById(string id)
        {
            string command = "select * from users where userid = @id;";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            return await connection.QueryFirstOrDefaultAsync<User>(command, new { id });
        }
    }
}
