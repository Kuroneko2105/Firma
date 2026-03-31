using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    //to jest glówny kontroler sklepu, bedzie on wystawiał dane do wszytkich widokow sklepu 
    public class SklepController : Controller
    {
        private readonly FirmaContext _context;
        public SklepController(FirmaContext context)
        {
            _context = context;
        }
        //ta funkcja ma wystawiac dane do widoku Index, który wyswietla wszstkie towary danego rodzaju
        public async Task<IActionResult> Index(int? id)//id to id rodzaju, ktorego towary mamy wyswietlic, przy pierwszym wejsciu do sklepu to id jest null
        {
            //na....
            ViewBag.Rodzaje = await _context.Rodzaj.ToListAsync();
            if (id == null) id = 1; //przy pierwszym wejsciu do sklepu maja się pojawic towary pierwszego rodzaju (PD: promowane)
            ViewBag.SelectedRodzaj = id;
            //z bazy danych pobieram towary danego rodzaju o danym kliknietym id
            var items=await _context.Towar.Where(t=>t.IdRodzaju==id).ToListAsync();
            //po pobraniu przekzuje te towary do widoku 
            return View(items);
        }
        //to jest 
        //
        public async Task<IActionResult> Szczegoly(int id)
        {
            //szczegoly towaru beda zgodne z layout sklep, zatem potrzeba rodzajow 
            ViewBag.Rodzaje = await _context.Rodzaj.ToListAsync();
            ViewBag.SelectedRodzaj = null;
            //z bazy danych pobieram towar o danym id
            var item=await _context.Towar.Where(t=>t.IdTowaru==id).FirstOrDefaultAsync();
            //ten towar przekazuje do view
            return View(item);
            
        }
    }
}
