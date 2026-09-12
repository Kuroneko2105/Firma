using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.CMS
{
    public class Ogloszenie
    {
        [Key]
        public int IdOgloszenia{ get; set; }

        [Required(ErrorMessage = "Tytuł linku jest wymagany")]
        [MaxLength(10, ErrorMessage = "Tytuł powinien zawierac max 10 znaków")]
        [Display(Name = "Tytuł odnośnika do ogłoszenia")]
        public required string LinkTytul { get; set; }

        [Required(ErrorMessage = "Tytuł ogłoszenia jest wymagany")]
        [MaxLength(40, ErrorMessage = "Tytuł ogłoszenia powinien zawierac max 40 znaków")]
        [Display(Name = "Tytuł ogłoszenia")]
        public required string Tytul { get; set; }

        [Display(Name = "Treść")]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required(ErrorMessage = "Treść ogłoszenia jest wymagana")]
        public required string Tresc { get; set; }

        [Display(Name = "Zdjęcie")]
        [MaxLength(200, ErrorMessage = "Link do zdjęcia powinien mieć maksymalnie 200 znaków")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ZdjecieUrl { get; set; }
    }
}
