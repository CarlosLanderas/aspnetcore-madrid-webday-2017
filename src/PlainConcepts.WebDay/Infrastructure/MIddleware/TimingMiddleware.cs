using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PlainConcepts.WebDay.Infrastructure.MIddleware
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;

        public TimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = Stopwatch.StartNew();
            await _next(context);
            stopWatch.Stop();
            Debug.WriteLine($"Request took {stopWatch.ElapsedMilliseconds} ms");
        }

    }
}
