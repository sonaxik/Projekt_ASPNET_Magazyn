using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Areas.Identity.Data;
using Test.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class ZamowieniaController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ZamowieniaController(ApplicationDBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var zamowienia = await _context.Zamowienia
                .Include(z => z.Produkt)
                .Include(z => z.User)
                .ToListAsync();

            var model = zamowienia.Select(z => new
            {
                z.Id,
                ProduktName = z.Produkt.Name,
                UserEmail = z.User.Email,
                z.Quantity,
                TotalPrice = z.Quantity * z.Produkt.Price,
                z.DataZamowienia
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserIndex()
        {
            var userId = User.Identity.Name;
            var zamowienia = await _context.Zamowienia
                .Include(z => z.Produkt)
                .Where(z => z.User.UserName == userId)
                .ToListAsync();

            var model = zamowienia.Select(z => new
            {
                z.Id,
                ProduktName = z.Produkt.Name,
                z.Quantity,
                TotalPrice = z.Quantity * z.Produkt.Price,
                z.DataZamowienia
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienia
                .Include(z => z.Produkt)
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zamowienie = await _context.Zamowienia
                .Include(z => z.Produkt)
                .FirstOrDefaultAsync(z => z.Id == id);
            if (zamowienie != null)
            {
                var produkt = zamowienie.Produkt;
                produkt.Quantity += zamowienie.Quantity;

                _context.Zamowienia.Remove(zamowienie);
                await _context.SaveChangesAsync();
            }

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                return RedirectToAction(nameof(UserIndex));
            }
        }
    }
}
