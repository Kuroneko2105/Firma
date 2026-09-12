using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOpiniaKlienta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF OBJECT_ID(N'[OpiniaKlienta]', N'U') IS NULL
                BEGIN
                    CREATE TABLE [OpiniaKlienta] (
                        [IdOpinii] int NOT NULL IDENTITY,
                        [Imie] nvarchar(60) NOT NULL,
                        [Email] nvarchar(256) NULL,
                        [Tresc] nvarchar(1000) NOT NULL,
                        [Ocena] int NOT NULL,
                        [CzyZatwierdzona] bit NOT NULL,
                        [DataDodania] datetime2 NOT NULL,
                        CONSTRAINT [PK_OpiniaKlienta] PRIMARY KEY ([IdOpinii])
                    );
                END
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF OBJECT_ID(N'[OpiniaKlienta]', N'U') IS NOT NULL
                BEGIN
                    DROP TABLE [OpiniaKlienta];
                END
                """);
        }
    }
}
