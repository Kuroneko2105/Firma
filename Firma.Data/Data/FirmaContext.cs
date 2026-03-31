using Firma.Data.Data.CMS;
using Firma.Data.Data.Sklep;
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
    }
}
