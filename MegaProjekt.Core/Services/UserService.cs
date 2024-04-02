using MegaProjekt.Core.Services.ServiceContracts;
using MegaProjekt.Core.Entities;
using MegaProjekt.Core.DTO;
using MegaProjekt.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Security.Policy;
using System.Text;

namespace MegaProjekt.Core.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _configuration;
        private IMailService _mailService;
        private readonly IJwtService _jwtService;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IMailService mailService, IJwtService jwtService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _mailService = mailService;
            _jwtService = jwtService;
        }
        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(code);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var link = $"{_configuration["AppUrl"]}/change-password?email={user.UserName}&activationToken={validToken}";
            var emailContent = "Your email content that contains the above link";

            await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{link}'>Click here</a></p>");

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {  


            var user = await _userManager.FindByNameAsync(resetPasswordDTO.Email);

            var decodedToken = WebEncoders.Base64UrlDecode(resetPasswordDTO.ResetPasswordToken);
            var validToken = Encoding.UTF8.GetString(decodedToken, 0, decodedToken.Length);


            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, validToken, resetPasswordDTO.Password);

                if (result.Succeeded)
                {
                    var emailContent = $"Your password is reset now";

                    await _mailService.SendEmailAsync(user.UserName, "Password Reset Complete", emailContent);

                    return true;
                }
            }
            return false;
        }

        public async Task<UserManagerResponse> LoginAsync(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginDTO.Email);

                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        IsSuccess = false,
                        Message = "Something went wrong"
                    };
                }

                var authenticationResponse = _jwtService.CreateJwtToken(user);

                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Logged successfully",
                    AuthenticationResponse = authenticationResponse
                };
            }
            else
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                };
            
            }
        }

        public async Task<UserManagerResponse> RegisterAsync(RegisterDTO registerDTO)
        {

            var user = new ApplicationUser
            {
                UserName = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                var authenticationResponse = _jwtService.CreateJwtToken(user);

                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "User created successfully",
                    AuthenticationResponse = authenticationResponse
                };
            }
            else
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = string.Join(" | ", result.Errors.Select(e => e.Description))
                };
            }
        }
    }
}
