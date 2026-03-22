using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkillUpAcademy.Infrastructure.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas_habilidad",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    slug = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    subtitulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    icono = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    color_primario = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    color_acento = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areas_habilidad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "configuraciones_avatar",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_avatar = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Aria"),
                    slug = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    descripcion_personalidad = table.Column<string>(type: "text", nullable: true),
                    tono = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "profesional"),
                    velocidad_habla = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 1.0m),
                    voz_tts = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValue: "es-ES-ElviraNeural"),
                    prompt_sistema_base = table.Column<string>(type: "text", nullable: true),
                    estilo_visual = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "corporativa"),
                    color_fondo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    estilo_celebracion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "moderado"),
                    estilo_correccion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "constructivo"),
                    es_default = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_configuraciones_avatar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "logros",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    slug = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    icono = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    puntos_requeridos = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    condicion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    url_avatar = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()"),
                    ultimo_acceso = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()"),
                    puntos_totales = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    idioma_preferido = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false, defaultValue: "es"),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    audio_habilitado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    racha_dias = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ultima_fecha_actividad = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "niveles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area_habilidad_id = table.Column<int>(type: "integer", nullable: false),
                    numero_nivel = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    puntos_desbloqueo = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_niveles", x => x.id);
                    table.ForeignKey(
                        name: "FK_niveles_areas_habilidad_area_habilidad_id",
                        column: x => x.area_habilidad_id,
                        principalTable: "areas_habilidad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roles_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roles_claims_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "logros_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    logro_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_desbloqueo = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logros_usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_logros_usuario_logros_logro_id",
                        column: x => x.logro_id,
                        principalTable: "logros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_logros_usuario_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarios_claims_usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_usuarios_logins_usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_roles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_usuarios_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_tokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_usuarios_tokens_usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lecciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nivel_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_leccion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    contenido = table.Column<string>(type: "text", nullable: true),
                    puntos_clave = table.Column<string>(type: "jsonb", nullable: true),
                    guion_audio = table.Column<string>(type: "text", nullable: true),
                    puntos_recompensa = table.Column<int>(type: "integer", nullable: false, defaultValue: 10),
                    orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    duracion_minutos = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lecciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_lecciones_niveles_nivel_id",
                        column: x => x.nivel_id,
                        principalTable: "niveles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "escenarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    leccion_id = table.Column<int>(type: "integer", nullable: false),
                    texto_situacion = table.Column<string>(type: "text", nullable: false),
                    contexto = table.Column<string>(type: "text", nullable: true),
                    guion_audio = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_escenarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_escenarios_lecciones_leccion_id",
                        column: x => x.leccion_id,
                        principalTable: "lecciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "escenas_leccion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    leccion_id = table.Column<int>(type: "integer", nullable: false),
                    orden = table.Column<int>(type: "integer", nullable: false),
                    tipo_contenido_visual = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    titulo_escena = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    guion_aria = table.Column<string>(type: "text", nullable: true),
                    contenido_visual = table.Column<string>(type: "text", nullable: true),
                    metadatos_visuales = table.Column<string>(type: "jsonb", nullable: true),
                    transicion_entrada = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    layout = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    duracion_segundos = table.Column<int>(type: "integer", nullable: false, defaultValue: 15),
                    es_pausa_reflexiva = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    segundos_pausa = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_escenas_leccion", x => x.id);
                    table.ForeignKey(
                        name: "FK_escenas_leccion_lecciones_leccion_id",
                        column: x => x.leccion_id,
                        principalTable: "lecciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "preguntas_quiz",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    leccion_id = table.Column<int>(type: "integer", nullable: false),
                    texto_pregunta = table.Column<string>(type: "text", nullable: false),
                    explicacion = table.Column<string>(type: "text", nullable: true),
                    orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntas_quiz", x => x.id);
                    table.ForeignKey(
                        name: "FK_preguntas_quiz_lecciones_leccion_id",
                        column: x => x.leccion_id,
                        principalTable: "lecciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "progreso_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    leccion_id = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "NoIniciado"),
                    puntuacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    intentos = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    fecha_completado = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    ultimo_acceso = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progreso_usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_progreso_usuario_lecciones_leccion_id",
                        column: x => x.leccion_id,
                        principalTable: "lecciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_progreso_usuario_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sesiones_chat_ia",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    leccion_id = table.Column<int>(type: "integer", nullable: true),
                    tipo_sesion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()"),
                    fecha_fin = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    contador_mensajes = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    fue_marcada = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    activa = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sesiones_chat_ia", x => x.id);
                    table.ForeignKey(
                        name: "FK_sesiones_chat_ia_lecciones_leccion_id",
                        column: x => x.leccion_id,
                        principalTable: "lecciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_sesiones_chat_ia_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "opciones_escenario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    escenario_id = table.Column<int>(type: "integer", nullable: false),
                    texto_opcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    tipo_resultado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    texto_retroalimentacion = table.Column<string>(type: "text", nullable: false),
                    puntos_otorgados = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    siguiente_escenario_id = table.Column<int>(type: "integer", nullable: true),
                    orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opciones_escenario", x => x.id);
                    table.ForeignKey(
                        name: "FK_opciones_escenario_escenarios_escenario_id",
                        column: x => x.escenario_id,
                        principalTable: "escenarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_opciones_escenario_escenarios_siguiente_escenario_id",
                        column: x => x.siguiente_escenario_id,
                        principalTable: "escenarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "recursos_visuales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    escena_leccion_id = table.Column<int>(type: "integer", nullable: true),
                    tipo_recurso = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    texto_alternativo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    etiquetas = table.Column<string>(type: "jsonb", nullable: true),
                    tamano_bytes = table.Column<long>(type: "bigint", nullable: false),
                    tipo_mime = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recursos_visuales", x => x.id);
                    table.ForeignKey(
                        name: "FK_recursos_visuales_escenas_leccion_escena_leccion_id",
                        column: x => x.escena_leccion_id,
                        principalTable: "escenas_leccion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "opciones_quiz",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pregunta_quiz_id = table.Column<int>(type: "integer", nullable: false),
                    texto_opcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    es_correcta = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    retroalimentacion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opciones_quiz", x => x.id);
                    table.ForeignKey(
                        name: "FK_opciones_quiz_preguntas_quiz_pregunta_quiz_id",
                        column: x => x.pregunta_quiz_id,
                        principalTable: "preguntas_quiz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mensajes_chat_ia",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sesion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    contenido = table.Column<string>(type: "text", nullable: false),
                    fue_marcado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    motivo_marca = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    tokens_usados = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    fecha_envio = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mensajes_chat_ia", x => x.id);
                    table.ForeignKey(
                        name: "FK_mensajes_chat_ia_sesiones_chat_ia_sesion_id",
                        column: x => x.sesion_id,
                        principalTable: "sesiones_chat_ia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registros_abuso",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sesion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tipo_violacion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mensaje_original = table.Column<string>(type: "text", nullable: true),
                    metodo_deteccion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    accion_tomada = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registros_abuso", x => x.id);
                    table.ForeignKey(
                        name: "FK_registros_abuso_sesiones_chat_ia_sesion_id",
                        column: x => x.sesion_id,
                        principalTable: "sesiones_chat_ia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_registros_abuso_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "elecciones_escenario_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    escenario_id = table.Column<int>(type: "integer", nullable: false),
                    opcion_escenario_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_eleccion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_elecciones_escenario_usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_elecciones_escenario_usuario_escenarios_escenario_id",
                        column: x => x.escenario_id,
                        principalTable: "escenarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_elecciones_escenario_usuario_opciones_escenario_opcion_esce~",
                        column: x => x.opcion_escenario_id,
                        principalTable: "opciones_escenario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_elecciones_escenario_usuario_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "respuestas_quiz_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pregunta_quiz_id = table.Column<int>(type: "integer", nullable: false),
                    opcion_seleccionada_id = table.Column<int>(type: "integer", nullable: false),
                    es_correcta = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_respuesta = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas_quiz_usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_respuestas_quiz_usuario_opciones_quiz_opcion_seleccionada_id",
                        column: x => x.opcion_seleccionada_id,
                        principalTable: "opciones_quiz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_respuestas_quiz_usuario_preguntas_quiz_pregunta_quiz_id",
                        column: x => x.pregunta_quiz_id,
                        principalTable: "preguntas_quiz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_respuestas_quiz_usuario_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_areas_habilidad_slug",
                table: "areas_habilidad",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_configuraciones_avatar_slug",
                table: "configuraciones_avatar",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_elecciones_escenario_usuario_escenario_id",
                table: "elecciones_escenario_usuario",
                column: "escenario_id");

            migrationBuilder.CreateIndex(
                name: "IX_elecciones_escenario_usuario_opcion_escenario_id",
                table: "elecciones_escenario_usuario",
                column: "opcion_escenario_id");

            migrationBuilder.CreateIndex(
                name: "IX_elecciones_escenario_usuario_usuario_id",
                table: "elecciones_escenario_usuario",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_escenarios_leccion_id",
                table: "escenarios",
                column: "leccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_escenas_leccion_leccion_id_orden",
                table: "escenas_leccion",
                columns: new[] { "leccion_id", "orden" });

            migrationBuilder.CreateIndex(
                name: "IX_lecciones_nivel_id",
                table: "lecciones",
                column: "nivel_id");

            migrationBuilder.CreateIndex(
                name: "IX_logros_slug",
                table: "logros",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_logros_usuario_logro_id",
                table: "logros_usuario",
                column: "logro_id");

            migrationBuilder.CreateIndex(
                name: "IX_logros_usuario_usuario_id_logro_id",
                table: "logros_usuario",
                columns: new[] { "usuario_id", "logro_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mensajes_chat_ia_sesion_id",
                table: "mensajes_chat_ia",
                column: "sesion_id");

            migrationBuilder.CreateIndex(
                name: "IX_niveles_area_habilidad_id_numero_nivel",
                table: "niveles",
                columns: new[] { "area_habilidad_id", "numero_nivel" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_opciones_escenario_escenario_id",
                table: "opciones_escenario",
                column: "escenario_id");

            migrationBuilder.CreateIndex(
                name: "IX_opciones_escenario_siguiente_escenario_id",
                table: "opciones_escenario",
                column: "siguiente_escenario_id");

            migrationBuilder.CreateIndex(
                name: "IX_opciones_quiz_pregunta_quiz_id",
                table: "opciones_quiz",
                column: "pregunta_quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_preguntas_quiz_leccion_id",
                table: "preguntas_quiz",
                column: "leccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_usuario_leccion_id",
                table: "progreso_usuario",
                column: "leccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_usuario_usuario_id",
                table: "progreso_usuario",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_usuario_usuario_id_leccion_id",
                table: "progreso_usuario",
                columns: new[] { "usuario_id", "leccion_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recursos_visuales_escena_leccion_id",
                table: "recursos_visuales",
                column: "escena_leccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_registros_abuso_sesion_id",
                table: "registros_abuso",
                column: "sesion_id");

            migrationBuilder.CreateIndex(
                name: "IX_registros_abuso_usuario_id",
                table: "registros_abuso",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_quiz_usuario_opcion_seleccionada_id",
                table: "respuestas_quiz_usuario",
                column: "opcion_seleccionada_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_quiz_usuario_pregunta_quiz_id",
                table: "respuestas_quiz_usuario",
                column: "pregunta_quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_quiz_usuario_usuario_id",
                table: "respuestas_quiz_usuario",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_claims_RoleId",
                table: "roles_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_sesiones_chat_ia_leccion_id",
                table: "sesiones_chat_ia",
                column: "leccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_sesiones_chat_ia_usuario_id",
                table: "sesiones_chat_ia",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "usuarios",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "usuarios",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_claims_UserId",
                table: "usuarios_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_logins_UserId",
                table: "usuarios_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_roles_RoleId",
                table: "usuarios_roles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "configuraciones_avatar");

            migrationBuilder.DropTable(
                name: "elecciones_escenario_usuario");

            migrationBuilder.DropTable(
                name: "logros_usuario");

            migrationBuilder.DropTable(
                name: "mensajes_chat_ia");

            migrationBuilder.DropTable(
                name: "progreso_usuario");

            migrationBuilder.DropTable(
                name: "recursos_visuales");

            migrationBuilder.DropTable(
                name: "registros_abuso");

            migrationBuilder.DropTable(
                name: "respuestas_quiz_usuario");

            migrationBuilder.DropTable(
                name: "roles_claims");

            migrationBuilder.DropTable(
                name: "usuarios_claims");

            migrationBuilder.DropTable(
                name: "usuarios_logins");

            migrationBuilder.DropTable(
                name: "usuarios_roles");

            migrationBuilder.DropTable(
                name: "usuarios_tokens");

            migrationBuilder.DropTable(
                name: "opciones_escenario");

            migrationBuilder.DropTable(
                name: "logros");

            migrationBuilder.DropTable(
                name: "escenas_leccion");

            migrationBuilder.DropTable(
                name: "sesiones_chat_ia");

            migrationBuilder.DropTable(
                name: "opciones_quiz");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "escenarios");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "preguntas_quiz");

            migrationBuilder.DropTable(
                name: "lecciones");

            migrationBuilder.DropTable(
                name: "niveles");

            migrationBuilder.DropTable(
                name: "areas_habilidad");
        }
    }
}
