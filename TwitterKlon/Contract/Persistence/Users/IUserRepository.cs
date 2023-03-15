using System.Collections.Generic;
using TwitterKlon.Logic.Users.DTOs;

namespace TwitterKlon.Contract.Persistence.Users
{
    public interface IUserRepository
    {
        List<User> GetAllUser();
        User GetUser(string id);
        User GetUserByUsername(string username);
        User AddUser(UserEditor editor);
        bool DeleteUser(string id);
        User UpdateUser(string id, UserEditor editor);
        bool CanEdit(string id);
    }
}
