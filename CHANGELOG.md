# Changelog — SkillUp Academy

## 2026-03-22

### feat: estructura inicial del proyecto
- Creada solución .NET 8 con Clean Architecture (Api, Core, Infrastructure)
- 19 entidades del dominio incluyendo motor de escenas para avatar (EscenaLeccion, RecursoVisual, ConfiguracionAvatar)
- 10 enums (TipoLeccion, EstadoProgreso, TipoSesionIA, TipoContenidoVisual, TipoTransicion, TipoLayout, etc.)
- DbContext configurado para PostgreSQL con 19 configuraciones Fluent API
- Tablas de Identity renombradas a snake_case
- Program.cs con Serilog, CORS, Swagger, Identity
- Archivos: toda la carpeta src/

### Archivos creados
- `src/SkillUpAcademy.Core/Entidades/` — 19 archivos
- `src/SkillUpAcademy.Core/Enums/` — 10 archivos
- `src/SkillUpAcademy.Infrastructure/Datos/AppDbContext.cs`
- `src/SkillUpAcademy.Infrastructure/Datos/Configuraciones/` — 19 archivos
- `src/SkillUpAcademy.Api/Program.cs`
- `src/SkillUpAcademy.Api/appsettings.json`
