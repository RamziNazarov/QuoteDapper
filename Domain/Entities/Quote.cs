using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Domain.Entities
{
    public class Quote
    {
        public int Id { get; set; }
        public bool Removed { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string QuoteText { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? RemoveAt { get; set; }
    }
}
