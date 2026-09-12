using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Sklep
{
    public class Platnosc
    {
        [Key]
        public int IdPlatnosc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Kwota")]
        public decimal Kwota { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Metoda płatności")]
        public string Metoda { get; set; }

        [Required]
        [Display(Name = "Status płatności")]
        public string Status { get; set; }

        public DateTime DataPlatnosci { get; set; } = DateTime.Now;

        public int ZamowienieId { get; set; }
        public Zamowienie Zamowienie { get; set; }
    }
}
