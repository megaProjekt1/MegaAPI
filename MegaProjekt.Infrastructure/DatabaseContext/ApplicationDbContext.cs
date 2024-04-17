using MegaProjekt.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MegaProjekt.Core.DTO;


namespace MegaProjekt.Infrastructure.DatabaseContext
{

    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<PostDTO> PostDTOs { get; set; }

    }
}
