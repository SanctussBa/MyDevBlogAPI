namespace TheDevBlog.API.Models.Enities
{
    public class Comment
    {

        public Guid Id { get; set; } 
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime CommentCreatedAt { get; set; }
        public Guid PostId { get; set; }
    }
}
