using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Domain.Wrappers
{
    public class GenericResponse<T> : Response
    {
        public T Payload { get; set; }
    }
}
