using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class M3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zamowienie",
                columns: table => new
                {
                    IdZamowienie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miejscowosc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumerBudynku = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    KodPocztowy = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Suma = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataZamowienia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienie", x => x.IdZamowienie);
                });

            migrationBuilder.CreateTable(
                name: "PozycjaZamowienia",
                columns: table => new
                {
                    IdPozycja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TowarId = table.Column<int>(type: "int", nullable: false),
                    Ilosc = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ZamowienieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjaZamowienia", x => x.IdPozycja);
                    table.ForeignKey(
                        name: "FK_PozycjaZamowienia_Towar_TowarId",
                        column: x => x.TowarId,
                        principalTable: "Towar",
                        principalColumn: "IdTowaru",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PozycjaZamowienia_Zamowienie_ZamowienieId",
                        column: x => x.ZamowienieId,
                        principalTable: "Zamowienie",
                        principalColumn: "IdZamowienie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaZamowienia_TowarId",
                table: "PozycjaZamowienia",
                column: "TowarId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaZamowienia_ZamowienieId",
                table: "PozycjaZamowienia",
                column: "ZamowienieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PozycjaZamowienia");

            migrationBuilder.DropTable(
                name: "Zamowienie");
        }
    }
}
