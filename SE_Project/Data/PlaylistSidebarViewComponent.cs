using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SE_Project.Data
{
    public class PlaylistSidebarViewComponent : ViewComponent
    {
        private readonly MyAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlaylistSidebarViewComponent(MyAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlists = _context.Playlists
                .Where(p => p.UserId == userId)
                .ToList();

            return View(playlists);
        }
    }

}
