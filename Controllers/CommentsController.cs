using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Data;
using TheDevBlog.API.Models.DTO;
using TheDevBlog.API.Models.Enities;

namespace TheDevBlog.API.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly TheDevBlogDbContext dbContext;
        public CommentsController(TheDevBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return NotFound(($"Comment with id '{id}' was not found"));  
            }
            return Ok(comment);
        }


        [HttpPost]
        public async Task<IActionResult> PostComment(AddCommentRequest addCommentRequest)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == addCommentRequest.PostId);
            if (post == null)
            {
                return NotFound("Post for this comment was not found");
            }
            var comment = new Comment()
            {
                Id = Guid.NewGuid(),
                Author= addCommentRequest.Author,
                Content= addCommentRequest.Content,
                CommentCreatedAt= DateTime.UtcNow,
                PostId=addCommentRequest.PostId
           
            };

            var log = new Log()
            {
                Id = Guid.NewGuid(),
                LogText = "New Comment was added",
                PostId = addCommentRequest.PostId
            };

            await dbContext.Comments.AddAsync(comment);
            await dbContext.Logs.AddAsync(log);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

    }
}
