namespace TwitterKlon.Security
{
    public interface ISessionContext {
        bool IsAuthenticated {get;}
        string UserId {get;}
        string AccessTokenId {get;}
        string RefreshTokenId {get;}
    }
}