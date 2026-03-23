# Sesión — SkillUp Academy

## Sesión 3 — 2026-03-23

### Qué se hizo
1. **Frontend React completo** — Aplicación creada con Vite + TypeScript + Tailwind CSS
2. **10 páginas implementadas** con React Router y TanStack Query:
   - **Home** — Landing page pública
   - **Login / Register** — Autenticación con formularios
   - **Areas** — Listado de las 6 áreas de habilidades
   - **AreaDetail** — Detalle de área con niveles y lecciones
   - **Lesson** — Motor de escenas del avatar con TTS integrado
   - **Quiz** — Preguntas con opciones y retroalimentación
   - **Scenario** — Escenarios interactivos con decisiones
   - **Dashboard** — Panel de progreso del usuario
   - **Achievements** — Logros desbloqueados y pendientes
   - **Chat** — Conversación con IA (Aria)
3. **Providers configurados** — QueryClient, React Router, AuthContext
4. **Rutas protegidas** — Redirección a login para páginas autenticadas

### Qué queda pendiente
- Tests frontend (React Testing Library + Vitest)
- CI/CD pipeline (GitHub Actions)
- Contenido de niveles 2 y 3 (Práctica y Dominio)
- Tests de integración backend (controladores API con WebApplicationFactory)
- Implementar streaming en ServicioChatIA (SSE)
- Integrar ServicioSeguridadIA en el flujo de ServicioChatIA
- Tests unitarios para ServicioChatIA y ServicioEscenas
- Configuración de producción (secrets, HTTPS, rate limiting real)

### Problemas encontrados
- (ninguno relevante en esta sesión)

### Siguiente paso sugerido
Añadir tests frontend con Vitest + React Testing Library, o bien implementar CI/CD con GitHub Actions.

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
