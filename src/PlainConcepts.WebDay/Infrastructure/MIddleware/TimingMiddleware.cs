using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PlainConcepts.WebDay.Infrastructure.MIddleware
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimingMiddleware> _logger;

        public TimingMiddleware(RequestDelegate next, ILogger<TimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = Stopwatch.StartNew();
            await _next(context);
            stopWatch.Stop();
            _logger.LogDebug($"Request took {stopWatch.ElapsedMilliseconds} ms");
        }

    }
}
