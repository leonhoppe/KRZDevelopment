using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterKlon.Logic.Users;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Users.DTOs;
using System.Collections.Generic;

namespace TwitterKlon.Tests.Users
{
    [TestClass]
    public class UserTesterDEFAULT
    {
        private UserLogic CreateTestUserLogic()
        {
            var users = Variables.CreateTestUserRepositoryDEFAULT();
            var tokens = Variables.CreateTestTokenRepositoryDEFAULT();
            var accessor = Variables.CreateTestContextAccessorDEFAULT();
            return new UserLogic(users.Object, tokens.Object, accessor.Object);
        }

        [TestMethod]
        public void TestAddUserDEFAULT()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<User> user = logic.AddUser(Variables.CreateTestUserEditor());
            Assert.AreEqual(Variables.CreateTestUser(), user.Data);
        }

        [TestMethod]
        public void TestDeleteUserDEFAULT()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult result = logic.DeleteUser(Variables.userId);
            Assert.AreEqual(LogicResultState.Ok, result.State);
        }

        [TestMethod]
        public void TestGetAllUsersDEFAULT()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<IEnumerable<User>> users = logic.GetAllUser();
            if (users.Data == null) Assert.Fail();
        }

        [TestMethod]
        public void TestGetUserDEFAULT()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<User> user = logic.GetUser(Variables.userId);
            Assert.AreEqual(Variables.CreateTestUserWithoutPassword(), user.Data);
        }

        [TestMethod]
        public void TestGetUserByUsernameDEFAULT()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<User> user = logic.GetUserByUsername(Variables.username);
            Assert.AreEqual(Variables.CreateTestUserWithoutPassword(), user.Data);
        }

        [TestMethod]
        public void TestUpdateUserDEFAULT()
        {
            UserLogic logic = CreateTestUserLogic();
            ILogicResult<User> user = logic.UpdateUser(Variables.userId, Variables.CreateTestUserEditor());
            Assert.AreEqual(Variables.CreateTestUser(), user.Data);
        }
    }
}
