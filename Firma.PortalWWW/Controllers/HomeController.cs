using Firma.Data.Data;
using Firma.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Firma.PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly FirmaContext _context;
        public HomeController(FirmaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id) //przy ...
        {
            //ViewBag.ModelStrony =
            //    (
            //        from strona in _context.Strona
            //        orderby strona.Pozycja
            //        select strona
            //     ).ToList();//do 
           ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
           //ViewBag.ModelAktualnosci=
           //     (
           //         from aktualnosc in _context.Aktualnosc
           //         orderby aktualnosc.Pozycja descending
           //         select aktualnosc
           //     ).Take(3).ToList(); //
            ViewBag.ModelAktualnosci = await _context.Aktualnosc.OrderByDescending(a => a.Pozycja)
                .Take(3).ToListAsync();
            ViewBag.ModelOgloszenia = await _context.Ogloszenie
                .Where(o => o.ZdjecieUrl != null && o.ZdjecieUrl != "")
                .OrderByDescending(o => o.IdOgloszenia)
                .Take(5)
                .ToListAsync();
            if (id == null) id = 1; //zatem przy pierwszym...
            var item=await _context.Strona.FindAsync(id);//z bazy danych, z tabli strona szukamy strony o danym id
            return View(item);//do widoku przekaz.....
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
