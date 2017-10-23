using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PlainConcepts.WebDay.API.Test
{
    [Route("api/[controller]")]
    public class TestController: ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> LongRunningTask([FromQuery] int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.WriteLine($"Task executed for {i} seconds");
                await Task.Delay(1000);
            }
            return Ok();
        }
    }
}
