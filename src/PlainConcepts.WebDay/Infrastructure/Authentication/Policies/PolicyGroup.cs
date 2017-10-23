using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlainConcepts.WebDay.Infrastructure.Authentication.Policies
{
    public class PolicyGroup
    {
        public static string[] CreateUserGroupPolicy = {
            Policy.Admin,
            Policy.Reader
        };

        public static string[] ListUsersGroupPolicy =
        {
            Policy.Admin,
            Policy.Reader,
            Policy.Writer
        };

        public static string[] RemoveUserGroupPolicy =
        {
            Policy.Admin
        };
    }
}
