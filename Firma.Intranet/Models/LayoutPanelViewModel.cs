using Firma.Data.Data.CMS;

namespace Firma.Intranet.Models
{
    public class LayoutPanelViewModel
    {
        public LayoutUstawienia Ustawienia { get; set; } = new();
        public IEnumerable<LayoutLink> Linki { get; set; } = Enumerable.Empty<LayoutLink>();
    }
}
