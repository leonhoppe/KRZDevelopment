using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace TwitterKlon.Security.Authorization
{
    public class AuthorizedFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (EndpointHasAllowAnonymousFilter(context))
            {
                return;
            }

            if (!this.IsAuthenticated(context))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }

        private static bool EndpointHasAllowAnonymousFilter(AuthorizationFilterContext context)
        {
            return context.Filters.Any(item => item is IAllowAnonymousFilter);
        }

        private bool IsAuthenticated(AuthorizationFilterContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}