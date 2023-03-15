using System.Collections.Generic;
using TwitterKlon.Logic.Posts.DTOs;
using TwitterKlon.Contract.Logic.Posts;
using TwitterKlon.Contract.Persistence.Posts;
using TwitterKlon.Contract.Persistence.Sessions;

namespace TwitterKlon.Logic.Posts
{
    public class PostLogic : IPostLogic
    {
        private readonly IPostRepository _posts;
        private readonly ITokenRepository _manager;

        public PostLogic(IPostRepository posts, ITokenRepository manager)
        {
            _posts = posts;
            _manager = manager;
        }

        public ILogicResult<Post> AddPost(PostEditor editor)
        {
            return LogicResult<Post>.Ok(_posts.AddPost(editor));
        }

        public ILogicResult DeletePost(string id)
        {
            if (!_posts.CanEdit(id)) return LogicResult.Forbidden();
            _posts.DeletePost(id);
            return LogicResult.Ok();
        }

        public ILogicResult<Post> EditPost(string id, PostEditor editor)
        {
            if (!_posts.CanEdit(id)) return LogicResult<Post>.Forbidden();
            return LogicResult<Post>.Ok(_posts.EditPost(id, editor));
        }

        public ILogicResult<IEnumerable<Post>> GetAllPosts()
        {
            return LogicResult<IEnumerable<Post>>.Ok(_posts.GetAllPosts());
        }

        public ILogicResult<Post> GetPost(string id)
        {
            return LogicResult<Post>.Ok(_posts.GetPost(id));
        }
    }
}
