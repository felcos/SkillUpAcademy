# SkillUp Academy — Visión General del Proyecto

## 1. Descripción

**SkillUp Academy** es una plataforma web de aprendizaje de soft skills (habilidades blandas) profesionales. La aplicación guía al usuario a través de un itinerario formativo personalizado con lecciones teóricas, quizzes, escenarios interactivos y asistencia por IA con voz.

## 2. Stack Tecnológico

| Capa | Tecnología |
|------|-----------|
| Backend | .NET 8 (ASP.NET Core Web API) |
| Frontend | Blazor WebAssembly (o Razor Pages + JS interop) — alternativa aceptable: React SPA servida desde .NET |
| Base de datos | SQL Server (o PostgreSQL) con Entity Framework Core |
| Autenticación | ASP.NET Core Identity + JWT Bearer Tokens |
| IA conversacional | Anthropic Claude API (claude-sonnet-4-20250514) |
| Text-to-Speech | Azure Cognitive Services Speech SDK (o Web Speech API como fallback gratuito) |
| Hosting | Preparado para Docker / Azure App Service |
| Cache | Redis (opcional, para sesiones y rate limiting) |

## 3. Módulos de Soft Skills

| # | Área | Icono | Color principal |
|---|------|-------|----------------|
| 1 | Comunicación | 💬 | #0F4C81 (azul marino) |
| 2 | Liderazgo | 🏛️ | #2D5016 (verde oscuro) |
| 3 | Trabajo en Equipo | 🤝 | #6B3FA0 (púrpura) |
| 4 | Inteligencia Emocional | 🧠 | #B8860B (dorado oscuro) |
| 5 | Small Talk & Networking | ☕ | #8B4513 (marrón cálido) |
| 6 | Persuasión e Influencia | 🎯 | #A0153E (rojo oscuro) |

## 4. Estructura de Aprendizaje

```
Área (6 áreas)
 └── Nivel (3 niveles por área)
      ├── Nivel 1: Fundamentos
      ├── Nivel 2: Práctica
      └── Nivel 3: Dominio
           └── Lecciones (4-6 por nivel)
                ├── 📖 Micro-lección (teoría + tips)
                ├── ❓ Quiz (opción múltiple con feedback)
                ├── 🎭 Escenario (elige tu aventura)
                └── 🤖 Roleplay con IA (nivel 3)
```

**Total estimado:** 6 áreas × 3 niveles × 5 lecciones = **90 unidades de contenido**

## 5. Funcionalidades Principales

### 5.1 Para el Usuario
- Registro e inicio de sesión seguro
- Dashboard personal con progreso por área y nivel
- Navegación por áreas → niveles → lecciones
- Lecciones interactivas (teoría, quiz, escenarios)
- Asistente IA con voz que guía, explica y da feedback
- Sistema de puntos / progreso (gamificación ligera)
- Modo práctica libre con la IA (roleplay de situaciones)
- Responsive: funciona en PC, tablet y móvil

### 5.2 Para Administración (futuro, fase 2)
- Panel admin para gestionar contenido
- Estadísticas de uso y progreso de usuarios
- Gestión de usuarios

## 6. Fases de Desarrollo

### Fase 1 — MVP (este proyecto)
- Auth (registro, login, perfil)
- Base de datos con esquema completo
- Las 6 áreas con contenido del nivel 1 completo (seed data)
- Niveles 2 y 3 con estructura lista y al menos 2 lecciones cada uno
- Quizzes funcionales con puntuación
- Escenarios interactivos
- Integración IA para guía y feedback
- Audio (TTS) para las lecciones y la IA
- Responsive design
- Seguridad anti-abuso en IA

### Fase 2 — Post-MVP
- Panel de administración
- Certificados de completitud
- Más contenido por nivel
- Ranking / leaderboard
- Notificaciones push
- PWA (Progressive Web App)

## 7. Requisitos No Funcionales

- **Rendimiento:** Respuesta < 500ms para páginas, < 3s para respuestas IA
- **Seguridad:** OWASP Top 10, protección XSS/CSRF, rate limiting
- **Accesibilidad:** WCAG 2.1 nivel AA mínimo
- **Internacionalización:** Español como idioma principal, estructura preparada para i18n
- **SEO:** Meta tags, Open Graph, sitemap
- **Logging:** Serilog con structured logging
- **Tests:** Unit tests para servicios críticos, integration tests para API
