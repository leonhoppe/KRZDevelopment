using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Users.DTOs;

namespace TwitterKlon.Contract.Logic.Users
{
    public interface IUserLogic
    {
        ILogicResult<User> AddUser(UserEditor editor);
        ILogicResult DeleteUser(string id);
        ILogicResult<IEnumerable<User>> GetAllUser();
        ILogicResult<User> GetUser(string id);
        ILogicResult<User> UpdateUser(string id, UserEditor editor);
        ILogicResult<User> GetUserByUsername(string username);
    }
}
