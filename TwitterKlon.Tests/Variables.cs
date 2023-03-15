using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using TwitterKlon.Contract.Persistence.Categories;
using TwitterKlon.Contract.Persistence.Comments;
using TwitterKlon.Contract.Persistence.Posts;
using TwitterKlon.Contract.Persistence.Sessions;
using TwitterKlon.Contract.Persistence.Users;
using TwitterKlon.Logic.Categories.DTOs;
using TwitterKlon.Logic.Comments.DTOs;
using TwitterKlon.Logic.Posts.DTOs;
using TwitterKlon.Logic.Sessions.DTOs;
using TwitterKlon.Logic.Users.DTOs;
using TwitterKlon.Security;
using TwitterKlon.Security.Authentication;

namespace TwitterKlon.Tests
{
    class Variables
    {
        public static readonly DateTime time = DateTime.Now;
        public static readonly string refreshTokenId = "test-refresh-token";
        public static readonly string accessTokenId = "test-access-token";
        public static readonly string userId = "test-user-id";
        public static readonly string firstName = "test-first-name";
        public static readonly string lastName = "test-last-name";
        public static readonly string address = "test-address";
        public static readonly string username = "test-username";
        public static readonly string password = "test-password";
        public static readonly string postTitle = "test-post-title";
        public static readonly string postMessage = "test-post-message";
        public static readonly string postId = "test-post-id";
        public static readonly string categoryId = "test-category-id";
        public static readonly string categoryName = "test-category-name";
        public static readonly string categoryTags = "test-category-tags";
        public static readonly string commentId = "test-comment-id";
        public static readonly string commentText = "test-comment-text";

        public static RefreshToken CreateTestRefreshToken() => new RefreshToken { ExpirationDate = time, Id = refreshTokenId, UserId = userId };
        public static AccessToken CreateTestAccessToken() => new AccessToken { SessionKey = accessTokenId, Time = time, Token = refreshTokenId };
        public static User CreateTestUser() => new User { FirstName = firstName, LastName = lastName, Id = userId, Address = address, Username = username, Password = password };
        public static User CreateTestUserWithoutPassword() => new User { FirstName = firstName, LastName = lastName, Id = userId, Address = address, Username = username, Password = null };
        public static UserEditor CreateTestUserEditor() => new UserEditor { FirstName = firstName, LastName = lastName, Address = address, Username = username, Password = password };
        public static UserLogin CreateTestUserLogin() => new UserLogin { Username = username, Password = password };
        public static IHeaderDictionary CreateTestHeaders() => new HeaderDictionary(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues> { { "Authorization", accessTokenId } });
        public static Post CreateTestPost() => new Post { CategoryId = categoryId, Id = postId, Message = postMessage, SenderId = userId, Title = postTitle };
        public static PostEditor CreateTestPostEditor() => new PostEditor { CategoryId = categoryId, Message = postMessage, Title = postTitle };
        public static Comment CreateTestComment() => new Comment { Id = commentId, PostId = postId, SenderId = userId, Text = commentText };
        public static CommentEditor CreateTestCommentEditor() => new CommentEditor { PostId = postId, SenderId = userId, Text = commentText };
        public static Category CreateTestCategory() => new Category { Id = categoryId, Name = categoryName, Tags = categoryTags };
        public static CategoryUpdate CreateTestCategoryUpdate() => new CategoryUpdate { Name = categoryName, Tags = categoryTags };

        public static Mock<ITokenRepository> CreateTestTokenRepositoryDEFAULT()
        {
            var mock = new Mock<ITokenRepository>(MockBehavior.Strict);
            mock.Setup(tokens => tokens.Login(CreateTestUserLogin(), It.IsAny<IUserRepository>())).Returns(CreateTestRefreshToken());
            mock.Setup(tokens => tokens.Validate(accessTokenId)).Returns(true);
            mock.Setup(tokens => tokens.ValidateRefreshToken(refreshTokenId)).Returns(true);
            mock.Setup(tokens => tokens.Register(CreateTestUserEditor(), It.IsAny<IUserRepository>())).Returns(CreateTestRefreshToken());
            mock.Setup(tokens => tokens.GetAccessTokens(refreshTokenId)).Returns(new AccessToken[] { CreateTestAccessToken() });
            mock.Setup(tokens => tokens.CreateAccessToken(refreshTokenId)).Returns(CreateTestAccessToken());
            mock.Setup(tokens => tokens.GetRefreshToken(refreshTokenId)).Returns(CreateTestRefreshToken());
            mock.Setup(tokens => tokens.GetAccessToken(accessTokenId)).Returns(CreateTestAccessToken());
            mock.Setup(tokens => tokens.Logout()).Verifiable();
            return mock;
        }
        public static Mock<IUserRepository> CreateTestUserRepositoryDEFAULT()
        {
            var mock = new Mock<IUserRepository>(MockBehavior.Strict);
            mock.Setup(users => users.AddUser(CreateTestUserEditor())).Returns(CreateTestUser());
            mock.Setup(users => users.CanEdit(userId)).Returns(true);
            mock.Setup(users => users.DeleteUser(userId)).Returns(true);
            mock.Setup(users => users.GetAllUser()).Returns(new List<User> { CreateTestUser() });
            mock.Setup(users => users.GetUser(userId)).Returns(CreateTestUser());
            mock.Setup(users => users.GetUserByUsername(username)).Returns(CreateTestUser());
            mock.Setup(users => users.UpdateUser(userId, CreateTestUserEditor())).Returns(CreateTestUser());
            return mock;
        }
        public static Mock<IPostRepository> CreateTestPostRepositoryDEFAULT()
        {
            var mock = new Mock<IPostRepository>(MockBehavior.Strict);
            mock.Setup(posts => posts.AddPost(CreateTestPostEditor())).Returns(CreateTestPost());
            mock.Setup(posts => posts.CanEdit(postId)).Returns(true);
            mock.Setup(posts => posts.DeletePost(postId)).Returns(true);
            mock.Setup(posts => posts.EditPost(postId, CreateTestPostEditor())).Returns(CreateTestPost());
            mock.Setup(posts => posts.GetAllPosts()).Returns(new List<Post> { CreateTestPost() });
            mock.Setup(posts => posts.GetPost(postId)).Returns(CreateTestPost());
            return mock;
        }
        public static Mock<ICommentRepository> CreateTestCommentRepositoryDEFAULT()
        {
            var mock = new Mock<ICommentRepository>(MockBehavior.Strict);
            mock.Setup(comments => comments.AddComment(CreateTestCommentEditor())).Returns(CreateTestComment());
            mock.Setup(comments => comments.CanEdit(commentId)).Returns(true);
            mock.Setup(comments => comments.DeleteComment(commentId)).Returns(true);
            mock.Setup(comments => comments.EditComment(commentId, CreateTestCommentEditor())).Returns(CreateTestComment());
            mock.Setup(comments => comments.GetCommentByID(commentId)).Returns(CreateTestComment());
            mock.Setup(comments => comments.GetComments(postId)).Returns(new List<Comment> { CreateTestComment() });
            return mock;
        }
        public static Mock<ICategoryRepository> CreateTestCategoryRepositoryDEFAULT() {
            var mock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            mock.Setup(categories => categories.AddCategory(CreateTestCategoryUpdate())).Returns(CreateTestCategory());
            mock.Setup(categories => categories.EditCategory(categoryId, CreateTestCategoryUpdate())).Returns(CreateTestCategory());
            mock.Setup(categories => categories.DeleteCategory(categoryId)).Returns(true);
            mock.Setup(categories => categories.GetCategory(categoryId)).Returns(CreateTestCategory());
            mock.Setup(categories => categories.GetAllCategories()).Returns(new List<Category> { CreateTestCategory() });
            return mock;
        }
        public static Mock<IHttpContextAccessor> CreateTestContextAccessorDEFAULT()
        {
            var mock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
            mock.Setup(accessor => accessor.HttpContext.Request.Headers).Returns(CreateTestHeaders());
            mock.Setup(accessor => accessor.HttpContext.Response.Cookies.Append("refresh_token", refreshTokenId, It.IsAny<CookieOptions>())).Verifiable();
            mock.Setup(accessor => accessor.HttpContext.Request.Cookies["refresh_token"]).Returns(refreshTokenId);
            mock.Setup(accessor => accessor.HttpContext.Response.Cookies.Delete("refresh_token")).Verifiable();
            return mock;
        }
        public static Mock<ISessionContext> CreateTestSessionContextDEFAULT()
        {
            var mock = new Mock<ISessionContext>(MockBehavior.Strict);
            mock.Setup(context => context.IsAuthenticated).Returns(true);
            mock.Setup(context => context.AccessTokenId).Returns(accessTokenId);
            mock.Setup(context => context.RefreshTokenId).Returns(refreshTokenId);
            mock.Setup(context => context.UserId).Returns(userId);
            return mock;
        }

        public static Mock<ITokenRepository> CreateTestTokenRepositoryERROR()
        {
            var mock = new Mock<ITokenRepository>(MockBehavior.Strict);
            mock.Setup(tokens => tokens.Login(null, It.IsAny<IUserRepository>())).Returns((RefreshToken)null);
            mock.Setup(tokens => tokens.Validate(accessTokenId)).Returns(false);
            mock.Setup(tokens => tokens.ValidateRefreshToken(refreshTokenId)).Returns(false);
            mock.Setup(tokens => tokens.Register(null, It.IsAny<IUserRepository>())).Returns((RefreshToken)null);
            mock.Setup(tokens => tokens.GetAccessTokens(null)).Returns(new AccessToken[] {});
            mock.Setup(tokens => tokens.CreateAccessToken(null)).Returns(CreateTestAccessToken());
            mock.Setup(tokens => tokens.GetRefreshToken(refreshTokenId)).Returns((RefreshToken)null);
            mock.Setup(tokens => tokens.GetAccessToken(accessTokenId)).Returns((AccessToken)null);
            mock.Setup(tokens => tokens.Logout()).Verifiable();
            return mock;
        }
        public static Mock<IUserRepository> CreateTestUserRepositoryERROR()
        {
            var mock = new Mock<IUserRepository>(MockBehavior.Strict);
            mock.Setup(users => users.AddUser(null)).Returns((User)null);
            mock.Setup(users => users.CanEdit(null)).Returns(false);
            mock.Setup(users => users.DeleteUser(null)).Returns(false);
            mock.Setup(users => users.GetAllUser()).Returns(new List<User> {});
            mock.Setup(users => users.GetUser(null)).Returns((User)null);
            mock.Setup(users => users.GetUserByUsername(null)).Returns((User)null);
            mock.Setup(users => users.UpdateUser(null, null)).Returns((User)null);
            return mock;
        }
        public static Mock<IPostRepository> CreateTestPostRepositoryERROR()
        {
            var mock = new Mock<IPostRepository>(MockBehavior.Strict);
            mock.Setup(posts => posts.AddPost(null)).Returns((Post)null);
            mock.Setup(posts => posts.CanEdit(null)).Returns(false);
            mock.Setup(posts => posts.DeletePost(null)).Returns(false);
            mock.Setup(posts => posts.EditPost(null, CreateTestPostEditor())).Returns((Post)null);
            mock.Setup(posts => posts.GetAllPosts()).Returns(new List<Post> {});
            mock.Setup(posts => posts.GetPost(null)).Returns((Post)null);
            return mock;
        }
        public static Mock<ICommentRepository> CreateTestCommentRepositoryERROR()
        {
            var mock = new Mock<ICommentRepository>(MockBehavior.Strict);
            mock.Setup(comments => comments.AddComment(null)).Returns((Comment)null);
            mock.Setup(comments => comments.CanEdit(null)).Returns(false);
            mock.Setup(comments => comments.DeleteComment(null)).Returns(false);
            mock.Setup(comments => comments.EditComment(null, CreateTestCommentEditor())).Returns((Comment)null);
            mock.Setup(comments => comments.GetCommentByID(null)).Returns((Comment)null);
            mock.Setup(comments => comments.GetComments(null)).Returns(new List<Comment> {});
            return mock;
        }
        public static Mock<ICategoryRepository> CreateTestCategoryRepositoryERROR() {
            var mock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            mock.Setup(categories => categories.AddCategory(null)).Returns((Category) null);
            mock.Setup(categories => categories.EditCategory(null, null)).Returns((Category) null);
            mock.Setup(categories => categories.DeleteCategory(null)).Returns(false);
            mock.Setup(categories => categories.GetCategory(null)).Returns((Category) null);
            mock.Setup(categories => categories.GetAllCategories()).Returns(new List<Category> {});
            return mock;
        }
        public static Mock<IHttpContextAccessor> CreateTestContextAccessorERROR()
        {
            var mock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
            mock.Setup(accessor => accessor.HttpContext.Request.Headers["Authorization"]).Returns((string)null);
            mock.Setup(accessor => accessor.HttpContext.Response.Cookies.Append("refresh_token", refreshTokenId, It.IsAny<CookieOptions>())).Verifiable();
            mock.Setup(accessor => accessor.HttpContext.Request.Cookies["refresh_token"]).Returns((string)null);
            mock.Setup(accessor => accessor.HttpContext.Response.Cookies.Delete("refresh_token")).Verifiable();
            return mock;
        }
    }
}
