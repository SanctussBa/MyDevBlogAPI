namespace TheDevBlog.API.Models.Enities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Summary { get; set; }
        public string? UrlHandle { get; set; }
        public string? ImageUrl { get; set; }
        public bool Favourite { get; set; }
        public string? Category { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
