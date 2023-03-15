using TwitterKlon.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TwitterKlon.Security.Authentication
{
    public static class JwtTokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddJwtTokenAuthentication(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddOptionsFromConfiguration<JwtTokenAuthenticationOptions>(configuration);

            return builder.AddScheme<JwtTokenAuthenticationHandlerOptions, JwtTokenAuthenticationHandler>(
                JwtTokenAuthentication.Scheme,
                _ => { });
        }
    }
}