using System;
using System.Linq;
using TwitterKlon.Logic.Sessions.DTOs;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Contract.Persistence.Sessions;
using TwitterKlon.Contract.Persistence.Users;
using TwitterKlon.Security;
using Microsoft.Extensions.Options;
using TwitterKlon.Security.Authentication;

namespace TwitterKlon.Persistence.Sessions
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DatabaseContext dbContext;
        private readonly ISessionContext session;
        private readonly JwtTokenAuthenticationOptions options;
        public TokenRepository(DatabaseContext context, ISessionContext session, IOptions<JwtTokenAuthenticationOptions> options)
        {
            dbContext = context;
            this.session = session;
            this.options = options.Value;
        }

        public AccessToken[] GetAccessTokens(string token)
        {
            return dbContext.AccessTokens.Where(s => s.Token == token).ToArray();
        }

        public RefreshToken Login(UserLogin login, IUserRepository users)
        {
            if (login == null || login.Equals(new UserLogin { Password = null, Username = null })) return null;
            User user = users.GetUserByUsername(login.Username);
            if (user == null) return null;
            if (!user.Password.Equals(login.Password)) return null;
            return CreateRefreshToken(user.Id);
        }

        public void Logout()
        {
            RefreshToken token = GetRefreshToken(session.RefreshTokenId);
            if (token == null) return;
            DeleteRefreshToken(token);
        }

        public RefreshToken Register(UserEditor editor, IUserRepository users)
        {
            User user = users.AddUser(editor);
            return CreateRefreshToken(user.Id);
        }

        public bool Validate(string accessTokenId)
        {
            return ValidateAccessToken(accessTokenId);
        }

        public AccessToken CreateAccessToken(string refreshTokenId)
        {
            if (!ValidateRefreshToken(refreshTokenId)) return null;
            AccessToken session = new AccessToken { SessionKey = Guid.NewGuid().ToString(), Token = refreshTokenId, Time = DateTime.Now };
            dbContext.AccessTokens.Add(session);
            dbContext.SaveChanges();
            return session;
        }

        private RefreshToken CreateRefreshToken(string userId)
        {
            RefreshToken token = new RefreshToken { UserId = userId, Id = Guid.NewGuid().ToString(), ExpirationDate = DateTime.Now.Add(new TimeSpan(int.Parse(options.RefreshTokenExpirationTimeInHours), 0, 0)) };
            dbContext.RefreshTokens.Add(token);
            dbContext.SaveChanges();
            return token;
        }
        public bool ValidateRefreshToken(string refreshTokenId)
        {
            if (refreshTokenId == null) return false;
            RefreshToken token = GetRefreshToken(refreshTokenId);
            if (token == null) return false;
            DateTime expiration = token.ExpirationDate;
            TimeSpan span = expiration - DateTime.Now;
            bool valid = span.TotalMilliseconds > 0;
            if (!valid) DeleteRefreshToken(token);
            return valid;
        }
        public RefreshToken GetRefreshToken(string refreshTokenId)
        {
            return dbContext.RefreshTokens.Where(t => t.Id == refreshTokenId).SingleOrDefault();
        }
        private void DeleteRefreshToken(RefreshToken token)
        {
            dbContext.AccessTokens.RemoveRange(GetAccessTokens(token.Id));
            dbContext.RefreshTokens.Remove(token);
            dbContext.SaveChanges();
        }

        private bool ValidateAccessToken(string accessTokenId)
        {
            if (accessTokenId is null) return false;
            AccessToken token = GetAccessToken(accessTokenId);
            if (token is null) return false;
            if (!ValidateRefreshToken(token.Token))
            {
                DeleteAccessToken(token);
                return false;
            }
            DateTime creation = token.Time;
            TimeSpan span = DateTime.Now - creation;
            bool valid = span.TotalMinutes <= int.Parse(options.AccessTokenExpirationTimeInMinutes);
            if (!valid) DeleteAccessToken(token);
            return valid;
        }
        public AccessToken GetAccessToken(string accessTokenId)
        {
            return dbContext.AccessTokens.Where(s => s.SessionKey == accessTokenId).SingleOrDefault();
        }
        private void DeleteAccessToken(AccessToken token)
        {
            dbContext.AccessTokens.Remove(token);
            dbContext.SaveChanges();
        }
    }
}
