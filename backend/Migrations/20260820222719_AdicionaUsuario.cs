using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkai.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Habitos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "HabitoLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitos_UsuarioId",
                table: "Habitos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitoLogs_UsuarioId",
                table: "HabitoLogs",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitoLogs_Usuario_UsuarioId",
                table: "HabitoLogs",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Habitos_Usuario_UsuarioId",
                table: "Habitos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitoLogs_Usuario_UsuarioId",
                table: "HabitoLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Habitos_Usuario_UsuarioId",
                table: "Habitos");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Habitos_UsuarioId",
                table: "Habitos");

            migrationBuilder.DropIndex(
                name: "IX_HabitoLogs_UsuarioId",
                table: "HabitoLogs");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Habitos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "HabitoLogs");
        }
    }
}
