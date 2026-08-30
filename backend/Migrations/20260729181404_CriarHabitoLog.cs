using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkai.Migrations
{
    /// <inheritdoc />
    public partial class CriarHabitoLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabitoLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Concluido = table.Column<bool>(type: "INTEGER", nullable: false),
                    HabitoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitoLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitoLogs_Habitos_HabitoId",
                        column: x => x.HabitoId,
                        principalTable: "Habitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitoLogs_HabitoId",
                table: "HabitoLogs",
                column: "HabitoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitoLogs");
        }
    }
}
