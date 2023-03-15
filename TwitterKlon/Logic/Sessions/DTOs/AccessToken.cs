using System;

#nullable disable

namespace TwitterKlon.Logic.Sessions.DTOs
{
    public partial class AccessToken
    {
        public string Token { get; set; }
        public string SessionKey { get; set; }
        public DateTime Time { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not AccessToken) return false;
            AccessToken other = (AccessToken)obj;
            if (other.Token != Token) return false;
            if (other.SessionKey != SessionKey) return false;
            if (other.Time != Time) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Token, SessionKey, Time);
    }
}
