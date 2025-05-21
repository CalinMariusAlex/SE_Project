using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SE_Project.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        [MaxLength(450)]
        public string? UserId { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<PlaylistSong>? PlaylistSongs { get; set; }

        public Playlist()
        {
            PlaylistSongs = new List<PlaylistSong>();
        }
    }
}

