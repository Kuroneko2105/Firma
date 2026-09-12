using Firma.Data.Data;
using Firma.Data.Data.Uzytkownicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers
{
    public class OpiniaKlientaController : Controller
    {
        private readonly FirmaContext _context;

        public OpiniaKlientaController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var opinie = await _context.OpiniaKlienta
                .Include(o => o.Towar)
                .OrderByDescending(o => o.DataDodania)
                .ToListAsync();

            return View(opinie);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var opinia = await _context.OpiniaKlienta
                .Include(o => o.Towar)
                .FirstOrDefaultAsync(o => o.IdOpinii == id);
            if (opinia == null) return NotFound();

            return View(opinia);
        }

        public IActionResult Create()
        {
            SetTowarySelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOpinii,Imie,Email,Tresc,Ocena,CzyZatwierdzona,DataDodania,TowarId")] OpiniaKlienta opinia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opinia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            SetTowarySelectList(opinia.TowarId);
            return View(opinia);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var opinia = await _context.OpiniaKlienta.FindAsync(id);
            if (opinia == null) return NotFound();

            SetTowarySelectList(opinia.TowarId);
            return View(opinia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOpinii,Imie,Email,Tresc,Ocena,CzyZatwierdzona,DataDodania,TowarId")] OpiniaKlienta opinia)
        {
            if (id != opinia.IdOpinii) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opinia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpiniaKlientaExists(opinia.IdOpinii)) return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            SetTowarySelectList(opinia.TowarId);
            return View(opinia);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var opinia = await _context.OpiniaKlienta
                .Include(o => o.Towar)
                .FirstOrDefaultAsync(o => o.IdOpinii == id);
            if (opinia == null) return NotFound();

            return View(opinia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opinia = await _context.OpiniaKlienta.FindAsync(id);
            if (opinia != null)
            {
                _context.OpiniaKlienta.Remove(opinia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpiniaKlientaExists(int id)
        {
            return _context.OpiniaKlienta.Any(o => o.IdOpinii == id);
        }

        private void SetTowarySelectList(int? selectedId = null)
        {
            ViewData["TowarId"] = new SelectList(_context.Towar.OrderBy(t => t.Nazwa), "IdTowaru", "Nazwa", selectedId);
        }
    }
}
