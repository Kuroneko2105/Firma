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
        private readonly IWebHostEnvironment _environment;

        public OgloszenieController(FirmaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
        public async Task<IActionResult> Create([Bind("IdOgloszenia,LinkTytul,Tytul,Tresc")] Ogloszenie ogloszenie, IFormFile? zdjeciePlik)
        {
            var nazwaPliku = await ZapiszZdjecieAsync(zdjeciePlik);
            if (!string.IsNullOrWhiteSpace(nazwaPliku))
            {
                ogloszenie.ZdjecieUrl = nazwaPliku;
            }

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
        public async Task<IActionResult> Edit(int id, [Bind("IdOgloszenia,LinkTytul,Tytul,Tresc")] Ogloszenie ogloszenie, IFormFile? zdjeciePlik)
        {
            if (id != ogloszenie.IdOgloszenia)
            {
                return NotFound();
            }

            var istniejaceZdjecieUrl = await _context.Ogloszenie
                .AsNoTracking()
                .Where(o => o.IdOgloszenia == ogloszenie.IdOgloszenia)
                .Select(o => o.ZdjecieUrl)
                .FirstOrDefaultAsync();

            var nazwaPliku = await ZapiszZdjecieAsync(zdjeciePlik);
            ogloszenie.ZdjecieUrl = !string.IsNullOrWhiteSpace(nazwaPliku)
                ? nazwaPliku
                : istniejaceZdjecieUrl;

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

        private async Task<string?> ZapiszZdjecieAsync(IFormFile? plik)
        {
            if (plik == null || plik.Length == 0)
            {
                return null;
            }

            var dozwoloneRozszerzenia = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var rozszerzenie = Path.GetExtension(plik.FileName).ToLowerInvariant();

            if (!dozwoloneRozszerzenia.Contains(rozszerzenie))
            {
                ModelState.AddModelError("ZdjecieUrl", "Dozwolone formaty zdjęcia: JPG, PNG, WEBP, GIF.");
                return null;
            }

            if (plik.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("ZdjecieUrl", "Zdjęcie może mieć maksymalnie 5 MB.");
                return null;
            }

            var nazwaBazowa = Path.GetFileNameWithoutExtension(plik.FileName);
            foreach (var znak in Path.GetInvalidFileNameChars())
            {
                nazwaBazowa = nazwaBazowa.Replace(znak, '-');
            }
            nazwaBazowa = string.IsNullOrWhiteSpace(nazwaBazowa)
                ? "zdjecie"
                : nazwaBazowa.Trim().Replace(' ', '-');
            if (nazwaBazowa.Length > 150)
            {
                nazwaBazowa = nazwaBazowa[..150];
            }

            var nazwaPliku = $"{nazwaBazowa}{rozszerzenie}";
            var katalogiDocelowe = PobierzKatalogiContent();

            foreach (var katalog in katalogiDocelowe)
            {
                Directory.CreateDirectory(katalog);
                var sciezka = Path.Combine(katalog, nazwaPliku);

                await using var stream = System.IO.File.Create(sciezka);
                await plik.CopyToAsync(stream);
            }

            return nazwaPliku;
        }

        private IEnumerable<string> PobierzKatalogiContent()
        {
            var katalogi = new List<string>
            {
                Path.Combine(_environment.WebRootPath, "content")
            };

            var katalogRozwiazania = Directory.GetParent(_environment.ContentRootPath)?.FullName;
            if (!string.IsNullOrWhiteSpace(katalogRozwiazania))
            {
                var portalContent = Path.Combine(katalogRozwiazania, "Firma.PortalWWW", "wwwroot", "content");
                if (!katalogi.Contains(portalContent))
                {
                    katalogi.Add(portalContent);
                }
            }

            return katalogi;
        }
    }
}
