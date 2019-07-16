using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebAppExample.Security
{
    public class CanAccessEmployeeRequirement : IAuthorizationRequirement
    {
    }

    public class CanAccessEmployeeHandler : AuthorizationHandler<CanAccessEmployeeRequirement, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanAccessEmployeeRequirement requirement, int employeeId)
        {
            if (context.User.HasClaim(claim => claim.Type == "IsAdmin") || context.User.HasClaim("EmployeeId", employeeId.ToString()))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
