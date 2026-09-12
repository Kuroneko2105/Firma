using Firma.Data.Data;
using Firma.Data.Data.Kontakt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class KontaktController : Controller
    {
        private readonly FirmaContext _context;

        public KontaktController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([Bind("Email,Wiadomosc")] Kontakt model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
                return View("Index", model);
            }

            _context.Kontakt.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
