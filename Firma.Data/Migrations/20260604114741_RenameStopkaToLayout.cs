using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameStopkaToLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StopkaLink",
                table: "StopkaLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StopkaUstawienia",
                table: "StopkaUstawienia");

            migrationBuilder.RenameTable(
                name: "StopkaLink",
                newName: "LayoutLink");

            migrationBuilder.RenameTable(
                name: "StopkaUstawienia",
                newName: "LayoutUstawienia");

            migrationBuilder.RenameColumn(
                name: "IdStopkaLink",
                table: "LayoutLink",
                newName: "IdLayoutLink");

            migrationBuilder.RenameColumn(
                name: "IdStopkaUstawienia",
                table: "LayoutUstawienia",
                newName: "IdLayoutUstawienia");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "LayoutUstawienia",
                newName: "OpisStopki");

            migrationBuilder.DropColumn(
                name: "NaglowekKontaktu",
                table: "LayoutUstawienia");

            migrationBuilder.DropColumn(
                name: "NaglowekLinkow",
                table: "LayoutUstawienia");

            migrationBuilder.DropColumn(
                name: "NaglowekOpisu",
                table: "LayoutUstawienia");

            migrationBuilder.AddColumn<string>(
                name: "DomyslnyMotyw",
                table: "LayoutUstawienia",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "light");

            migrationBuilder.AddColumn<string>(
                name: "LogoTekst",
                table: "LayoutUstawienia",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "Logo");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "LayoutUstawienia",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LayoutLink",
                table: "LayoutLink",
                column: "IdLayoutLink");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LayoutUstawienia",
                table: "LayoutUstawienia",
                column: "IdLayoutUstawienia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LayoutLink",
                table: "LayoutLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LayoutUstawienia",
                table: "LayoutUstawienia");

            migrationBuilder.DropColumn(
                name: "DomyslnyMotyw",
                table: "LayoutUstawienia");

            migrationBuilder.DropColumn(
                name: "LogoTekst",
                table: "LayoutUstawienia");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "LayoutUstawienia");

            migrationBuilder.AddColumn<string>(
                name: "NaglowekKontaktu",
                table: "LayoutUstawienia",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "Kontakt");

            migrationBuilder.AddColumn<string>(
                name: "NaglowekLinkow",
                table: "LayoutUstawienia",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "Szybkie linki");

            migrationBuilder.AddColumn<string>(
                name: "NaglowekOpisu",
                table: "LayoutUstawienia",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "O nas");

            migrationBuilder.RenameColumn(
                name: "IdLayoutLink",
                table: "LayoutLink",
                newName: "IdStopkaLink");

            migrationBuilder.RenameColumn(
                name: "IdLayoutUstawienia",
                table: "LayoutUstawienia",
                newName: "IdStopkaUstawienia");

            migrationBuilder.RenameColumn(
                name: "OpisStopki",
                table: "LayoutUstawienia",
                newName: "Opis");

            migrationBuilder.RenameTable(
                name: "LayoutLink",
                newName: "StopkaLink");

            migrationBuilder.RenameTable(
                name: "LayoutUstawienia",
                newName: "StopkaUstawienia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StopkaLink",
                table: "StopkaLink",
                column: "IdStopkaLink");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StopkaUstawienia",
                table: "StopkaUstawienia",
                column: "IdStopkaUstawienia");
        }
    }
}
