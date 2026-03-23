# Changelog

## [2026-03-23] — Sesión 3

### feat: frontend React completo con 13 páginas
- 13 páginas: Home, Login, Register, Areas, AreaDetail, Lesson, Quiz, Scenario, Dashboard, Achievements, Chat, Profile, 404
- Componentes: LoadingSpinner, ErrorMessage, AvatarAria, Navbar, Layout, ProtectedRoute
- Custom hooks: useSkills, useLessons, useProgress, useChat
- Cliente API tipado con 29 endpoints
- Build: 97KB gzip

### feat: CI/CD con GitHub Actions
- Jobs paralelos: backend (.NET 8 build+test) + frontend (tsc+vite build)
- Triggers: push/PR a develop y main

### feat: seguridad IA integrada en chat
- ServicioSeguridadIA conectado en ServicioChatIA
- Validación entrada (5 capas) + validación salida + sistema de strikes

### feat: contenido niveles 2-3
- 60 lecciones nuevas (30 por nivel) con contenido educativo real
- 12 quizzes (60 preguntas, 240 opciones)
- 12 escenarios interactivos
- Archivos parciales: SembradoDatos.Nivel2.cs, SembradoDatos.Nivel3.cs

### feat: SPA serving y Dockerfile
- Program.cs: UseStaticFiles + MapFallbackToFile para producción
- Dockerfile multi-stage con Node 20 + .NET 8

### refactor: páginas usan custom hooks
- Todas las páginas refactorizadas para usar hooks reutilizables

### feat: tests frontend con Vitest
- Setup: Vitest + Testing Library + jsdom
- Tests de API client, AuthContext, HomePage

---

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
- `Api/Extensiones/ExtensionesDeServicios.cs` — 4 servicios nuevos en DI

### feat: Tests unitarios
- 10 tests para ServicioSeguridadIA (formato, rate limit, inyección, salida)
- 8 tests para ServicioQuiz (preguntas, quiz completo, aprobado/reprobado)
- Paquetes: InMemory EF Core, Moq, FluentAssertions

### feat: Docker
- `Dockerfile` — Multi-stage build Alpine, usuario no-root, healthcheck
- `docker-compose.yml` — Servicio API + depends_on PostgreSQL — SkillUp Academy

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
