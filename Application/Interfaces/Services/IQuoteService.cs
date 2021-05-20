using Quotes.Application.DTOs.Quote;
using Quotes.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Application.Interfaces.Services
{
    public interface IQuoteService
    {
        Task<Response> CreateAsync(CreateQuoteRequest request);
        Task<GenericResponse<IEnumerable<QuoteResponse>>> GetAllAsync();
        Task<Response> UpdateAsync(UpdateQuoteRequest request);
        Task<Response> DeleteAsync(int id);
        Task<GenericResponse<QuoteResponse>> GetByIdAsync(int id);
        Task<GenericResponse<QuoteResponse>> GetRandom();
        Task GetStatistic(string url);
        Task DeleteOldQuotes();
    }
}
