using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MegaProjekt.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDateTime { get; set; }
    }
}
