using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.CMS
{
    public class Aktualnosc
    {
        [Key]
        public int IdAktualnosci { get; set; }

        [Required(ErrorMessage = "Tytuł linku jest wymagany")]
        [MaxLength(10, ErrorMessage = "Tytuł powinien zawierac max 10 znaków")]
        [Display(Name = "Tytuł odnośnika do aktualności")]
        public required string LinkTytul { get; set; }

        [Required(ErrorMessage = "Tytuł aktualności jest wymagany")]
        [MaxLength(40, ErrorMessage = "Tytuł aktualności powinien zawierac max 40 znaków")]
        [Display(Name = "Tytuł aktualności")]
        public required string Tytul { get; set; }

        [Display(Name = "Treść")]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required(ErrorMessage = "Treść aktualności jest wymagana")]
        public required string Tresc { get; set; }

        [Required(ErrorMessage = "Pozycja jest wymagana")]
        [Display(Name = "Pozycja wyświetlania aktualności")]
        public int Pozycja { get; set; }
    }
}
