# Sesión — SkillUp Academy

## Sesión 2 — 2026-03-22 (continuación)

### Qué se hizo
1. Commit inicial y push al repositorio GitHub (felcos/SkillUpAcademy)
2. Creación de rama develop (GitFlow)
3. **ServicioChatIA** — Chat con API de Anthropic vía HttpClient directo, fallback sin API key, historial de conversación, prompt personalizado por tipo de sesión
4. **ServicioSeguridadIA** — 5 capas de protección: rate limiting (IMemoryCache), validación formato, filtro palabras prohibidas, detección inyección de prompt (regex), clasificador IA (preparado)
5. **ServicioTts** — Fallback Web Speech API para MVP + integración Azure Speech REST API para producción
6. **ServicioEscenas** — Motor de escenas con generación automática desde contenido markdown, actualización parcial, reordenamiento
7. **SembradoDatos** completo:
   - 6 áreas de habilidad con colores, iconos y descripciones
   - 18 niveles (3 por área: Fundamentos, Práctica, Dominio)
   - 30 lecciones de nivel 1 (5 por área: 2 teoría + quiz + escenario + roleplay)
   - 30 preguntas de quiz con 4 opciones y retroalimentación cada una
   - 6 escenarios interactivos con 3 opciones (positiva, neutral, negativa)
   - Escenas visuales para todas las lecciones teóricas (5 escenas por lección)
   - 1 ConfiguracionAvatar por defecto (Aria)
   - 10 logros iniciales
8. Registro de todos los servicios en DI (ExtensionesDeServicios.cs)
9. Program.cs actualizado con MigrateAsync + SembrarAsync en desarrollo

### Estadísticas
- **141 archivos .cs** creados
- **~7000 líneas de código** (sin contar migraciones)
- **0 errores, 0 warnings**
- **Tests pasando** (2/2)
- **Contenido educativo real** en 6 áreas de soft skills

### Qué queda pendiente
- Tests unitarios reales (ServicioChatIA, ServicioSeguridadIA, ServicioQuiz, etc.)
- Tests de integración (controladores API)
- Dockerfile para la API
- Frontend React completo (motor de escenas, avatar SVG, chat, quizzes, transiciones)
- Contenido de niveles 2 y 3 (Práctica y Dominio)
- Implementar streaming en ServicioChatIA
- Integrar ServicioSeguridadIA en el flujo de ServicioChatIA

### Problemas encontrados
- Worktree de git no funcionaba por path de Windows → se implementó secuencialmente
- Enums reales diferían de los asumidos (AccionTomada, TipoSesionIA, TipoViolacion, TipoContenidoVisual) → corregido

### Siguiente paso sugerido
Crear tests unitarios para los servicios clave (ServicioChatIA, ServicioSeguridadIA, ServicioQuiz) y luego iniciar el frontend React.

---

## Sesión 1 — 2026-03-22

### Qué se hizo
1. Revisión completa de las 7 specs del proyecto
2. Propuestas de 200 mejoras (100 originales + 100 de avatar/CMS/empresa)
3. Decisiones de arquitectura: PostgreSQL, React, HttpClient directo, avatar SVG
4. Creación de solución .NET 8 con Clean Architecture (Api, Core, Infrastructure)
5. 19 entidades + 10 enums del dominio
6. DbContext + 19 configuraciones Fluent API para PostgreSQL (snake_case, jsonb, timestamptz)
7. 10 interfaces de servicios + 5 interfaces de repositorios
8. 28 DTOs organizados en 7 carpetas
9. 4 excepciones del dominio
10. 5 repositorios implementados con EF Core
11. 6 servicios implementados (Auth, Habilidades, Lecciones, Quiz, Escenario, Progreso)
12. 8 controladores API REST (Health, Auth, Skills, Lessons, Quiz, Scenario, AiChat, Progress)
13. 2 middleware (excepciones, cabeceras de seguridad)
14. Program.cs completo con Serilog, PostgreSQL, Identity, JWT (HS256), CORS, Swagger
15. Extensión de DI para registrar todos los servicios
16. Migración inicial de EF Core generada
17. docker-compose.yml con PostgreSQL 16
18. Archivos de proyecto: CONTEXT.md, CHANGELOG.md, ESTRUCTURA.md, .gitignore, .editorconfig

### Problemas encontrados
- Npgsql 10.x requiere .NET 10 → resuelto fijando versión 8.x
- System.IdentityModel.Tokens.Jwt no incluido en Infrastructure → añadido
