# SkillUp Academy — Instrucciones Maestras para Claude Code

## ¿Qué es este proyecto?

SkillUp Academy es una aplicación web completa para enseñar soft skills profesionales. Este paquete de documentación contiene **todas las especificaciones** necesarias para construir el proyecto de principio a fin.

## Documentos de Especificación

Lee **TODOS** estos documentos antes de empezar a construir:

| Orden | Archivo | Contenido |
|-------|---------|-----------|
| 1 | `01-PROJECT-OVERVIEW.md` | Visión general, stack tecnológico, funcionalidades, fases |
| 2 | `02-DATABASE-SCHEMA.md` | Todas las tablas, relaciones, índices, seed data |
| 3 | `03-API-ENDPOINTS.md` | Endpoints REST completos con request/response examples |
| 4 | `04-AI-INTEGRATION-SECURITY.md` | System prompts, TTS, 5 capas anti-abuso, seguridad general |
| 5 | `05-FRONTEND-UI.md` | Diseño visual, paleta, tipografía, wireframes ASCII, componentes, responsive |
| 6 | `06-CONTENT-CURRICULUM.md` | Las 6 áreas con niveles, lecciones, quizzes, escenarios — contenido real |
| 7 | `07-PROJECT-STRUCTURE.md` | Estructura de carpetas, arquitectura, paquetes, docker, orden de construcción |

## Resumen Ejecutivo del Proyecto

- **Stack:** .NET 8 Web API + React (TypeScript) + SQL Server + EF Core
- **Auth:** ASP.NET Core Identity + JWT
- **IA:** Anthropic Claude API con system prompts específicos por tipo de sesión
- **Audio:** Azure TTS (o Web Speech API como fallback)
- **6 áreas de soft skills:** Comunicación, Liderazgo, Trabajo en Equipo, Inteligencia Emocional, Small Talk & Networking, Persuasión e Influencia
- **3 niveles por área:** Fundamentos → Práctica → Dominio
- **4 tipos de lección:** Teoría, Quiz, Escenario interactivo, Roleplay con IA
- **Seguridad IA:** 5 capas (rate limit → input validation → keyword filter → AI classifier → output filter)
- **Gamificación:** Puntos, logros/achievements, rachas diarias
- **Responsive:** PC, tablet, móvil
- **Estilo:** Corporativo profesional (azul marino, tipografía serif/sans-serif)

## Cómo usar estos documentos con Claude Code

### Opción A: Proyecto completo de una vez
```
Lee todos los archivos .md en esta carpeta de specs.
Construye el proyecto completo SkillUp Academy siguiendo el orden de construcción 
detallado en 07-PROJECT-STRUCTURE.md.
Empieza por el backend (.NET 8), luego la base de datos con seed data completo,
después los servicios de IA y seguridad, y finalmente el frontend React.
```

### Opción B: Por partes (recomendado para proyectos grandes)

**Parte 1 — Backend Core:**
```
Lee 01-PROJECT-OVERVIEW.md, 02-DATABASE-SCHEMA.md y 07-PROJECT-STRUCTURE.md.
Crea la solución .NET 8, los 4 proyectos (Api, Core, Infrastructure, Web), 
las entidades, el DbContext, las configuraciones EF Core y la migración inicial.
```

**Parte 2 — Seed Data:**
```
Lee 06-CONTENT-CURRICULUM.md y 02-DATABASE-SCHEMA.md.
Crea todos los seeders con el contenido completo del Nivel 1 de las 6 áreas
(teorías completas, quizzes con 4 opciones y feedback, escenarios con 3 opciones).
Incluye la estructura de niveles 2 y 3, y los 15 achievements.
```

**Parte 3 — API y Servicios:**
```
Lee 03-API-ENDPOINTS.md y 07-PROJECT-STRUCTURE.md.
Implementa las interfaces, repositorios, servicios y controladores.
Incluye AuthService con JWT, validación con FluentValidation,
y middleware de errores y rate limiting.
```

**Parte 4 — IA y Seguridad:**
```
Lee 04-AI-INTEGRATION-SECURITY.md.
Implementa AnthropicClient, AiChatService con los 3 tipos de sesión,
AiSafetyService con las 5 capas de protección, y TtsService.
```

**Parte 5 — Frontend:**
```
Lee 05-FRONTEND-UI.md y 03-API-ENDPOINTS.md.
Crea el proyecto React con TypeScript + Tailwind.
Implementa todas las páginas y componentes listados en el documento.
Asegura que es totalmente responsive.
```

**Parte 6 — Docker y Tests:**
```
Lee 07-PROJECT-STRUCTURE.md.
Crea el Dockerfile, docker-compose.yml, y los proyectos de test
(unit tests para servicios críticos, integration tests para la API).
```

## Notas Importantes

1. **El contenido del seed data debe ser REAL y COMPLETO**, no placeholders. Las micro-lecciones deben tener 2-3 párrafos reales, los quizzes 4 opciones con feedback, los escenarios situaciones realistas de oficina.

2. **La seguridad de la IA es CRÍTICA.** El asistente "Aria" SOLO puede hablar de soft skills. Implementar las 5 capas descritas en el documento de seguridad.

3. **El audio es una feature importante.** La IA debe poder "hablar" las lecciones y sus respuestas. Implementar con Azure TTS si hay key disponible, o Web Speech API como fallback.

4. **El diseño debe ser profesional y corporativo,** no colorido ni infantil. Seguir la paleta y tipografía del documento 05.

5. **Todo en español** (contenido, UI, mensajes de error visibles al usuario). Código y nombres técnicos en inglés.
