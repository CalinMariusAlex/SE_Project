using Microsoft.AspNetCore.Mvc;
using SE_Project.Data;
using SE_Project.Models;
using System;
using Microsoft.AspNetCore.Http;


namespace SE_Project.Controllers
{
    public class AuthController : Controller
    {

        private readonly MyAppContext _context;

        public AuthController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(User user, string password)
        {
            if (ModelState.IsValid)
            {
                user.Password = password;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // optionally auto-login:
                //HttpContext.Session.SetInt32("UserId", user.Id);
                //HttpContext.Session.SetString("UserRole", user.Role.ToString());

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user != null && user.Password == password)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }



    }
}
