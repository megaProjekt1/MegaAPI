using MegaProjekt.Core.DTO;
using MegaProjekt.Core.Identity;
using MegaProjekt.Core.Services.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MegaProjekt.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/")]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private IMailService _mailService;
        private IUserService _userService;
        private readonly IJwtService _jwtService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager,
            IMailService mailService, IUserService userService, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
        {
            //Validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e =>
                    e.ErrorMessage));
                return Problem(errorMessage);
            }

            var registerResult = await _userService.RegisterAsync(registerDTO);

            if (registerResult.IsSuccess)
            {
                return Ok(registerResult.AuthenticationResponse);
            }
            else
            {
                return Problem(registerResult.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> PostLogin(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e =>
                    e.ErrorMessage));
                return Problem(errorMessage);
            }

            var loginResult = await _userService.LoginAsync(loginDTO);

            if (loginResult.IsSuccess)
            {
                return Ok(loginResult.AuthenticationResponse);
            }
            else
            {
                return Problem(loginResult.Message);
            }
        }


        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpPost("remind-password")]
        public async Task<IActionResult> ForgetPassword(RemindPasswordDTO remindPassword)
        {
            if (string.IsNullOrEmpty(remindPassword.Email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(remindPassword.Email);

            if (result.IsSuccess)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var result = await _userService.ResetPassword(resetPasswordDTO);

            if (!result)
            {
                return StatusCode(500, "nie dziala");
            }

            return Ok();
        }
    }
}
