using System.Security.Claims;

namespace TwitterKlon.Security.Authorization
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetAccessTokenId(this ClaimsPrincipal principal) => principal.FindFirstValue(CustomClaimTypes.AccessTokenId);
        public static string GetRefreshTokenId(this ClaimsPrincipal principal) => principal.FindFirstValue(CustomClaimTypes.RefreshTokenId);
        public static string GetUserId(this ClaimsPrincipal principal) => principal.FindFirstValue(CustomClaimTypes.UserId);
    }
}