using Dapper;
using Npgsql;
using Quotes.Application.DTOs.Quote;
using Quotes.Application.Interfaces.Repositories;
using Quotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Persistence.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly string _connectionString;

        public QuoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateAsync(CreateQuoteRequest request)
        {
            string command = "INSERT INTO Quotes (quotetext,authorid,categoryid) values (@QuoteText,@AuthorId,@CategoryId);";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            await connection.ExecuteAsync(command, request);
        }

        public async Task DeleteAsync(int id)
        {
            string command = "DELETE FROM quotes WHERE id = @id;";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            await connection.ExecuteAsync(command, new { id });
        }

        public async Task<IEnumerable<QuoteResponse>> GetAllAsync()
        {
            string command = @"SELECT q.id, 
                             q.quotetext, 
                             q.createat, 
                             a.fullname as Author, 
                             c.name as Category 
                             FROM quotes q 
                             join authors a on q.authorid = a.id 
                             join categories c on q.categoryid = c.id
                             where q.removed = false; ";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            return await connection.QueryAsync<QuoteResponse>(command);
        }

        public async Task<IEnumerable<int>> GetAllIds()
        {
            string command = "select id from quotes where removed = false";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            return await connection.QueryAsync<int>(command);
        }

        public async Task<IEnumerable<Quote>> GetAllWithRemovedAsync()
        {
            string command = @"SELECT * FROM quotes;";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            return await connection.QueryAsync<Quote>(command);
        }

        public async Task<QuoteResponse> GetByIdAsync(int id)
        {
            string command = @"SELECT q.id, 
                             q.quotetext, 
                             q.createat, 
                             a.fullname as Author, 
                             c.name as Category 
                             FROM quotes q 
                             join authors a on q.authorid = a.id 
                             join categories c on q.categoryid = c.id
                             where q.id = @id and q.removed = false;";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            return await connection.QueryFirstOrDefaultAsync<QuoteResponse>(command, new { id });
        }

        public async Task<QuotesStatisticResponse> GetStatistic()
        {
            string command = "select count(select id from quotes)";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            return await connection.QueryFirstOrDefaultAsync<QuotesStatisticResponse>(command);
        }

        public async Task SetRemoveAsync(int id)
        {
            string command = "UPDATE Quotes SET removed = true, removeat = CURRENT_TIMESTAMP WHERE id = @id;";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            await connection.ExecuteAsync(command, new { id });
        }

        public async Task SetRemoveRangeAsync(List<int> ids)
        {
            if (ids.Count == 0)
                return;
            string idsArray = string.Join(",", ids);
            string command = $"update quotes set removed = true, removeat = now() where id in({idsArray})";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            await connection.ExecuteAsync(command);
        }

        public async Task UpdateAsync(UpdateQuoteRequest request)
        {
            string command = @"update quotes set quotetext = @QuoteText, updateat = CURRENT_TIMESTAMP, authorid = @AuthorId, categoryId = @CategoryId where id = @Id";
            using var connection = new NpgsqlConnection(_connectionString ?? throw new ArgumentNullException());
            await connection.ExecuteAsync(command, request);
        }
    }
}
