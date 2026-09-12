using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Firma.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class LayoutViewComponent : ViewComponent
    {
        private readonly FirmaContext _context;

        public LayoutViewComponent(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ustawienia = await _context.LayoutUstawienia.FirstOrDefaultAsync()
                ?? new LayoutUstawienia();

            var model = new LayoutViewModel
            {
                Ustawienia = ustawienia,
                Linki = await _context.LayoutLink
                    .Where(l => l.CzyAktywny)
                    .OrderBy(l => l.Pozycja)
                    .ThenBy(l => l.Tytul)
                    .ToListAsync(),
                Strony = await _context.Strona
                    .OrderBy(s => s.Pozycja)
                    .ToListAsync()
            };

            return View(model);
        }
    }
}
