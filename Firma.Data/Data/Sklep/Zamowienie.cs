using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Sklep
{
    public class Zamowienie
    {
        [Key]
        public int IdZamowienie { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [MaxLength(30, ErrorMessage = "Imię max 30 znaków")]
        [Display(Name = "Imię")]
        public required string Imie { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [MaxLength(50, ErrorMessage = "Nazwisko max 50 znaków")]
        [Display(Name = "Nazwisko")]
        public required string Nazwisko { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny email")]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Miejscowość jest wymagana")]
        [MaxLength(50)]
        [Display(Name = "Miejscowość")]
        public required string Miejscowosc { get; set; }

        [Required(ErrorMessage = "Ulica jest wymagana")]
        [MaxLength(50)]
        [Display(Name = "Ulica")]
        public required string Ulica { get; set; }

        [Required(ErrorMessage = "Numer budynku jest wymagany")]
        [MaxLength(10)]
        [Display(Name = "Numer budynku")]
        public required string NumerBudynku { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [MaxLength(6)]
        [Display(Name = "Kod pocztowy")]
        public required string KodPocztowy { get; set; }

        [Required(ErrorMessage = "Suma jest wymagana")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Suma zamówienia")]
        public decimal Suma { get; set; }

        [Required(ErrorMessage = "Status jest wymagany")]
        [MaxLength(20)]
        [Display(Name = "Status zamówienia")]
        public required string Status { get; set; }

        [Display(Name = "Data zamówienia")]
        public DateTime DataZamowienia { get; set; } = DateTime.Now;

        public List<PozycjaZamowienia> Pozycje { get; set; } = new();
        public List<Platnosc> Platnosci { get; set; } = new();
    }
}
