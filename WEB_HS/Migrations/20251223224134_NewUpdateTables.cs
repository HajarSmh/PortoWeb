using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_HS.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projets_Personnes_PersonneId",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "CreePar",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "EstSupperime",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "Lien",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "SupperimeA",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "SupperimePar",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "DateCreation",
                table: "Personnes",
                newName: "DateInscription");

            migrationBuilder.RenameColumn(
                name: "Lu",
                table: "Messages",
                newName: "EstLu");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Messages",
                newName: "NomAuteur");

            migrationBuilder.RenameColumn(
                name: "DateEnvoi",
                table: "Messages",
                newName: "DateCreation");

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Projets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PersonneId",
                table: "Projets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projets",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Technologies",
                table: "Projets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Personnes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Titre",
                table: "Personnes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Contenu",
                table: "Messages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "EmailAuteur",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonneId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Categorie",
                table: "Competence",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Competence",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PersonneId",
                table: "Messages",
                column: "PersonneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Personnes_PersonneId",
                table: "Messages",
                column: "PersonneId",
                principalTable: "Personnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projets_Personnes_PersonneId",
                table: "Projets",
                column: "PersonneId",
                principalTable: "Personnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Personnes_PersonneId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Projets_Personnes_PersonneId",
                table: "Projets");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PersonneId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Technologies",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Titre",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "EmailAuteur",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PersonneId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Categorie",
                table: "Competence");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Competence");

            migrationBuilder.RenameColumn(
                name: "DateInscription",
                table: "Personnes",
                newName: "DateCreation");

            migrationBuilder.RenameColumn(
                name: "NomAuteur",
                table: "Messages",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "EstLu",
                table: "Messages",
                newName: "Lu");

            migrationBuilder.RenameColumn(
                name: "DateCreation",
                table: "Messages",
                newName: "DateEnvoi");

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Projets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "PersonneId",
                table: "Projets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<int>(
                name: "CreePar",
                table: "Projets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EstSupperime",
                table: "Projets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lien",
                table: "Projets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SupperimeA",
                table: "Projets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupperimePar",
                table: "Projets",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contenu",
                table: "Messages",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Messages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Projets_Personnes_PersonneId",
                table: "Projets",
                column: "PersonneId",
                principalTable: "Personnes",
                principalColumn: "Id");
        }
    }
}
