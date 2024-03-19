using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MegaProjekt.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
