using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
using Firma.Data.Data.Sklep;

namespace Firma.Intranet.Controllers
{
    public class PozycjaZamowieniaController : Controller
    {
        private readonly FirmaContext _context;

        public PozycjaZamowieniaController(FirmaContext context)
        {
            _context = context;
        }

        // GET: PozycjaZamowienia
        public async Task<IActionResult> Index(int? zamowienieId)
        {
            var firmaContext = _context.PozycjaZamowienia
                .Include(p => p.Towar)
                .Include(p => p.Zamowienie)
                .AsQueryable();

            if (zamowienieId != null)
            {
                firmaContext = firmaContext.Where(p => p.ZamowienieId == zamowienieId);
                ViewBag.ZamowienieId = zamowienieId;
            }

            return View(await firmaContext.ToListAsync());
        }

        // GET: PozycjaZamowienia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozycjaZamowienia = await _context.PozycjaZamowienia
                .Include(p => p.Towar)
                .Include(p => p.Zamowienie)
                .FirstOrDefaultAsync(m => m.IdPozycja == id);
            if (pozycjaZamowienia == null)
            {
                return NotFound();
            }

            return View(pozycjaZamowienia);
        }

        // GET: PozycjaZamowienia/Create
        public IActionResult Create(int? zamowienieId)
        {
            ViewData["TowarId"] = new SelectList(_context.Towar, "IdTowaru", "Nazwa");
            ViewData["ZamowienieId"] = new SelectList(_context.Zamowienie, "IdZamowienie", "Email", zamowienieId);
            ViewBag.WybraneZamowienieId = zamowienieId;
            return View();
        }

        // POST: PozycjaZamowienia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPozycja,TowarId,Ilosc,Cena,ZamowienieId")] PozycjaZamowienia pozycjaZamowienia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pozycjaZamowienia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { zamowienieId = pozycjaZamowienia.ZamowienieId });
            }
            ViewData["TowarId"] = new SelectList(_context.Towar, "IdTowaru", "Nazwa", pozycjaZamowienia.TowarId);
            ViewData["ZamowienieId"] = new SelectList(_context.Zamowienie, "IdZamowienie", "Email", pozycjaZamowienia.ZamowienieId);
            ViewBag.WybraneZamowienieId = pozycjaZamowienia.ZamowienieId;
            return View(pozycjaZamowienia);
        }

        // GET: PozycjaZamowienia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozycjaZamowienia = await _context.PozycjaZamowienia.FindAsync(id);
            if (pozycjaZamowienia == null)
            {
                return NotFound();
            }
            ViewData["TowarId"] = new SelectList(_context.Towar, "IdTowaru", "Nazwa", pozycjaZamowienia.TowarId);
            ViewData["ZamowienieId"] = new SelectList(_context.Zamowienie, "IdZamowienie", "Email", pozycjaZamowienia.ZamowienieId);
            return View(pozycjaZamowienia);
        }

        // POST: PozycjaZamowienia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPozycja,TowarId,Ilosc,Cena,ZamowienieId")] PozycjaZamowienia pozycjaZamowienia)
        {
            if (id != pozycjaZamowienia.IdPozycja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pozycjaZamowienia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PozycjaZamowieniaExists(pozycjaZamowienia.IdPozycja))
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
            ViewData["TowarId"] = new SelectList(_context.Towar, "IdTowaru", "Nazwa", pozycjaZamowienia.TowarId);
            ViewData["ZamowienieId"] = new SelectList(_context.Zamowienie, "IdZamowienie", "Email", pozycjaZamowienia.ZamowienieId);
            return View(pozycjaZamowienia);
        }

        // GET: PozycjaZamowienia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozycjaZamowienia = await _context.PozycjaZamowienia
                .Include(p => p.Towar)
                .Include(p => p.Zamowienie)
                .FirstOrDefaultAsync(m => m.IdPozycja == id);
            if (pozycjaZamowienia == null)
            {
                return NotFound();
            }

            return View(pozycjaZamowienia);
        }

        // POST: PozycjaZamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pozycjaZamowienia = await _context.PozycjaZamowienia.FindAsync(id);
            if (pozycjaZamowienia != null)
            {
                _context.PozycjaZamowienia.Remove(pozycjaZamowienia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PozycjaZamowieniaExists(int id)
        {
            return _context.PozycjaZamowienia.Any(e => e.IdPozycja == id);
        }
    }
}
