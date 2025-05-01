using System.ComponentModel.DataAnnotations;

namespace SE_Project.Models
{
    public class User
    {
        public string Id { get; set; }

        [MaxLength(255)]
        public  string Name { get; set; }

        [MaxLength(255)]
        public  string Email { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
