using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Posts.DTOs;

namespace TwitterKlon.Contract.Logic.Posts
{
    public interface IPostLogic
    {
        ILogicResult<IEnumerable<Post>> GetAllPosts();
        ILogicResult<Post> GetPost(string id);
        ILogicResult<Post> AddPost(PostEditor editor);
        ILogicResult<Post> EditPost(string id, PostEditor editor);
        ILogicResult DeletePost(string id);
    }
}
