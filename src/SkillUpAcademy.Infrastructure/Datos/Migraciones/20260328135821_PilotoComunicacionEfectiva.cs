using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkillUpAcademy.Infrastructure.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class PilotoComunicacionEfectiva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tipo_sesion",
                table: "sesiones_chat_ia",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "tipo_leccion",
                table: "lecciones",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "planes_accion_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    leccion_id = table.Column<int>(type: "integer", nullable: false),
                    compromiso = table.Column<string>(type: "text", nullable: false),
                    contexto_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_objetivo = table.Column<DateTime>(type: "date", nullable: false),
                    completado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    reflexion_resultado = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()"),
                    fecha_completado = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planes_accion_usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_planes_accion_usuario_lecciones_leccion_id",
                        column: x => x.leccion_id,
                        principalTable: "lecciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planes_accion_usuario_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "resultados_autoevaluacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    leccion_id = table.Column<int>(type: "integer", nullable: false),
                    respuestas_json = table.Column<string>(type: "jsonb", nullable: true),
                    nivel_detectado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    resumen_ia = table.Column<string>(type: "text", nullable: true),
                    fecha_evaluacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resultados_autoevaluacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_resultados_autoevaluacion_lecciones_leccion_id",
                        column: x => x.leccion_id,
                        principalTable: "lecciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_resultados_autoevaluacion_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_planes_accion_usuario_leccion_id",
                table: "planes_accion_usuario",
                column: "leccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_planes_accion_usuario_usuario_id",
                table: "planes_accion_usuario",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_planes_accion_usuario_usuario_id_leccion_id",
                table: "planes_accion_usuario",
                columns: new[] { "usuario_id", "leccion_id" });

            migrationBuilder.CreateIndex(
                name: "IX_resultados_autoevaluacion_leccion_id",
                table: "resultados_autoevaluacion",
                column: "leccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_resultados_autoevaluacion_usuario_id",
                table: "resultados_autoevaluacion",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_resultados_autoevaluacion_usuario_id_leccion_id",
                table: "resultados_autoevaluacion",
                columns: new[] { "usuario_id", "leccion_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "planes_accion_usuario");

            migrationBuilder.DropTable(
                name: "resultados_autoevaluacion");

            migrationBuilder.AlterColumn<string>(
                name: "tipo_sesion",
                table: "sesiones_chat_ia",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "tipo_leccion",
                table: "lecciones",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);
        }
    }
}
