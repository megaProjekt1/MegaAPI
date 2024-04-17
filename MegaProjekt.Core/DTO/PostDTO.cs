using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MegaProjekt.Core.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public DateTime DataDodania { get ; set; } 

    }
}
