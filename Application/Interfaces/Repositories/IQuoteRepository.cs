using Quotes.Application.DTOs.Quote;
using Quotes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Application.Interfaces.Repositories
{
    public interface IQuoteRepository
    {
        Task<QuoteResponse> GetByIdAsync(int id);
        Task<IEnumerable<QuoteResponse>> GetAllAsync();
        Task CreateAsync(CreateQuoteRequest request);
        Task UpdateAsync(UpdateQuoteRequest request);
        Task SetRemoveAsync(int id);
        Task<IEnumerable<int>> GetAllIds();
        Task<QuotesStatisticResponse> GetStatistic();
        Task SetRemoveRangeAsync(List<int> ids);
        Task<IEnumerable<Quote>> GetAllWithRemovedAsync();
    }
}
