using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Comments;
using TwitterKlon.Logic.Comments.DTOs;

namespace TwitterKlon.Tests.Comments
{
    [TestClass]
    public class CommentTesterERROR
    {
        private CommentLogic CreateTestCommentLogic()
        {
            var comments = Variables.CreateTestCommentRepositoryERROR();
            return new CommentLogic(comments.Object);
        }

        [TestMethod]
        public void TestAddCommentERROR()
        {
            CommentLogic logic = CreateTestCommentLogic();
            ILogicResult<Comment> comment = logic.AddComment(null);
            Assert.AreEqual(null, comment.Data);
        }

        [TestMethod]
        public void TestEditCommentERROR()
        {
            CommentLogic logic = CreateTestCommentLogic();
            ILogicResult<Comment> comment = logic.EditComment(null, null);
            Assert.AreEqual(LogicResultState.Forbidden, comment.State);
        }

        [TestMethod]
        public void TestRemoveCommentERROR()
        {
            CommentLogic logic = CreateTestCommentLogic();
            ILogicResult result = logic.RemoveComment(null);
            Assert.AreEqual(LogicResultState.Forbidden, result.State);
        }

        [TestMethod]
        public void TestGetAllCommentsOnPostERROR()
        {
            CommentLogic logic = CreateTestCommentLogic();
            ILogicResult<IEnumerable<Comment>> comments = logic.GetCommentsOnPost(null);
            if (comments.Data == null) Assert.Fail();
        }
    }
}
