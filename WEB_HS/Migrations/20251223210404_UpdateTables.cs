using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_HS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreePar",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "EstSupperime",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "NumeroTel",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "SupperimeA",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "SupperimePar",
                table: "Personnes");

            migrationBuilder.RenameColumn(
                name: "AdresseMail",
                table: "Personnes",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Projets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LienDemo",
                table: "Projets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LienSource",
                table: "Projets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonneId",
                table: "Projets",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Personnes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Personnes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Projets_PersonneId",
                table: "Projets",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_Competence_PersonneId",
                table: "Competence",
                column: "PersonneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competence_Personnes_PersonneId",
                table: "Competence",
                column: "PersonneId",
                principalTable: "Personnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projets_Personnes_PersonneId",
                table: "Projets",
                column: "PersonneId",
                principalTable: "Personnes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competence_Personnes_PersonneId",
                table: "Competence");

            migrationBuilder.DropForeignKey(
                name: "FK_Projets_Personnes_PersonneId",
                table: "Projets");

            migrationBuilder.DropIndex(
                name: "IX_Projets_PersonneId",
                table: "Projets");

            migrationBuilder.DropIndex(
                name: "IX_Competence_PersonneId",
                table: "Competence");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "LienDemo",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "LienSource",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "PersonneId",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Personnes");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Personnes",
                newName: "AdresseMail");

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Personnes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "AdresseMail",
                table: "Personnes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CreePar",
                table: "Personnes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EstSupperime",
                table: "Personnes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroTel",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SupperimeA",
                table: "Personnes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupperimePar",
                table: "Personnes",
                type: "int",
                nullable: true);
        }
    }
}
