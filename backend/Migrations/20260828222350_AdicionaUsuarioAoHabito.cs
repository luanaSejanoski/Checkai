using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkai.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaUsuarioAoHabito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitos_Usuario_UsuarioId",
                table: "Habitos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Habitos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Habitos_Usuario_UsuarioId",
                table: "Habitos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitos_Usuario_UsuarioId",
                table: "Habitos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Habitos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitos_Usuario_UsuarioId",
                table: "Habitos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }
    }
}
