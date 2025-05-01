using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SE_Project.Models;

namespace SE_Project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreatorPage()
    {
        if(!IsAdmin())
        {
            return RedirectToAction("Privacy"); // Return to Privacy
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
