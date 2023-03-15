using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterKlon.Logic.Sessions;
using TwitterKlon.Logic.Sessions.DTOs;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Logic;

namespace TwitterKlon.Tests.Tokens
{
    [TestClass]
    public class TokenTesterDEFAULT
    {
        private TokenLogic CreateTestTokenLogic()
        {
            var tokens = Variables.CreateTestTokenRepositoryDEFAULT();
            var users = Variables.CreateTestUserRepositoryDEFAULT();
            var accessor = Variables.CreateTestContextAccessorDEFAULT();
            var context = Variables.CreateTestSessionContextDEFAULT();
            return new TokenLogic(accessor.Object, tokens.Object, users.Object, context.Object);
        }

        [TestMethod]
        public void TestRegisterDEFALUT()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult register = logic.Register(Variables.CreateTestUserEditor());
            Assert.AreEqual(LogicResultState.Ok, register.State);
        }

        [TestMethod]
        public void TestLoginDEFAULT()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<bool> login = logic.Login(Variables.CreateTestUserLogin());
            Assert.AreEqual(LogicResultState.Ok, login.State);
            Assert.AreEqual(true, login.Data);
        }

        [TestMethod]
        public void TestGetOwnUserDEFAULT()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<User> ownUser = logic.GetOwnUser();
            Assert.AreEqual(Variables.CreateTestUser(), ownUser.Data);
        }

        [TestMethod]
        public void TestGetTokenDEFAULT()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<AccessToken> token = logic.GetToken();
            Assert.AreEqual(Variables.CreateTestAccessToken(), token.Data);
        }

        [TestMethod]
        public void TestHasPermissionDEFAULT()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<bool> permission = logic.HasPermission(Variables.userId);
            Assert.AreEqual(true, permission.Data);
        }

        [TestMethod]
        public void TestLogoutDEFAULT()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult logout = logic.Logout();
            Assert.AreEqual(LogicResultState.Ok, logout.State);
        }

        [TestMethod]
        public void TestValidateDEFAULT()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<bool> valid = logic.Validate();
            Assert.AreEqual(true, valid.Data);
        }
    }
}
