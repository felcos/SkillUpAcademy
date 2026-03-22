# Changelog

## [2026-03-22] — Sesión 2

### feat: Servicios de IA y contenido educativo
- `ServicioChatIA` — Chat con API de Anthropic via HttpClient, fallback, historial
- `ServicioSeguridadIA` — 5 capas anti-abuso (rate limit, formato, keywords, regex, clasificador)
- `ServicioTts` — Web Speech API + Azure Speech
- `ServicioEscenas` — Motor de escenas con generación automática
- `SembradoDatos` — 6 áreas × 5 lecciones = 30 lecciones de nivel 1 con contenido real
- 30 preguntas quiz, 6 escenarios interactivos, 10 logros, config avatar Aria
- Todos los servicios registrados en DI con HttpClientFactory
- Program.cs con auto-migración y seeding en desarrollo

### Archivos creados
- `Infrastructure/Servicios/ServicioChatIA.cs`
- `Infrastructure/Servicios/ServicioSeguridadIA.cs`
- `Infrastructure/Servicios/ServicioTts.cs`
- `Infrastructure/Servicios/ServicioEscenas.cs`
- `Infrastructure/Datos/SembradoDatos.cs`

### Archivos modificados
- `Api/Program.cs` — Auto-migración + seeder
- `Api/Extensiones/ExtensionesDeServicios.cs` — 4 servicios nuevos en DI — SkillUp Academy

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
