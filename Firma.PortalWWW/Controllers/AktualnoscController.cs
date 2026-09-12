using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class AktualnoscController : Controller
    {
        private readonly FirmaContext _context;
        public AktualnoscController(FirmaContext context)
        {
            _context = context;
        }
        // Wyświetla listę aktualności
        public async Task<IActionResult> Index()
        {
            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            var items = await _context.Aktualnosc.OrderByDescending(a => a.Pozycja).ToListAsync();
            // Explicitly render the List view (Index.cshtml expects a single Aktualnosc)
            return View("List", items);
        }

        // Wyświetla szczegóły pojedynczej aktualności
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            ViewBag.ModelAktualnosci = await _context.Aktualnosc.OrderByDescending(a => a.Pozycja)
                .Take(3).ToListAsync();
            var item = await _context.Aktualnosc.FirstOrDefaultAsync(a => a.IdAktualnosci == id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}
