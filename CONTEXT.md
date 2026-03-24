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

## Estado actual (actualizado 2026-03-24 — Sesión 5)
### Backend ✅
- ✅ Solución .NET 8 con Clean Architecture (Api, Core, Infrastructure)
- ✅ 19 entidades, 10 enums, 19 configuraciones Fluent API (snake_case, jsonb)
- ✅ 11 servicios + 5 repositorios registrados en DI
- ✅ 9 controladores con 34 endpoints (29 REST + 1 SSE streaming + 4 admin)
- ✅ Seguridad IA: 5 capas anti-abuso integradas en ServicioChatIA
- ✅ SSE streaming en chat IA (IAsyncEnumerable + Anthropic streaming API)
- ✅ SembradoDatos: 90 lecciones, 18 quizzes, 18 escenarios, 180 escenas, 10 logros
- ✅ Middleware: cabeceras seguridad + manejo excepciones global
- ✅ Admin dashboard: ServicioAdmin, AdminController, SembradoAdmin (rol + usuario)
- ✅ Roles en JWT claims + autorización por rol

### Frontend ✅
- ✅ 15 páginas React + TypeScript + Tailwind CSS + Vite
- ✅ 7 componentes reutilizables, 5 custom hooks (wrapping TanStack Query)
- ✅ Cliente API tipado con 34 endpoints (29 REST + 1 streaming + 4 admin)
- ✅ Motor de escenas con TTS (Web Speech API)
- ✅ Chat con renderizado progresivo (streaming SSE)
- ✅ Avatar SVG animado con 4 estados (idle, hablando, pensando, saludando)
- ✅ Admin: dashboard + gestión usuarios + ProtectedAdminRoute

### Testing ✅
- ✅ 54 tests unitarios backend (ServicioSeguridadIA, ServicioQuiz, ServicioChatIA, ServicioEscenas, ServicioAdmin)
- ✅ 29 tests de integración (WebApplicationFactory + InMemory DB)
- ✅ 21 tests frontend (Vitest + Testing Library)
- ✅ **Total: 104 tests (103 pasando + 1 skip), 0 fallando**

### DevOps ✅
- ✅ CI/CD GitHub Actions (backend + frontend en paralelo)
- ✅ Dockerfile multi-stage (Node 20 + .NET 8)
- ✅ docker-compose con PostgreSQL + docker-compose.production.yml
- ✅ SPA serving (MapFallbackToFile)
- ✅ Configuración producción (appsettings.Production.json, .env.example, HSTS, HTTPS, CORS configurable)

### Pendiente 📋
- ✅ Tests para módulo admin (6 unitarios + 4 integración)
- 📋 Despliegue a producción con secrets reales y dominio
- 📋 Rate limiting real (Redis en vez de IMemoryCache)
- 📋 Notificaciones en tiempo real
- 📋 Video AI generado para avatar V2
