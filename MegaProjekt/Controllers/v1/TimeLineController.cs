using MegaProjekt.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MegaProjekt.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MegaProjekt.WebAPI.Controllers.v1
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/profile")]
    public class TimeLineController : CustomControllerBase
    {
        private ApplicationDbContext _dbcontext;
        public TimeLineController(ApplicationDbContext dbContext) 
        {
            _dbcontext = dbContext;
        }

        //do przekminy jak to zrobimy, na razie baza wszytskich funkcjonalności gotowa
        [HttpPost("add")]
        public async Task<IActionResult> AddPost([FromBody] PostDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nowyPost = new PostDTO
            {
                Photo = request.Photo,
                Description = request.Description,
                DataDodania = DateTime.Now
            };

            _dbcontext.PostDTOs.Add(nowyPost);
            await _dbcontext.SaveChangesAsync();

            return Ok(nowyPost); 
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditPost(int id, [FromBody] PostDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _dbcontext.PostDTOs.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            post.Photo = request.Photo;
            post.Description = request.Description;

            _dbcontext.Update(post);
            await _dbcontext.SaveChangesAsync();

            return Ok(post);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _dbcontext.PostDTOs.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            _dbcontext.PostDTOs.Remove(post);
            await _dbcontext.SaveChangesAsync();

            return Ok(post);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _dbcontext.PostDTOs.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // GET api/v1/timeLine
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _dbcontext.PostDTOs.ToListAsync();

            return Ok(posts);
        }

    }
}

