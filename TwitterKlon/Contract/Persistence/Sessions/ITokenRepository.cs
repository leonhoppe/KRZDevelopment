using TwitterKlon.Contract.Persistence.Users;
using TwitterKlon.Logic.Sessions.DTOs;
using TwitterKlon.Logic.Users.DTOs;

namespace TwitterKlon.Contract.Persistence.Sessions
{
    public interface ITokenRepository
    {
        RefreshToken Login(UserLogin login, IUserRepository users);
        void Logout();
        bool Validate(string accessTokenId);
        bool ValidateRefreshToken(string refreshTokenId);
        RefreshToken Register(UserEditor editor, IUserRepository users);
        AccessToken[] GetAccessTokens(string refreshTokenId);
        AccessToken CreateAccessToken(string refreshTokenId);
        RefreshToken GetRefreshToken(string refreshTokenId);
        AccessToken GetAccessToken(string accessTokenId);
    }
}
