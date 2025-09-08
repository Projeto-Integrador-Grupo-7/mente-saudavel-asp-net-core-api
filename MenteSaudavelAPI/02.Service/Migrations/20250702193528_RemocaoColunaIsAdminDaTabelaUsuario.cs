using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenteSaudavelAPI._02.Services.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoColunaIsAdminDaTabelaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
