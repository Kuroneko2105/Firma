using System.ComponentModel.DataAnnotations;

namespace Firma.Data.Data.CMS
{
    public class LayoutLink
    {
        [Key]
        public int IdLayoutLink { get; set; }

        [Required(ErrorMessage = "Tytuł linku jest wymagany")]
        [MaxLength(60, ErrorMessage = "Tytuł linku może mieć maksymalnie 60 znaków")]
        [Display(Name = "Tytuł linku")]
        public required string Tytul { get; set; }

        [Required(ErrorMessage = "Adres linku jest wymagany")]
        [MaxLength(300, ErrorMessage = "Adres linku może mieć maksymalnie 300 znaków")]
        [Display(Name = "Adres linku")]
        public required string Url { get; set; }

        [Required(ErrorMessage = "Pozycja jest wymagana")]
        [Display(Name = "Pozycja")]
        public int Pozycja { get; set; }

        [Display(Name = "Aktywny")]
        public bool CzyAktywny { get; set; } = true;
    }
}
