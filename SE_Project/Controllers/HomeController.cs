using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE_Project.Models;
using SE_Project.Data;
using Microsoft.AspNetCore.Identity;

namespace SE_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, MyAppContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var recentSongs = _context.Songs
                .OrderByDescending(s => s.CreatedAt)
                .Take(3)
                .ToList();

            var trendingSongs = _context.Songs
                .OrderByDescending(s => s.PlayCount)
                .Take(15)
                .ToList();

            ViewBag.RecentSongs = recentSongs;
            ViewBag.TrendingSongs = trendingSongs;

            // Favorite songs for logged-in user
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var favoriteIds = _context.FavoriteSongs
                    .Where(f => f.UserId == userId)
                    .Select(f => f.SongId)
                    .ToList();

                ViewBag.FavoriteIds = favoriteIds;
            }

            return View();
        }

        public IActionResult CreatorPage()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Privacy");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }
    }
}
