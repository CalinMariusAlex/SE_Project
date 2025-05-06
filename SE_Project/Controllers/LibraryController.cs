using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            if (int.TryParse(userIdString, out int userId))
            {
                // Now, you can compare the int userId with the Playlist's UserId (which is of type int)
                var playlists = _context.Playlists.Where(p => p.UserId == userId).ToList();
                return View(playlists);
            }

            // If parsing fails, return some error (or redirect)
            return RedirectToAction("Error", "Home"); // Or handle appropriately
        }

        public IActionResult CreatePlaylist()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePlaylist(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                // Convert the logged-in user's ID to int when creating the playlist
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdString, out int userId))
                {
                    playlist.UserId = userId;
                    playlist.DateCreated = DateTime.Now;
                    playlist.LastModified = DateTime.Now;

                    _context.Playlists.Add(playlist);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }

                // If parsing fails, return some error (or handle appropriately)
                return RedirectToAction("Error", "Home"); // Or handle appropriately
            }
            return View(playlist);
        }

        // Other CRUD operations for playlists
    }
}
