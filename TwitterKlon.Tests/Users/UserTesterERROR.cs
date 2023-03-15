using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterKlon.Logic.Users;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Users.DTOs;
using System.Collections.Generic;

namespace TwitterKlon.Tests.Users
{
    [TestClass]
    public class UserTesterERROR
    {
        private UserLogic CreateTestUserLogic()
        {
            var users = Variables.CreateTestUserRepositoryERROR();
            var tokens = Variables.CreateTestTokenRepositoryERROR();
            var accessor = Variables.CreateTestContextAccessorERROR();
            return new UserLogic(users.Object, tokens.Object, accessor.Object);
        }

        [TestMethod]
        public void TestAddUserERROR()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<User> user = logic.AddUser(null);
            Assert.AreEqual(null, user.Data);
        }

        [TestMethod]
        public void TestDeleteUserERROR()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult result = logic.DeleteUser(null);
            Assert.AreEqual(LogicResultState.Forbidden, result.State);
        }

        [TestMethod]
        public void TestGetAllUsersERROR()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<IEnumerable<User>> users = logic.GetAllUser();
            if (users.Data == null) Assert.Fail();
        }

        [TestMethod]
        public void TestGetUserERROR()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<User> user = logic.GetUser(null);
            Assert.AreEqual(LogicResultState.NotFound, user.State);
        }

        [TestMethod]
        public void TestGetUserByUsernameERROR()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<User> user = logic.GetUserByUsername(null);
            Assert.AreEqual(LogicResultState.NotFound, user.State);
        }

        [TestMethod]
        public void TestUpdateUserERROR()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<User> user = logic.UpdateUser(null, null);
            Assert.AreEqual(LogicResultState.Forbidden, user.State);
        }
    }
}
