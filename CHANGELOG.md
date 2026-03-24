# Changelog

## [2026-03-24] — Sesión 6

### feat: tests para módulo admin
- 6 tests unitarios para ServicioAdmin con SQLite in-memory + Identity real
  - ObtenerResumenAsync, ObtenerUsuariosAsync (paginado), ObtenerTotalUsuariosAsync
  - ObtenerEstadisticasContenidoAsync (skip: incompatibilidad LINQ/SQLite)
  - AlternarBloqueoIAUsuarioAsync (éxito + usuario inexistente)
- 4 tests de integración: endpoints admin añadidos a EndpointsProtegidosTests (401 sin token)
- Paquete Microsoft.EntityFrameworkCore.Sqlite añadido a UnitTests.csproj
- Total: 104 tests (103 pass + 1 skip)

### Archivos creados
- `tests/SkillUpAcademy.UnitTests/Servicios/ServicioAdminTests.cs`

### Archivos modificados
- `tests/SkillUpAcademy.UnitTests/SkillUpAcademy.UnitTests.csproj` — paquete SQLite
- `tests/SkillUpAcademy.IntegrationTests/Controladores/EndpointsProtegidosTests.cs` — 4 endpoints admin

---

## [2026-03-24] — Sesión 5

### feat: admin dashboard completo (backend + frontend)
- `AdminController` con 4 endpoints: resumen, usuarios, estadísticas contenido, alternar bloqueo IA
- `IServicioAdmin` / `ServicioAdmin` — lógica de administración
- 3 DTOs: ResumenAdminDto, EstadisticasContenidoDto, UsuarioAdminDto (en Core/DTOs/Admin/)
- `SembradoAdmin` — rol Admin + usuario admin@skillupacademy.com/Admin123!
- `AdminDashboardPage` — panel con resumen y estadísticas de contenido
- `AdminUsersPage` — gestión de usuarios con bloqueo IA
- `ProtectedAdminRoute` — componente de ruta protegida para admins
- `useAdmin` hook — wrapping de endpoints admin con TanStack Query
- Enlace Admin en Navbar (visible solo para rol Admin)

### feat: avatar SVG animado con 4 estados
- `AvatarAria.tsx` reescrito con personaje femenino SVG completo
- 4 estados animados: idle, hablando, pensando, saludando
- Integrado en ChatPage y LessonPage

### feat: configuración de producción (secrets, CORS, HTTPS)
- `appsettings.Production.json` — configuración específica de producción
- `docker-compose.production.yml` — compose para despliegue productivo
- `.env.example` — plantilla de variables de entorno
- Validación JWT en producción (rechaza clave por defecto)
- CORS configurable por entorno
- HSTS y redirección HTTPS habilitados

### feat: roles y autorización por rol en JWT
- Roles incluidos como claims en token JWT
- `esAdmin` añadido a PerfilUsuarioDto
- `EstaBloqueadoIA` añadido a UsuarioApp
- `[Authorize(Roles = "Admin")]` en AdminController

### Archivos creados
- `src/SkillUpAcademy.Api/Controllers/AdminController.cs`
- `src/SkillUpAcademy.Core/Interfaces/IServicioAdmin.cs`
- `src/SkillUpAcademy.Infrastructure/Servicios/ServicioAdmin.cs`
- `src/SkillUpAcademy.Core/DTOs/Admin/ResumenAdminDto.cs`
- `src/SkillUpAcademy.Core/DTOs/Admin/EstadisticasContenidoDto.cs`
- `src/SkillUpAcademy.Core/DTOs/Admin/UsuarioAdminDto.cs`
- `src/SkillUpAcademy.Infrastructure/Datos/SembradoAdmin.cs`
- `src/SkillUpAcademy.Api/appsettings.Production.json`
- `docker-compose.production.yml`
- `.env.example`
- `client/src/pages/AdminDashboardPage.tsx`
- `client/src/pages/AdminUsersPage.tsx`
- `client/src/components/layout/ProtectedAdminRoute.tsx`
- `client/src/hooks/useAdmin.ts`

### Archivos modificados
- `src/SkillUpAcademy.Api/Program.cs` — registro ServicioAdmin en DI, validación JWT prod
- `src/SkillUpAcademy.Api/Controllers/AuthController.cs` — roles en JWT claims
- `src/SkillUpAcademy.Core/DTOs/PerfilUsuarioDto.cs` — campo esAdmin
- `src/SkillUpAcademy.Core/Entidades/UsuarioApp.cs` — campo EstaBloqueadoIA
- `client/src/components/avatar/AvatarAria.tsx` — reescrito con SVG animado
- `client/src/components/layout/Navbar.tsx` — enlace admin
- `client/src/App.tsx` — rutas admin
- `client/src/lib/api.ts` — endpoints admin

---

## [2026-03-23] — Sesión 4

### feat: SSE streaming en chat IA
- `IServicioChatIA.EnviarMensajeStreamAsync` con `IAsyncEnumerable<string>`
- `ServicioChatIA.LlamarApiAnthropicStreamAsync` con parseo de eventos Anthropic (stream: true)
- `ServicioChatIA.ProcesarEventoStream` extraído como método auxiliar (C# no permite yield en try-catch)
- Nuevo endpoint `POST api/v1/ai/session/{id}/message/stream` con text/event-stream
- Protocolo SSE: eventos texto (fragmentos), reemplazo (filtro seguridad), fin (metadata + sugerencias)
- Archivos modificados: IServicioChatIA.cs, ServicioChatIA.cs, AiChatController.cs

### feat: streaming SSE en frontend
- `api.ts`: función `enviarMensajeStream` con `ReadableStream` y parseo SSE, interface `EventoStreamChat`
- `ChatPage.tsx`: reescrito con renderizado progresivo (mensaje se llena caracter por caracter)
- `useChat.ts`: nuevo hook `useEnviarMensajeStreamIA`
- El endpoint no-streaming original se mantiene como fallback

### feat: escenas visuales niveles 2-3
- 12 llamadas a `SembrarEscenasTeoricasAsync` en SembradoDatos.Nivel2.cs (60 escenas)
- 12 llamadas a `SembrarEscenasTeoricasAsync` en SembradoDatos.Nivel3.cs (60 escenas)
- Total: 180 escenas (60 por nivel), 5 por lección teórica
- Guiones personalizados: Nivel 2 enfocado en práctica, Nivel 3 en dominio/maestría

### feat: tests SSE streaming
- 5 tests unitarios para EnviarMensajeStreamAsync (sesión no existe, usuario bloqueado, mensaje inseguro, fallback, sesión cerrada)
- 2 tests de integración nuevos (endpoint /message y /message/stream verifican 401 sin token)
- Total: 94 tests pasando (48 unit + 25 integration + 21 frontend)

---

## [2026-03-23] — Sesión 3

### feat: tests unitarios ServicioChatIA y ServicioEscenas
- 15 tests unitarios para ServicioChatIA (sesiones, mensajes, seguridad, fallback, historial)
- 10 tests unitarios para ServicioEscenas (CRUD, generación automática, reordenamiento)
- Archivos: ServicioChatIATests.cs, ServicioEscenasTests.cs

### feat: tests de integración con WebApplicationFactory
- 23 tests de integración: Health (2), Auth (6), Skills (5), Endpoints protegidos (9), placeholder (1)
- CustomWebApplicationFactory con InMemory DB reemplazando PostgreSQL
- IntegrationTestCollection para ejecución secuencial de tests
- Program.cs: clase parcial para WebApplicationFactory
- Archivos: CustomWebApplicationFactory.cs, IntegrationTestCollection.cs, 4 archivos de tests

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
