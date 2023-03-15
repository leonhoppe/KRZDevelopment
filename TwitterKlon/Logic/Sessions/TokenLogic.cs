using Microsoft.AspNetCore.Http;
using TwitterKlon.Contract.Logic.Tokens;
using TwitterKlon.Contract.Persistence.Sessions;
using TwitterKlon.Contract.Persistence.Users;
using TwitterKlon.Logic.Sessions.DTOs;
using TwitterKlon.Logic.Users.DTOs;
using System;
using TwitterKlon.Security;

namespace TwitterKlon.Logic.Sessions
{
    public class TokenLogic : ITokenLogic
    {
        private readonly IHttpContextAccessor accessor;
        private readonly ITokenRepository tokens;
        private readonly IUserRepository users;
        private readonly ISessionContext session;

        public TokenLogic(IHttpContextAccessor accessor, ITokenRepository tokens, IUserRepository users, ISessionContext session) {
            this.accessor = accessor;
            this.tokens = tokens;
            this.users = users;
            this.session = session;
        }

        public ILogicResult<User> GetOwnUser()
        {
            RefreshToken token = tokens.GetRefreshToken(session.RefreshTokenId);
            if (token == null) return LogicResult<User>.Conflict();
            return LogicResult<User>.Ok(users.GetUser(token.UserId));
        }

        public ILogicResult<AccessToken> GetToken()
        {
            if (GetRefreshTokenId() == null) return LogicResult<AccessToken>.Forbidden();
            if (!tokens.ValidateRefreshToken(GetRefreshTokenId())) {
                DeleteRefreshToken();
                return LogicResult<AccessToken>.Forbidden();
            }
            AccessToken[] accessTokens = tokens.GetAccessTokens(GetRefreshTokenId());
            if (accessTokens.Length == 0) return LogicResult<AccessToken>.Ok(tokens.CreateAccessToken(GetRefreshTokenId()));
            return LogicResult<AccessToken>.Ok(accessTokens[0]);
        }

        public ILogicResult<bool> HasPermission(string userId)
        {
            if (userId == null) return LogicResult<bool>.NotFound();
            return LogicResult<bool>.Ok(users.CanEdit(userId));
        }

        public ILogicResult<bool> Login(UserLogin login)
        {
            RefreshToken refreshToken = tokens.Login(login, users);
            if (refreshToken == null) return LogicResult<bool>.Ok(false);
            SetRefreshToken(refreshToken);
            return LogicResult<bool>.Ok(true);
        }

        public ILogicResult Logout()
        {
            tokens.Logout();
            DeleteRefreshToken();
            return LogicResult.Ok();
        }

        public ILogicResult Register(UserEditor editor)
        {
            RefreshToken refreshToken = tokens.Register(editor, users);
            if (refreshToken == null) return LogicResult.Conflict();
            SetRefreshToken(refreshToken);
            return LogicResult.Ok();
        }

        public ILogicResult<bool> Validate()
        {
            bool valid = tokens.Validate(session.AccessTokenId);
            return LogicResult<bool>.Ok(valid);
        }

        private void DeleteRefreshToken()
        {
            accessor.HttpContext.Response.Cookies.Delete("refresh_token");
        }
        private void SetRefreshToken(RefreshToken token)
        {
            accessor.HttpContext.Response.Cookies.Append("refresh_token", token.Id, new CookieOptions()
            {
                MaxAge = token.ExpirationDate - DateTime.Now,
                HttpOnly = true,
                Secure = true
            });
        }
        private string GetRefreshTokenId() {
            return accessor.HttpContext.Request.Cookies["refresh_token"];
        }
    }
}