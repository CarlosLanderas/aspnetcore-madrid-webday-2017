using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlainConcepts.WebDay.Model.Shared;

namespace PlainConcepts.WebDay.Model
{
    public class Role: Entity
    {
        private Role() { }
        public string Name { get; set; }

        public Role Create(string name)
        {
            return new Role()
            {
                Name = name
            };
        }
    }
}
