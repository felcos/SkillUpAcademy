# SkillUp Academy — Contexto del Proyecto

## Qué es
Plataforma web de aprendizaje de soft skills (habilidades blandas) profesionales con asistente IA (Aria) que enseña mediante un avatar animado con pizarra visual, lecciones interactivas, quizzes, escenarios y roleplay.

## Por qué existe
Para que profesionales mejoren habilidades clave (comunicación, liderazgo, trabajo en equipo, inteligencia emocional, networking, persuasión) de forma práctica e interactiva, no solo teórica.

## Stack
| Capa | Tecnología | Por qué |
|------|-----------|---------|
| Backend | .NET 8 (ASP.NET Core Web API) | LTS, robusto, ecosistema empresarial |
| Frontend | React + TypeScript + Tailwind | Motor de escenas del avatar necesita framework reactivo |
| Base de datos | PostgreSQL + EF Core 8 | Gratis, jsonb para escenas, snake_case nativo |
| Auth | ASP.NET Core Identity + JWT (HS256) | Nativo de .NET, simple para MVP |
| IA | Anthropic Claude API (HttpClient directo) | Sin SDK de terceros, control total del streaming |
| TTS | Web Speech API (fallback) → Azure/ElevenLabs (premium) | Coste 0 para MVP |
| Avatar | SVG/CSS animado con lip-sync | Parece video, coste 0 |

## Decisiones tomadas
| Fecha | Decisión |
|-------|----------|
| 2026-03-22 | PostgreSQL en vez de SQL Server (gratis, jsonb, Docker ligero) |
| 2026-03-22 | React + TS en vez de Razor Views (motor de escenas necesita framework reactivo) |
| 2026-03-22 | HttpClient directo en vez de Anthropic.SDK NuGet (es de terceros, no oficial) |
| 2026-03-22 | HS256 para JWT en vez de RS256 (más simple para MVP, sin gestión de certs) |
| 2026-03-22 | Clean Architecture con 3 proyectos: Api, Core, Infrastructure |
| 2026-03-22 | Avatar SVG animado para MVP, video AI generado para V2 |
| 2026-03-22 | Context API de React en vez de Zustand (suficiente hasta demostrar lo contrario) |
| 2026-03-22 | IMemoryCache en vez de Redis (suficiente para instancia única) |
| 2026-03-22 | Nombres en español para entidades, propiedades y servicios |
| 2026-03-22 | Motor de escenas como pieza central nueva (EscenaLeccion, RecursoVisual, ConfiguracionAvatar) |

## Decisiones tomadas (cont.)
| Fecha | Decisión |
|-------|----------|
| 2026-03-23 | SSE streaming con IAsyncEnumerable + text/event-stream (no SignalR, más simple y compatible) |
| 2026-03-23 | Protocolo SSE propio con tipos texto/reemplazo/fin (permite filtrado de seguridad post-stream) |
| 2026-03-24 | Roles en JWT claims para autorización admin (no políticas separadas, suficiente con roles nativos de Identity) |
| 2026-03-24 | Validación JWT en producción: rechazar clave por defecto, forzar secrets reales via variables de entorno |
| 2026-03-24 | Avatar SVG con 4 estados animados (no Lottie/canvas — SVG nativo es más ligero y accesible) |
| 2026-03-24 | Rate limiting nativo .NET 8 (no AspNetCoreRateLimit — paquete nativo es más simple y no requiere dependencia externa) |
| 2026-03-24 | TTS multi-proveedor (Azure + ElevenLabs + WebSpeech) configurable desde admin, preferencias por usuario en BD |
| 2026-03-24 | 12 áreas de habilidades (6 nuevas con partial class pattern para seeding modular) |

## Estado actual (actualizado 2026-03-24 — Sesión 8)
### Backend ✅
- ✅ Solución .NET 8 con Clean Architecture (Api, Core, Infrastructure)
- ✅ 20 entidades, 10 enums, 20 configuraciones Fluent API (snake_case, jsonb)
- ✅ 12 servicios + 5 repositorios registrados en DI
- ✅ 10 controladores con 44 endpoints (29 REST + 1 SSE streaming + 4 admin + 4 TTS usuario + 4 TTS admin + 2 extras)
- ✅ Seguridad IA: 5 capas anti-abuso integradas en ServicioChatIA
- ✅ SSE streaming en chat IA (IAsyncEnumerable + Anthropic streaming API)
- ✅ SembradoDatos: 180 lecciones (12 áreas × 3 niveles × 5), 24 quizzes, 24 escenarios, 240 escenas, 10 logros
- ✅ Middleware: cabeceras seguridad + manejo excepciones global
- ✅ Admin dashboard: ServicioAdmin, AdminController, SembradoAdmin (rol + usuario)
- ✅ Roles en JWT claims + autorización por rol
- ✅ TTS multi-proveedor (Azure + ElevenLabs + WebSpeech) configurable desde admin

### Frontend ✅
- ✅ 16 páginas React + TypeScript + Tailwind CSS + Vite
- ✅ 7 componentes reutilizables, 5 custom hooks (wrapping TanStack Query)
- ✅ Cliente API tipado con 34 endpoints (29 REST + 1 streaming + 4 admin)
- ✅ Motor de escenas con TTS (Web Speech API)
- ✅ Chat con renderizado progresivo (streaming SSE)
- ✅ Avatar SVG animado con 4 estados (idle, hablando, pensando, saludando)
- ✅ Admin: dashboard + gestión usuarios + ProtectedAdminRoute

### Testing ✅
- ✅ 54 tests unitarios backend (ServicioSeguridadIA, ServicioQuiz, ServicioChatIA, ServicioEscenas, ServicioAdmin)
- ✅ 31 tests de integración (WebApplicationFactory + InMemory DB)
- ✅ 21 tests frontend (Vitest + Testing Library)
- ✅ **Total: 130 tests (129 pasando + 1 skip), 0 fallando**

### DevOps ✅
- ✅ CI/CD GitHub Actions (backend + frontend en paralelo)
- ✅ Dockerfile multi-stage (Node 20 + .NET 8)
- ✅ docker-compose con PostgreSQL + docker-compose.production.yml
- ✅ SPA serving (MapFallbackToFile)
- ✅ Configuración producción (appsettings.Production.json, .env.example, HSTS, HTTPS, CORS configurable)

### Pendiente 📋
- ✅ Tests para módulo admin (6 unitarios + 4 integración)
- ✅ Despliegue a producción en skillupacademy.felcos.es
- ✅ Rate limiting nativo .NET 8 con 3 políticas (general, ia, tts) desde appsettings
- ✅ TTS multi-proveedor (Azure + ElevenLabs + WebSpeech) configurable desde admin
- ✅ 12 áreas de habilidades (6 nuevas: Resolución de Conflictos, Pensamiento Crítico, Gestión del Tiempo, Negociación, Presentaciones en Público, Adaptabilidad y Resiliencia)
- 📋 Desplegar nuevas áreas a producción (re-seed BD)
- 📋 Mejoras admin (CRUD completo, gestión roles, estadísticas avanzadas)
- 📋 Notificaciones en tiempo real
- 📋 Video AI generado para avatar V2
