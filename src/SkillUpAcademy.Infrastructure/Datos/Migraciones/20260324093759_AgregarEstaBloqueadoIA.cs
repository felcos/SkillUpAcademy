using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillUpAcademy.Infrastructure.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class AgregarEstaBloqueadoIA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaBloqueadoIA",
                table: "usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaBloqueadoIA",
                table: "usuarios");
        }
    }
}
