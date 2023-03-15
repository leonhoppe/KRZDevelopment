using TwitterKlon.Logic;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Logic.Sessions.DTOs;

namespace TwitterKlon.Contract.Logic.Tokens {
    public interface ITokenLogic {
        ILogicResult<bool> Login(UserLogin login);
        ILogicResult Logout();
        ILogicResult<bool> Validate();
        ILogicResult Register(UserEditor editor);
        ILogicResult<AccessToken> GetToken();
        ILogicResult<bool> HasPermission(string userId);
        ILogicResult<User> GetOwnUser();
    }
}