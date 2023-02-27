using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Models.Enities;

namespace TheDevBlog.API.Data
{
    public class TheDevBlogDbContext : DbContext
    {
        public TheDevBlogDbContext(DbContextOptions options) : base(options)
        {
        }

        //dbset
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
