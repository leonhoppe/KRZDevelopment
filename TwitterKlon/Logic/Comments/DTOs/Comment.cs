#nullable disable

using System;

namespace TwitterKlon.Logic.Comments.DTOs
{
    public partial class Comment : CommentEditor
    {
        public string Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Comment) return false;
            Comment other = (Comment)obj;
            if (other.Id != Id) return false;
            if (other.PostId != PostId) return false;
            if (other.SenderId != SenderId) return false;
            if (other.Text != Text) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Id, PostId, SenderId, Text);
    }

    public class CommentEditor
    {
        public string PostId { get; set; }
        public string SenderId { get; set; }
        public string Text { get; set; }

        public void EditComment(Comment comment)
        {
            comment.PostId = PostId;
            comment.SenderId = SenderId;
            comment.Text = Text;
        }

        public override bool Equals(object obj)
        {
            if (obj is not CommentEditor) return false;
            CommentEditor other = (CommentEditor)obj;
            if (other.PostId != PostId) return false;
            if (other.SenderId != SenderId) return false;
            if (other.Text != Text) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(PostId, SenderId, Text);
    }
}
