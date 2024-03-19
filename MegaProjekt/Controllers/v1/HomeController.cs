using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MegaProjekt.WebAPI.Controllers.v1
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("api/home")]
        [HttpPost]
        public IActionResult Kupa()
        {
            return Ok("kocham cyce");
        }

        [Route("api/home")]
        [HttpGet]
        public IActionResult Fjut()
        {
            return Ok("kocham cyce get");
        }
    }
}
