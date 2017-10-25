using Microsoft.AspNetCore.Authorization;

namespace PlainConcepts.WebDay.Infrastructure.Authorization.Requirements
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
