using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Data;

namespace TheDevBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly TheDevBlogDbContext dbContext;
        public LogsController(TheDevBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await dbContext.Logs.ToListAsync();
            return Ok(logs);
        }
    }
}
