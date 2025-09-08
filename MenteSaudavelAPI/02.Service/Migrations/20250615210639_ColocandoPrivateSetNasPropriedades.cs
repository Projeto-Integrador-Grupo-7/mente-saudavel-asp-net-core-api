using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenteSaudavelAPI._02.Services.Migrations
{
    /// <inheritdoc />
    public partial class ColocandoPrivateSetNasPropriedades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "IsAdmin",
                table: "Usuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "Genero",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Resposta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Valor",
                table: "Resposta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEnvio",
                table: "Questionario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Resposta");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Resposta");

            migrationBuilder.DropColumn(
                name: "DataEnvio",
                table: "Questionario");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Usuario",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Usuario",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }
    }
}
