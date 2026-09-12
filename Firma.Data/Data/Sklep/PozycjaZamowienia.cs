using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Sklep
{
    public class PozycjaZamowienia
    {
        [Key]
        public int IdPozycja { get; set; }

        [Required]
        public int TowarId { get; set; }

        public Towar Towar { get; set; }

        [Required(ErrorMessage = "Ilość jest wymagana")]
        [Display(Name = "Ilość")]
        public int Ilosc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Cena jednostkowa")]
        public decimal Cena { get; set; }

        [Required]
        public int ZamowienieId { get; set; }

        public Zamowienie Zamowienie { get; set; }
    }
}
