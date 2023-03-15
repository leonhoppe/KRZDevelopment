using Microsoft.AspNetCore.Http;
using TwitterKlon.Security.Authorization;

namespace TwitterKlon.Security
{
    internal class SessionContext : ISessionContext
    {
        private readonly IHttpContextAccessor accessor;

        public SessionContext(IHttpContextAccessor accessor) {
            this.accessor = accessor;
        }

        public bool IsAuthenticated => accessor.HttpContext.User.Identity.IsAuthenticated;

        public string UserId => accessor.HttpContext.User.GetUserId();

        public string AccessTokenId => accessor.HttpContext.User.GetAccessTokenId();

        public string RefreshTokenId => accessor.HttpContext.User.GetRefreshTokenId();
    }
}