using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlainConcepts.WebDay.Model
{
    public class UserRole
    {
        private UserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
        public int UserId { get; protected set; }
        public User User { get; set; }
        public int RoleId { get; protected set; }
        public Role Role { get; set; }

        public static UserRole Create(int userId, int roleId)
        {
            return new UserRole(userId, roleId);
        }
    }
}
