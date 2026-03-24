using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkillUpAcademy.Infrastructure.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class AgregarProveedoresIA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "proveedores_ia",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    nombre_visible = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    habilitado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    es_activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    api_key = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    url_base = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    modelo_chat = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    max_tokens = table.Column<int>(type: "integer", nullable: false, defaultValue: 1000),
                    temperatura = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.69999999999999996),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()"),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proveedores_ia", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_ia_tipo",
                table: "proveedores_ia",
                column: "tipo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "proveedores_ia");
        }
    }
}
