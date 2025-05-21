using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SE_Project.Data;
using SE_Project.Models;

namespace SE_Project.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly MyAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FavoriteController(MyAppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Toggle([FromBody] int songId)
        {
            var userId = _userManager.GetUserId(User);

            // Verificăm dacă melodia există în baza de date
            var songExists = _context.Songs.Any(s => s.Id == songId);
            if (!songExists)
                return NotFound("Melodia nu există.");

            var existing = _context.FavoriteSongs
                .FirstOrDefault(f => f.UserId == userId && f.SongId == songId);

            if (existing != null)
            {
                _context.FavoriteSongs.Remove(existing);
            }
            else
            {
                _context.FavoriteSongs.Add(new FavoriteSong { SongId = songId, UserId = userId });
            }

            _context.SaveChanges();
            return Ok();
        }


        public IActionResult MyFavorites()
        {
            var userId = _userManager.GetUserId(User);
            var favs = _context.FavoriteSongs
                .Where(f => f.UserId == userId)
                .Select(f => f.Song)
                .ToList();

            return View(favs);
        }

        [HttpPost]
        public IActionResult IsFavorite([FromBody] int songId)
        {
            var userId = _userManager.GetUserId(User);
            var isFav = _context.FavoriteSongs.Any(f => f.SongId == songId && f.UserId == userId);
            return Json(isFav);
        }

    }
}
