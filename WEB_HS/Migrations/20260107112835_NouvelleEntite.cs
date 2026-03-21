using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_HS.Migrations
{
    /// <inheritdoc />
    public partial class NouvelleEntite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreePar = table.Column<int>(type: "int", nullable: false),
                    EstSupperime = table.Column<bool>(type: "bit", nullable: true),
                    SupperimeA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SupperimePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entreprise");
        }
    }
}
