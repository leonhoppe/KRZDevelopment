using System.Collections.Generic;
using TwitterKlon.Contract.Persistence.Users;
using TwitterKlon.Contract.Logic.Users;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Contract.Persistence.Sessions;
using Microsoft.AspNetCore.Http;

namespace TwitterKlon.Logic.Users
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _users;
        private readonly ITokenRepository _manager;
        private readonly IHttpContextAccessor _accessor;


        public UserLogic(IUserRepository users, ITokenRepository manager, IHttpContextAccessor accessor)
        {
            _users = users;
            _manager = manager;
            _accessor = accessor;
        }

        public ILogicResult<User> AddUser(UserEditor editor)
        {
            return LogicResult<User>.Ok(_users.AddUser(editor));
        }

        public ILogicResult DeleteUser(string id)
        {
            if (!_users.CanEdit(id)) return LogicResult.Forbidden();
            _manager.Logout();
            _accessor.HttpContext.Response.Cookies.Delete("refresh_token");
            _users.DeleteUser(id);
            return LogicResult.Ok();
        }

        public ILogicResult<IEnumerable<User>> GetAllUser()
        {
            return LogicResult<IEnumerable<User>>.Ok(_users.GetAllUser());
        }

        public ILogicResult<User> GetUser(string id)
        {
            User user = _users.GetUser(id);
            if (user == null) return LogicResult<User>.NotFound();
            user.Password = null;
            return LogicResult<User>.Ok(user);
        }

        public ILogicResult<User> GetUserByUsername(string username)
        {
            User user = _users.GetUserByUsername(username);
            if (user == null) return LogicResult<User>.NotFound();
            user.Password = null;
            return LogicResult<User>.Ok(user);
        }

        public ILogicResult<User> UpdateUser(string id, UserEditor editor)
        {
            if (!_users.CanEdit(id)) return LogicResult<User>.Forbidden();
            return LogicResult<User>.Ok(_users.UpdateUser(id, editor));
        }
    }
}
