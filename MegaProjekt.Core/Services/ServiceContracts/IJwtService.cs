using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaProjekt.Core.DTO;
using MegaProjekt.Core.Identity;

namespace MegaProjekt.Core.Services.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}
