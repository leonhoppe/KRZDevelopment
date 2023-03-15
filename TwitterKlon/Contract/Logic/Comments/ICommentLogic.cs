using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Comments.DTOs;

namespace TwitterKlon.Contract.Logic.Comments
{
    public interface ICommentLogic
    {
        ILogicResult<IEnumerable<Comment>> GetCommentsOnPost(string postId);
        ILogicResult<Comment> AddComment(CommentEditor editor);
        ILogicResult<Comment> EditComment(string id, CommentEditor editor);
        ILogicResult RemoveComment(string id);
    }
}
