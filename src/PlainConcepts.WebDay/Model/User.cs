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
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
        }
        private User() { }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string UserName { get; protected set; }
        private List<UserRole> _userRoles { get; set; } = new List<UserRole>();

        public List<Role> Roles => _userRoles.Select(ur => Role.From(ur.RoleId)).ToList();

        public void AddRole(int roleId)
        {
            var role = Role.From(roleId);
            if (_userRoles.All(r => r.RoleId != roleId))
            {
                var newUserRole = UserRole.Create(Id, Role.From(roleId).Id);
                _userRoles.Add(newUserRole);
            }
        }
        public static User Create(string userName, string name, string surname)
        {
            return new User(userName, name, surname);
        }
    }
}
