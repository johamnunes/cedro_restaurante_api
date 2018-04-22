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
                  Id = table.Column<string>(nullable: false, defaultValue: "newid()"),
                  AtualizadoEm = table.Column<DateTime>(nullable: false),
                  CriadoEm = table.Column<DateTime>(nullable: false, defaultValue: "sysutcdatetime()"),
                  Nome = table.Column<string>(nullable: false),
                  Removido = table.Column<bool>(nullable: false, defaultValue: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Restaurantes", x => x.Id);
              });

            migrationBuilder.CreateTable(
                name: "Pratos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValue: "newid()"),
                    AtualizadoEm = table.Column<DateTime>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false, defaultValue: "sysutcdatetime()"),
                    Descricao = table.Column<string>(nullable: false),
                    Removido = table.Column<bool>(nullable: false, defaultValue: false),
                    RestauranteId = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pratos", x => x.Id);
                    table.ForeignKey("FK_Pratos_RestauranteId", x => x.RestauranteId, "Restaurantes", "Id", onDelete: ReferentialAction.Cascade);
                });
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
