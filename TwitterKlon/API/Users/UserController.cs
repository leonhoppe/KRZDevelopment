using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Contract.Logic.Users;
using TwitterKlon.Security.Authorization;

namespace TwitterKlon.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _logic;

        public UserController(IUserLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return this.FromLogicResult(_logic.GetAllUser());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<User> GetUser(string id)
        {
            return this.FromLogicResult(_logic.GetUser(id));
        }

        [HttpGet]
        [Route("byusername/{username}")]
        public ActionResult<User> GetUserByUsername(string username)
        {
            return this.FromLogicResult(_logic.GetUserByUsername(username));
        }

        [HttpPost]
        public ActionResult<User> AddUser([FromBody] UserEditor editor)
        {
            return this.FromLogicResult(_logic.AddUser(editor));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorized]
        public ActionResult<User> UpdateUser(string id, [FromBody] UserEditor editor)
        {
            return this.FromLogicResult(_logic.UpdateUser(id, editor));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorized]
        public ActionResult DeleteUser(string id)
        {
            return this.FromLogicResult(_logic.DeleteUser(id));
        }

        private string GetUserKey()
        {
            return HttpContext.Request.Headers["Authorization"];
        }
    }
}
