using Firma.Intranet.Models;
using Firma.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Firma.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly FirmaContext _context;

        public HomeController(FirmaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeStatsViewModel
            {
                StronyCount = _context.Strona.Count(),
                AktualnosciCount = _context.Aktualnosc.Count(),
                TowaryCount = _context.Towar.Count(),
                RodzajeCount = _context.Rodzaj.Count(),
                OgloszeniaCount = _context.Ogloszenie.Count()
            };
            return View(model);
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
