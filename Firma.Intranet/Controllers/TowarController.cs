using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Firma.Intranet.Controllers
{
    public class TowarController : Controller
    {
        private readonly FirmaContext _context;

        public TowarController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Towar
        public async Task<IActionResult> Index()
        {
            var firmaContext = _context.Towar.Include(t => t.Rodzaj);
            return View(await firmaContext.ToListAsync());
        }

        // GET: Towar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var towar = await _context.Towar
                .Include(t => t.Rodzaj)
                .FirstOrDefaultAsync(m => m.IdTowaru == id);
            if (towar == null)
            {
                return NotFound();
            }

            return View(towar);
        }

        // GET: Towar/Create
        public IActionResult Create()
        {
            ViewData["IdRodzaju"] = new SelectList(_context.Rodzaj, "IdRodzaju", "Nazwa");
            return View();
        }

        // POST: Towar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTowaru,Kod,Nazwa,Cena,FotoUrl,Opis,IdRodzaju")] Towar towar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(towar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRodzaju"] = new SelectList(_context.Rodzaj, "IdRodzaju", "Nazwa", towar.IdRodzaju);
            return View(towar);
        }

        // GET: Towar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var towar = await _context.Towar.FindAsync(id);
            if (towar == null)
            {
                return NotFound();
            }
            ViewData["IdRodzaju"] = new SelectList(_context.Rodzaj, "IdRodzaju", "Nazwa", towar.IdRodzaju);
            return View(towar);
        }

        // POST: Towar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTowaru,Kod,Nazwa,Cena,FotoUrl,Opis,IdRodzaju")] Towar towar)
        {
            if (id != towar.IdTowaru)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(towar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TowarExists(towar.IdTowaru))
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
            ViewData["IdRodzaju"] = new SelectList(_context.Rodzaj, "IdRodzaju", "Nazwa", towar.IdRodzaju);
            return View(towar);
        }

        // GET: Towar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var towar = await _context.Towar
                .Include(t => t.Rodzaj)
                .FirstOrDefaultAsync(m => m.IdTowaru == id);
            if (towar == null)
            {
                return NotFound();
            }

            return View(towar);
        }

        // POST: Towar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var towar = await _context.Towar.FindAsync(id);
            if (towar != null)
            {
                _context.Towar.Remove(towar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TowarExists(int id)
        {
            return _context.Towar.Any(e => e.IdTowaru == id);
        }
    }
}
