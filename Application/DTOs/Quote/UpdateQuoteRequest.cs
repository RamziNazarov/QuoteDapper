using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Application.DTOs.Quote
{
    public class UpdateQuoteRequest
    {
        public int Id { get; set; }
        public string QuoteText { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
