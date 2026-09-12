using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class RodzajeMenuComponent : ViewComponent
    {
        private readonly FirmaContext _context;

        public RodzajeMenuComponent(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RodzajeMenu", await _context.Rodzaj.ToListAsync());
        }
    }
}
