namespace TheDevBlog.API.Models.DTO
{
    public class AddPostRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Summary { get; set; }
        public string? UrlHandle { get; set; }
        public string? ImageUrl { get; set; }
        public bool Favourite { get; set; }
        public string? Category { get; set; }
    }
}
