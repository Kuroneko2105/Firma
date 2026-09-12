using Firma.Data.Data.CMS;
using Firma.Data.Data.Sklep;
using Firma.Data.Data.Uzytkownicy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Data.Data
{
    //to jest klasa, ktora reprezentuje całą bazę danych
    public class FirmaContext :DbContext
    {
        public FirmaContext(DbContextOptions<FirmaContext> options)
            : base(options)
        {
        }
        public DbSet<Aktualnosc> Aktualnosc { get; set; } = default!;
        public DbSet<Strona> Strona { get; set; } = default!;
        public DbSet<Rodzaj> Rodzaj { get; set; } = default!;
        public DbSet<Towar> Towar { get; set; } = default!;
        public DbSet<Ogloszenie> Ogloszenie { get; set; } = default!;
        public DbSet<LayoutUstawienia> LayoutUstawienia { get; set; } = default!;
        public DbSet<LayoutLink> LayoutLink { get; set; } = default!;
        public DbSet<Zamowienie> Zamowienie { get; set; }
        public DbSet<PozycjaZamowienia> PozycjaZamowienia { get; set; }
        public DbSet<Firma.Data.Data.Kontakt.Kontakt> Kontakt { get; set; } = default!;
        public DbSet<Platnosc> Platnosc { get; set; }
        public DbSet<OpiniaKlienta> OpiniaKlienta { get; set; } = default!;
    }
}
