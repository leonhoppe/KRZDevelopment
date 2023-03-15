using System.Collections.Generic;
using TwitterKlon.Logic.Comments.DTOs;

namespace TwitterKlon.Contract.Persistence.Comments
{
    public interface ICommentRepository
    {
        List<Comment> GetComments(string postID);
        Comment AddComment(CommentEditor editor);
        Comment EditComment(string id, CommentEditor editor);
        bool DeleteComment(string id);
        Comment GetCommentByID(string id);
        bool CanEdit(string id);
    }
}
