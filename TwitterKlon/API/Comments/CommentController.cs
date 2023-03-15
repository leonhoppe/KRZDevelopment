using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Contract.Logic.Comments;
using TwitterKlon.Logic.Comments.DTOs;
using TwitterKlon.Security.Authorization;

namespace TwitterKlon.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentLogic _logic; 

        public CommentController(ICommentLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("posts/{postID}")]
        public ActionResult<IEnumerable<Comment>> GetCommentsOnPost(string postID)
        {
            return this.FromLogicResult(_logic.GetCommentsOnPost(postID));
        }

        [HttpPost]
        [Authorized]
        public ActionResult<Comment> AddComment([FromBody] CommentEditor editor)
        {
            return this.FromLogicResult(_logic.AddComment(editor));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorized]
        public ActionResult<Comment> EditComment(string id, [FromBody] CommentEditor editor)
        {
            return this.FromLogicResult(_logic.EditComment(id, editor));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorized]
        public ActionResult DeleteComment(string id)
        {
            return this.FromLogicResult(_logic.RemoveComment(id));
        }

        private string GetUserKey()
        {
            return HttpContext.Request.Headers["Authorization"];
        }
    }
}
