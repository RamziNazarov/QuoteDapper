using Microsoft.AspNetCore.Mvc;
using Quotes.Application.DTOs.Quote;
using Quotes.Application.Interfaces.Repositories;
using Quotes.Application.Interfaces.Services;
using Quotes.Domain.Wrappers;
using Quotes.Persistence.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public async Task<Response> GetStatistics([FromQuery] string url)
        {
            _quoteService.GetStatistic(url);
            return await Task.FromResult(new Response { Succeeded = true, Message = "Статистика собирается, по мере готовности будет отправлена на ссылку указанную вами )" });
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthenticateAttribute))]
        public async Task<GenericResponse<IEnumerable<QuoteResponse>>> GetAll()
        {
            return await _quoteService.GetAllAsync();
        }

        [HttpGet]
        public async Task<GenericResponse<QuoteResponse>> GetRandom()
        {
            return await _quoteService.GetRandom();
        }

        [HttpGet]
        public async Task<GenericResponse<QuoteResponse>> GetById([FromQuery] int id)
        {
            return await _quoteService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<Response> Create([FromBody] CreateQuoteRequest request)
        {
            return await _quoteService.CreateAsync(request);
        }

        [HttpPut]
        public async Task<Response> Update([FromBody] UpdateQuoteRequest request)
        {
            return await _quoteService.UpdateAsync(request);
        }

        [HttpDelete]
        public async Task<Response> Delete(int id)
        {
            return await _quoteService.DeleteAsync(id);
        }
    }
}
