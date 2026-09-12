using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;

namespace Firma.Intranet.Controllers
{
    public class KontaktController : Controller
    {
        private readonly FirmaContext _context;

        public KontaktController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Intranet/Kontakt
        public async Task<IActionResult> Index()
        {
            var items = await _context.Kontakt.OrderByDescending(k => k.DataDodania).ToListAsync();
            return View(items);
        }

        // GET: Intranet/Kontakt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var kontakt = await _context.Kontakt.FirstOrDefaultAsync(m => m.Id == id.Value);
            if (kontakt == null) return NotFound();

            return View(kontakt);
        }
    }
}
