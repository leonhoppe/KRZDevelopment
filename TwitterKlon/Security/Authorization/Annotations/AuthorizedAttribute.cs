using Microsoft.AspNetCore.Mvc;

namespace TwitterKlon.Security.Authorization
{
    public sealed class AuthorizedAttribute : TypeFilterAttribute
    {
        public AuthorizedAttribute()
            : base(typeof(AuthorizedFilter))
        {
            this.Arguments = new object[] {};
        }
    }
}