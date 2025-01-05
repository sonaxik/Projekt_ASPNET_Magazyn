using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Areas.Identity.Data;
using Test.Models;

namespace Test.Controllers
{
    public class AsortymentController : Controller
    {
        private readonly ApplicationDBContext context;
        public AsortymentController(ApplicationDBContext context) 
        {
            this.context = context;
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
        public async Task<IActionResult> User()
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
    }
}
