using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Firma.Data.Data.Uzytkownicy;

namespace Firma.Data.Data.Sklep
{
    public class Towar
    {
        [Key]
        public int IdTowaru { get; set; }

        [Required(ErrorMessage = "Kod towaru jest wymagany")]
        public required string Kod { get; set; }

        [Required(ErrorMessage = "Nazwa towaru jest wymagana")]
        public required string Nazwa { get; set; }

        [Required(ErrorMessage = "Cena towaru jest wymagana")]
        [Column(TypeName = "money")]
        public decimal Cena { get; set; }

        [Required(ErrorMessage = "Zdjęcie jest wymagane")]
        [Display(Name = "Wybierz zdjęcie")]
        public required string FotoUrl { get; set; }
        public string Opis { get; set; } = string.Empty;

        [ForeignKey("Rodzaj")]
        public int IdRodzaju { get; set; }
        public Rodzaj? Rodzaj { get; set; }
        public ICollection<OpiniaKlienta> Opinie { get; set; } = new List<OpiniaKlienta>();
    }
}
