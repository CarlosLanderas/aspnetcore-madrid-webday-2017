using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlainConcepts.WebDay.Application.Queries.Response
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string  Name { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
