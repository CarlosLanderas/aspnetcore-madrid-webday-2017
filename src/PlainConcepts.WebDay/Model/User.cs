using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlainConcepts.WebDay.Model.Shared;

namespace PlainConcepts.WebDay.Model
{
    public class User : Entity
    {
        private User(string userName, string name, string surname)
        {
            UserName = userName;
            Name = name;
            Surname = surname;
        }
        private User() { }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string UserName { get; protected set; }
        private List<UserRole> _userRoles { get; set; } = new List<UserRole>();

        public List<Role> Roles => _userRoles.Select(ur => Role.From(ur.RoleId)).ToList();

        public static User Create(string userName, string name, string surname)
        {
            return new User(userName, name, surname);
        }
    }
}
