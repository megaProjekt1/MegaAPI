using MegaProject.Services;
using MegaProject.Services.Services;
using MegaProject.Services.Services.Interfaces;
using MegaProjekt.Core.DTO;
using MegaProjekt.Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Reflection.Metadata.Ecma335;
using System.Text;

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

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager,
            IMailService mailService, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _userService = userService;
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

            //Create User
            ApplicationUser user = new ApplicationUser()
            {
                //Email = registerDTO.Email,
                //PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email
                //PersonName = registerDTO.PersonName
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(user);
            }
            else
            {
                string errorMessage = string.Join(" | ",
                    result.Errors.Select(e => e.Description));

                return Problem(errorMessage);
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

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByNameAsync(loginDTO.Email);

                if (user == null)
                {
                    return Problem();
                }

                return Ok(new { email = user.UserName });
            }
            else
            {
                return Problem("Invalid email or passowrd");
            }
        }


        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();


            return NoContent();
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);

            if (result.IsSuccess)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }
    }
}
