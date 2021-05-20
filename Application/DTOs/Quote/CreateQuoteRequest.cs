using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Application.DTOs.Quote
{
    public class CreateQuoteRequest
    {

        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string QuoteText { get; set; }
    }
}
