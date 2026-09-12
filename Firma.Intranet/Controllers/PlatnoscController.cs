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
    public class PlatnoscController : Controller
    {
        private readonly FirmaContext _context;

        public PlatnoscController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Platnosc
        public async Task<IActionResult> Index(int? zamowienieId)
        {
            var firmaContext = _context.Platnosc
                .Include(p => p.Zamowienie)
                .AsQueryable();

            if (zamowienieId != null)
            {
                firmaContext = firmaContext.Where(p => p.ZamowienieId == zamowienieId);
                ViewBag.ZamowienieId = zamowienieId;
            }

            return View(await firmaContext.ToListAsync());
        }

        // GET: Platnosc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platnosc = await _context.Platnosc
                .Include(p => p.Zamowienie)
                .FirstOrDefaultAsync(m => m.IdPlatnosc == id);
            if (platnosc == null)
            {
                return NotFound();
            }

            return View(platnosc);
        }

        // GET: Platnosc/Create
        public IActionResult Create(int? zamowienieId)
        {
            ViewData["ZamowienieId"] = new SelectList(_context.Zamowienie, "IdZamowienie", "Email", zamowienieId);
            ViewBag.WybraneZamowienieId = zamowienieId;
            return View();
        }

        // POST: Platnosc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlatnosc,Kwota,Metoda,Status,DataPlatnosci,ZamowienieId")] Platnosc platnosc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platnosc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { zamowienieId = platnosc.ZamowienieId });
            }
            ViewData["ZamowienieId"] = new SelectList(_context.Zamowienie, "IdZamowienie", "Email", platnosc.ZamowienieId);
            ViewBag.WybraneZamowienieId = platnosc.ZamowienieId;
            return View(platnosc);
        }

        // GET: Platnosc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platnosc = await _context.Platnosc.FindAsync(id);
            if (platnosc == null)
            {
                return NotFound();
            }
            ViewData["ZamowienieId"] = new SelectList(_context.Zamowienie, "IdZamowienie", "Email", platnosc.ZamowienieId);
            return View(platnosc);
        }

        // POST: Platnosc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlatnosc,Kwota,Metoda,Status,DataPlatnosci,ZamowienieId")] Platnosc platnosc)
        {
            if (id != platnosc.IdPlatnosc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platnosc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatnoscExists(platnosc.IdPlatnosc))
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
            ViewData["ZamowienieId"] = new SelectList(_context.Zamowienie, "IdZamowienie", "Email", platnosc.ZamowienieId);
            return View(platnosc);
        }

        // GET: Platnosc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platnosc = await _context.Platnosc
                .Include(p => p.Zamowienie)
                .FirstOrDefaultAsync(m => m.IdPlatnosc == id);
            if (platnosc == null)
            {
                return NotFound();
            }

            return View(platnosc);
        }

        // POST: Platnosc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platnosc = await _context.Platnosc.FindAsync(id);
            if (platnosc != null)
            {
                _context.Platnosc.Remove(platnosc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatnoscExists(int id)
        {
            return _context.Platnosc.Any(e => e.IdPlatnosc == id);
        }
    }
}
