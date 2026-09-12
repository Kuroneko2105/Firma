using System.ComponentModel.DataAnnotations;

namespace Firma.PortalWWW.Models
{
    public class ZamowienieKoszykViewModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        [MaxLength(30, ErrorMessage = "Imię max 30 znaków")]
        [Display(Name = "Imię")]
        public string Imie { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [MaxLength(50, ErrorMessage = "Nazwisko max 50 znaków")]
        public string Nazwisko { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Miejscowość jest wymagana")]
        [MaxLength(50)]
        [Display(Name = "Miejscowość")]
        public string Miejscowosc { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ulica jest wymagana")]
        [MaxLength(50)]
        public string Ulica { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numer budynku jest wymagany")]
        [MaxLength(10)]
        [Display(Name = "Numer budynku")]
        public string NumerBudynku { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [MaxLength(6)]
        [Display(Name = "Kod pocztowy")]
        public string KodPocztowy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Metoda płatności jest wymagana")]
        [MaxLength(30)]
        [Display(Name = "Metoda płatności")]
        public string MetodaPlatnosci { get; set; } = "Przelew";

        public List<KoszykItem> Koszyk { get; set; } = new();
        public decimal Suma => Koszyk.Sum(p => p.Wartosc);
    }
}
