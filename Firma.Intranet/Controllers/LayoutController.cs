using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Firma.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers
{
    public class LayoutController : Controller
    {
        private readonly FirmaContext _context;
        private readonly IWebHostEnvironment _environment; //katalog wwwroot

        public LayoutController(FirmaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var ustawienia = await PobierzLubUtworzUstawienia();

            var model = new LayoutPanelViewModel
            {
                Ustawienia = ustawienia,
                Linki = await _context.LayoutLink
                    .OrderBy(l => l.Pozycja)
                    .ThenBy(l => l.Tytul)
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUstawienia([Bind("IdLayoutUstawienia,LogoTekst,DomyslnyMotyw,KolorPrzyciskow,WielkoscCzcionki,KolorCzcionki,OpisStopki,Telefon,Email,Adres,Copyright,TekstKoncowy")] LayoutUstawienia ustawienia, IFormFile? logoPlik)
        {
            var istniejaceLogoUrl = await _context.LayoutUstawienia
                .AsNoTracking()
                .Where(s => s.IdLayoutUstawienia == ustawienia.IdLayoutUstawienia)
                .Select(s => s.LogoUrl)
                .FirstOrDefaultAsync();

            var nazwaLogo = await ZapiszLogoAsync(logoPlik);
            ustawienia.LogoUrl = !string.IsNullOrWhiteSpace(nazwaLogo)
                ? nazwaLogo
                : istniejaceLogoUrl;

            if (ustawienia.DomyslnyMotyw != "dark")
            {
                ustawienia.DomyslnyMotyw = "light";
            }

            var koloryPrzyciskow = new[] { "primary", "secondary", "success", "danger", "warning", "info", "dark" };
            if (!koloryPrzyciskow.Contains(ustawienia.KolorPrzyciskow))
            {
                ustawienia.KolorPrzyciskow = "primary";
            }

            var wielkosciCzcionki = new[] { "small", "normal", "large" };
            if (!wielkosciCzcionki.Contains(ustawienia.WielkoscCzcionki))
            {
                ustawienia.WielkoscCzcionki = "normal";
            }

            var koloryCzcionki = new[] { "body", "secondary", "primary", "success", "danger", "dark" };
            if (!koloryCzcionki.Contains(ustawienia.KolorCzcionki))
            {
                ustawienia.KolorCzcionki = "body";
            }

            if (!ModelState.IsValid)
            {
                var model = new LayoutPanelViewModel
                {
                    Ustawienia = ustawienia,
                    Linki = await _context.LayoutLink.OrderBy(l => l.Pozycja).ToListAsync()
                };

                return View("Index", model);
            }

            if (await _context.LayoutUstawienia.AnyAsync(s => s.IdLayoutUstawienia == ustawienia.IdLayoutUstawienia))
            {
                _context.Update(ustawienia);
            }
            else
            {
                _context.Add(ustawienia);
            }

            await _context.SaveChangesAsync();
            TempData["Komunikat"] = "Zapisano ustawienia layoutu.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateLink()
        {
            return View(new LayoutLink { Tytul = string.Empty, Url = string.Empty, CzyAktywny = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLink([Bind("IdLayoutLink,Tytul,Url,Pozycja,CzyAktywny")] LayoutLink link)
        {
            if (ModelState.IsValid)
            {
                _context.Add(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(link);
        }

        public async Task<IActionResult> EditLink(int? id)
        {
            if (id == null) return NotFound();

            var link = await _context.LayoutLink.FindAsync(id);
            if (link == null) return NotFound();

            return View(link);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLink(int id, [Bind("IdLayoutLink,Tytul,Url,Pozycja,CzyAktywny")] LayoutLink link)
        {
            if (id != link.IdLayoutLink) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(link);
            }

            _context.Update(link);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteLink(int? id)
        {
            if (id == null) return NotFound();

            var link = await _context.LayoutLink.FirstOrDefaultAsync(l => l.IdLayoutLink == id);
            if (link == null) return NotFound();

            return View(link);
        }

        [HttpPost, ActionName("DeleteLink")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLinkConfirmed(int id)
        {
            var link = await _context.LayoutLink.FindAsync(id);
            if (link != null)
            {
                _context.LayoutLink.Remove(link);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<LayoutUstawienia> PobierzLubUtworzUstawienia()
        {
            var ustawienia = await _context.LayoutUstawienia.FirstOrDefaultAsync();
            if (ustawienia != null)
            {
                return ustawienia;
            }

            ustawienia = new LayoutUstawienia();
            _context.LayoutUstawienia.Add(ustawienia);
            await _context.SaveChangesAsync();

            return ustawienia;
        }

        private async Task<string?> ZapiszLogoAsync(IFormFile? plik)
        {
            if (plik == null || plik.Length == 0 || string.IsNullOrWhiteSpace(plik.FileName)) //sprawdza czy plik jest przesłany, ma zawartość i nazwę
            {
                return null;
            }

            var dozwoloneRozszerzenia = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var rozszerzenie = Path.GetExtension(plik.FileName).ToLowerInvariant(); //pobiera rozszerzenie i zamienia na małe litery

            if (!dozwoloneRozszerzenia.Contains(rozszerzenie)) //sprawdza czy rozszerzenie jest dozwolone
            {
                ModelState.AddModelError("Ustawienia.LogoUrl", "Dozwolone formaty logo: JPG, PNG, WEBP, GIF.");
                return null;
            }

            if (plik.Length > 5 * 1024 * 1024) //sprawdza czy plik nie przekracza maksymalnego rozmiaru
            {
                ModelState.AddModelError("Ustawienia.LogoUrl", "Logo może mieć maksymalnie 5 MB.");
                return null;
            }

            var nazwaBazowa = Path.GetFileNameWithoutExtension(plik.FileName); //pobiera nazwę pliku bez rozszerzenia
            foreach (var znak in Path.GetInvalidFileNameChars()) //zamienia niedozwolone znaki w nazwie na myślniki
            {
                nazwaBazowa = nazwaBazowa.Replace(znak, '-');
            }

            nazwaBazowa = string.IsNullOrWhiteSpace(nazwaBazowa)
                ? "logo"
                : nazwaBazowa.Trim().Replace(' ', '-');
            if (nazwaBazowa.Length > 150) //ograniczenie do 150 znaków
            {
                nazwaBazowa = nazwaBazowa[..150];
            }

            var nazwaPliku = $"{nazwaBazowa}{rozszerzenie}";
            var katalogiDocelowe = PobierzKatalogiContent(); //pobiera katalogi docelowe, w których ma być zapisany plik

            foreach (var katalog in katalogiDocelowe)
            {
                Directory.CreateDirectory(katalog); //Tworzy katalog, jeśli jeszcze nie istnieje
                var sciezka = Path.Combine(katalog, nazwaPliku); //buduje pełną ścieżkę do pliku

                await using var stream = System.IO.File.Create(sciezka); //tworzy strumień do zapisu pliku
                await plik.CopyToAsync(stream); //kopiuje zawartość przesłanego pliku do strumienia, zapisuje go na dysku
            }

            return nazwaPliku;
        }

        private IEnumerable<string> PobierzKatalogiContent()
        {
            var katalogi = new List<string>
            {
                Path.Combine(_environment.WebRootPath, "content") //katalog wwwroot/content
            };

            var katalogRozwiazania = Directory.GetParent(_environment.ContentRootPath)?.FullName; //pobera folder nadrzędny projektu
            if (!string.IsNullOrWhiteSpace(katalogRozwiazania)) //czy udało się pobrać katalog
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
