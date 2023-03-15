#nullable disable

using System;

namespace TwitterKlon.Logic.Users.DTOs
{
    public partial class User : UserEditor
    {
        public string Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not User) return false;
            User other = (User)obj;
            if (other.Id != Id) return false;
            if (other.FirstName != FirstName) return false;
            if (other.LastName != LastName) return false;
            if (other.Address != Address) return false;
            if (other.Username != Username) return false;
            if (other.Password != Password) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Id, FirstName, LastName, Address, Username, Password);
    }

    public class UserEditor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public void EditUser(User user)
        {
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Address = Address;
            user.Username = Username;
            user.Password = Password;
        }

        public override bool Equals(object obj)
        {
            if (obj is not UserEditor) return false;
            UserEditor other = (UserEditor)obj;
            if (other.FirstName != FirstName) return false;
            if (other.LastName != LastName) return false;
            if (other.Address != Address) return false;
            if (other.Username != Username) return false;
            if (other.Password != Password) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(FirstName, LastName, Address, Username, Password);

        public override string ToString() => $"{FirstName} {LastName}, {Address}, {Username}, {Password}";
    }

    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not UserLogin) return false;
            UserLogin other = (UserLogin)obj;
            if (other.Username != Username) return false;
            if (other.Password != Password) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Username, Password);
    }
}
