using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TheDevBlog.API.Data;
using TheDevBlog.API.Models.DTO;
using TheDevBlog.API.Models.Enities;

namespace TheDevBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly TheDevBlogDbContext dbContext;
        public PostsController(TheDevBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await dbContext.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post !=null)
            {
                return Ok(post);
            }
            return NotFound($"Post with id '{id}' was not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Category = addPostRequest.Category,
                ImageUrl = addPostRequest.ImageUrl,
                PublishDate = DateTime.UtcNow,
                Summary = addPostRequest.Summary,
                UrlHandle = addPostRequest.UrlHandle,
                Favourite = addPostRequest.Favourite

            };

            var log = new Log()
            {
                Id= Guid.NewGuid(),
                LogText="New Post was created",
                PostId=post.Id
            };

            await dbContext.Posts.AddAsync(post);
            await dbContext.Logs.AddAsync(log);

            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {

            var existingPost = await dbContext.Posts.FindAsync(id);
            if (existingPost != null)
            {
                existingPost.Title= updatePostRequest.Title;
                existingPost.Content= updatePostRequest.Content;
                existingPost.Category = updatePostRequest.Category;
                existingPost.ImageUrl= updatePostRequest.ImageUrl;
                existingPost.UpdatedDate = DateTime.UtcNow;
                existingPost.Summary= updatePostRequest.Summary;
                existingPost.UrlHandle= updatePostRequest.UrlHandle;
                existingPost.Favourite= updatePostRequest.Favourite;

                var log = new Log()
                {
                    Id = Guid.NewGuid(),
                    LogText = "Post was updated",
                    PostId = existingPost.Id
                };

                await dbContext.Logs.AddAsync(log);
                await dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound($"Post with id '{id}' was not found");
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingPost = await dbContext.Posts.FindAsync(id);
            if (existingPost != null )
            {
                dbContext.Posts.Remove(existingPost);
                await dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound($"Post with id '{id}' was not found");
        }
    }
}
