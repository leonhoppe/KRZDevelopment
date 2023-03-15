using System.Collections.Generic;
using TwitterKlon.Logic.Comments.DTOs;
using TwitterKlon.Contract.Logic.Comments;
using TwitterKlon.Contract.Persistence.Comments;

namespace TwitterKlon.Logic.Comments
{
    public class CommentLogic : ICommentLogic
    {
        private readonly ICommentRepository _comments;

        public CommentLogic(ICommentRepository comments)
        {
            _comments = comments;
        }

        public ILogicResult<Comment> AddComment(CommentEditor editor)
        {
            return LogicResult<Comment>.Ok(_comments.AddComment(editor));
        }

        public ILogicResult<Comment> EditComment(string id, CommentEditor editor)
        {
            if (!_comments.CanEdit(id)) return LogicResult<Comment>.Forbidden();
            return LogicResult<Comment>.Ok(_comments.EditComment(id, editor));
        }

        public ILogicResult<IEnumerable<Comment>> GetCommentsOnPost(string postId)
        {
            return LogicResult<IEnumerable<Comment>>.Ok(_comments.GetComments(postId));
        }

        public ILogicResult RemoveComment(string id)
        {
            if (!_comments.CanEdit(id)) return LogicResult.Forbidden();
            _comments.DeleteComment(id);
            return LogicResult.Ok();
        }
    }
}
