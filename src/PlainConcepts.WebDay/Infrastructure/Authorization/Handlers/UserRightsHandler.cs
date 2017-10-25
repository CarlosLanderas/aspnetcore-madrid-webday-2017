using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PlainConcepts.WebDay.Infrastructure.Authorization.Requirements;

namespace PlainConcepts.WebDay.Infrastructure.Authorization.Handlers
{
    public class UserRightsHandler : AuthorizationHandler<UserLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserLevelRequirement requirement)
        {
            var roles = context.User.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(r => r.Value);
            
            if(requirement.Roles.Intersect(roles).Any())
            {
                context.Succeed(requirement);
                return Task.FromResult(true);
            }

            context.Fail();
            return Task.FromResult(false);
        }
    }
}
