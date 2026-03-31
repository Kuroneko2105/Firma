using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.CMS
{
    public class Strona
    {
        [Key]//to co nizej bedzie kluczem podsatwowym tabeli
        public int IdStrony { get; set; }

        [Required(ErrorMessage = "Tytuł linku jest wymagany")]//pole wymagane
        [MaxLength(10, ErrorMessage = "Tytuł powinien zawierac max 10 znaków")]
        [Display(Name = "Tytuł odnośnika")]//to jest nazwa pola dla zwyklego uzytkownika, jak jest rozna jak nazwa nizej
        public required string LinkTytul { get; set; }

        [Required(ErrorMessage = "Tytuł strony jest wymagany")]//pole wymagane
        [MaxLength(40, ErrorMessage = "Tytuł strony powinien zawierac max 40 znaków")]
        [Display(Name = "Tytuł strony")]//to jest na
        public required string Tytul { get; set; }

        [Display(Name = "Treść")]
        [Column(TypeName = "nvarchar(MAX)")]//decyduje jakiego typu jest pole w BD
        [Required(ErrorMessage = "Treść strony jest wymagana")]
        public required string Tresc { get; set; }

        [Required(ErrorMessage = "Pozycja jest wymagana")]
        [Display(Name = "Pozycja wyświetlania strony")]
        public int Pozycja { get; set; }
    }
}
