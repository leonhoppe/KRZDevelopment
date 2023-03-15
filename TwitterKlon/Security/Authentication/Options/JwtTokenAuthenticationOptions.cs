using TwitterKlon.Options;

namespace TwitterKlon.Security.Authentication
{
    public class JwtTokenAuthenticationOptions : OptionsFromConfiguration
    {
        public override string Position => "JwtTokenAuthentication:Jwt";

        public string RefreshTokenExpirationTimeInHours { get; set; }
        public string AccessTokenExpirationTimeInMinutes { get; set; }
    }
}