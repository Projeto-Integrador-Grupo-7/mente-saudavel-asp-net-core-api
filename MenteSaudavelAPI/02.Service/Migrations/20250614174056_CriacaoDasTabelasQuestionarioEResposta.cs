using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenteSaudavelAPI._02.Services.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDasTabelasQuestionarioEResposta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RespondenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pontuacao = table.Column<int>(type: "int", nullable: false),
                    Estratificacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionario_Usuario_RespondenteId",
                        column: x => x.RespondenteId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resposta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resposta_Questionario_QuestionarioId",
                        column: x => x.QuestionarioId,
                        principalTable: "Questionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questionario_RespondenteId",
                table: "Questionario",
                column: "RespondenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_QuestionarioId",
                table: "Resposta",
                column: "QuestionarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resposta");

            migrationBuilder.DropTable(
                name: "Questionario");
        }
    }
}
