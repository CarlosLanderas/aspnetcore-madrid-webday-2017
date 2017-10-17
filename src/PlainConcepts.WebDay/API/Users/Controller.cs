using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PlainConcepts.WebDay.API.Users
{
    [Route("api/users")]
    public class Controller : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok(new[]
            {
                "Pepe",
                "lopez"
            });
        }
    }
}
