using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CedroRestaurante.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Restaurantes",
               columns: table => new
               {
                   Id = table.Column<string>(nullable: false, defaultValueSql: "newid()", maxLength: 128),
                   AtualizadoEm = table.Column<DateTimeOffset>(nullable: true, type: "DATETIMEOFFSET"),
                   CriadoEm = table.Column<DateTimeOffset>(nullable: false, type: "DATETIMEOFFSET", defaultValueSql: "sysutcdatetime()"),
                   Nome = table.Column<string>(nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Restaurantes", x => x.Id);
               });

            migrationBuilder.CreateTable(
                name: "Pratos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValueSql: "newid()", maxLength: 128),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true, type: "DATETIMEOFFSET"),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false, type: "DATETIMEOFFSET", defaultValueSql: "sysutcdatetime()"),
                    Descricao = table.Column<string>(nullable: false),
                    RestauranteId = table.Column<string>(nullable: true, maxLength: 128),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pratos", x => x.Id);
                    table.ForeignKey("FK_Pratos_RestauranteId", x => x.RestauranteId, "Restaurantes", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pratos_CriadoEm",
                table: "Pratos",
                column: "CriadoEm");

            migrationBuilder.CreateIndex(
               name: "IX_Restaurantes_CriadoEm",
               table: "Restaurantes",
               column: "CriadoEm");

            migrationBuilder.Sql("CREATE TRIGGER [TR_Restaurantes_InsertUpdateDelete] ON [dbo].[Restaurantes] AFTER INSERT, UPDATE, DELETE AS BEGIN UPDATE [dbo].[Restaurantes] SET [dbo].[Restaurantes].[AtualizadoEm] = CONVERT(DATETIMEOFFSET, SYSUTCDATETIME()) FROM INSERTED WHERE inserted.[Id] = [dbo].[Restaurantes].[Id] END");

            migrationBuilder.Sql("CREATE TRIGGER [TR_Pratos_InsertUpdateDelete] ON [dbo].[Pratos] AFTER INSERT, UPDATE, DELETE AS BEGIN UPDATE [dbo].[Pratos] SET [dbo].[Pratos].[AtualizadoEm] = CONVERT(DATETIMEOFFSET, SYSUTCDATETIME()) FROM INSERTED WHERE inserted.[Id] = [dbo].[Pratos].[Id] END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pratos");

            migrationBuilder.DropTable(
                name: "Restaurantes");
        }
    }
}
