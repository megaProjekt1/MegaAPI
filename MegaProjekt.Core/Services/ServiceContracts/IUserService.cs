using MegaProjekt.Core.Entities;
using MegaProjekt.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaProjekt.Core.Services.ServiceContracts
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterAsync(RegisterDTO registerDTO);

        Task<UserManagerResponse> LoginAsync(LoginDTO loginDTO);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);
        Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO);

        //Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);

    }
}
