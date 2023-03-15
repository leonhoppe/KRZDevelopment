using System;
using System.Collections.Generic;
using System.Linq;
using TwitterKlon.Contract.Persistence.Posts;
using TwitterKlon.Logic.Posts.DTOs;
using TwitterKlon.Security;

namespace TwitterKlon.Persistence.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext dbContext;
        private readonly ISessionContext session;

        public PostRepository(DatabaseContext context, ISessionContext session)
        {
            dbContext = context;
            this.session = session;
        }

        public Post AddPost(PostEditor editor)
        {

            Post post = new Post { Id = Guid.NewGuid().ToString(), SenderId = session.UserId };
            editor.EditPost(post);
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();
            return post;
        }

        public bool DeletePost(string id)
        {
            Post post = GetPost(id);
            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();
            return true;
        }

        public Post EditPost(string id, PostEditor editor)
        {
            Post post = GetPost(id);
            editor.EditPost(post);
            dbContext.Posts.Update(post);
            dbContext.SaveChanges();
            return post;
        }

        public List<Post> GetAllPosts()
        {
            return dbContext.Posts.ToList();
        }

        public Post GetPost(string id)
        {
            if (!PostExists(id)) return null;
            return dbContext.Posts
                .Where(post => post.Id == id)
                .SingleOrDefault();
        }

        private bool PostExists(string id)
        {
            return dbContext.Posts
                    .Any(post => post.Id == id);
        }

        public bool CanEdit(string postId) {
            Post post = GetPost(postId);
            return session.UserId == post.SenderId;
        }
    }
}
