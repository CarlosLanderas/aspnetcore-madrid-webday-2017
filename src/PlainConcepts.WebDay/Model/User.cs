using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlainConcepts.WebDay.Model.Shared;

namespace PlainConcepts.WebDay.Model
{
    public class User : Entity
    {
        private User(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        private User() { }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public List<UserRole> Roles => _roles.ToList();
        private List<UserRole> _roles = new List<UserRole>();
        

        public static User Create(string name, string surname)
        {
            return new User(name, surname);
        }
    }
}
