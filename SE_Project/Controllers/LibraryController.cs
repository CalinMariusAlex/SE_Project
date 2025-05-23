using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE_Project.Data;
using SE_Project.Models;
using System.Security.Claims;

namespace SE_Project.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly MyAppContext _context;

        public LibraryController(MyAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get the logged-in user's ID from Claims as a string
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Convert userIdString (string) to an int
            
            // Now, you can compare the int userId with the Playlist's UserId (which is of type int)
            var playlists = _context.Playlists.Where(p => p.UserId == userIdString).ToList();
            return View(playlists);
            
        }

        public IActionResult CreatePlaylist()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePlaylist(Playlist playlist)
        {

            System.Diagnostics.Debug.WriteLine("CreatePlaylist POST called");
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            System.Diagnostics.Debug.WriteLine("UserIdString: " + userIdString + "Model is: "+ ModelState.IsValid);
            System.Diagnostics.Debug.WriteLine("Modelul face figuri: err count "+ ModelState.ErrorCount);
            
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("E ntered If");
                
               
                playlist.UserId = userIdString;
                playlist.PlaylistSongs = [];
                playlist.DateCreated = DateTime.Now;
                playlist.LastModified = DateTime.Now;

                _context.Playlists.Add(playlist);
                _context.SaveChanges();

                System.Diagnostics.Debug.WriteLine("Right Befroe redirect");
                return RedirectToAction("Index", "Home");
                
            }
            System.Diagnostics.Debug.WriteLine("Right Befroe nothing");
            return View(playlist);
        }

        // Other CRUD operations for playlists
        public async Task<IActionResult> Edit(int id)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id);
            return View(playlist);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Playlists.Update(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(playlist);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id);
            return View(playlist);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Index","Home");

        }


        public IActionResult AddSong(int id)
        {

            var songs = _context.Songs.ToList();
            var playlist = _context.Playlists.FirstOrDefault(p => p.Id == id);
            var viewModel = new AddSongToPlaylistViewModel
            {
                Songs = songs,
                Playlist = playlist
            };
            return View(viewModel);
            
        }

        [HttpPost]
        public IActionResult AddSong(int songId, int playlistId)
        {
            if(ModelState.IsValid)
            {
                var song = _context.Songs.Find(songId);
                var playlist = _context.Playlists.Find(playlistId);

                PlaylistSong Psong = new PlaylistSong();
                Psong.Song = song;
                Psong.Playlist = playlist;
                Psong.PlaylistId = playlist.Id;
                Psong.SongId = song.Id;
                Psong.Order = playlist.PlaylistSongs.ToList().Count;

                _context.PlaylistSongs.Add(Psong);
                _context.PlaylistSongs.Update(Psong);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Error", "Home");
        }
    }
}

public class AddSongToPlaylistViewModel
{
    public List<Song> Songs { get; set; }
    public Playlist Playlist { get; set; }

    // Optional: for dropdowns or UI choices
    public List<Playlist> AllPlaylists { get; set; }
}