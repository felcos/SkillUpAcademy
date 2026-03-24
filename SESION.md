# Sesión — SkillUp Academy

## Sesión 7 — 2026-03-24

### Qué se hizo
1. **TTS multi-proveedor completo** — Sistema de Text-to-Speech con 3 proveedores: Azure Speech (voces neurales), ElevenLabs (voces ultra-realistas), y Web Speech API (fallback navegador). Todo configurable desde admin sin tocar código.
2. **Backend TTS** — Nueva entidad `ProveedorTts`, `ServicioTts` reescrito como multi-proveedor, `ServicioAdminTts` para CRUD, `TtsController` con 5 endpoints, 4 endpoints admin TTS. 12 voces Azure + 6 ElevenLabs precargadas.
3. **Preferencias de voz por usuario** — 3 columnas nuevas en `UsuarioApp` (VozPreferida, VelocidadVoz, ProveedorTtsPreferido). El usuario puede elegir proveedor, voz específica y velocidad desde su perfil.
4. **Frontend TTS** — ProfilePage con selector de voz completo (tipo, voz con preview, slider velocidad). LessonPage usa audio del servidor cuando hay proveedor habilitado, fallback automático a Web Speech. AdminTtsPage para gestionar proveedores.
5. **Tests** — 10 tests ServicioTts + 8 tests ServicioAdminTts + 6 tests integración endpoints TTS = 24 nuevos tests. Total: 130 (129 pass + 1 skip).

### Estadísticas
- **130 tests totales** (71 unit + 1 skip + 37 integration + 21 frontend)
- **44 endpoints** — **16 páginas React**
- **0 errores, 0 warnings**
- **20 entidades** — **12 servicios** — **5 repositorios**

### Qué queda pendiente
- Configurar API keys reales (Azure Speech y/o ElevenLabs) desde admin en producción
- Video AI generado para avatar V2
- Notificaciones en tiempo real
- Caché de audio para lecciones (los guiones son fijos, no necesitan regenerarse)

### Problemas encontrados
- Ninguno en esta sesión — implementación limpia

### Siguiente paso sugerido
Configurar una API key de Azure Speech en producción (desde /admin/tts) para activar voces de alta calidad. Alternativamente, implementar caché de audio para evitar regenerar TTS en cada visita a una lección.

---

## Sesión 6 — 2026-03-24

### Qué se hizo
1. **Tests unitarios para ServicioAdmin** — 6 tests con SQLite in-memory (ObtenerResumen, ObtenerUsuarios paginado, ObtenerTotalUsuarios, EstadisticasContenido [skip por incompatibilidad LINQ/SQLite], AlternarBloqueoIA éxito, AlternarBloqueoIA usuario inexistente)
2. **Tests de integración para AdminController** — 4 endpoints admin añadidos a EndpointsProtegidosTests (resumen, usuarios, estadisticas-contenido, alternar-bloqueo-ia verifican 401 sin token)
3. **Paquete SQLite añadido** a UnitTests.csproj para tests de ServicioAdmin con Identity real
4. **Rate limiting nativo .NET 8** — Reemplazado paquete terceros `AspNetCoreRateLimit` por middleware nativo `Microsoft.AspNetCore.RateLimiting`. 3 políticas: general (100/min por IP), ia (20/min por usuario), tts (30/min por usuario). Configuración desde `LimitesDeUso` en appsettings.json. `[EnableRateLimiting]` aplicado a los 8 controladores (excepto Health). ServicioSeguridadIA ahora lee `MaxMensajesPorMinuto` desde config.
5. **Tests rate limiting** — 2 tests de integración verificando que endpoints con y sin rate limiting funcionan correctamente
6. **Despliegue a producción** — App desplegada en `skillupacademy.felcos.es` (Ubuntu ARM64 vía SSH tunnel). Self-contained publish linux-arm64, systemd service, Nginx reverse proxy con SSE, Let's Encrypt SSL, PostgreSQL con usuario dedicado, migración EstaBloqueadoIA aplicada, seeding completo.
7. **Fuentes Inter locales** — Eliminadas todas las referencias a Google Fonts CDN. Descargada fuente Inter (woff2, pesos 300-700) desde @fontsource/inter a `client/public/fonts/`. Declaraciones @font-face en index.css. CSP `font-src 'self'` ahora coherente con assets 100% locales.

### Estadísticas
- **106 tests totales** (53 unit + 1 skip + 31 integration + 21 frontend)
- **34 endpoints** — **15 páginas React**
- **0 errores, 0 warnings**
- **Producción**: https://skillupacademy.felcos.es — HTTPS válido, 0 referencias externas

### Qué queda pendiente
- Video AI generado para avatar V2
- Notificaciones en tiempo real
- Anthropic API key en producción (chat IA usa fallback)
- Azure Speech key en producción (TTS usa fallback)

### Problemas encontrados
- Query LINQ compleja con record constructor en Select + SelectMany no compatible con SQLite ni InMemory (bug EF Core 8) — test marcado con Skip, requiere PostgreSQL real
- Sesión anterior interrumpida por reinicio del ordenador — trabajo recuperado sin pérdida
- WebApplicationFactory con minimal hosting no permite override de `builder.Configuration.GetValue()` desde tests — rate limiting testeado verificando comportamiento dentro del límite
- Google Fonts CDN bloqueado por CSP `font-src 'self'` — resuelto descargando Inter localmente
- rsync --delete borró appsettings.Production.json configurado — recreado manualmente
- PostgreSQL password con `!` causa problemas en Npgsql — cambiada a password alfanumérico

### Siguiente paso sugerido
Configurar API keys reales de Anthropic y Azure Speech en producción para activar chat IA y TTS.

---

## Sesión 5 — 2026-03-24

### Qué se hizo
1. **Admin Dashboard completo (backend)** — AdminController con 4 endpoints: GET /resumen, GET /usuarios, GET /estadisticas-contenido, POST /usuarios/{id}/alternar-bloqueo-ia. ServicioAdmin con IServicioAdmin. 3 DTOs en Core/DTOs/Admin/. SembradoAdmin con rol Admin y usuario admin@skillupacademy.com/Admin123!
2. **Admin Dashboard completo (frontend)** — AdminDashboardPage (resumen + estadísticas), AdminUsersPage (gestión usuarios con bloqueo IA). ProtectedAdminRoute para rutas admin. Hook useAdmin. Enlace Admin en Navbar (solo visible para admins).
3. **Avatar SVG animado** — AvatarAria.tsx reescrito con personaje femenino completo, 4 estados animados (idle, hablando, pensando, saludando). Integrado en ChatPage y LessonPage.
4. **Configuración de producción** — appsettings.Production.json, docker-compose.production.yml, .env.example. Validación JWT en producción (rechaza clave por defecto). CORS configurable por entorno. HSTS y HTTPS habilitados.
5. **Auth mejorada con roles** — Roles incluidos en JWT claims. Campo esAdmin en PerfilUsuarioDto. Campo EstaBloqueadoIA en UsuarioApp. Autorización por rol [Authorize(Roles = "Admin")] en AdminController.

### Estadísticas
- **94 tests totales pasando** (48 unit + 25 integration + 21 frontend)
- **34 endpoints** (30 existentes + 4 admin)
- **15 páginas React** (13 existentes + 2 admin)
- **0 errores, 0 warnings**

### Qué queda pendiente
- Tests específicos para AdminController y ServicioAdmin
- Rate limiting real (no IMemoryCache)
- Despliegue a producción con secrets reales
- Video AI generado para avatar V2
- Notificaciones en tiempo real

### Problemas encontrados
- Validación JWT en entorno de testing: la clave por defecto se rechaza en producción, requiere configuración separada para tests
- Worktrees con divergencia entre ramas al trabajar en paralelo

### Siguiente paso sugerido
Añadir tests para el módulo admin (unitarios para ServicioAdmin + integración para AdminController). Luego desplegar a producción con secrets reales y dominio configurado.

---

## Sesión 4 — 2026-03-23

### Qué se hizo
1. **SSE streaming en chat IA** — Feature completa end-to-end
   - Backend: `EnviarMensajeStreamAsync` con `IAsyncEnumerable<string>` en IServicioChatIA/ServicioChatIA
   - Backend: `LlamarApiAnthropicStreamAsync` con parseo de eventos Anthropic (`content_block_delta`, `message_start`, `message_delta`)
   - Backend: Nuevo endpoint `POST session/{id}/message/stream` en AiChatController con `text/event-stream`
   - Backend: Método auxiliar `ProcesarEventoStream` (extraído para evitar yield en try-catch)
   - Frontend: `enviarMensajeStream` en api.ts con `ReadableStream` y parseo SSE
   - Frontend: ChatPage.tsx reescrito con renderizado progresivo (texto aparece caracter por caracter)
   - Frontend: Soporte para eventos `texto`, `reemplazo` (filtro seguridad) y `fin` (sugerencias)
   - Frontend: Hook `useEnviarMensajeStreamIA` en useChat.ts
2. **Escenas visuales niveles 2-3** — 120 escenas nuevas
   - 12 llamadas a `SembrarEscenasTeoricasAsync` en SembradoDatos.Nivel2.cs (60 escenas)
   - 12 llamadas a `SembrarEscenasTeoricasAsync` en SembradoDatos.Nivel3.cs (60 escenas)
   - Guiones personalizados: Nivel 2 enfocado en práctica, Nivel 3 en dominio/maestría
3. **Tests unitarios SSE streaming** — 5 tests nuevos para EnviarMensajeStreamAsync
   - SesionNoExiste, UsuarioBloqueado, MensajeInseguro, FallbackSinApiKey, SesionCerrada
4. **Tests de integración SSE** — 2 InlineData nuevos en EndpointsProtegidosTests
   - Endpoint `/message` y `/message/stream` verifican 401 sin token

### Estadísticas
- **94 tests totales pasando** (48 unit + 25 integration + 21 frontend)
- **180 escenas totales** (60 nivel 1 + 60 nivel 2 + 60 nivel 3)
- **30 endpoints** (29 existentes + 1 SSE streaming)
- **0 errores, 0 warnings**

### Qué queda pendiente
- Configuración de producción (secrets reales, HTTPS, CORS dominio)
- Animaciones de avatar SVG más elaboradas
- Admin dashboard

### Problemas encontrados
- `yield return` dentro de `try-catch` no permitido en C# (CS1626) — resuelto extrayendo `ProcesarEventoStream` como método separado
- Variable `errorStatus` no usada (warning) — eliminada
- Tests frontend fallan si se ejecutan desde raíz del proyecto (deben ejecutarse desde `client/`)

### Siguiente paso sugerido
Configurar producción: secrets reales (Anthropic API key, JWT key), HTTPS, CORS con dominio, y desplegar.

---

## Sesión 3 — 2026-03-23

### Qué se hizo
1. **Frontend React completo** — 13 páginas con Vite + TypeScript + Tailwind CSS
   - Home, Login, Register, Areas, AreaDetail, Lesson (motor de escenas + TTS Web Speech API), Quiz, Scenario, Dashboard, Achievements, Chat IA, Profile, 404
2. **Componentes reutilizables** — LoadingSpinner, ErrorMessage, AvatarAria, Navbar, Layout, ProtectedRoute
3. **Custom hooks** — useSkills, useLessons, useProgress, useChat (wrapping TanStack Query)
4. **Cliente API tipado** — 29 endpoints en client/src/lib/api.ts
5. **CI/CD** — GitHub Actions con jobs paralelos: backend (.NET build+test) + frontend (tsc+vite build)
6. **Seguridad IA integrada** — ServicioSeguridadIA conectado en ServicioChatIA: validación entrada 5 capas, validación salida, strikes y bloqueo
7. **Contenido niveles 2-3** — 60 lecciones nuevas (30 por nivel) con quizzes, escenarios y contenido real
8. **SPA serving** — Program.cs sirve React en producción con fallback a index.html
9. **Dockerfile** actualizado — Multi-stage con Node 20 para compilar React + .NET 8 runtime
10. **Tests frontend** — Vitest + Testing Library configurado con tests de API, AuthContext y páginas
11. **Refactoring páginas** — Todas las páginas usan custom hooks en lugar de queries inline
12. **.dockerignore** — Optimización de contexto Docker
13. **Tests unitarios ServicioChatIA** — 15 tests: sesiones, mensajes, seguridad 5 capas, fallback sin API key, historial
14. **Tests unitarios ServicioEscenas** — 10 tests: CRUD, generación automática, reordenamiento, actualización parcial
15. **Tests de integración** — 23 tests con WebApplicationFactory + InMemory DB: Health, Auth (registro/login/perfil), Skills, endpoints protegidos

### Estadísticas
- **13 páginas React**, 6 componentes, 4 archivos de hooks
- **90 lecciones totales** (30 × 3 niveles), 18 quizzes, 18 escenarios
- **66 tests backend** pasando (43 unit + 23 integration)
- **21 tests frontend** pasando (API client, AuthContext, HomePage)
- **Build frontend**: 97KB gzip
- **6 commits** en develop

### Qué queda pendiente
- Implementar streaming en ServicioChatIA (SSE)
- Configuración de producción (secrets, HTTPS, rate limiting real)
- Escenas visuales para lecciones de niveles 2-3
- Animaciones de avatar SVG más elaboradas

### Problemas encontrados
- Nombres de entidades incorrectos en seeder (EscenarioInteractivo→Escenario, Retroalimentacion→TextoRetroalimentacion)
- Agentes de background sin permisos de escritura (resuelto haciendo cambios manualmente)
- File lock de DLL durante builds paralelos (resuelto reintentando)

### Siguiente paso sugerido
Implementar SSE streaming en ServicioChatIA para mejorar la UX del chat con Aria. Alternativamente, configurar secrets y HTTPS para producción.

---

## Sesión 2 — 2026-03-22

### Qué se hizo
1. Commit inicial y push al repositorio GitHub (felcos/SkillUpAcademy)
2. Creación de rama develop (GitFlow)
3. **ServicioChatIA** — Chat con API de Anthropic vía HttpClient, fallback sin API key, historial, prompt por tipo sesión
4. **ServicioSeguridadIA** — 5 capas: rate limiting (IMemoryCache), formato, palabras prohibidas, inyección prompt (regex), clasificador IA (preparado)
5. **ServicioTts** — Fallback Web Speech API MVP + Azure Speech REST API
6. **ServicioEscenas** — Motor de escenas con generación automática desde markdown, actualización parcial, reordenamiento
7. **SembradoDatos** completo nivel 1:
   - 6 áreas × 5 lecciones = 30 lecciones (2 teoría + quiz + escenario + roleplay)
   - 30 preguntas quiz con 4 opciones y retroalimentación
   - 6 escenarios interactivos con 3 opciones
   - Escenas visuales para todas las lecciones teóricas
   - ConfiguracionAvatar Aria + 10 logros
8. **18 tests unitarios** (ServicioSeguridadIA: 10, ServicioQuiz: 8) — todos pasan
9. **Dockerfile** multi-stage (restore → build → runtime) con Alpine, usuario no-root, healthcheck
10. **docker-compose** actualizado con servicio API
11. Registrados todos los servicios en DI (10 servicios + 5 repositorios)

### Estadísticas
- **148 archivos .cs** creados
- **~8500 líneas de código**
- **0 errores, 0 warnings**
- **Tests pasando**: 18 unitarios + 2 placeholder = 20 total
- **5 commits** en develop

### Qué queda pendiente
- Frontend React completo (motor de escenas, avatar SVG, chat, quizzes, transiciones)
- Tests de integración (controladores API con WebApplicationFactory)
- Contenido de niveles 2 y 3 (Práctica y Dominio)
- Implementar streaming en ServicioChatIA (SSE)
- Integrar ServicioSeguridadIA en el flujo de ServicioChatIA (llamar ValidarEntradaAsync antes de enviar a Anthropic)
- Tests unitarios para ServicioChatIA y ServicioEscenas
- CI/CD pipeline (GitHub Actions)
- Configuración de producción (secrets, HTTPS, rate limiting real)

### Problemas encontrados
- Worktree de git no funcionaba por path de Windows
- Enums reales diferían de los asumidos (corregidos en 3 servicios + seeder)
- Using duplicado en Program.cs (corregido)
- PeticionRespuestaQuiz es record posicional (corregido en tests)

### Siguiente paso sugerido
Iniciar el frontend React con el motor de escenas del avatar. O bien, añadir tests de integración con WebApplicationFactory.

---

## Sesión 1 — 2026-03-22

### Qué se hizo
1-18. Ver detalle en el commit inicial (134 archivos .cs, Clean Architecture completa)

### Problemas encontrados
- Npgsql 10.x requiere .NET 10 → fijado versión 8.x
- System.IdentityModel.Tokens.Jwt no incluido en Infrastructure → añadido
