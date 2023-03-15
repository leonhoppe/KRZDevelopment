using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterKlon.Logic.Posts;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Posts.DTOs;
using System.Collections.Generic;

namespace TwitterKlon.Tests.Posts
{
    [TestClass]
    public class PostTesterDEFAULT
    {
        private PostLogic CreateTestPostLogic()
        {
            var posts = Variables.CreateTestPostRepositoryDEFAULT();
            var tokens = Variables.CreateTestTokenRepositoryDEFAULT();
            return new PostLogic(posts.Object, tokens.Object);
        }

        [TestMethod]
        public void TestAddPostDEFAULT()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult<Post> post = logic.AddPost(Variables.CreateTestPostEditor());
            Assert.AreEqual(Variables.CreateTestPost(), post.Data);
        }

        [TestMethod]
        public void TestDeletePostDEFAULT()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult result = logic.DeletePost(Variables.postId);
            Assert.AreEqual(LogicResultState.Ok, result.State);
        }

        [TestMethod]
        public void TestEditPostDEFAULT()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult<Post> post = logic.EditPost(Variables.postId, Variables.CreateTestPostEditor());
            Assert.AreEqual(Variables.CreateTestPost(), post.Data);
        }

        [TestMethod]
        public void TestGetAllPostsDEFAULT()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult<IEnumerable<Post>> posts = logic.GetAllPosts();
            if (posts.Data == null) Assert.Fail();
        }

        [TestMethod]
        public void TestGetPostDEFAULT()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult<Post> post = logic.GetPost(Variables.postId);
            Assert.AreEqual(Variables.CreateTestPost(), post.Data);
        }
    }
}
