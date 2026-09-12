using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLayoutStyleSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KolorCzcionki",
                table: "LayoutUstawienia",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "body");

            migrationBuilder.AddColumn<string>(
                name: "KolorPrzyciskow",
                table: "LayoutUstawienia",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "primary");

            migrationBuilder.AddColumn<string>(
                name: "WielkoscCzcionki",
                table: "LayoutUstawienia",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "normal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KolorCzcionki",
                table: "LayoutUstawienia");

            migrationBuilder.DropColumn(
                name: "KolorPrzyciskow",
                table: "LayoutUstawienia");

            migrationBuilder.DropColumn(
                name: "WielkoscCzcionki",
                table: "LayoutUstawienia");
        }
    }
}
