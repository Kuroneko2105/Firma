using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStopkaZarzadzanie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StopkaLink",
                columns: table => new
                {
                    IdStopkaLink = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Pozycja = table.Column<int>(type: "int", nullable: false),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopkaLink", x => x.IdStopkaLink);
                });

            migrationBuilder.CreateTable(
                name: "StopkaUstawienia",
                columns: table => new
                {
                    IdStopkaUstawienia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaglowekOpisu = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    NaglowekKontaktu = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NaglowekLinkow = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Copyright = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    TekstKoncowy = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopkaUstawienia", x => x.IdStopkaUstawienia);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StopkaLink");

            migrationBuilder.DropTable(
                name: "StopkaUstawienia");
        }
    }
}
