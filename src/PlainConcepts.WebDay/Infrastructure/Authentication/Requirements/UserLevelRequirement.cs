using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PlainConcepts.WebDay.Infrastructure.Authentication.Requirements
{
    public class UserLevelRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; }
        public UserLevelRequirement(string[] roles)
        {
            Roles = roles;
        }
    }
}
