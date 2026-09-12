using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Data.Data.CMS
{
    public class LayoutUstawienia
    {
        [Key]
        public int IdLayoutUstawienia { get; set; }

        [Required(ErrorMessage = "Nazwa logo jest wymagana")]
        [MaxLength(60, ErrorMessage = "Nazwa logo może mieć maksymalnie 60 znaków")]
        [Display(Name = "Nazwa logo")]
        public string LogoTekst { get; set; } = "Logo";

        [MaxLength(200, ErrorMessage = "Adres logo może mieć maksymalnie 200 znaków")]
        [Display(Name = "Adres obrazka logo")]
        public string? LogoUrl { get; set; }

        [Required(ErrorMessage = "Domyślny motyw jest wymagany")]
        [MaxLength(10, ErrorMessage = "Motyw może mieć maksymalnie 10 znaków")]
        [Display(Name = "Domyślny motyw")]
        public string DomyslnyMotyw { get; set; } = "light";

        [Required(ErrorMessage = "Kolor przycisków jest wymagany")]
        [MaxLength(20, ErrorMessage = "Kolor przycisków może mieć maksymalnie 20 znaków")]
        [Display(Name = "Kolor przycisków")]
        public string KolorPrzyciskow { get; set; } = "primary";

        [Required(ErrorMessage = "Wielkość czcionki jest wymagana")]
        [MaxLength(20, ErrorMessage = "Wielkość czcionki może mieć maksymalnie 20 znaków")]
        [Display(Name = "Wielkość czcionki")]
        public string WielkoscCzcionki { get; set; } = "normal";

        [Required(ErrorMessage = "Kolor czcionki jest wymagany")]
        [MaxLength(20, ErrorMessage = "Kolor czcionki może mieć maksymalnie 20 znaków")]
        [Display(Name = "Kolor czcionki")]
        public string KolorCzcionki { get; set; } = "body";

        [Required(ErrorMessage = "Opis stopki jest wymagany")]
        [Column(TypeName = "nvarchar(MAX)")]
        [Display(Name = "Opis stopki")]
        public string OpisStopki { get; set; } = "Jesteśmy sklepem z odzieżą, który łączy styl, wygodę i jakość.";

        [MaxLength(40, ErrorMessage = "Telefon może mieć maksymalnie 40 znaków")]
        [Display(Name = "Telefon")]
        public string? Telefon { get; set; } = "000-000-000";

        [MaxLength(120, ErrorMessage = "Email może mieć maksymalnie 120 znaków")]
        [Display(Name = "Email")]
        public string? Email { get; set; } = "kontakt@firma.pl";

        [MaxLength(200, ErrorMessage = "Adres może mieć maksymalnie 200 znaków")]
        [Display(Name = "Adres")]
        public string? Adres { get; set; } = "Lipinki Łużyckie, Łączna 43";

        [MaxLength(120, ErrorMessage = "Tekst praw autorskich może mieć maksymalnie 120 znaków")]
        [Display(Name = "Tekst praw autorskich")]
        public string? Copyright { get; set; } = "2026 - Firma.PortalWWW";

        [MaxLength(120, ErrorMessage = "Tekst końcowy może mieć maksymalnie 120 znaków")]
        [Display(Name = "Tekst końcowy")]
        public string? TekstKoncowy { get; set; } = "Portal WWW";
    }
}
