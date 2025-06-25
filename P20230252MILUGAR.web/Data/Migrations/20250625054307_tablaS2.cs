using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P20230252MILUGAR.web.Migrations
{
    /// <inheritdoc />
    public partial class tablaS2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Propietario",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Propietario",
                table: "Reservas");
        }
    }
}
