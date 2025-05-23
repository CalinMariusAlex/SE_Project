using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SE_Project.Data;
using SE_Project.Models;

namespace SE_Project.Controllers
{
   
    public class SongController : Controller
    {
        private readonly MyAppContext _context;

        public SongController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Song song)
        {
            if (ModelState.IsValid)
            {
                song.CreatedAt = DateTime.Now;
                song.PlayCount = 0;

                _context.Songs.Add(song);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(song);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var song = _context.Songs.Find(id);
            if (song == null)
                return NotFound();

            return View(song);
        }

        [HttpPost]
        public IActionResult Edit(Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Songs.Update(song);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(song);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult IncrementPlayCount([FromBody] PlayCountRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Request null");

                var song = _context.Songs.FirstOrDefault(s => s.Id == request.SongId);

                if (song == null)
                    return NotFound("Song not found");

                song.PlayCount++;
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        //[HttpGet]
        //public IActionResult AddSongToGivenPlaylist(Playlist playlist)
        //{
        //    var songs = _context.Songs.ToList();
        //    return View(playlist);
        //}


    }
}
