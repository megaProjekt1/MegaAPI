using MegaProjekt.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MegaProjekt.Core.DTO;
using Microsoft.AspNetCore.Authorization;

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

        //do przekminy jak to zrobimy
        [HttpPost("addPost")]
        public async Task<IActionResult> DodajPost([FromForm] PostDTO request)
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
    }
}

