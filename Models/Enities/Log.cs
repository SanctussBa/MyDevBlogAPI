namespace TheDevBlog.API.Models.Enities
{
    public class Log
    {
        public Guid Id { get; set; }
        public string? LogText { get; set; }
        public DateTime LoggedAt { get; set; }
        public Guid PostId { get; set; }
    }
}
