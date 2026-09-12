using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class PlatnoscUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platnosc",
                columns: table => new
                {
                    IdPlatnosc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kwota = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Metoda = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPlatnosci = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ZamowienieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platnosc", x => x.IdPlatnosc);
                    table.ForeignKey(
                        name: "FK_Platnosc_Zamowienie_ZamowienieId",
                        column: x => x.ZamowienieId,
                        principalTable: "Zamowienie",
                        principalColumn: "IdZamowienie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Platnosc_ZamowienieId",
                table: "Platnosc",
                column: "ZamowienieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Platnosc");
        }
    }
}
