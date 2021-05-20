using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Domain.Wrappers
{
    public class Response
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
