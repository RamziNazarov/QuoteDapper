using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Application.DTOs.Quote
{
    public class QuotesStatisticResponse
    {
        public int LastYearDeleted { get; set; }
        public int LastYearCreated { get; set; }
        public int LastYearUpdated { get; set; }
        public int LastMonthDeleted { get; set; }
        public int LastMonthCreated { get; set; }
        public int LastMonthUpdated { get; set; }
    }
}
