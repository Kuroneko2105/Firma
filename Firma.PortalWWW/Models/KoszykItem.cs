namespace Firma.PortalWWW.Models
{
    public class KoszykItem
    {
        public int IdTowaru { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public decimal Cena { get; set; }
        public string FotoUrl { get; set; } = string.Empty;
        public int Ilosc { get; set; }
        public decimal Wartosc => Cena * Ilosc;
    }
}
