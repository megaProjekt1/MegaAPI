using System.ComponentModel.DataAnnotations;

namespace MegaProjekt.Models
{
    public class Users
    {
        [Key]
        public Guid UserID { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
