using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterKlon.Logic.Sessions;
using TwitterKlon.Logic.Sessions.DTOs;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Logic;

namespace TwitterKlon.Tests.Tokens
{
    [TestClass]
    public class TokenTesterERROR
    {
        private TokenLogic CreateTestTokenLogic()
        {
            var tokens = Variables.CreateTestTokenRepositoryERROR();
            var users = Variables.CreateTestUserRepositoryERROR();
            var accessor = Variables.CreateTestContextAccessorERROR();
            var context = Variables.CreateTestSessionContextDEFAULT();
            return new TokenLogic(accessor.Object, tokens.Object, users.Object, context.Object);
        }

        [TestMethod]
        public void TestRegisterERROR()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult register = logic.Register(null);
            Assert.AreEqual(LogicResultState.Conflict, register.State);
        }

        [TestMethod]
        public void TestLoginERROR()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<bool> login = logic.Login(null);
            Assert.AreEqual(false, login.Data);
        }

        [TestMethod]
        public void TestGetOwnUserERROR()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<User> ownUser = logic.GetOwnUser();
            Assert.AreEqual(null, ownUser.Data);
        }

        [TestMethod]
        public void TestGetTokenERROR()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<AccessToken> token = logic.GetToken();
            Assert.AreEqual(LogicResultState.Forbidden, token.State);
        }

        [TestMethod]
        public void TestHasPermissionERROR()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<bool> permission = logic.HasPermission(null);
            Assert.AreEqual(false, permission.Data);
        }

        [TestMethod]
        public void TestLogoutERROR()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult logout = logic.Logout();
            Assert.AreEqual(LogicResultState.Ok, logout.State);
        }

        [TestMethod]
        public void TestValidateERROR()
        {
            var logic = CreateTestTokenLogic();
            ILogicResult<bool> valid = logic.Validate();
            Assert.AreEqual(false, valid.Data);
        }
    }
}
