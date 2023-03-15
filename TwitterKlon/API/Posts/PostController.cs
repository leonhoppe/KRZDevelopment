using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Contract.Logic.Posts;
using TwitterKlon.Logic.Posts.DTOs;
using TwitterKlon.Persistence.Sessions;
using TwitterKlon.Security.Authorization;

namespace TwitterKlon.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostLogic _posts;

        public PostController(IPostLogic posts)
        {
            _posts = posts;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {
            return this.FromLogicResult(_posts.GetAllPosts());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Post> GetPost(string id)
        {
            return this.FromLogicResult(_posts.GetPost(id));
        }

        [HttpPost]
        [Authorized]
        public ActionResult<Post> AddPost([FromBody] PostEditor editor)
        {
            return this.FromLogicResult(_posts.AddPost(editor));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorized]
        public ActionResult<Post> EditPost(string id, [FromBody] PostEditor editor)
        {
            return this.FromLogicResult(_posts.EditPost(id, editor));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorized]
        public ActionResult DeletePost(string id)
        {
            return this.FromLogicResult(_posts.DeletePost(id));
        }
    }
}
