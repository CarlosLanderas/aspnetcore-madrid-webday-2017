using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlainConcepts.WebDay.Model.Shared;

namespace PlainConcepts.WebDay.Model
{
    public class Role : Entity
    {
        public static Role Admin = new Role(1, nameof(Admin));
        public static Role Writer = new Role(2, nameof(Writer));
        public static Role Reader = new Role(3, nameof(Reader));

        public string Name { get; protected set; }

        private Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<Role> List()
        {
            return new[] {Admin, Writer, Reader};
        }

        public static Role From(int id)
        {
            return List().FirstOrDefault(r => r.Id == id) ??
                   throw new Exception($"{nameof(Role)} not found");
        }

        public static Role From(string name)
        {
            return List().FirstOrDefault
                       (r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                   throw new Exception($"{nameof(Role)} not found");
        }

        public static bool Contains(int id)
        {
            return List().Any(r => r.Id == id);
        }

        public static bool Contains(string name)
        {
            return List().Any(r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
