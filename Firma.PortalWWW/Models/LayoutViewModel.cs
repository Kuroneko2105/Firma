using Firma.Data.Data.CMS;

namespace Firma.PortalWWW.Models
{
    public class LayoutViewModel
    {
        public LayoutUstawienia Ustawienia { get; set; } = new();
        public IEnumerable<LayoutLink> Linki { get; set; } = Enumerable.Empty<LayoutLink>();
        public IEnumerable<Strona> Strony { get; set; } = Enumerable.Empty<Strona>();
    }
}
