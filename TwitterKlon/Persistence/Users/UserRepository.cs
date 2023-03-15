using System;
using System.Collections.Generic;
using System.Linq;
using TwitterKlon.Contract.Persistence.Users;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Security;

namespace TwitterKlon.Persistence.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext dbContext;
        private readonly ISessionContext session;

        public UserRepository(DatabaseContext dbContext, ISessionContext session)
        {
            this.dbContext = dbContext;
            this.session = session;
        }
        
        public User AddUser(UserEditor editor)
        {
            User user = new User() {Id = Guid.NewGuid().ToString()};
            editor.EditUser(user);
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public bool DeleteUser(string id)
        {
            User user = GetUser(id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return true;
        }

        public List<User> GetAllUser()
        {
            List<User> users = dbContext.Users.ToList();
            users.ForEach(user => user.Password = null);
            return users;
        }

        public User GetUser(string id)
        {
            if (!UserExists(id)) return null;
            return dbContext.Users
                .Where(user => user.Id == id)
                .SingleOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            if (!UserExistsWithUsername(username)) return null;
            return dbContext.Users
                .Where(user => user.Username == username)
                .SingleOrDefault();
        }

        public User UpdateUser(string id, UserEditor editor)
        {
            User user = GetUser(id);
            editor.EditUser(user);
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
            return user;
        }

        private bool UserExists(string id)
        {
            return dbContext.Users
                .Any(user => user.Id == id);
        }
        private bool UserExistsWithUsername(string username)
        {
            return dbContext.Users
                .Any(user => user.Username == username);
        }

        public bool CanEdit(string id) {
            return session.UserId == id;
        }
    }
}
