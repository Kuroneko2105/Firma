using System;
using System.ComponentModel.DataAnnotations;

namespace Firma.Data.Data.Kontakt
{
    public class Kontakt
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres e-mail")]
        [MaxLength(256)]
        [Display(Name = "E-mail")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Wiadomość jest wymagana")]
        [MaxLength(2000, ErrorMessage = "Wiadomość może mieć maksymalnie 2000 znaków")]
        [Display(Name = "Wiadomość")]
        public required string Wiadomosc { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime DataDodania { get; set; } = DateTime.UtcNow;
    }
}
