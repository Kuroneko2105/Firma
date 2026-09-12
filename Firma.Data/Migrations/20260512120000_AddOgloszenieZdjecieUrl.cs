using Firma.Data.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    [DbContext(typeof(FirmaContext))]
    [Migration("20260512120000_AddOgloszenieZdjecieUrl")]
    public partial class AddOgloszenieZdjecieUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZdjecieUrl",
                table: "Ogloszenie",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZdjecieUrl",
                table: "Ogloszenie");
        }
    }
}
