using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MegaProjekt.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //public string? PersonName { get; set; }
    }
}
