using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PlainConcepts.WebDay.API.Test.Request
{
    public class InboundRequest
    {
        public string ClientId { get; set; }
        public string Sorting { get; set; }
        public string Page { get; set; }
        public string Language { get; set; }
        public QueryString Query { get; set; }
    }
}
