using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Comments;
using TwitterKlon.Logic.Comments.DTOs;

namespace TwitterKlon.Tests.Comments
{
    [TestClass]
    public class CommentTesterDEFAULT
    {
        private CommentLogic CreateTestCommentLogic()
        {
            var comments = Variables.CreateTestCommentRepositoryDEFAULT();
            return new CommentLogic(comments.Object);
        }

        [TestMethod]
        public void TestAddCommentDEFAULT()
        {
            CommentLogic logic = CreateTestCommentLogic();
            ILogicResult<Comment> comment = logic.AddComment(Variables.CreateTestCommentEditor());
            Assert.AreEqual(Variables.CreateTestComment(), comment.Data);
        }

        [TestMethod]
        public void TestEditCommentDEFAULT()
        {
            CommentLogic logic = CreateTestCommentLogic();
            ILogicResult<Comment> comment = logic.EditComment(Variables.commentId, Variables.CreateTestCommentEditor());
            Assert.AreEqual(Variables.CreateTestComment(), comment.Data);
        }

        [TestMethod]
        public void TestRemoveCommentDEFAULT()
        {
            CommentLogic logic = CreateTestCommentLogic();
            ILogicResult result = logic.RemoveComment(Variables.commentId);
            Assert.AreEqual(LogicResultState.Ok, result.State);
        }

        [TestMethod]
        public void TestGetAllCommentsOnPostDEFAULT()
        {
            CommentLogic logic = CreateTestCommentLogic();
            ILogicResult<IEnumerable<Comment>> comments = logic.GetCommentsOnPost(Variables.postId);
            if (comments.Data == null) Assert.Fail();
        }
    }
}
