using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterKlon.Logic.Posts;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Posts.DTOs;
using System.Collections.Generic;

namespace TwitterKlon.Tests.Posts
{
    [TestClass]
    public class PostTesterERROR
    {
        private PostLogic CreateTestPostLogic()
        {
            var posts = Variables.CreateTestPostRepositoryERROR();
            var tokens = Variables.CreateTestTokenRepositoryERROR();
            return new PostLogic(posts.Object, tokens.Object);
        }

        [TestMethod]
        public void TestAddPostERROR()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult<Post> post = logic.AddPost(null);
            Assert.AreEqual(null, post.Data);
        }

        [TestMethod]
        public void TestDeletePostERROR()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult result = logic.DeletePost(null);
            Assert.AreEqual(LogicResultState.Forbidden, result.State);
        }

        [TestMethod]
        public void TestEditPostERROR()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult<Post> post = logic.EditPost(null, null);
            Assert.AreEqual(LogicResultState.Forbidden, post.State);
        }

        [TestMethod]
        public void TestGetAllPostsERROR()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult<IEnumerable<Post>> posts = logic.GetAllPosts();
            if (posts.Data == null) Assert.Fail();
        }

        [TestMethod]
        public void TestGetPostERROR()
        {
            PostLogic logic = CreateTestPostLogic();
            ILogicResult<Post> post = logic.GetPost(null);
            Assert.AreEqual(null, post.Data);
        }
    }
}
