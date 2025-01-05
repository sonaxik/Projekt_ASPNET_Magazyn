using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test.Areas.Identity.Data;
using Test.Models;

namespace Test.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ZamowieniaController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ZamowieniaController(ApplicationDBContext context)
        {
            _context = context;
        }

        // Wyświetlanie listy zamówień
        public async Task<IActionResult> Index()
        {
            var zamowienia = await _context.Zamowienia
                .Include(z => z.Produkt)      // Dołączenie danych produktu (Asortyment)
                .Include(z => z.User)         // Dołączenie danych użytkownika (AspNetUsers)
                .ToListAsync();

            // Przekazanie danych do widoku
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

        // POST: Zamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zamowienie = await _context.Zamowienia.FindAsync(id);
            if (zamowienie != null)
            {
                _context.Zamowienia.Remove(zamowienie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index)); // Możesz przekierować na listę zamówień
        }

    }
}