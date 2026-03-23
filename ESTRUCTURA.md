# Estructura — SkillUp Academy

## Proyectos y Dependencias
```
SkillUpAcademy.Api → Core, Infrastructure
SkillUpAcademy.Infrastructure → Core
SkillUpAcademy.Core → (ninguna)
SkillUpAcademy.UnitTests → Core, Infrastructure
SkillUpAcademy.IntegrationTests → Api
```

## Entidades (19)
| Entidad | Tabla PostgreSQL | Descripción |
|---------|-----------------|-------------|
| UsuarioApp | usuarios | Usuario extendido de Identity |
| AreaHabilidad | areas_habilidad | 6 áreas de soft skills |
| Nivel | niveles | 3 niveles por área |
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
| **EscenaLeccion** | escenas_leccion | **Motor de escenas del avatar** |
| **RecursoVisual** | recursos_visuales | **Banco de imágenes/assets** |
| **ConfiguracionAvatar** | configuraciones_avatar | **Personalidad de Aria** |

## Frontend — client/src/
```
client/src/
├── main.tsx                    — Entry point, providers (QueryClient, Router, Auth)
├── App.tsx                     — React Router con rutas protegidas
├── pages/
│   ├── AreasPage.tsx           — Listado de 6 áreas de habilidades
│   ├── AreaDetailPage.tsx      — Detalle de área con niveles y lecciones
│   ├── LessonPage.tsx          — Motor de escenas del avatar + TTS
│   ├── QuizPage.tsx            — Preguntas con opciones y retroalimentación
│   ├── ScenarioPage.tsx        — Escenarios interactivos con decisiones
│   ├── DashboardPage.tsx       — Panel de progreso del usuario
│   ├── AchievementsPage.tsx    — Logros desbloqueados y pendientes
│   └── ChatPage.tsx            — Chat con IA (Aria)
├── components/                 — Componentes reutilizables (Layout, Avatar, etc.)
├── contexts/                   — React Context (AuthContext, etc.)
└── lib/                        — Utilidades, API client, tipos compartidos
```

**Stack frontend:** React + TypeScript + Vite + Tailwind CSS + React Router + TanStack Query

## Features
- ✅ Estructura de solución Clean Architecture
- ✅ Modelo de datos completo (19 entidades, 10 enums)
- ✅ Configuración PostgreSQL con snake_case
- ✅ Logging con Serilog
- 🔨 Autenticación (Identity + JWT)
- 📋 API REST completa
- 📋 Integración IA (Claude)
- 📋 Motor de escenas del avatar
- 📋 TTS (Text-to-Speech)
- 📋 Frontend React
- 📋 Seeders con contenido real
- 📋 Tests
- 📋 Docker
