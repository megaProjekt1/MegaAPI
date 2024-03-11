using MegaProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaProjekt.DatabaseContext
{

    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().HasData(new Users() 
            { 
                UserID = Guid.Parse("37B44801-D7C8-4CC4-ADA2-30EE90104694"), 
                Login = "Test", 
                Password = "Test" 
            });
        }
    }
}
