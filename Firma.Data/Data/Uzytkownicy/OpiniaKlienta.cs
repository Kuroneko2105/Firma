using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Firma.Data.Data.Sklep;

namespace Firma.Data.Data.Uzytkownicy
{
    public class OpiniaKlienta
    {
        [Key]
        public int IdOpinii { get; set; }

        [Required(ErrorMessage = "Imie jest wymagane")]
        [MaxLength(60, ErrorMessage = "Imie moze miec maksymalnie 60 znakow")]
        [Display(Name = "Imie")]
        public required string Imie { get; set; }

        [EmailAddress(ErrorMessage = "Podaj poprawny adres e-mail")]
        [MaxLength(256)]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Tresc opinii jest wymagana")]
        [MaxLength(1000, ErrorMessage = "Opinia moze miec maksymalnie 1000 znakow")]
        [Column(TypeName = "nvarchar(1000)")]
        [Display(Name = "Tresc opinii")]
        public required string Tresc { get; set; }

        [Range(1, 5, ErrorMessage = "Ocena musi byc od 1 do 5")]
        [Display(Name = "Ocena")]
        public int Ocena { get; set; } = 5;

        [Display(Name = "Zatwierdzona")]
        public bool CzyZatwierdzona { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime DataDodania { get; set; } = DateTime.UtcNow;

        [ForeignKey("Towar")]
        [Display(Name = "Towar")]
        public int TowarId { get; set; }

        public Towar? Towar { get; set; }
    }
}
