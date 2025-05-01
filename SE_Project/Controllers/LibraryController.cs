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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlists = _context.Playlists.Where(p => p.UserId == userId).ToList();

            return View(playlists);
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
                playlist.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                playlist.DateCreated = DateTime.Now;
                playlist.LastModified = DateTime.Now;

                _context.Playlists.Add(playlist);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        // Other CRUD operations for playlists
    }
}
