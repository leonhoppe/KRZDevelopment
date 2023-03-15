using System;
using System.Collections.Generic;
using System.Linq;
using TwitterKlon.Contract.Persistence.Comments;
using TwitterKlon.Logic.Comments.DTOs;
using TwitterKlon.Security;

namespace TwitterKlon.Persistence.Comments
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContext dbContext;
        private readonly ISessionContext session;

        public CommentRepository(DatabaseContext context, ISessionContext session)
        {
            dbContext = context;
            this.session = session;
        }

        public Comment AddComment(CommentEditor editor)
        {
            Comment comment = new Comment { Id = Guid.NewGuid().ToString() };
            editor.EditComment(comment);
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();
            return comment;
        }

        public bool DeleteComment(string id)
        {
            Comment comment = GetCommentByID(id);
            dbContext.Comments.Remove(comment);
            dbContext.SaveChanges();
            return true;
        }

        public Comment EditComment(string id, CommentEditor editor)
        {
            Comment comment = dbContext.Comments
                .Where(comment => comment.Id == id)
                .SingleOrDefault();
            editor.EditComment(comment);
            dbContext.Comments.Update(comment);
            dbContext.SaveChanges();
            return comment;
        }

        public List<Comment> GetComments(string postID)
        {
            return dbContext.Comments
                .Where(comment => comment.PostId == postID)
                .ToList();
        }

        public Comment GetCommentByID(string id)
        {
            return dbContext.Comments
                .Where(comment => comment.Id == id)
                .SingleOrDefault();
        }

        public bool CanEdit(string id) {
            var comment = GetCommentByID(id);
            return session.UserId == comment.SenderId;
        }
    }
}
