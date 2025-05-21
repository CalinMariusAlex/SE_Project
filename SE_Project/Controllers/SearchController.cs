using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE_Project.Data;
using SE_Project.Models;
using System.Linq;

namespace SE_Project.Controllers
{
    public class SearchController : Controller
    {
        private readonly MyAppContext _context;

        public SearchController(MyAppContext context)
        {
            _context = context;
        }

        public IActionResult Results(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View(new List<Song>());
            }

            var results = _context.Songs
                .Where(s => EF.Functions.Like(s.Title, $"%{query}%") || EF.Functions.Like(s.Artist, $"%{query}%"))
                .ToList();

            return View(results);
        }
    }
}
