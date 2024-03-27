using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaProjekt.Core.DTO
{
    public class ResetPasswordDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}
