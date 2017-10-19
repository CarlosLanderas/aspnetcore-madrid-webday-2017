using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlainConcepts.WebDay.API.Users.Requests
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
