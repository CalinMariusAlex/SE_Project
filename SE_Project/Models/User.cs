using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SE_Project.Models
{
    public enum Role
    {
        User,
        Creator,
        Admin
    }

    public class User
    {
        public int Id { get; set; }


        [MaxLength(255)]
        public string? FirstName { get; set; }


        [MaxLength(255)]
        public string? LastName { get; set; }


        public string? Password { get; set; }


        [EmailAddress]
        public string? Email { get; set; }


        public DateTime? BirthDate { get; set; }


        [Column(TypeName = "varchar(20)")]
        public Role? Role { get; set; }

        public ICollection<Playlist> Playlists { get; set; }
        public User()
        {
            Playlists = new List<Playlist>();
        }
    }

}