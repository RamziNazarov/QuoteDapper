using Newtonsoft.Json;
using Quotes.Application.DTOs.Quote;
using Quotes.Application.Interfaces.Repositories;
using Quotes.Application.Interfaces.Services;
using Quotes.Domain.Entities;
using Quotes.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Persistence.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoterepository;

        public QuoteService(IQuoteRepository quoterepository)
        {
            _quoterepository = quoterepository;
        }        

        public async Task<Response> CreateAsync(CreateQuoteRequest request)
        {
            await _quoterepository.CreateAsync(request);
            return new Response { Succeeded = true, Message = default };
        }

        public async Task<Response> DeleteAsync(int id)
        {
            await _quoterepository.SetRemoveAsync(id);
            return new Response { Succeeded = true, Message = default };
        }

        public async Task DeleteOldQuotes()
        {
            var quotes = await _quoterepository.GetAllAsync();
            DateTime currentDateTime = DateTime.Now;
            var quotesId = quotes.Where(x =>
                x.CreateAt.Hour < currentDateTime.Hour && x.CreateAt.Day <= currentDateTime.Day &&
                x.CreateAt.Year <= currentDateTime.Year).Select(x=>x.Id).ToList();
            await _quoterepository.SetRemoveRangeAsync(quotesId);
        }

        public async Task<GenericResponse<IEnumerable<QuoteResponse>>> GetAllAsync()
        {
            return new GenericResponse<IEnumerable<QuoteResponse>>
            {
                Succeeded = true,
                Payload = await _quoterepository.GetAllAsync()
            };
        }

        public async Task<GenericResponse<QuoteResponse>> GetByIdAsync(int id)
        {
            return new GenericResponse<QuoteResponse>
            {
                Succeeded = true,
                Payload = await _quoterepository.GetByIdAsync(id)
            };
        }

        public async Task<GenericResponse<QuoteResponse>> GetRandom()
        {
            List<int> quotesId = _quoterepository.GetAllIds().Result.ToList();
            if (quotesId.Count == 0)
                return new GenericResponse<QuoteResponse> { Succeeded = true, Message = default, Payload = default };
            int randomNumber = new Random().Next(0, quotesId.Count - 1);
            return new GenericResponse<QuoteResponse>
            {
                Succeeded = true,
                Payload = await _quoterepository.GetByIdAsync(quotesId[randomNumber])
            };
        }

        public async Task GetStatistic(string url)
        {
            List<Quote> quotes = _quoterepository.GetAllWithRemovedAsync().Result.ToList();
            DateTime currentDateTime = DateTime.Now;
            var statistic = new QuotesStatisticResponse 
            {
                LastMonthCreated = quotes.Where(x=> (x.CreateAt.Month - currentDateTime.Month <= 1) &&
                x.CreateAt.Year == currentDateTime.Year).Count(),
                LastMonthDeleted = quotes.Where(x => x.Removed && (x.RemoveAt.Value.Month - currentDateTime.Month <= 1) &&
                x.RemoveAt.Value.Year == currentDateTime.Year).Count(),
                LastMonthUpdated = quotes.Where(x => x.UpdateAt.HasValue && (x.UpdateAt.Value.Month - currentDateTime.Month <= 1) &&
                x.UpdateAt.Value.Year == currentDateTime.Year).Count(),
                LastYearCreated = quotes.Where(x=>(x.CreateAt.Year - currentDateTime.Year) <= 1).Count(),
                LastYearDeleted = quotes.Where(x=>x.Removed && (x.RemoveAt.Value.Year - currentDateTime.Year) <= 1).Count(),
                LastYearUpdated = quotes.Where(x=>x.UpdateAt.HasValue && (x.UpdateAt.Value.Year - currentDateTime.Year) <= 1).Count()
            };
            try
            {
                HttpClient client = new HttpClient();
                var content = JsonConvert.SerializeObject(statistic);
                var data = new StringContent(content, Encoding.UTF8, "application/json");
                await client.PostAsync(url, data);
            }
            catch
            {

            }
        }

        public async Task<Response> UpdateAsync(UpdateQuoteRequest request)
        {
            await _quoterepository.UpdateAsync(request);
            return new Response { Succeeded = true, Message = default };
        }
    }
}
