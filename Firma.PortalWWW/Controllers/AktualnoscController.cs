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
        //to jest fukcja, ktora wystawia dane widkowi Index
        public async Task<IActionResult> Index(int id)//zaw...
            //w id siedzi id kliknietej aktualnosci 
        {
            //to poprawimy, bo mamy powtarzalnosc kodu 
            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            ViewBag.ModelAktualnosci = await _context.Aktualnosc.OrderByDescending(a => a.Pozycja)
                .Take(3).ToListAsync();
            var item = await _context.Aktualnosc.FirstOrDefaultAsync(a => a.IdAktualnosci == id);
            if (item == null) return NotFound();    
            return View(item);//do widoku przek....
        }
    }
}
