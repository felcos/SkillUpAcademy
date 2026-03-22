# SkillUp Academy — Frontend y Diseño UI

## 1. Tecnología Frontend

### Opción Recomendada: React SPA + ASP.NET Core como API
- **React 18+** con TypeScript
- **Vite** como bundler
- **Tailwind CSS** para estilos
- **React Router v6** para navegación
- **Axios** o **fetch** para llamadas API
- **Zustand** o **Context API** para estado global
- Servido como archivos estáticos desde .NET (`wwwroot`) o como proyecto separado

### Alternativa: Blazor WebAssembly
- Todo en C# (ventaja si el equipo es .NET puro)
- MudBlazor como librería de componentes
- Menor ecosistema de componentes vs React

---

## 2. Estilo Visual: Corporativo Profesional

### 2.1 Paleta de Colores
```css
:root {
  /* Fondos */
  --bg-primary: #FAFBFC;        /* gris muy claro */
  --bg-secondary: #FFFFFF;       /* blanco tarjetas */
  --bg-dark: #1A1D23;           /* modo oscuro / hero */
  
  /* Textos */
  --text-primary: #1A1D23;      /* casi negro */
  --text-secondary: #5A6171;    /* gris medio */
  --text-muted: #9CA3B0;        /* gris claro */
  
  /* Accentos corporativos */
  --brand-primary: #0F4C81;     /* azul marino principal */
  --brand-secondary: #1A6FB5;   /* azul intermedio */
  --brand-light: #E8F1FA;       /* azul muy claro para fondos */
  
  /* Colores de cada skill (se usan como acento por sección) */
  --skill-comunicacion: #0F4C81;
  --skill-liderazgo: #2D5016;
  --skill-equipo: #6B3FA0;
  --skill-emocional: #B8860B;
  --skill-networking: #8B4513;
  --skill-persuasion: #A0153E;
  
  /* Estados */
  --success: #16A34A;
  --warning: #D97706;
  --error: #DC2626;
  --info: #0F4C81;
  
  /* Bordes y sombras */
  --border-light: #E5E7EB;
  --shadow-sm: 0 1px 3px rgba(0,0,0,0.08);
  --shadow-md: 0 4px 12px rgba(0,0,0,0.1);
  --shadow-lg: 0 10px 30px rgba(0,0,0,0.12);
}
```

### 2.2 Tipografía
```css
/* Títulos: serif profesional */
--font-display: 'Playfair Display', Georgia, serif;

/* Cuerpo: sans-serif limpio */
--font-body: 'DM Sans', 'Segoe UI', sans-serif;

/* Código/datos: monospace */
--font-mono: 'JetBrains Mono', monospace;

/* Escala tipográfica */
--text-xs: 0.75rem;    /* 12px */
--text-sm: 0.875rem;   /* 14px */
--text-base: 1rem;     /* 16px */
--text-lg: 1.125rem;   /* 18px */
--text-xl: 1.25rem;    /* 20px */
--text-2xl: 1.5rem;    /* 24px */
--text-3xl: 2rem;      /* 32px */
--text-4xl: 2.5rem;    /* 40px */
```

### 2.3 Espaciado y Layout
- Sistema de 8px grid
- Contenedor máximo: 1280px
- Padding lateral: 24px (móvil), 48px (desktop)
- Border radius: 8px (tarjetas), 12px (modales), 9999px (pills/badges)
- Transiciones: `all 0.2s ease` por defecto

### 2.4 Componentes Clave de Diseño
- **Tarjetas de skill:** borde izquierdo con el color de la skill, hover con sombra elevada
- **Progress bars:** borde redondeado, gradiente sutil, animación de llenado
- **Badges de nivel:** icono circular con número, estados: bloqueado (gris), activo (color), completado (dorado)
- **Botones:** filled (primario), outlined (secundario), ghost (terciario)
- **Chat IA:** burbuja de mensaje con avatar de "Aria", indicador de "escribiendo...", botón de audio

---

## 3. Pantallas y Navegación

### 3.1 Mapa de Pantallas

```
Landing Page (público)
├── /login
├── /register
├── /forgot-password
│
└── [Autenticado] ─────────────────────────────────
    ├── /dashboard                    ← Pantalla principal
    │
    ├── /skills                       ← Grid de 6 áreas
    │   └── /skills/:slug             ← Detalle área + niveles
    │       └── /skills/:slug/:level  ← Lista lecciones del nivel
    │           └── /lesson/:id       ← Lección interactiva
    │               ├── Teoría
    │               ├── Quiz
    │               ├── Escenario
    │               └── Roleplay IA
    │
    ├── /practice                     ← Práctica libre con IA
    │
    ├── /achievements                 ← Logros y medallas
    │
    ├── /profile                      ← Perfil y ajustes
    │   ├── Datos personales
    │   ├── Preferencias (audio, idioma)
    │   └── Cambiar contraseña
    │
    └── /admin (solo admin)           ← Panel admin (fase 2)
```

### 3.2 Responsive Breakpoints
```css
/* Mobile first */
sm: 640px    /* teléfono grande / landscape */
md: 768px    /* tablet portrait */
lg: 1024px   /* tablet landscape / laptop */
xl: 1280px   /* desktop */
2xl: 1536px  /* desktop grande */
```

---

## 4. Detalle de Pantallas Principales

### 4.1 Landing Page (público)
- Hero section: título grande, subtítulo, CTA "Empieza Gratis"
- Sección: Las 6 áreas de skill con iconos
- Sección: Cómo funciona (3 pasos)
- Sección: Testimonios / estadísticas
- Footer con enlaces legales
- **Diseño:** fondo oscuro hero → blanco contenido → gris claro footer

### 4.2 Dashboard (tras login)
```
┌─────────────────────────────────────────────────────────┐
│  Header: Logo | Dashboard | Skills | Práctica | Perfil  │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  👋 Bienvenido, Carlos              [🔥 Racha: 5 días] │
│                                                         │
│  ┌─────────────────────┐  ┌─────────────────────────┐   │
│  │ PROGRESO GENERAL    │  │ PRÓXIMA LECCIÓN          │   │
│  │ ████████░░ 45%      │  │ 📖 Elevator Pitch       │   │
│  │ 28/90 lecciones     │  │ Small Talk & Networking  │   │
│  │ 450 puntos          │  │ [Continuar →]            │   │
│  └─────────────────────┘  └─────────────────────────┘   │
│                                                         │
│  TUS ÁREAS                                              │
│  ┌─────┐ ┌─────┐ ┌─────┐ ┌─────┐ ┌─────┐ ┌─────┐     │
│  │ 💬  │ │ 🏛️  │ │ 🤝  │ │ 🧠  │ │ ☕  │ │ 🎯  │     │
│  │ 60% │ │ 40% │ │ 35% │ │ 50% │ │ 20% │ │ 45% │     │
│  └─────┘ └─────┘ └─────┘ └─────┘ └─────┘ └─────┘     │
│                                                         │
│  LOGROS RECIENTES                                       │
│  🎯 Primer Paso  🗣️ Comunicador  ⚡ Racha de 3        │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

### 4.3 Pantalla de Skill (ejemplo: Comunicación)

```
┌─────────────────────────────────────────────────────────┐
│  ← Volver    Comunicación 💬                            │
│  "Expresa ideas con claridad e impacto"                 │
│  ████████████████░░░░░░░ 60%                            │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  NIVEL 1: FUNDAMENTOS ✅                                │
│  ┌─────────────────────────────────────────────┐        │
│  │ ✅ Escucha Activa (teoría)         10pts    │        │
│  │ ✅ Test: Escucha Activa (quiz)      8pts    │        │
│  │ ✅ La Reunión Difícil (escenario)  10pts    │        │
│  │ ✅ Comunicación No Verbal (teoría) 10pts    │        │
│  └─────────────────────────────────────────────┘        │
│                                                         │
│  NIVEL 2: PRÁCTICA 🔓                                   │
│  ┌─────────────────────────────────────────────┐        │
│  │ 📖 Feedback Constructivo           ●        │        │
│  │ ❓ Test: Dar y Recibir Feedback     ○        │        │
│  │ 🎭 El Empleado Desmotivado         ○        │        │
│  └─────────────────────────────────────────────┘        │
│                                                         │
│  NIVEL 3: DOMINIO 🔒                                    │
│  (Completa el Nivel 2 para desbloquear)                 │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

### 4.4 Pantalla de Lección — Teoría

```
┌─────────────────────────────────────────────────────────┐
│  ← Comunicación > Nivel 1 > Escucha Activa             │
│                                                         │
│  ┌───────────────────────────────────────────────┐      │
│  │  🔊 [▶ Reproducir audio]  1x  ━━━━━━━●━━ 3:24│      │
│  └───────────────────────────────────────────────┘      │
│                                                         │
│  ┌─ ARIA dice: ────────────────────────────────┐        │
│  │ 🤖 "La escucha activa es una de las         │        │
│  │ habilidades más subestimadas..."             │        │
│  └──────────────────────────────────────────────┘       │
│                                                         │
│  📖 CONTENIDO                                           │
│  La escucha activa es más que oír: implica              │
│  atender, comprender, responder y recordar.             │
│  El 55% de la comunicación es no verbal...              │
│                                                         │
│  💡 PUNTOS CLAVE                                        │
│  ┌──────────────────────────────────┐                   │
│  │ ✦ Mantén contacto visual        │                   │
│  │ ✦ Parafrasea lo que escuchas    │                   │
│  │ ✦ No interrumpas                │                   │
│  │ ✦ Haz preguntas abiertas        │                   │
│  └──────────────────────────────────┘                   │
│                                                         │
│                    [Completar Lección ✓]                 │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

### 4.5 Pantalla de Quiz

```
┌─────────────────────────────────────────────────────────┐
│  Test: Escucha Activa          Pregunta 2 de 4          │
│  ━━━━━━━━━━━━━━━━━━━●━━━━━━━━━━━                        │
│                                                         │
│  Tu compañero te explica un problema técnico            │
│  complejo. ¿Cuál es la mejor respuesta?                 │
│                                                         │
│  ┌────────────────────────────────────────────┐         │
│  │ ○  Le das la solución de inmediato         │         │
│  ├────────────────────────────────────────────┤         │
│  │ ●  Parafraseas: "Si entiendo bien..."  ✅  │         │
│  ├────────────────────────────────────────────┤         │
│  │ ○  Asientes sin decir nada                 │         │
│  ├────────────────────────────────────────────┤         │
│  │ ○  Cambias de tema para relajar tensión    │         │
│  └────────────────────────────────────────────┘         │
│                                                         │
│  ┌─ FEEDBACK ────────────────────────────────┐          │
│  │ ✅ ¡Exacto! Parafrasear demuestra que     │          │
│  │ escuchaste y permite corregir              │          │
│  │ malentendidos antes de actuar.             │          │
│  └───────────────────────────────────────────┘          │
│                                                         │
│                    [Siguiente Pregunta →]                │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

### 4.6 Pantalla de Escenario

```
┌─────────────────────────────────────────────────────────┐
│  🎭 Escenario: La Reunión Difícil                       │
│                                                         │
│  ┌─ SITUACIÓN ───────────────────────────────┐          │
│  │ Estás en una reunión y un colega           │          │
│  │ contradice tu propuesta frente al equipo   │          │
│  │ de forma agresiva. Todos te miran.         │          │
│  └───────────────────────────────────────────┘          │
│                                                         │
│  ¿Qué haces?                                            │
│                                                         │
│  ┌──────────────────────────────────────────┐           │
│  │ 😤 Responder con el mismo tono agresivo  │           │
│  └──────────────────────────────────────────┘           │
│  ┌──────────────────────────────────────────┐           │
│  │ 😶 Callarte y dejar pasar el momento     │           │
│  └──────────────────────────────────────────┘           │
│  ┌──────────────────────────────────────────┐           │
│  │ 🤝 Agradecer, pedir datos y proponer     │ ← hover  │
│  │    una discusión estructurada             │           │
│  └──────────────────────────────────────────┘           │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

### 4.7 Chat IA / Roleplay

```
┌─────────────────────────────────────────────────────────┐
│  🤖 Práctica con Aria          [Cerrar sesión]          │
│  Tema: Elevator Pitch                                   │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  ┌─ 🤖 Aria ────────────────────────────────┐           │
│  │ ¡Hola! Vamos a practicar tu elevator     │ 🔊       │
│  │ pitch. Imagina que estás en el ascensor   │           │
│  │ con la directora de innovación de una     │           │
│  │ empresa que te interesa mucho.            │           │
│  │                                           │           │
│  │ Tienes 30 segundos. ¿Qué le dirías?      │           │
│  └───────────────────────────────────────────┘           │
│                                                         │
│  ┌─ 👤 Tú ──────────────────────────────────┐           │
│  │ Buenos días, soy Carlos García, trabajo   │           │
│  │ en desarrollo de producto y...            │           │
│  └───────────────────────────────────────────┘           │
│                                                         │
│  ┌─ 🤖 Aria ────────────────────────────────┐           │
│  │ Buen comienzo con la presentación         │ 🔊       │
│  │ personal. Pero falta el gancho...         │           │
│  │                                           │           │
│  │ 💡 Sugerencias:                           │           │
│  │ • Menciona un problema que resuelves      │           │
│  │ • Añade un dato impactante                │           │
│  └───────────────────────────────────────────┘           │
│                                                         │
│  ┌──────────────────────────────────┐ [🔊] [📎] [Send]  │
│  │ Escribe tu respuesta...          │                   │
│  └──────────────────────────────────┘                   │
└─────────────────────────────────────────────────────────┘
```

---

## 5. Componentes Reutilizables

Lista de componentes que Claude Code debe crear:

| Componente | Uso |
|-----------|-----|
| `Header` | Navegación principal + avatar |
| `Sidebar` | Menú lateral (desktop) |
| `BottomNav` | Navegación inferior (móvil) |
| `SkillCard` | Tarjeta de área de skill |
| `LevelCard` | Tarjeta de nivel con progreso |
| `LessonCard` | Item de lección con estado |
| `ProgressBar` | Barra de progreso animada |
| `Badge` | Badge/medalla con icono |
| `QuizQuestion` | Pregunta con opciones |
| `FeedbackBanner` | Banner de resultado (correcto/incorrecto) |
| `ScenarioCard` | Situación con opciones |
| `ChatBubble` | Mensaje de chat (user/AI) |
| `ChatInput` | Input de chat + botones |
| `AudioPlayer` | Reproductor de audio inline |
| `AchievementToast` | Notificación de logro desbloqueado |
| `LoadingSpinner` | Indicador de carga |
| `EmptyState` | Estado vacío con ilustración |
| `Modal` | Modal genérico |
| `Toast` | Notificaciones temporales |

---

## 6. Animaciones y Microinteracciones

| Elemento | Animación |
|---------|-----------|
| Tarjetas de skill | Fade in escalonado al cargar el dashboard |
| Progress bar | Llenado progresivo con ease-out |
| Quiz: opción correcta | Border verde + checkmark con bounce |
| Quiz: opción incorrecta | Shake sutil + border rojo |
| Logro desbloqueado | Toast desde abajo con confetti sutil |
| Chat: mensaje IA | Fade in + typing indicator previo |
| Navegación | Transiciones suaves entre páginas |
| Hover en tarjetas | Elevación con sombra + scale 1.02 |
| Audio: reproduciendo | Icono de ondas sonoras animado |
| Nivel desbloqueado | Unlock animation (candado → abierto) |

---

## 7. Diseño Móvil (< 768px)

### Adaptaciones principales:
- Navegación: hamburger menu o bottom tab bar (Dashboard, Skills, Práctica, Perfil)
- Dashboard: cards apiladas verticalmente
- Grid de skills: 1 columna (stack vertical)
- Chat IA: pantalla completa tipo WhatsApp
- Quizzes: opciones full-width
- Audio player: sticky en la parte superior
- Texto: reducir tamaños 1 escala (h1: 2rem → 1.5rem)
- Padding lateral: 16px
- Botones: full-width en acciones principales
