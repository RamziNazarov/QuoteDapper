using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Application.DTOs.Quote
{
    public class QuoteResponse
    {
        public int Id { get; set; }
        public string QuoteText { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
