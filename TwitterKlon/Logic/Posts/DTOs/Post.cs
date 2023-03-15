using System;

#nullable disable

namespace TwitterKlon.Logic.Posts.DTOs
{
    public partial class Post : PostEditor
    {
        public string Id { get; set; }
        public string SenderId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Post) return false;
            Post other = (Post)obj;
            if (other.Id != Id) return false;
            if (other.SenderId != SenderId) return false;
            if (other.Title != Title) return false;
            if (other.Message != Message) return false;
            if (other.CategoryId != CategoryId) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Id, SenderId, Title, Message, CategoryId);
    }

    public class PostEditor
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string CategoryId { get; set; }

        public void EditPost(Post post)
        {
            post.Title = Title;
            post.Message = Message;
            post.CategoryId = CategoryId;
        }

        public override bool Equals(object obj)
        {
            if (obj is not PostEditor) return false;
            PostEditor other = (PostEditor)obj;
            if (other.Title != Title) return false;
            if (other.Message != Message) return false;
            if (other.CategoryId != CategoryId) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Title, Message, CategoryId);
    }
}
