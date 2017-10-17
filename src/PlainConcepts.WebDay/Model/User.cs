using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlainConcepts.WebDay.Model.Shared;

namespace PlainConcepts.WebDay.Model
{
    public class User : Entity
    {
        private User() { }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Role> Roles => _roles.ToList();
        private List<Role> _roles = new List<Role>();

        public static User Create(string name, string surname, List<Role> roles)
        {
            return new User()
            {
                Name = name,
                Surname = surname,
                _roles = roles
            };
        }
    }
}
