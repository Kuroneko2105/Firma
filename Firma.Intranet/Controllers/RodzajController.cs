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
    public class RodzajController : Controller
    {
        private readonly FirmaContext _context;

        public RodzajController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Rodzaj
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rodzaj.ToListAsync());
        }

        // GET: Rodzaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaj
                .FirstOrDefaultAsync(m => m.IdRodzaju == id);
            if (rodzaj == null)
            {
                return NotFound();
            }

            return View(rodzaj);
        }

        // GET: Rodzaj/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rodzaj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRodzaju,Nazwa,Opis")] Rodzaj rodzaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rodzaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rodzaj);
        }

        // GET: Rodzaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaj.FindAsync(id);
            if (rodzaj == null)
            {
                return NotFound();
            }
            return View(rodzaj);
        }

        // POST: Rodzaj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRodzaju,Nazwa,Opis")] Rodzaj rodzaj)
        {
            if (id != rodzaj.IdRodzaju)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rodzaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RodzajExists(rodzaj.IdRodzaju))
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
            return View(rodzaj);
        }

        // GET: Rodzaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaj
                .FirstOrDefaultAsync(m => m.IdRodzaju == id);
            if (rodzaj == null)
            {
                return NotFound();
            }

            return View(rodzaj);
        }

        // POST: Rodzaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rodzaj = await _context.Rodzaj.FindAsync(id);
            if (rodzaj != null)
            {
                _context.Rodzaj.Remove(rodzaj);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RodzajExists(int id)
        {
            return _context.Rodzaj.Any(e => e.IdRodzaju == id);
        }
    }
}
