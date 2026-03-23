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

## Estado actual
- ✅ Solución .NET 8 creada con 3 proyectos + 2 de tests
- ✅ 19 entidades del dominio (incluyendo 3 nuevas para el motor de escenas)
- ✅ 10 enums
- ✅ DbContext con 18 DbSets configurado para PostgreSQL
- ✅ 19 configuraciones Fluent API (snake_case, jsonb, timestamptz)
- ✅ Paquetes NuGet instalados (Npgsql, Identity, Serilog, JWT, RateLimit)
- ✅ Compila sin errores ni warnings
- ✅ Program.cs configurado con Serilog, PostgreSQL, Identity, CORS, Swagger
- ✅ Servicios de IA: ChatIA, SeguridadIA, TTS, Escenas — registrados en DI
- ✅ SembradoDatos nivel 1 completo (6 áreas × 5 lecciones, quizzes, escenarios, logros)
- ✅ 18 tests unitarios pasando (ServicioSeguridadIA + ServicioQuiz)
- ✅ Docker (Dockerfile multi-stage + docker-compose)
- ✅ **Frontend React completo** — 10 páginas con React Router, TanStack Query, Tailwind CSS
  - Home, Login, Register, Areas, AreaDetail, Lesson (motor escenas + TTS), Quiz, Scenario, Dashboard, Achievements, Chat IA
- 📋 Pendiente: Tests frontend (Vitest + React Testing Library)
- 📋 Pendiente: CI/CD pipeline (GitHub Actions)
- 📋 Pendiente: Contenido niveles 2 y 3
