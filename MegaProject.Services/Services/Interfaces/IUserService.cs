using MegaProjekt.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaProject.Services.Services.Interfaces
{
    public interface IUserService
    {
        //Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        //Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

        //Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);

        //Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);

    }
}
