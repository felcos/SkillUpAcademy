# Estructura — SkillUp Academy

## Proyectos y Dependencias
```
SkillUpAcademy.Api → Core, Infrastructure
SkillUpAcademy.Infrastructure → Core
SkillUpAcademy.Core → (ninguna)
SkillUpAcademy.UnitTests → Core, Infrastructure
SkillUpAcademy.IntegrationTests → Api
client/ → React + TypeScript + Vite + Tailwind
```

## Entidades (19)
| Entidad | Tabla PostgreSQL | Descripcion |
|---------|-----------------|-------------|
| UsuarioApp | usuarios | Usuario extendido de Identity (+VozPreferida, VelocidadVoz, ProveedorTtsPreferido) |
| AreaHabilidad | areas_habilidad | 6 areas de soft skills |
| Nivel | niveles | 3 niveles por area |
| Leccion | lecciones | Lecciones individuales |
| PreguntaQuiz | preguntas_quiz | Preguntas de quiz |
| OpcionQuiz | opciones_quiz | Opciones de respuesta |
| Escenario | escenarios | Escenarios interactivos |
| OpcionEscenario | opciones_escenario | Opciones de escenario |
| ProgresoUsuario | progreso_usuario | Tracking de progreso |
| RespuestaQuizUsuario | respuestas_quiz_usuario | Respuestas a quizzes |
| EleccionEscenarioUsuario | elecciones_escenario_usuario | Decisiones en escenarios |
| SesionChatIA | sesiones_chat_ia | Sesiones con Aria |
| MensajeChatIA | mensajes_chat_ia | Mensajes del chat |
| Logro | logros | Achievements |
| LogroUsuario | logros_usuario | Logros desbloqueados |
| RegistroAbuso | registros_abuso | Logs de seguridad IA |
| EscenaLeccion | escenas_leccion | Motor de escenas del avatar |
| RecursoVisual | recursos_visuales | Banco de imagenes/assets |
| ConfiguracionAvatar | configuraciones_avatar | Personalidad de Aria |
| ProveedorTts | proveedores_tts | Configuración de proveedores TTS (Azure, ElevenLabs) |

## Servicios registrados en DI
| Interfaz | Implementacion | Lifetime |
|----------|---------------|----------|
| IServicioAutenticacion | ServicioAutenticacion | Scoped |
| IServicioHabilidades | ServicioHabilidades | Scoped |
| IServicioLecciones | ServicioLecciones | Scoped |
| IServicioQuiz | ServicioQuiz | Scoped |
| IServicioEscenario | ServicioEscenario | Scoped |
| IServicioProgreso | ServicioProgreso | Scoped |
| IServicioEscenas | ServicioEscenas | Scoped |
| IServicioSeguridadIA | ServicioSeguridadIA | Scoped |
| IServicioChatIA | ServicioChatIA | Scoped (HttpClient) |
| IServicioTts | ServicioTts | Scoped (HttpClient) |
| IServicioAdmin | ServicioAdmin | Scoped |
| IServicioAdminTts | ServicioAdminTts | Scoped |

## Repositorios
| Interfaz | Implementacion |
|----------|---------------|
| IRepositorioAreasHabilidad | RepositorioAreasHabilidad |
| IRepositorioLecciones | RepositorioLecciones |
| IRepositorioProgreso | RepositorioProgreso |
| IRepositorioChatIA | RepositorioChatIA |
| IRepositorioLogros | RepositorioLogros |

## Controladores API (9)
| Controlador | Ruta base | Endpoints | Auth |
|-------------|-----------|-----------|------|
| HealthController | api/v1/health | GET /, GET /ready | No |
| AuthController | api/v1/auth | register, login, refresh, logout, me, me/password | Mixta |
| SkillsController | api/v1/skills | /, /{slug}, /{slug}/levels, /{slug}/levels/{n} | Mixta |
| LessonsController | api/v1/lessons | /{id}, /{id}/scenes, /{id}/start, /{id}/complete | Si |
| QuizController | api/v1/lessons/{id}/quiz | /, /answer, /submit | Si |
| ScenarioController | api/v1/lessons/{id}/scenario | /, /choose | Si |
| ProgressController | api/v1/progress | /dashboard, /skill/{slug}, /achievements | Si |
| AiChatController | api/v1/ai | session/start, session/{id}/message, session/{id}/message/stream (SSE), history, end | Si |
| AdminController | api/v1/admin | resumen, usuarios, estadisticas-contenido, usuarios/{id}/alternar-bloqueo-ia, tts/proveedores (CRUD+alternar) | Si (Admin) |
| TtsController | api/v1/tts | voces, configuracion, preferencias, sintetizar, preview/{proveedor}/{id} | Si |

## Frontend — client/src/
```
client/src/
├── main.tsx                         — Entry point, providers
├── App.tsx                          — React Router (15 rutas)
├── pages/
│   ├── HomePage.tsx                 — Landing con hero, areas, features
│   ├── LoginPage.tsx                — Login con JWT
│   ├── RegisterPage.tsx             — Registro completo
│   ├── AreasPage.tsx                — Grid de 6 areas con progreso
│   ├── AreaDetailPage.tsx           — Niveles y lecciones por area
│   ├── LessonPage.tsx               — Motor de escenas + TTS
│   ├── QuizPage.tsx                 — Preguntas MCQ interactivas
│   ├── ScenarioPage.tsx             — Escenarios con decisiones
│   ├── DashboardPage.tsx            — Panel de progreso
│   ├── AchievementsPage.tsx         — Logros
│   ├── ChatPage.tsx                 — Chat con Aria (IA)
│   ├── ProfilePage.tsx              — Perfil de usuario
│   ├── AdminDashboardPage.tsx       — Panel admin (resumen + estadísticas)
│   ├── AdminUsersPage.tsx           — Gestión usuarios (bloqueo IA)
│   ├── AdminTtsPage.tsx             — Configuración proveedores TTS (admin)
│   └── NotFoundPage.tsx             — 404
├── components/
│   ├── layout/ (Navbar, Layout, ProtectedRoute, ProtectedAdminRoute)
│   ├── ui/ (LoadingSpinner, ErrorMessage)
│   └── avatar/ (AvatarAria — SVG animado con 4 estados)
├── hooks/ (useSkills, useLessons, useProgress, useChat, useAdmin, useTts)
├── contexts/ (AuthContext)
├── lib/ (api.ts — 34 endpoints tipados)
└── test/ (setup.ts, utils.tsx)
```

## Tests
| Proyecto | Archivos | Tests | Framework |
|----------|----------|-------|-----------|
| UnitTests | 7 archivos | 72 (71+1 skip) | xUnit + FluentAssertions + Moq + SQLite |
| IntegrationTests | 7 archivos | 37 | xUnit + WebApplicationFactory + InMemory |
| Frontend | 3 archivos | 21 | Vitest + Testing Library + jsdom |
| **Total** | **17** | **130** | |

## Features
- ✅ Estructura Clean Architecture
- ✅ Modelo de datos (19 entidades, 10 enums)
- ✅ PostgreSQL con snake_case + jsonb
- ✅ Autenticacion (Identity + JWT HS256)
- ✅ API REST completa (8 controladores, 29 endpoints)
- ✅ Integracion IA (Anthropic Claude API)
- ✅ Seguridad IA (5 capas anti-abuso + strikes)
- ✅ Motor de escenas del avatar
- ✅ TTS (Web Speech API)
- ✅ Frontend React (13 paginas, 6 componentes, 4 hooks)
- ✅ Contenido educativo (90 lecciones, 18 quizzes, 18 escenarios)
- ✅ Testing (94 tests: 48 unit + 25 integration + 21 frontend)
- ✅ CI/CD (GitHub Actions)
- ✅ Docker (multi-stage + docker-compose)
- ✅ SPA serving desde .NET
- ✅ Logging (Serilog)
- ✅ SSE streaming en chat (IAsyncEnumerable + text/event-stream)
- ✅ Escenas visuales completas (180 escenas: 60 por nivel)
- ✅ Configuracion produccion (HTTPS, secrets, CORS, HSTS)
- ✅ Admin dashboard (backend + frontend, 4 endpoints, 2 páginas)
- ✅ Avatar SVG animado (4 estados: idle, hablando, pensando, saludando)
- ✅ Roles y autorización por rol en JWT
- ✅ Tests para módulo admin (6 unitarios + 4 integración)
- ✅ Rate limiting nativo .NET 8 (3 políticas: general, ia, tts)
- 📋 Despliegue producción con secrets reales
- 📋 Notificaciones en tiempo real
