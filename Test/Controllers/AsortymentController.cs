using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Areas.Identity.Data;
using Test.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Test.Controllers
{
    public class AsortymentController : Controller
    {
        private readonly ApplicationDBContext context;
        private readonly UserManager<IdentityUser> _userManager;
        public AsortymentController(ApplicationDBContext context, UserManager<IdentityUser> userManager) 
        {
            this.context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var asortyment = await context.Asortyment.ToListAsync();

            if (asortyment == null)
            {
                asortyment = new List<Asortyment>();
            }

            return View(asortyment);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserIndex()
        {
            var asortyment = await context.Asortyment.ToListAsync();

            if (asortyment == null)
            {
                asortyment = new List<Asortyment>();
            }

            return View(asortyment);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Asortyment asortyment)
        {
            if (ModelState.IsValid)
            {
                context.Asortyment.Add(asortyment);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asortyment);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asortyment = await context.Asortyment
                .FirstOrDefaultAsync(m => m.Id == id);

            if (asortyment == null)
            {
                return NotFound();
            }

            return View(asortyment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asortyment = await context.Asortyment.FindAsync(id);

            if (asortyment != null)
            {
                context.Asortyment.Remove(asortyment);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asortyment = await context.Asortyment.FindAsync(id);
            if (asortyment == null)
            {
                return NotFound();
            }

            return View(asortyment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Asortyment asortyment)
        {
            if (id != asortyment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(asortyment);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Asortyment.Any(e => e.Id == asortyment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(asortyment);
        }   

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Zamow(int id)
        {
            var produkt = await context.Asortyment.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Zamow(int id, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var produkt = await context.Asortyment.FindAsync(id);
            if (produkt == null || produkt.Quantity < quantity)
            {
                ModelState.AddModelError("", "Brak wystarczającej liczby produktów.");
                return RedirectToAction("UserIndex");
            }

            var zamowienie = new Zamowienia
            {
                UserId = user.Id,
                ProduktId = id,
                DataZamowienia = DateTime.Now,
                Quantity = quantity
            };

            context.Zamowienia.Add(zamowienie);
            produkt.Quantity -= quantity;
            await context.SaveChangesAsync();

            return RedirectToAction("UserIndex");
        }
    }
}
