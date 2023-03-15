using System;
using Microsoft.AspNetCore.Authentication;
using TwitterKlon.Logic.Sessions.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TwitterKlon.Contract.Persistence.Sessions;
using System.Collections.Generic;
using System.Security.Claims;
using TwitterKlon.Security.Authorization;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace TwitterKlon.Security.Authentication
{
    public class JwtTokenAuthenticationHandler : AuthenticationHandler<JwtTokenAuthenticationHandlerOptions>
    {
        private readonly ITokenRepository tokens;

        public JwtTokenAuthenticationHandler(
            IOptionsMonitor<JwtTokenAuthenticationHandlerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenRepository tokens)
            : base(options, logger, encoder, clock)
        {
            this.tokens = tokens;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var accessToken = GetAccessToken();
            if (accessToken == null) return AuthenticateResult.Fail("Token invalid");
            var refreshToken = tokens.GetRefreshToken(accessToken.Token);
            bool valid = tokens.Validate(accessToken.SessionKey);
            return valid ? 
                        AuthenticateResult.Success(GetAuthenticationTicket(accessToken, refreshToken)) : 
                        AuthenticateResult.Fail("Token invalid");
        }

        private AuthenticationTicket GetAuthenticationTicket(AccessToken accessToken, RefreshToken refreshToken) {
            List<Claim> claims = GenerateClaims(accessToken, refreshToken);
            ClaimsPrincipal principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(claims, JwtTokenAuthentication.Scheme));
            AuthenticationTicket ticket = new AuthenticationTicket(principal, this.Scheme.Name);
            return ticket;
        }

        private List<Claim> GenerateClaims(AccessToken accessToken, RefreshToken refreshToken) {
            List<Claim> claims = new List<Claim>() {
                new Claim(CustomClaimTypes.AccessTokenId, accessToken.SessionKey),
                new Claim(CustomClaimTypes.RefreshTokenId, refreshToken.Id),
                new Claim(CustomClaimTypes.UserId, refreshToken.UserId),
            };

            return claims;
        }

        private AccessToken GetAccessToken() {
            string key = this.Request.Headers["Authorization"];
            AccessToken token = tokens.GetAccessToken(key);
            return token;
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously