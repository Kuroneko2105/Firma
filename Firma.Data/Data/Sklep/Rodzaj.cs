using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Firma.Data.Data.Sklep
{
    public class Rodzaj
    {
        [Key]
        public int IdRodzaju { get; set; }

        [Required(ErrorMessage = "Nazwa rodzaju jest wymagana")]
        [MaxLength(20, ErrorMessage = "Nazwa kategorii towaru może zawierac max 20 znaków")]
        public required string Nazwa { get; set; }

        public string Opis { get; set; } = string.Empty;
        //to jest powiazanie tabel, rodzaj ma kolekcje towarow danego rodzaju
        public ICollection<Towar> Towar { get; } = new List<Towar>();
    }
}
