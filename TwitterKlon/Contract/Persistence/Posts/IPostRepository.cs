using System.Collections.Generic;
using TwitterKlon.Logic.Posts.DTOs;

namespace TwitterKlon.Contract.Persistence.Posts
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();
        Post GetPost(string id);
        Post AddPost(PostEditor editor);
        Post EditPost(string id, PostEditor editor);
        bool DeletePost(string id);
        bool CanEdit(string postId);
    }
}
