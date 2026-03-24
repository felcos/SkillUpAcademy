using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkillUpAcademy.Infrastructure.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class AgregarProveedoresTtsYPreferenciasVoz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "proveedor_tts_preferido",
                table: "usuarios",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "WebSpeechApi");

            migrationBuilder.AddColumn<decimal>(
                name: "velocidad_voz",
                table: "usuarios",
                type: "numeric",
                nullable: false,
                defaultValue: 1.0m);

            migrationBuilder.AddColumn<string>(
                name: "voz_preferida",
                table: "usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "proveedores_tts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    nombre_visible = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    habilitado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    api_key = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    region = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    voz_por_defecto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    voces_disponibles = table.Column<string>(type: "jsonb", nullable: true),
                    orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()"),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proveedores_tts", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_tts_tipo",
                table: "proveedores_tts",
                column: "tipo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "proveedores_tts");

            migrationBuilder.DropColumn(
                name: "proveedor_tts_preferido",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "velocidad_voz",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "voz_preferida",
                table: "usuarios");
        }
    }
}
