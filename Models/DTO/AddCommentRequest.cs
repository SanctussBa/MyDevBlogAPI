namespace TheDevBlog.API.Models.DTO
{
    public class AddCommentRequest
    {
        public string? Author { get; set; }
        public string? Content { get; set; }
        public Guid PostId { get; set; }
    }
}
