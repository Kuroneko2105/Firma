using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers
{
    public class AktualnoscController : Controller
    {
        private readonly FirmaContext _context;
        private readonly IWebHostEnvironment _environment;

        public AktualnoscController(FirmaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Aktualnosc
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aktualnosc.ToListAsync());
        }

        // GET: Aktualnosc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktualnosc = await _context.Aktualnosc
                .FirstOrDefaultAsync(m => m.IdAktualnosci == id);
            if (aktualnosc == null)
            {
                return NotFound();
            }

            return View(aktualnosc);
        }

        // GET: Aktualnosc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aktualnosc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAktualnosci,LinkTytul,Tytul,Tresc,Pozycja")] Aktualnosc aktualnosc, IFormFile? zdjeciePlik)
        {
            var nazwaPliku = await ZapiszZdjecieAsync(zdjeciePlik);
            if (!string.IsNullOrWhiteSpace(nazwaPliku))
            {
                aktualnosc.ZdjecieUrl = nazwaPliku;
            }

            if (ModelState.IsValid)
            {
                _context.Add(aktualnosc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aktualnosc);
        }

        // GET: Aktualnosc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktualnosc = await _context.Aktualnosc.FindAsync(id);
            if (aktualnosc == null)
            {
                return NotFound();
            }
            return View(aktualnosc);
        }

        // POST: Aktualnosc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAktualnosci,LinkTytul,Tytul,Tresc,Pozycja")] Aktualnosc aktualnosc, IFormFile? zdjeciePlik)
        {
            if (id != aktualnosc.IdAktualnosci)
            {
                return NotFound();
            }

            var istniejaceZdjecieUrl = await _context.Aktualnosc
                .AsNoTracking()
                .Where(a => a.IdAktualnosci == aktualnosc.IdAktualnosci)
                .Select(a => a.ZdjecieUrl)
                .FirstOrDefaultAsync();

            var nazwaPliku = await ZapiszZdjecieAsync(zdjeciePlik);
            aktualnosc.ZdjecieUrl = !string.IsNullOrWhiteSpace(nazwaPliku)
                ? nazwaPliku
                : istniejaceZdjecieUrl;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aktualnosc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktualnoscExists(aktualnosc.IdAktualnosci))
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
            return View(aktualnosc);
        }

        // GET: Aktualnosc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktualnosc = await _context.Aktualnosc
                .FirstOrDefaultAsync(m => m.IdAktualnosci == id);
            if (aktualnosc == null)
            {
                return NotFound();
            }

            return View(aktualnosc);
        }

        // POST: Aktualnosc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aktualnosc = await _context.Aktualnosc.FindAsync(id);
            if (aktualnosc != null)
            {
                _context.Aktualnosc.Remove(aktualnosc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AktualnoscExists(int id)
        {
            return _context.Aktualnosc.Any(e => e.IdAktualnosci == id);
        }

        private async Task<string?> ZapiszZdjecieAsync(IFormFile? plik)
        {
            if (plik == null || plik.Length == 0 || string.IsNullOrWhiteSpace(plik.FileName))
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
