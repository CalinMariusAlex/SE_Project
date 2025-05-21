using Microsoft.EntityFrameworkCore;
using SE_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace SE_Project.Data
{
   

    public class MyAppContext : IdentityDbContext

    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<FavoriteSong> FavoriteSongs { get; set; }


    }
}
