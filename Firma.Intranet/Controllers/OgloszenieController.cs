using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
using Firma.Data.Data.CMS;

namespace Firma.Intranet.Controllers
{
    public class OgloszenieController : Controller
    {
        private readonly FirmaContext _context;

        public OgloszenieController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Ogloszenie
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogloszenie.ToListAsync());
        }

        // GET: Ogloszenie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie
                .FirstOrDefaultAsync(m => m.IdOgloszenia == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }

            return View(ogloszenie);
        }

        // GET: Ogloszenie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ogloszenie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOgloszenia,LinkTytul,Tytul,Tresc")] Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogloszenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ogloszenie);
        }

        // GET: Ogloszenie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie.FindAsync(id);
            if (ogloszenie == null)
            {
                return NotFound();
            }
            return View(ogloszenie);
        }

        // POST: Ogloszenie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOgloszenia,LinkTytul,Tytul,Tresc")] Ogloszenie ogloszenie)
        {
            if (id != ogloszenie.IdOgloszenia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogloszenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgloszenieExists(ogloszenie.IdOgloszenia))
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
            return View(ogloszenie);
        }

        // GET: Ogloszenie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie
                .FirstOrDefaultAsync(m => m.IdOgloszenia == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }

            return View(ogloszenie);
        }

        // POST: Ogloszenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ogloszenie = await _context.Ogloszenie.FindAsync(id);
            if (ogloszenie != null)
            {
                _context.Ogloszenie.Remove(ogloszenie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgloszenieExists(int id)
        {
            return _context.Ogloszenie.Any(e => e.IdOgloszenia == id);
        }
    }
}
