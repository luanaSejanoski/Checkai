using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkai.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaMetaDias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MetaDias",
                table: "Habitos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaDias",
                table: "Habitos");
        }
    }
}
