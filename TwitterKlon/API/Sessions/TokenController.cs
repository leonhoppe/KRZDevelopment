using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TwitterKlon.Logic.Sessions.DTOs;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Logic;
using TwitterKlon.Contract.Logic.Tokens;
using TwitterKlon.Security.Authorization;

namespace TwitterKlon.API.Sessions
{
    [Route("api")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenLogic _logic;

        public TokenController(ITokenLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<bool> Login([FromBody] UserLogin login)
        {
            /* // CREATE Access Token
            RefreshToken token = tokens.Login(login);
            if (token == null) return this.FromLogicResult(LogicResult<bool>.Ok(false));
            SetRefreshToken(token);
            return this.FromLogicResult(LogicResult<bool>.Ok(true)); */
            return this.FromLogicResult(_logic.Login(login));
        }

        [HttpDelete]
        [Route("logout")]
        [Authorized]
        public ActionResult Logout()
        {
            /* tokens.Logout();
            DeleteRefreshToken();
            return this.FromLogicResult(LogicResult.Ok()); */
            return this.FromLogicResult(_logic.Logout());
        }

        [HttpGet]
        [Route("validate")]
        public ActionResult<bool> Validate()
        {
            /* return this.FromLogicResult(LogicResult<bool>.Ok(tokens.Validate(GetAccessToken()))); */
            return this.FromLogicResult(_logic.Validate());
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] UserEditor editor)
        {
            /* RefreshToken token = tokens.Register(editor);
            SetRefreshToken(token);
            return this.FromLogicResult(LogicResult<bool>.Ok(true)); */
            return this.FromLogicResult(_logic.Register(editor));
        }

        [HttpGet]
        [Route("token")]
        public ActionResult<AccessToken> GetToken()
        {
            /* if (!tokens.ValidateRefreshToken(GetRefreshTokenId())) return this.FromLogicResult(LogicResult.Ok());
            AccessToken[] accessTokens = tokens.GetAccessTokens(GetRefreshTokenId());
            if (accessTokens.Length == 0) return this.FromLogicResult(LogicResult<AccessToken>.Ok(tokens.CreateAccessToken(GetRefreshTokenId())));
            return this.FromLogicResult(LogicResult<AccessToken>.Ok(accessTokens[0])); */
            return this.FromLogicResult(_logic.GetToken());
        }

        [HttpGet]
        [Route("permission/{userId}")]
        public ActionResult<bool> HasPermission(string userId)
        {
            /* return users.CanEdit(userId); */
            return this.FromLogicResult(_logic.HasPermission(userId));
        }

        [HttpGet]
        [Route("ownuser")]
        [Authorized]
        public ActionResult<User> GetOwnUser() {
            /* RefreshToken token = tokens.GetRefreshToken(GetRefreshTokenId());
            return this.FromLogicResult(LogicResult<User>.Ok(users.GetUser(token.UserId))); */
            return this.FromLogicResult(_logic.GetOwnUser());
        }
    }
}
