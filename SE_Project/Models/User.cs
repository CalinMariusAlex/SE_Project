using System.ComponentModel.DataAnnotations;

namespace SE_Project.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public  string Name { get; set; }

        [MaxLength(255)]
        public  string Email { get; set; }
    }
}
