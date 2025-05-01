using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE_Project.Models;
using SE_Project.Data;

namespace SE_Project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyAppContext _context;

    public HomeController(ILogger<HomeController> logger, MyAppContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["Layout"] = "_LayoutHome";
        var viewModel = new HomeViewModel
        {
            Playlists = _context.Playlists.Where(p => p.IsPublic).Take(6).ToList(),
            PopularPlaylists = _context.Playlists.Where(p => p.IsPublic)
                .OrderByDescending(p => p.PlaylistSongs.Count).Take(6).ToList()
        };

        return View(viewModel);
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
}
