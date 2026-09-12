using System.Text.Json;
using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Firma.Data.Data.Uzytkownicy;
using Firma.PortalWWW.Models;
using Firma.PortalWWW.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class KoszykController : Controller
    {
        private const string KoszykSessionKey = "Koszyk";
        private const string KoszykIloscSessionKey = "KoszykIlosc";
        private readonly FirmaContext _context;

        public KoszykController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            return View(PobierzKoszyk());
        }

        public async Task<IActionResult> Zamowienie()
        {
            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            var koszyk = PobierzKoszyk();

            if (!koszyk.Any())
            {
                TempData["KoszykKomunikat"] = "Koszyk jest pusty. Dodaj produkt przed złożeniem zamówienia.";
                return RedirectToAction(nameof(Index));
            }

            return View(new ZamowienieKoszykViewModel
            {
                Koszyk = koszyk
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Zamowienie(ZamowienieKoszykViewModel model)
        {
            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            var koszyk = PobierzKoszyk();
            model.Koszyk = koszyk;

            if (!koszyk.Any())
            {
                ModelState.AddModelError(string.Empty, "Koszyk jest pusty.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var towaryIds = koszyk.Select(p => p.IdTowaru).ToList();
            var towary = await _context.Towar
                .Where(t => towaryIds.Contains(t.IdTowaru))
                .ToDictionaryAsync(t => t.IdTowaru);

            if (towary.Count != koszyk.Count)
            {
                ModelState.AddModelError(string.Empty, "Niektóre produkty z koszyka nie są już dostępne.");
                return View(model);
            }

            var pozycje = koszyk.Select(item =>
            {
                var towar = towary[item.IdTowaru];
                return new PozycjaZamowienia
                {
                    TowarId = towar.IdTowaru,
                    Ilosc = item.Ilosc,
                    Cena = towar.Cena
                };
            }).ToList();

            var suma = pozycje.Sum(p => p.Cena * p.Ilosc);
            var zamowienie = new Zamowienie
            {
                Imie = model.Imie,
                Nazwisko = model.Nazwisko,
                Email = model.Email,
                Miejscowosc = model.Miejscowosc,
                Ulica = model.Ulica,
                NumerBudynku = model.NumerBudynku,
                KodPocztowy = model.KodPocztowy,
                Suma = suma,
                Status = "Nowe",
                DataZamowienia = DateTime.Now,
                Pozycje = pozycje,
                Platnosci = new List<Platnosc>
                {
                    new Platnosc
                    {
                        Kwota = suma,
                        Metoda = model.MetodaPlatnosci,
                        Status = "Oczekuje",
                        DataPlatnosci = DateTime.Now
                    }
                }
            };

            _context.Zamowienie.Add(zamowienie);
            await _context.SaveChangesAsync();

            WyczyscKoszyk();

            return RedirectToAction(nameof(Potwierdzenie), new { id = zamowienie.IdZamowienie });
        }

        public async Task<IActionResult> Potwierdzenie(int id)
        {
            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();

            var zamowienie = await _context.Zamowienie
                .Include(z => z.Pozycje)
                    .ThenInclude(p => p.Towar)
                .Include(z => z.Platnosci)
                .FirstOrDefaultAsync(z => z.IdZamowienie == id);

            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }
        //generowanie faktury PDF
        public async Task<IActionResult> Faktura(int id)
        {
            //pobieramy dane z bazy
            var zamowienie = await _context.Zamowienie
                .Include(z => z.Pozycje)
                    .ThenInclude(p => p.Towar)
                .Include(z => z.Platnosci)
                .FirstOrDefaultAsync(z => z.IdZamowienie == id);
            //zabezpieczenie - sprawdzamy czy zamówienie istnieje
            if (zamowienie == null)
            {
                return NotFound();
            }
            //generujemy PDF w serwisie FakturaPdfGenerator i zwracamy go jako plik do pobrania
            var pdf = FakturaPdfGenerator.Generuj(zamowienie);
            //generujemy unikalną nazwę pliku
            var nazwaPliku = $"faktura-{zamowienie.IdZamowienie}.pdf";
            return File(pdf, "application/pdf", nazwaPliku);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajOpiniePoZakupie(int zamowienieId, [Bind("Imie,Email,Tresc,Ocena,TowarId")] OpiniaKlienta opinia)
        {
            var zamowienie = await _context.Zamowienie
                .Include(z => z.Pozycje)
                .FirstOrDefaultAsync(z => z.IdZamowienie == zamowienieId);

            if (zamowienie == null)
            {
                return NotFound();
            }

            if (!zamowienie.Pozycje.Any(p => p.TowarId == opinia.TowarId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                TempData["OpiniaBlad"] = "Nie udało się zapisać opinii. Sprawdź wymagane pola.";
                return RedirectToAction(nameof(Potwierdzenie), new { id = zamowienieId });
            }

            opinia.CzyZatwierdzona = false;
            opinia.DataDodania = DateTime.UtcNow;
            _context.OpiniaKlienta.Add(opinia);
            await _context.SaveChangesAsync();

            TempData["OpiniaDodana"] = "Dziękujemy za opinię. Zostanie wyświetlona po zatwierdzeniu.";
            return RedirectToAction(nameof(Potwierdzenie), new { id = zamowienieId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dodaj(int id)
        {
            var towar = await _context.Towar.FirstOrDefaultAsync(t => t.IdTowaru == id);
            if (towar == null)
            {
                return NotFound();
            }

            var koszyk = PobierzKoszyk();
            var pozycja = koszyk.FirstOrDefault(p => p.IdTowaru == id);
            if (pozycja == null)
            {
                koszyk.Add(new KoszykItem
                {
                    IdTowaru = towar.IdTowaru,
                    Nazwa = towar.Nazwa,
                    Cena = towar.Cena,
                    FotoUrl = towar.FotoUrl,
                    Ilosc = 1
                });
            }
            else
            {
                pozycja.Ilosc++;
            }

            ZapiszKoszyk(koszyk);
            TempData["KoszykKomunikat"] = $"Dodano do koszyka: {towar.Nazwa}";

            if (Request.Headers.Referer.Count > 0)
            {
                return Redirect(Request.Headers.Referer.ToString());
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Usun(int id)
        {
            var koszyk = PobierzKoszyk();
            var pozycja = koszyk.FirstOrDefault(p => p.IdTowaru == id);

            if (pozycja != null)
            {
                koszyk.Remove(pozycja);
                ZapiszKoszyk(koszyk);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Wyczysc()
        {
            WyczyscKoszyk();

            return RedirectToAction(nameof(Index));
        }

        private List<KoszykItem> PobierzKoszyk()
        {
            var koszykJson = HttpContext.Session.GetString(KoszykSessionKey);

            if (string.IsNullOrEmpty(koszykJson))
            {
                return new List<KoszykItem>();
            }

            return JsonSerializer.Deserialize<List<KoszykItem>>(koszykJson) ?? new List<KoszykItem>();
        }

        private void ZapiszKoszyk(List<KoszykItem> koszyk)
        {
            HttpContext.Session.SetString(KoszykSessionKey, JsonSerializer.Serialize(koszyk));
            HttpContext.Session.SetInt32(KoszykIloscSessionKey, koszyk.Sum(p => p.Ilosc));
        }

        private void WyczyscKoszyk()
        {
            HttpContext.Session.Remove(KoszykSessionKey);
            HttpContext.Session.SetInt32(KoszykIloscSessionKey, 0);
        }
    }
}
