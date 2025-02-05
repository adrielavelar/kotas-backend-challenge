using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokemon.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemovePokemonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EvolutionId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SpriteBase64 = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemons_Pokemons_EvolutionId",
                        column: x => x.EvolutionId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_EvolutionId",
                table: "Pokemons",
                column: "EvolutionId");
        }
    }
}
