using Microsoft.AspNetCore.Mvc;
using SE_Project.Data;

namespace SE_Project.Controllers
{
    public class PlayerController : Controller
    {
        private readonly MyAppContext _context;

        public PlayerController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSongDetails(int id)
        {
            var song = _context.Songs.Find(id);
            return Json(song);
        }

        [HttpGet]
        public IActionResult GetPlaylistSongs(int playlistId)
        {
            var songs = _context.PlaylistSongs
                .Where(ps => ps.PlaylistId == playlistId)
                .OrderBy(ps => ps.Order)
                .Select(ps => ps.Song)
                .ToList();

            return Json(songs);
        }
    }
}
