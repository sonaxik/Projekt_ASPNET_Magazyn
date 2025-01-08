using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Areas.Identity.Data;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            int noweZamowienia = 0;

            if (User.IsInRole("Admin"))
            {
                var dzisiejszaData = DateTime.Today;
                noweZamowienia = _context.Zamowienia
                    .Where(z => z.DataZamowienia >= dzisiejszaData)
                    .Count();
            }

            ViewBag.NoweZamowienia = noweZamowienia;
            return View();
        }

        [Authorize]
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
}
