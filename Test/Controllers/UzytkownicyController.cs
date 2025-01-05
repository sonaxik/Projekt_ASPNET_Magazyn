using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Areas.Identity.Data;
using Test.Models;

namespace Test.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UzytkownicyController : Controller
    {
        private readonly ApplicationDBContext _context;

        public UzytkownicyController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usersWithOrders = await _context.Users
                .Select(user => new
                {
                    user.UserName,
                    OrderCount = _context.Zamowienia.Count(z => z.User.Id == user.Id)
                })
                .ToListAsync();

            return View(usersWithOrders);
        }
    }
}