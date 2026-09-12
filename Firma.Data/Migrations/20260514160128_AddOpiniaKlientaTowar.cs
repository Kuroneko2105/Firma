using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOpiniaKlientaTowar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF COL_LENGTH(N'[OpiniaKlienta]', N'TowarId') IS NULL
                BEGIN
                    ALTER TABLE [OpiniaKlienta] ADD [TowarId] int NOT NULL DEFAULT 0;
                END

                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE [name] = N'IX_OpiniaKlienta_TowarId'
                    AND [object_id] = OBJECT_ID(N'[OpiniaKlienta]')
                )
                BEGIN
                    CREATE INDEX [IX_OpiniaKlienta_TowarId] ON [OpiniaKlienta] ([TowarId]);
                END

                IF NOT EXISTS (
                    SELECT 1 FROM sys.foreign_keys
                    WHERE [name] = N'FK_OpiniaKlienta_Towar_TowarId'
                )
                BEGIN
                    ALTER TABLE [OpiniaKlienta] ADD CONSTRAINT [FK_OpiniaKlienta_Towar_TowarId]
                    FOREIGN KEY ([TowarId]) REFERENCES [Towar] ([IdTowaru]) ON DELETE CASCADE;
                END
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF EXISTS (
                    SELECT 1 FROM sys.foreign_keys
                    WHERE [name] = N'FK_OpiniaKlienta_Towar_TowarId'
                )
                BEGIN
                    ALTER TABLE [OpiniaKlienta] DROP CONSTRAINT [FK_OpiniaKlienta_Towar_TowarId];
                END

                IF EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE [name] = N'IX_OpiniaKlienta_TowarId'
                    AND [object_id] = OBJECT_ID(N'[OpiniaKlienta]')
                )
                BEGIN
                    DROP INDEX [IX_OpiniaKlienta_TowarId] ON [OpiniaKlienta];
                END

                IF COL_LENGTH(N'[OpiniaKlienta]', N'TowarId') IS NOT NULL
                BEGIN
                    ALTER TABLE [OpiniaKlienta] DROP COLUMN [TowarId];
                END
                """);
        }
    }
}
