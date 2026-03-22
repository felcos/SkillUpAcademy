using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de datos iniciales para desarrollo.
/// </summary>
public static class SembradoDatos
{
    /// <summary>
    /// Siembra datos iniciales si la base de datos está vacía.
    /// </summary>
    public static async Task SembrarAsync(AppDbContext contexto)
    {
        if (await contexto.AreasHabilidad.AnyAsync())
            return;

        await SembrarConfiguracionAvatarAsync(contexto);
        await SembrarAreasYNivelesAsync(contexto);
        await SembrarLeccionesComunicacionAsync(contexto);
        await SembrarLeccionesLiderazgoAsync(contexto);
        await SembrarLeccionesTrabajoEnEquipoAsync(contexto);
        await SembrarLeccionesInteligenciaEmocionalAsync(contexto);
        await SembrarLeccionesNetworkingAsync(contexto);
        await SembrarLeccionesPersuasionAsync(contexto);
        await SembrarLogrosAsync(contexto);
    }

    private static async Task SembrarConfiguracionAvatarAsync(AppDbContext contexto)
    {
        ConfiguracionAvatar aria = new ConfiguracionAvatar
        {
            NombreAvatar = "Aria",
            Slug = "aria-default",
            DescripcionPersonalidad = "Instructora profesional, empática y motivadora. Usa ejemplos reales del mundo laboral.",
            Tono = "profesional",
            VelocidadHabla = 1.0m,
            VozTTS = "es-ES-ElviraNeural",
            PromptSistemaBase = "Eres Aria, instructora virtual de SkillUp Academy especializada en habilidades blandas profesionales. Responde siempre en español, sé motivadora y usa ejemplos prácticos.",
            EstiloVisual = "corporativa",
            ColorFondo = "#1A1A2E",
            EstiloCelebracion = "moderado",
            EstiloCorreccion = "constructivo",
            EsDefault = true,
            Activo = true
        };

        contexto.Set<ConfiguracionAvatar>().Add(aria);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarAreasYNivelesAsync(AppDbContext contexto)
    {
        List<AreaHabilidad> areas = new List<AreaHabilidad>
        {
            new AreaHabilidad
            {
                Id = 1, Slug = "comunicacion-efectiva", Titulo = "Comunicación Efectiva",
                Subtitulo = "Domina el arte de transmitir ideas con claridad e impacto",
                Descripcion = "Aprende a comunicarte con claridad, escuchar activamente y adaptar tu mensaje a diferentes audiencias profesionales.",
                Icono = "💬", ColorPrimario = "#0F4C81", ColorAcento = "#3498DB", Orden = 1
            },
            new AreaHabilidad
            {
                Id = 2, Slug = "liderazgo", Titulo = "Liderazgo",
                Subtitulo = "Inspira, guía y potencia a tu equipo",
                Descripcion = "Desarrolla tu capacidad de liderar equipos, tomar decisiones estratégicas y crear una visión compartida.",
                Icono = "👑", ColorPrimario = "#8E44AD", ColorAcento = "#9B59B6", Orden = 2
            },
            new AreaHabilidad
            {
                Id = 3, Slug = "trabajo-en-equipo", Titulo = "Trabajo en Equipo",
                Subtitulo = "Colabora, construye y alcanza metas juntos",
                Descripcion = "Aprende a colaborar eficazmente, resolver conflictos y construir equipos de alto rendimiento.",
                Icono = "🤝", ColorPrimario = "#27AE60", ColorAcento = "#2ECC71", Orden = 3
            },
            new AreaHabilidad
            {
                Id = 4, Slug = "inteligencia-emocional", Titulo = "Inteligencia Emocional",
                Subtitulo = "Comprende y gestiona tus emociones y las de los demás",
                Descripcion = "Desarrolla autoconciencia emocional, empatía y habilidades para gestionar relaciones interpersonales.",
                Icono = "🧠", ColorPrimario = "#E74C3C", ColorAcento = "#E67E22", Orden = 4
            },
            new AreaHabilidad
            {
                Id = 5, Slug = "networking", Titulo = "Networking",
                Subtitulo = "Construye relaciones profesionales estratégicas",
                Descripcion = "Aprende a crear y mantener una red de contactos profesionales que impulse tu carrera.",
                Icono = "🌐", ColorPrimario = "#F39C12", ColorAcento = "#F1C40F", Orden = 5
            },
            new AreaHabilidad
            {
                Id = 6, Slug = "persuasion", Titulo = "Persuasión",
                Subtitulo = "Influye con ética y convence con argumentos sólidos",
                Descripcion = "Domina las técnicas de persuasión ética para presentar ideas, negociar y generar compromiso.",
                Icono = "🎯", ColorPrimario = "#16A085", ColorAcento = "#1ABC9C", Orden = 6
            }
        };

        contexto.AreasHabilidad.AddRange(areas);
        await contexto.SaveChangesAsync();

        // Niveles: 3 por área (solo nivel 1 con lecciones por ahora)
        int nivelId = 1;
        foreach (AreaHabilidad area in areas)
        {
            contexto.Set<Nivel>().AddRange(new List<Nivel>
            {
                new Nivel { Id = nivelId++, AreaHabilidadId = area.Id, NumeroNivel = 1, Nombre = "Fundamentos", Descripcion = $"Bases esenciales de {area.Titulo}", PuntosDesbloqueo = 0 },
                new Nivel { Id = nivelId++, AreaHabilidadId = area.Id, NumeroNivel = 2, Nombre = "Práctica", Descripcion = $"Aplicación práctica de {area.Titulo}", PuntosDesbloqueo = 100 },
                new Nivel { Id = nivelId++, AreaHabilidadId = area.Id, NumeroNivel = 3, Nombre = "Dominio", Descripcion = $"Dominio avanzado de {area.Titulo}", PuntosDesbloqueo = 300 }
            });
        }

        await contexto.SaveChangesAsync();
    }

    // ============================================================
    // COMUNICACIÓN EFECTIVA — Nivel 1 (NivelId = 1)
    // ============================================================
    private static async Task SembrarLeccionesComunicacionAsync(AppDbContext contexto)
    {
        int nivelId = 1;
        int leccionBase = 1;

        // Lección 1: Teoría introductoria
        Leccion l1 = new Leccion
        {
            Id = leccionBase, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Los pilares de la comunicación efectiva",
            Descripcion = "Descubre los fundamentos que hacen que un mensaje sea claro, persuasivo y memorable.",
            Contenido = "## ¿Qué es comunicar eficazmente?\n\nComunicar no es solo hablar. Es lograr que tu interlocutor entienda, sienta y actúe según tu mensaje. En el entorno profesional, la comunicación efectiva es la habilidad #1 que los empleadores buscan.\n\n## Los 4 pilares\n\n### 1. Claridad\nUsa palabras sencillas y frases cortas. Si tu abuela no lo entendería, simplifícalo.\n\n### 2. Empatía\nPonte en los zapatos del otro. ¿Qué necesita escuchar? ¿Qué le preocupa?\n\n### 3. Escucha activa\nEl 50% de comunicar bien es escuchar. No interrumpas, haz preguntas, parafrasea.\n\n### 4. Asertividad\nExpresa tus ideas con firmeza pero sin agresividad. Di lo que piensas respetando al otro.",
            PuntosClave = "[\"La comunicación efectiva tiene 4 pilares: claridad, empatía, escucha activa y asertividad\",\"Comunicar no es solo hablar, es lograr comprensión mutua\",\"La escucha activa representa el 50% de la comunicación efectiva\",\"La asertividad equilibra firmeza con respeto\"]",
            GuionAudio = "Hoy vamos a hablar sobre los pilares de la comunicación efectiva. Comunicar bien no es solo hablar mucho o usar palabras bonitas. Es lograr que la otra persona entienda exactamente lo que quieres transmitir.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        // Lección 2: Teoría conceptos clave
        Leccion l2 = new Leccion
        {
            Id = leccionBase + 1, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Escucha activa: la habilidad olvidada",
            Descripcion = "Aprende las técnicas de escucha activa que transformarán tus conversaciones profesionales.",
            Contenido = "## El problema: oímos pero no escuchamos\n\nEstudios muestran que retenemos solo el 25% de lo que escuchamos. En reuniones, la mayoría está pensando en qué va a decir, no en lo que el otro dice.\n\n## Técnicas de escucha activa\n\n### Parafraseo\nRepite con tus palabras lo que entendiste: «Si te entiendo bien, lo que dices es que...»\n\n### Preguntas abiertas\nEn lugar de «¿Estás de acuerdo?» pregunta «¿Qué opinas sobre esto?»\n\n### Lenguaje corporal\nMantén contacto visual, asiente, inclina ligeramente el cuerpo hacia adelante.\n\n### Silencio estratégico\nNo llenes cada pausa. A veces el silencio invita al otro a profundizar.",
            PuntosClave = "[\"Solo retenemos el 25% de lo que escuchamos\",\"Parafrasear demuestra que realmente escuchaste\",\"Las preguntas abiertas generan conversaciones más ricas\",\"El lenguaje corporal comunica más que las palabras\",\"El silencio estratégico es una herramienta poderosa\"]",
            GuionAudio = "¿Sabías que solo retenemos un 25% de lo que escuchamos? Hoy vamos a cambiar eso con técnicas concretas de escucha activa.",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 8
        };

        // Lección 3: Quiz
        Leccion l3 = new Leccion
        {
            Id = leccionBase + 2, NivelId = nivelId, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Comunicación Efectiva",
            Descripcion = "Pon a prueba tus conocimientos sobre comunicación efectiva.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        // Lección 4: Escenario
        Leccion l4 = new Leccion
        {
            Id = leccionBase + 3, NivelId = nivelId, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: La reunión difícil",
            Descripcion = "Practica tus habilidades de comunicación en una reunión tensa con un cliente insatisfecho.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        // Lección 5: Roleplay
        Leccion l5 = new Leccion
        {
            Id = leccionBase + 4, NivelId = nivelId, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Presenta tu idea al director",
            Descripcion = "Practica cómo presentar una propuesta a un directivo escéptico usando comunicación efectiva.",
            Contenido = "Vas a practicar una presentación ante un director que tiene poco tiempo y muchas dudas. Tu objetivo es convencerle de aprobar tu proyecto en 5 minutos.",
            GuionAudio = "En este roleplay, yo seré el director de tu empresa. Tienes 5 minutos para convencerme de aprobar tu proyecto. Recuerda: claridad, datos y empatía.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1
        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "comunicación efectiva",
            "Los pilares de la comunicación efectiva",
            "Hoy vamos a descubrir los 4 pilares que hacen que un mensaje sea claro, persuasivo y memorable. Esto cambiará la forma en que te comunicas en el trabajo.",
            "Claridad, empatía, escucha activa y asertividad. Estos 4 pilares son la base de toda comunicación profesional exitosa.",
            "Usa palabras simples, ponte en los zapatos del otro, escucha de verdad y expresa tus ideas con firmeza pero con respeto.");

        // Escenas para lección 2
        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "escucha activa",
            "La habilidad olvidada",
            "Solo retenemos un 25% de lo que escuchamos. Hoy vamos a aprender técnicas concretas para ser mejores oyentes.",
            "Parafraseo, preguntas abiertas, lenguaje corporal y silencio estratégico son las 4 técnicas clave.",
            "Practica el parafraseo esta semana: repite con tus palabras lo que tu interlocutor acaba de decir. Verás cómo cambian las conversaciones.");

        // Quiz comunicación
        await SembrarQuizComunicacionAsync(contexto, l3.Id);

        // Escenario comunicación
        await SembrarEscenarioComunicacionAsync(contexto, l4.Id);
    }

    // ============================================================
    // LIDERAZGO — Nivel 1 (NivelId = 4)
    // ============================================================
    private static async Task SembrarLeccionesLiderazgoAsync(AppDbContext contexto)
    {
        int nivelId = 4;
        int leccionBase = 6;

        Leccion l1 = new Leccion
        {
            Id = leccionBase, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Liderazgo vs. jefatura: la diferencia clave",
            Descripcion = "Entiende por qué ser líder no es lo mismo que ser jefe y cómo desarrollar un liderazgo auténtico.",
            Contenido = "## Jefe vs. Líder\n\nUn jefe tiene autoridad por su cargo. Un líder tiene influencia por su ejemplo. La diferencia es abismal.\n\n## Características del líder auténtico\n\n### Visión\nUn líder sabe hacia dónde va y puede articular esa visión de forma inspiradora.\n\n### Servicio\nEl liderazgo servicial pone al equipo primero. Tu éxito se mide por el éxito de tu gente.\n\n### Coherencia\nHaces lo que dices. No hay nada más destructivo para un líder que la incoherencia.\n\n### Vulnerabilidad\nAdmitir que no sabes algo genera más confianza que fingir que lo sabes todo.",
            PuntosClave = "[\"Un jefe tiene autoridad por cargo, un líder tiene influencia por ejemplo\",\"El liderazgo servicial pone al equipo primero\",\"La coherencia entre palabras y acciones es fundamental\",\"Mostrar vulnerabilidad genera confianza\"]",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = leccionBase + 1, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Estilos de liderazgo situacional",
            Descripcion = "Aprende a adaptar tu estilo de liderazgo según la madurez y necesidades de cada miembro del equipo.",
            Contenido = "## No hay un solo estilo de liderazgo\n\nEl modelo de Hersey y Blanchard identifica 4 estilos según la situación:\n\n## Los 4 estilos\n\n### Directivo\nDa instrucciones claras y supervisa de cerca. Ideal para empleados nuevos o tareas desconocidas.\n\n### Coaching\nExplica el porqué, escucha y guía. Para personas con algo de experiencia pero poca confianza.\n\n### Participativo\nComparte la toma de decisiones. Para personas capaces pero que necesitan motivación.\n\n### Delegativo\nConfía y da autonomía. Para expertos motivados que solo necesitan recursos y libertad.",
            PuntosClave = "[\"No existe un único estilo de liderazgo correcto\",\"El estilo directivo funciona con empleados nuevos\",\"El coaching combina instrucción con escucha\",\"Delegar requiere confianza mutua y seguimiento\"]",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 8
        };

        Leccion l3 = new Leccion
        {
            Id = leccionBase + 2, NivelId = nivelId, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Liderazgo", Descripcion = "Evalúa tus conocimientos sobre liderazgo.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = leccionBase + 3, NivelId = nivelId, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: El equipo desmotivado",
            Descripcion = "Tu equipo lleva semanas sin cumplir objetivos. Decide cómo abordar la situación.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = leccionBase + 4, NivelId = nivelId, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Feedback a un empleado",
            Descripcion = "Practica cómo dar feedback constructivo a un empleado con bajo rendimiento.",
            Contenido = "Un miembro de tu equipo ha bajado su rendimiento en las últimas semanas. Debes tener una conversación honesta pero empática.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "liderazgo", "Jefe vs. Líder",
            "Hoy vamos a explorar una distinción fundamental: la diferencia entre ser jefe y ser líder.",
            "Visión, servicio, coherencia y vulnerabilidad. Estas son las 4 características del líder auténtico.",
            "Reflexiona: ¿eres más jefe o más líder? La buena noticia es que el liderazgo se aprende.");

        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "estilos de liderazgo", "Liderazgo situacional",
            "No existe un único estilo de liderazgo. Los mejores líderes adaptan su enfoque según la persona y la situación.",
            "Directivo, coaching, participativo y delegativo. Cada estilo tiene su momento ideal.",
            "Esta semana, identifica qué estilo necesita cada persona de tu equipo. Adaptar tu liderazgo es un superpoder.");

        await SembrarQuizLiderazgoAsync(contexto, l3.Id);
        await SembrarEscenarioLiderazgoAsync(contexto, l4.Id);
    }

    // ============================================================
    // TRABAJO EN EQUIPO — Nivel 1 (NivelId = 7)
    // ============================================================
    private static async Task SembrarLeccionesTrabajoEnEquipoAsync(AppDbContext contexto)
    {
        int nivelId = 7;
        int leccionBase = 11;

        Leccion l1 = new Leccion
        {
            Id = leccionBase, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Las 5 disfunciones de un equipo",
            Descripcion = "Conoce el modelo de Patrick Lencioni y cómo superar las barreras del trabajo en equipo.",
            Contenido = "## El modelo de Lencioni\n\nPatrick Lencioni identificó 5 disfunciones que destruyen equipos:\n\n### 1. Ausencia de confianza\nCuando los miembros no se atreven a ser vulnerables ni a pedir ayuda.\n\n### 2. Temor al conflicto\nEvitar debates honestos lleva a decisiones mediocres.\n\n### 3. Falta de compromiso\nSin debate real, no hay buy-in genuino.\n\n### 4. Evitar la responsabilidad\nNadie se atreve a señalar cuando un compañero no cumple.\n\n### 5. Desatención a resultados\nPriorizar el ego individual sobre los objetivos del equipo.",
            PuntosClave = "[\"La confianza es la base de todo equipo funcional\",\"El conflicto constructivo mejora las decisiones\",\"Sin debate real no hay compromiso genuino\",\"Los equipos exitosos se exigen mutuamente responsabilidad\",\"Los resultados del equipo van antes que el ego individual\"]",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = leccionBase + 1, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Roles en un equipo: el modelo Belbin",
            Descripcion = "Identifica tu rol natural en equipos y aprende a valorar la diversidad de roles.",
            Contenido = "## Los 9 roles de Belbin\n\nMeredith Belbin descubrió que los equipos exitosos tienen diversidad de roles:\n\n## Roles de acción\n- **Impulsor**: Tiene energía y empuja al equipo hacia adelante.\n- **Implementador**: Convierte ideas en planes de acción concretos.\n- **Finalizador**: Cuida los detalles y asegura la calidad.\n\n## Roles sociales\n- **Coordinador**: Facilita la participación de todos.\n- **Cohesionador**: Mantiene la armonía y resuelve tensiones.\n- **Investigador de recursos**: Conecta al equipo con el exterior.\n\n## Roles mentales\n- **Cerebro**: Genera ideas creativas e innovadoras.\n- **Monitor evaluador**: Analiza opciones con objetividad.\n- **Especialista**: Aporta conocimiento técnico profundo.",
            PuntosClave = "[\"Un equipo necesita diversidad de roles para funcionar\",\"No hay roles buenos o malos, todos son necesarios\",\"Conocer tu rol natural te ayuda a aportar más valor\",\"Los mejores equipos combinan roles de acción, sociales y mentales\"]",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 8
        };

        Leccion l3 = new Leccion
        {
            Id = leccionBase + 2, NivelId = nivelId, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Trabajo en Equipo", Descripcion = "Evalúa tus conocimientos sobre trabajo en equipo.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = leccionBase + 3, NivelId = nivelId, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: El conflicto entre compañeros",
            Descripcion = "Dos miembros de tu equipo no se hablan. Decide cómo mediar.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = leccionBase + 4, NivelId = nivelId, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Reunión de retrospectiva",
            Descripcion = "Facilita una retrospectiva donde el equipo identifica qué mejorar.",
            Contenido = "Debes facilitar una retrospectiva sprint. El equipo está frustrado porque no cumplieron los objetivos.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "trabajo en equipo", "Las 5 disfunciones",
            "Patrick Lencioni identificó 5 disfunciones que destruyen equipos. Vamos a conocerlas para poder superarlas.",
            "Ausencia de confianza, temor al conflicto, falta de compromiso, evasión de responsabilidad y desatención a resultados.",
            "La confianza es la base. Sin ella, el resto se derrumba. ¿Tu equipo se atreve a ser vulnerable?");

        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "roles de equipo", "El modelo Belbin",
            "Meredith Belbin descubrió que los equipos exitosos necesitan diversidad de roles. Vamos a identificar el tuyo.",
            "Hay 9 roles: impulsor, implementador, finalizador, coordinador, cohesionador, investigador, cerebro, monitor y especialista.",
            "No hay roles buenos o malos. Conocer tu rol natural te ayuda a aportar más valor al equipo.");

        await SembrarQuizTrabajoEquipoAsync(contexto, l3.Id);
        await SembrarEscenarioTrabajoEquipoAsync(contexto, l4.Id);
    }

    // ============================================================
    // INTELIGENCIA EMOCIONAL — Nivel 1 (NivelId = 10)
    // ============================================================
    private static async Task SembrarLeccionesInteligenciaEmocionalAsync(AppDbContext contexto)
    {
        int nivelId = 10;
        int leccionBase = 16;

        Leccion l1 = new Leccion
        {
            Id = leccionBase, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Las 5 dimensiones de Goleman",
            Descripcion = "Conoce el modelo de inteligencia emocional de Daniel Goleman aplicado al trabajo.",
            Contenido = "## ¿Qué es la inteligencia emocional?\n\nDaniel Goleman demostró que el éxito profesional depende más de la IE que del coeficiente intelectual.\n\n## Las 5 dimensiones\n\n### 1. Autoconciencia\nReconocer tus emociones en tiempo real. ¿Qué sientes ahora mismo y por qué?\n\n### 2. Autorregulación\nGestionar tus impulsos. No reaccionar, sino responder con intención.\n\n### 3. Motivación\nTener un motor interno que va más allá del sueldo o el reconocimiento externo.\n\n### 4. Empatía\nPercibirlas emociones de los demás y responder de forma adecuada.\n\n### 5. Habilidades sociales\nGestionar relaciones, resolver conflictos e influir positivamente.",
            PuntosClave = "[\"La IE predice más el éxito laboral que el coeficiente intelectual\",\"Autoconciencia es el punto de partida: reconoce tus emociones\",\"Autorregulación es responder con intención en vez de reaccionar\",\"La empatía no es estar de acuerdo, es comprender al otro\"]",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = leccionBase + 1, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Gestión emocional en el trabajo",
            Descripcion = "Técnicas prácticas para gestionar el estrés, la frustración y la ansiedad laboral.",
            Contenido = "## El secuestro amigdalar\n\nCuando una emoción intensa nos desborda, la amígdala toma el control. Perdemos capacidad de razonar. En el trabajo esto se traduce en respuestas de las que luego nos arrepentimos.\n\n## Técnicas de gestión\n\n### Pausa de 6 segundos\nAntes de responder a algo que te enfada, cuenta hasta 6. Es el tiempo que tarda el córtex prefrontal en recuperar el control.\n\n### Etiquetado emocional\nPonle nombre a lo que sientes: «Estoy frustrado porque...». Nombrar la emoción reduce su intensidad un 50%.\n\n### Reencuadre cognitivo\nCambia la interpretación: de «esto es un desastre» a «esto es un reto que puedo resolver».\n\n### Respiración 4-7-8\nInhala 4 segundos, mantén 7, exhala 8. Activa el sistema nervioso parasimpático.",
            PuntosClave = "[\"El secuestro amigdalar nos hace reaccionar sin pensar\",\"La pausa de 6 segundos da tiempo al cerebro racional\",\"Nombrar la emoción reduce su intensidad un 50%\",\"El reencuadre cognitivo transforma amenazas en retos\"]",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 8
        };

        Leccion l3 = new Leccion
        {
            Id = leccionBase + 2, NivelId = nivelId, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Inteligencia Emocional", Descripcion = "Evalúa tus conocimientos sobre IE.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = leccionBase + 3, NivelId = nivelId, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Crítica injusta del jefe",
            Descripcion = "Tu jefe te critica duramente en una reunión. ¿Cómo gestionas tus emociones?",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = leccionBase + 4, NivelId = nivelId, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Conversación difícil con un compañero",
            Descripcion = "Practica cómo abordar un tema sensible con empatía e inteligencia emocional.",
            Contenido = "Un compañero ha hecho un comentario que te ha molestado. Debes abordar el tema con inteligencia emocional.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "inteligencia emocional", "Las 5 dimensiones de Goleman",
            "Daniel Goleman demostró que la inteligencia emocional predice más el éxito laboral que el coeficiente intelectual.",
            "Autoconciencia, autorregulación, motivación, empatía y habilidades sociales son las 5 dimensiones.",
            "Empieza por la autoconciencia. Hoy, cada vez que sientas una emoción fuerte, ponle nombre.");

        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "gestión emocional", "Técnicas prácticas",
            "Cuando una emoción intensa nos desborda, la amígdala toma el control. Vamos a aprender a recuperarlo.",
            "Pausa de 6 segundos, etiquetado emocional, reencuadre cognitivo y respiración 4-7-8 son tus herramientas.",
            "Practica la pausa de 6 segundos esta semana. Antes de responder a algo que te enfade, cuenta hasta 6.");

        await SembrarQuizInteligenciaEmocionalAsync(contexto, l3.Id);
        await SembrarEscenarioInteligenciaEmocionalAsync(contexto, l4.Id);
    }

    // ============================================================
    // NETWORKING — Nivel 1 (NivelId = 13)
    // ============================================================
    private static async Task SembrarLeccionesNetworkingAsync(AppDbContext contexto)
    {
        int nivelId = 13;
        int leccionBase = 21;

        Leccion l1 = new Leccion
        {
            Id = leccionBase, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Networking estratégico: más allá de repartir tarjetas",
            Descripcion = "Aprende a construir relaciones profesionales genuinas y duraderas.",
            Contenido = "## El error del networking transaccional\n\nLa mayoría piensa en networking como «conocer gente para pedir favores». Eso no funciona. El networking efectivo se basa en dar valor antes de pedir.\n\n## Principios del networking estratégico\n\n### Generosidad primero\nOfrece ayuda, conecta personas, comparte conocimiento. Sin esperar nada a cambio.\n\n### Calidad sobre cantidad\n10 relaciones profundas valen más que 1000 contactos de LinkedIn.\n\n### Seguimiento consistente\nEl 80% del networking efectivo ocurre en el seguimiento, no en el primer encuentro.\n\n### Autenticidad\nSé tú mismo. Las relaciones construidas sobre una fachada no duran.",
            PuntosClave = "[\"El networking efectivo se basa en dar valor antes de pedir\",\"10 relaciones profundas valen más que 1000 contactos superficiales\",\"El 80% del éxito está en el seguimiento posterior\",\"La autenticidad es la base de relaciones duraderas\"]",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = leccionBase + 1, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "El elevator pitch perfecto",
            Descripcion = "Aprende a presentarte de forma memorable en 30 segundos.",
            Contenido = "## ¿Qué es un elevator pitch?\n\nEs tu presentación profesional en 30 segundos. Lo que dirías si compartieras ascensor con tu CEO ideal.\n\n## La fórmula PVR\n\n### Problema\nEmpieza con el problema que resuelves: «¿Sabes cuando los equipos no cumplen plazos?»\n\n### Valor\nExplica tu valor único: «Yo ayudo a equipos a organizarse para entregar un 40% más rápido.»\n\n### Resultado\nTermina con un resultado concreto: «Mi último equipo pasó de entregas tardías crónicas a cumplir el 95% de deadlines.»\n\n## Consejos extra\n- Sonríe y mantén contacto visual\n- Practica hasta que suene natural, no memorizado\n- Adapta el pitch según tu audiencia\n- Termina con una pregunta que invite a conversar",
            PuntosClave = "[\"Un elevator pitch dura máximo 30 segundos\",\"La fórmula PVR: Problema, Valor, Resultado\",\"Debe sonar natural, no memorizado\",\"Siempre termina con una pregunta que invite a conversar\"]",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 8
        };

        Leccion l3 = new Leccion
        {
            Id = leccionBase + 2, NivelId = nivelId, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Networking", Descripcion = "Evalúa tus conocimientos sobre networking.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = leccionBase + 3, NivelId = nivelId, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: El evento de la industria",
            Descripcion = "Estás en un evento profesional. Decide cómo abordar el networking.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = leccionBase + 4, NivelId = nivelId, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Tu elevator pitch",
            Descripcion = "Practica tu presentación profesional con Aria actuando como un potencial contacto.",
            Contenido = "Estás en un evento y te encuentras con alguien importante de tu sector. Tienes 30 segundos para presentarte.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "networking", "Networking estratégico",
            "Olvídate de repartir tarjetas. El networking real se basa en construir relaciones genuinas dando valor.",
            "Generosidad primero, calidad sobre cantidad, seguimiento consistente y autenticidad.",
            "Esta semana, ayuda a alguien de tu red profesional sin esperar nada a cambio. Así empieza el networking real.");

        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "elevator pitch", "Preséntate en 30 segundos",
            "Imagina que compartes ascensor con la persona que puede cambiar tu carrera. ¿Qué le dirías?",
            "La fórmula PVR: Problema que resuelves, Valor que aportas, Resultado que has logrado.",
            "Escribe tu elevator pitch esta semana usando la fórmula PVR. Practícalo frente al espejo hasta que suene natural.");

        await SembrarQuizNetworkingAsync(contexto, l3.Id);
        await SembrarEscenarioNetworkingAsync(contexto, l4.Id);
    }

    // ============================================================
    // PERSUASIÓN — Nivel 1 (NivelId = 16)
    // ============================================================
    private static async Task SembrarLeccionesPersuasionAsync(AppDbContext contexto)
    {
        int nivelId = 16;
        int leccionBase = 26;

        Leccion l1 = new Leccion
        {
            Id = leccionBase, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Los 6 principios de Cialdini",
            Descripcion = "Conoce los 6 principios de influencia de Robert Cialdini aplicados al entorno laboral.",
            Contenido = "## La ciencia de la persuasión\n\nRobert Cialdini identificó 6 principios universales de persuasión:\n\n### 1. Reciprocidad\nCuando das algo, la gente siente la necesidad de devolver el favor.\n\n### 2. Compromiso y coherencia\nUna vez que alguien se compromete con algo, tiende a ser coherente con ese compromiso.\n\n### 3. Prueba social\n«Si otros lo hacen, debe ser bueno.» Las personas seguimos el comportamiento de la mayoría.\n\n### 4. Autoridad\nConfiamos en expertos y figuras de autoridad. Demuestra tu expertise.\n\n### 5. Simpatía\nNos dejamos persuadir más por personas que nos caen bien.\n\n### 6. Escasez\nLo escaso se percibe como más valioso. La urgencia mueve a la acción.",
            PuntosClave = "[\"La reciprocidad es el principio más poderoso: da primero\",\"La prueba social influye más de lo que creemos\",\"La autoridad se demuestra con datos y credenciales\",\"La escasez genuina genera urgencia de acción\"]",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = leccionBase + 1, NivelId = nivelId, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Persuasión ética en la oficina",
            Descripcion = "Aprende a persuadir sin manipular usando técnicas éticas y transparentes.",
            Contenido = "## Persuadir no es manipular\n\nLa diferencia es la intención. Persuadir busca un beneficio mutuo. Manipular busca solo tu beneficio.\n\n## Técnicas éticas\n\n### Storytelling\nLas historias son 22 veces más memorables que los datos solos. Combina datos con narrativa.\n\n### Preguntas socarráticas\nEn lugar de imponer tu idea, guía al otro con preguntas: «¿Qué pasaría si...?»\n\n### Escuchar para entender\nAntes de persuadir, entiende qué necesita y valora tu interlocutor.\n\n### Presentar alternativas\nDar opciones hace que la persona sienta que decide, no que le imponen.",
            PuntosClave = "[\"Persuadir busca beneficio mutuo; manipular busca solo el tuyo\",\"Las historias son 22 veces más memorables que los datos\",\"Las preguntas socráticas guían sin imponer\",\"Dar alternativas empodera al interlocutor\"]",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 8
        };

        Leccion l3 = new Leccion
        {
            Id = leccionBase + 2, NivelId = nivelId, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Persuasión", Descripcion = "Evalúa tus conocimientos sobre persuasión.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = leccionBase + 3, NivelId = nivelId, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Convencer al equipo",
            Descripcion = "Tu equipo es escéptico sobre un cambio. Decide cómo persuadirles.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = leccionBase + 4, NivelId = nivelId, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Negociación salarial",
            Descripcion = "Practica cómo negociar un aumento de sueldo usando técnicas de persuasión ética.",
            Contenido = "Llevas un año superando objetivos. Es momento de pedir un aumento. Practica con Aria como tu jefa.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "persuasión", "Los 6 principios de Cialdini",
            "Robert Cialdini identificó los 6 principios universales de persuasión. Estos principios funcionan porque están arraigados en la psicología humana.",
            "Reciprocidad, compromiso, prueba social, autoridad, simpatía y escasez. Los 6 resortes de la influencia.",
            "Esta semana, usa la reciprocidad: haz un favor a un compañero sin pedir nada a cambio. Observa qué pasa.");

        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "persuasión ética", "Persuadir sin manipular",
            "Hay una línea clara entre persuadir y manipular. Hoy vamos a aprender a estar siempre del lado correcto.",
            "Storytelling, preguntas socráticas, escucha activa y presentar alternativas son herramientas éticas de persuasión.",
            "Practica el storytelling: la próxima vez que presentes una idea, acompáñala de una historia real.");

        await SembrarQuizPersuasionAsync(contexto, l3.Id);
        await SembrarEscenarioPersuasionAsync(contexto, l4.Id);
    }

    // ============================================================
    // MÉTODOS AUXILIARES: ESCENAS, QUIZZES, ESCENARIOS
    // ============================================================

    private static async Task SembrarEscenasTeoricasAsync(AppDbContext contexto, int leccionId,
        string tema, string titulo, string guionIntro, string guionContenido, string guionCierre)
    {
        List<EscenaLeccion> escenas = new List<EscenaLeccion>
        {
            new EscenaLeccion
            {
                LeccionId = leccionId, Orden = 1,
                TipoContenidoVisual = TipoContenidoVisual.Texto,
                TituloEscena = "Introducción",
                GuionAria = $"¡Hola! Hoy vamos a hablar sobre {tema}. {guionIntro}",
                ContenidoVisual = titulo,
                TransicionEntrada = TipoTransicion.Fade,
                Layout = TipoLayout.SoloAvatar,
                DuracionSegundos = 20
            },
            new EscenaLeccion
            {
                LeccionId = leccionId, Orden = 2,
                TipoContenidoVisual = TipoContenidoVisual.Texto,
                TituloEscena = "Concepto principal",
                GuionAria = guionContenido,
                ContenidoVisual = guionContenido,
                TransicionEntrada = TipoTransicion.SlideLeft,
                Layout = TipoLayout.AvatarYContenido,
                DuracionSegundos = 30
            },
            new EscenaLeccion
            {
                LeccionId = leccionId, Orden = 3,
                TipoContenidoVisual = TipoContenidoVisual.Diagrama,
                TituloEscena = "En profundidad",
                GuionAria = "Veamos esto con más detalle para que puedas aplicarlo desde hoy.",
                ContenidoVisual = $"Profundización sobre {tema}",
                TransicionEntrada = TipoTransicion.ZoomIn,
                Layout = TipoLayout.AvatarYContenido,
                DuracionSegundos = 25
            },
            new EscenaLeccion
            {
                LeccionId = leccionId, Orden = 4,
                TipoContenidoVisual = TipoContenidoVisual.ListaDePuntos,
                TituloEscena = "Puntos clave",
                GuionAria = "Repasemos los puntos más importantes que debes recordar.",
                ContenidoVisual = guionContenido,
                TransicionEntrada = TipoTransicion.SlideUp,
                Layout = TipoLayout.SoloContenido,
                DuracionSegundos = 20,
                EsPausaReflexiva = true,
                SegundosPausa = 5
            },
            new EscenaLeccion
            {
                LeccionId = leccionId, Orden = 5,
                TipoContenidoVisual = TipoContenidoVisual.Texto,
                TituloEscena = "Cierre",
                GuionAria = $"¡Excelente! {guionCierre} ¡Nos vemos en la siguiente lección!",
                ContenidoVisual = "¡Bien hecho!",
                TransicionEntrada = TipoTransicion.Fade,
                Layout = TipoLayout.SoloAvatar,
                DuracionSegundos = 15
            }
        };

        contexto.Set<EscenaLeccion>().AddRange(escenas);
        await contexto.SaveChangesAsync();
    }

    // --- QUIZZES ---

    private static async Task SembrarQuizComunicacionAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Cuál de los siguientes NO es uno de los 4 pilares de la comunicación efectiva?",
                "Los 4 pilares son claridad, empatía, escucha activa y asertividad.",
                new[] { ("Claridad", false, "La claridad sí es uno de los 4 pilares."), ("Velocidad", true, "¡Correcto! La velocidad no es un pilar. Los 4 son: claridad, empatía, escucha activa y asertividad."), ("Empatía", false, "La empatía sí es uno de los 4 pilares."), ("Asertividad", false, "La asertividad sí es uno de los 4 pilares.") }),
            CrearPregunta(leccionId, 2, "¿Qué porcentaje de lo que escuchamos retenemos según los estudios?",
                "Los estudios muestran que solo retenemos un 25% de lo que escuchamos.",
                new[] { ("75%", false, "Es mucho más bajo. Solo retenemos un 25%."), ("50%", false, "Es incluso menor. Solo retenemos un 25%."), ("25%", true, "¡Correcto! Solo retenemos un 25%, por eso la escucha activa es tan importante."), ("10%", false, "Es un poco más, el 25%, pero sigue siendo muy bajo.") }),
            CrearPregunta(leccionId, 3, "¿Qué técnica de escucha activa consiste en repetir con tus palabras lo que entendiste?",
                "El parafraseo consiste en repetir con tus propias palabras lo que tu interlocutor acaba de decir.",
                new[] { ("Silencio estratégico", false, "El silencio estratégico es dejar pausas para que el otro profundice."), ("Parafraseo", true, "¡Correcto! Parafrasear demuestra que realmente escuchaste y entendiste."), ("Preguntas cerradas", false, "Las preguntas cerradas se responden con sí o no."), ("Resumen ejecutivo", false, "El resumen es útil pero no es la técnica específica de repetir lo entendido.") }),
            CrearPregunta(leccionId, 4, "La asertividad se define como:",
                "La asertividad equilibra la firmeza con el respeto por el otro.",
                new[] { ("Imponer tu opinión siempre", false, "Eso es agresividad, no asertividad."), ("Evitar conflictos a toda costa", false, "Eso es pasividad, no asertividad."), ("Expresar ideas con firmeza respetando al otro", true, "¡Correcto! La asertividad es el punto medio entre pasividad y agresividad."), ("Hablar más fuerte que los demás", false, "El volumen no tiene nada que ver con la asertividad.") }),
            CrearPregunta(leccionId, 5, "¿Qué tipo de preguntas generan conversaciones más ricas?",
                "Las preguntas abiertas invitan a respuestas elaboradas, a diferencia de las cerradas.",
                new[] { ("Preguntas cerradas", false, "Las preguntas cerradas limitan la respuesta a sí o no."), ("Preguntas retóricas", false, "Las retóricas no esperan respuesta real."), ("Preguntas abiertas", true, "¡Correcto! Las preguntas abiertas invitan a reflexionar y compartir más."), ("Preguntas capciosas", false, "Las capciosas buscan confundir, no enriquecer la conversación.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarQuizLiderazgoAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Cuál es la diferencia principal entre un jefe y un líder?",
                "Un jefe tiene autoridad por su cargo; un líder tiene influencia por su ejemplo.",
                new[] { ("El jefe gana más dinero", false, "La diferencia no es económica sino de influencia."), ("El líder tiene más empleados", false, "No es cuestión de cantidad sino de tipo de influencia."), ("El líder influye por ejemplo, el jefe por cargo", true, "¡Correcto! El liderazgo se basa en influencia, no en autoridad jerárquica."), ("No hay diferencia real", false, "La diferencia es fundamental para el desarrollo profesional.") }),
            CrearPregunta(leccionId, 2, "Según el liderazgo situacional, ¿qué estilo es mejor para un empleado nuevo?",
                "El estilo directivo funciona mejor con personas nuevas que necesitan instrucciones claras.",
                new[] { ("Delegativo", false, "Delegar con alguien sin experiencia puede generar frustración."), ("Directivo", true, "¡Correcto! Los empleados nuevos necesitan instrucciones claras y supervisión."), ("Participativo", false, "El participativo requiere que la persona tenga experiencia previa."), ("Laissez-faire", false, "Dejar hacer sin guía a alguien nuevo es contraproducente.") }),
            CrearPregunta(leccionId, 3, "¿Qué característica del líder genera más confianza según los estudios?",
                "Admitir vulnerabilidades genera más confianza que aparentar perfección.",
                new[] { ("Nunca equivocarse", false, "La perfección no genera confianza, genera distancia."), ("Mostrar vulnerabilidad", true, "¡Correcto! Admitir que no sabes algo genera confianza y cercanía."), ("Tener siempre la razón", false, "Nadie tiene siempre la razón y pretenderlo destruye la confianza."), ("Controlar todo", false, "El micromanagement erosiona la confianza del equipo.") }),
            CrearPregunta(leccionId, 4, "El liderazgo servicial se caracteriza por:",
                "El liderazgo servicial pone las necesidades del equipo por encima de las del líder.",
                new[] { ("Hacer todo el trabajo tú solo", false, "Eso no es servicio, es falta de delegación."), ("Poner al equipo primero", true, "¡Correcto! Tu éxito como líder se mide por el éxito de tu equipo."), ("Servir café en las reuniones", false, "El servicio se refiere a apoyar el crecimiento del equipo."), ("No tomar nunca decisiones difíciles", false, "Un líder servicial sí toma decisiones difíciles pensando en el equipo.") }),
            CrearPregunta(leccionId, 5, "¿Cuándo es apropiado usar el estilo delegativo?",
                "El estilo delegativo funciona con personas experimentadas y motivadas.",
                new[] { ("Con empleados nuevos", false, "Los nuevos necesitan más guía y estructura."), ("Cuando hay crisis", false, "En crisis se necesita más dirección, no menos."), ("Con expertos motivados", true, "¡Correcto! Los expertos motivados solo necesitan recursos y autonomía."), ("Siempre, es el mejor estilo", false, "No hay un estilo universal; depende de la situación.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarQuizTrabajoEquipoAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "Según Lencioni, ¿cuál es la base de un equipo funcional?",
                "Sin confianza, las demás dinámicas del equipo se desmoronan.",
                new[] { ("Resultados económicos", false, "Los resultados son la consecuencia, no la base."), ("Confianza", true, "¡Correcto! La confianza es el cimiento sobre el que se construye todo lo demás."), ("Reglas estrictas", false, "Las reglas ayudan pero no son la base."), ("Un jefe fuerte", false, "Un equipo depende de todos, no solo del jefe.") }),
            CrearPregunta(leccionId, 2, "¿Por qué el conflicto constructivo es positivo para un equipo?",
                "Los debates honestos llevan a mejores decisiones que el falso consenso.",
                new[] { ("Mantiene el drama interesante", false, "No se trata de drama sino de debate productivo."), ("Mejora las decisiones del equipo", true, "¡Correcto! El debate honesto evita decisiones mediocres por falso consenso."), ("Elimina a los miembros débiles", false, "El conflicto constructivo no busca eliminar sino mejorar."), ("No es positivo, siempre es dañino", false, "El conflicto constructivo es esencial para buenos resultados.") }),
            CrearPregunta(leccionId, 3, "En el modelo Belbin, ¿qué rol se encarga de generar ideas creativas?",
                "El Cerebro es el rol que aporta creatividad e innovación.",
                new[] { ("Implementador", false, "El implementador convierte ideas en planes, no las genera."), ("Cerebro", true, "¡Correcto! El Cerebro es la fuente de ideas creativas e innovadoras."), ("Finalizador", false, "El finalizador cuida los detalles de ejecución."), ("Coordinador", false, "El coordinador facilita la participación pero no es el generador de ideas.") }),
            CrearPregunta(leccionId, 4, "¿Cuántos roles identifica el modelo Belbin?",
                "Belbin identifica 9 roles distribuidos en acción, sociales y mentales.",
                new[] { ("5", false, "Son más. Belbin identifica 9 roles."), ("7", false, "Son 9, distribuidos en 3 categorías."), ("9", true, "¡Correcto! 3 de acción, 3 sociales y 3 mentales."), ("12", false, "Son 9, no 12.") }),
            CrearPregunta(leccionId, 5, "¿Qué ocurre cuando un equipo evita la responsabilidad mutua?",
                "Sin responsabilidad mutua, los estándares bajan y los objetivos no se cumplen.",
                new[] { ("El equipo mejora su autonomía", false, "Evitar la responsabilidad no es autonomía, es negligencia."), ("Los estándares bajan", true, "¡Correcto! Sin exigencia mutua, el rendimiento cae progresivamente."), ("Todos son más felices", false, "La falta de accountability genera frustración a largo plazo."), ("No ocurre nada relevante", false, "La evasión de responsabilidad es una de las 5 disfunciones más dañinas.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarQuizInteligenciaEmocionalAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Cuál es la primera dimensión de la inteligencia emocional según Goleman?",
                "Todo empieza por reconocer tus propias emociones.",
                new[] { ("Empatía", false, "La empatía es la 4ª dimensión. Primero hay que conocerse a uno mismo."), ("Autoconciencia", true, "¡Correcto! Reconocer tus emociones es el punto de partida de la IE."), ("Motivación", false, "La motivación es la 3ª dimensión."), ("Habilidades sociales", false, "Las habilidades sociales son la 5ª y última dimensión.") }),
            CrearPregunta(leccionId, 2, "¿Cuántos segundos tarda el córtex prefrontal en recuperar el control tras un secuestro amigdalar?",
                "La pausa de 6 segundos es el tiempo que tarda el cerebro racional en retomar el control.",
                new[] { ("2 segundos", false, "Es un poco más. Se necesitan al menos 6 segundos."), ("6 segundos", true, "¡Correcto! Por eso la técnica de la pausa de 6 segundos es tan efectiva."), ("30 segundos", false, "Son solo 6 segundos, menos de lo que imaginas."), ("2 minutos", false, "El cerebro puede recuperar el control mucho más rápido: en 6 segundos.") }),
            CrearPregunta(leccionId, 3, "¿Qué reduce un 50% la intensidad de una emoción?",
                "Nombrar lo que sientes activa el córtex prefrontal y reduce la activación emocional.",
                new[] { ("Ignorar la emoción", false, "Ignorar las emociones las intensifica, no las reduce."), ("Gritar para desahogarse", false, "Gritar suele amplificar la emoción."), ("Ponerle nombre a la emoción", true, "¡Correcto! El etiquetado emocional reduce la intensidad un 50%."), ("Respirar profundamente", false, "Respirar ayuda pero no es lo que reduce un 50%.") }),
            CrearPregunta(leccionId, 4, "El reencuadre cognitivo consiste en:",
                "Cambiar la interpretación de una situación cambia la respuesta emocional.",
                new[] { ("Negar los problemas", false, "No se trata de negar sino de reinterpretar."), ("Cambiar la interpretación de una situación", true, "¡Correcto! De 'esto es un desastre' a 'esto es un reto que puedo resolver'."), ("Culpar a otros", false, "Culpar es lo opuesto al reencuadre constructivo."), ("Evitar situaciones difíciles", false, "Evitar no resuelve nada; reencuadrar sí.") }),
            CrearPregunta(leccionId, 5, "¿Qué demostró Goleman sobre la IE y el éxito profesional?",
                "La IE es mejor predictor de éxito laboral que el CI.",
                new[] { ("La IE no influye en el trabajo", false, "Todo lo contrario: la IE es fundamental."), ("La IE predice más el éxito que el CI", true, "¡Correcto! La inteligencia emocional supera al coeficiente intelectual como predictor."), ("Solo importa para psicólogos", false, "La IE es relevante para todos los profesionales."), ("Es innata y no se puede desarrollar", false, "La IE se puede y se debe desarrollar a lo largo de la vida.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarQuizNetworkingAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Cuál es el principio fundamental del networking efectivo?",
                "El networking real se basa en dar antes de recibir.",
                new[] { ("Pedir favores rápidamente", false, "Pedir sin dar primero destruye relaciones."), ("Dar valor antes de pedir", true, "¡Correcto! La generosidad es la base del networking auténtico."), ("Tener muchas tarjetas de visita", false, "Las tarjetas no son lo importante, las relaciones sí."), ("Hablar solo de ti mismo", false, "El networking se basa en escuchar y dar valor.") }),
            CrearPregunta(leccionId, 2, "¿Qué porcentaje del networking efectivo ocurre en el seguimiento?",
                "El primer encuentro es solo el comienzo; el seguimiento es donde se construye la relación.",
                new[] { ("20%", false, "Es mucho más. El seguimiento es la clave."), ("50%", false, "Es aún más. El 80% del éxito está en el seguimiento."), ("80%", true, "¡Correcto! El seguimiento posterior es donde realmente se construyen las relaciones."), ("100%", false, "El primer contacto también importa, pero el 80% está en el seguimiento.") }),
            CrearPregunta(leccionId, 3, "¿Qué es un elevator pitch?",
                "Es tu presentación profesional en 30 segundos.",
                new[] { ("Un discurso de 10 minutos", false, "Un elevator pitch dura máximo 30 segundos."), ("Tu presentación profesional en 30 segundos", true, "¡Correcto! Es lo que dirías en un ascensor con alguien importante."), ("Una carta de presentación", false, "El elevator pitch es oral y breve."), ("Tu currículum resumido", false, "Va más allá del CV: es tu propuesta de valor.") }),
            CrearPregunta(leccionId, 4, "¿Cuáles son las 3 partes de la fórmula PVR para el elevator pitch?",
                "Problema, Valor y Resultado forman la estructura ideal.",
                new[] { ("Persona, Valor, Razón", false, "La P es de Problema, no de Persona."), ("Problema, Valor, Resultado", true, "¡Correcto! Describe el problema, tu valor único y un resultado concreto."), ("Pasión, Visión, Realidad", false, "La fórmula PVR es más específica y práctica."), ("Presentación, Venta, Remate", false, "El elevator pitch no es un discurso de venta agresivo.") }),
            CrearPregunta(leccionId, 5, "¿Qué es más valioso en networking?",
                "La profundidad de las relaciones supera a la cantidad de contactos.",
                new[] { ("1000 conexiones en LinkedIn", false, "La cantidad no equivale a calidad."), ("Asistir a muchos eventos", false, "La asistencia sin seguimiento no genera valor."), ("10 relaciones profundas y genuinas", true, "¡Correcto! La calidad siempre supera a la cantidad en networking."), ("Tener muchos seguidores en redes", false, "Los seguidores no son lo mismo que una red profesional.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarQuizPersuasionAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Cuántos principios de persuasión identificó Robert Cialdini?",
                "Cialdini identificó 6 principios universales de influencia.",
                new[] { ("3", false, "Son más. Cialdini identificó 6 principios."), ("6", true, "¡Correcto! Reciprocidad, compromiso, prueba social, autoridad, simpatía y escasez."), ("10", false, "Son 6, no 10."), ("12", false, "Cialdini identificó exactamente 6 principios.") }),
            CrearPregunta(leccionId, 2, "¿Cuál es la diferencia entre persuasión y manipulación?",
                "La intención marca la diferencia: beneficio mutuo vs. beneficio propio.",
                new[] { ("No hay diferencia", false, "La diferencia es fundamental y ética."), ("La persuasión busca beneficio mutuo", true, "¡Correcto! Persuadir es ganar-ganar; manipular es ganar-perder."), ("La manipulación es más efectiva", false, "La manipulación puede funcionar a corto plazo pero destruye la confianza."), ("La persuasión usa datos falsos", false, "La persuasión ética se basa en verdad y transparencia.") }),
            CrearPregunta(leccionId, 3, "Las historias son __ veces más memorables que los datos solos:",
                "El storytelling potencia enormemente la retención del mensaje.",
                new[] { ("2 veces", false, "El impacto es mucho mayor."), ("5 veces", false, "Es aún más impactante."), ("22 veces", true, "¡Correcto! Las historias son 22 veces más memorables que los datos solos."), ("100 veces", false, "Son 22 veces, que ya es un impacto enorme.") }),
            CrearPregunta(leccionId, 4, "¿Qué principio de Cialdini explica por qué seguimos a la mayoría?",
                "La prueba social nos lleva a imitar el comportamiento de otros.",
                new[] { ("Reciprocidad", false, "La reciprocidad se basa en devolver favores."), ("Autoridad", false, "La autoridad se basa en la expertise."), ("Prueba social", true, "¡Correcto! Si otros lo hacen, nuestro cerebro asume que es correcto."), ("Escasez", false, "La escasez se basa en la disponibilidad limitada.") }),
            CrearPregunta(leccionId, 5, "Las preguntas socráticas sirven para:",
                "Las preguntas socráticas guían al interlocutor hacia la conclusión sin imponerla.",
                new[] { ("Confundir al interlocutor", false, "El objetivo no es confundir sino guiar."), ("Demostrar superioridad intelectual", false, "Las preguntas socráticas no buscan dominar."), ("Guiar hacia una conclusión sin imponerla", true, "¡Correcto! Preguntar es más persuasivo que afirmar."), ("Alargar la conversación", false, "El objetivo es guiar, no alargar.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    // --- ESCENARIOS ---

    private static async Task SembrarEscenarioComunicacionAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Estás en una reunión con un cliente que está visiblemente molesto porque el proyecto se ha retrasado dos semanas. El cliente levanta la voz y dice: «Esto es inaceptable, llevamos meses esperando y siempre hay excusas». Tu jefe te mira esperando que tú respondas.",
            Contexto = "Eres el responsable del proyecto. El retraso se debe a cambios de requisitos que pidió el propio cliente, pero él no lo recuerda así.",
            GuionAudio = "Este escenario pondrá a prueba tu capacidad de comunicación bajo presión. Recuerda los pilares: claridad, empatía, escucha activa y asertividad."
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Escuchar sin interrumpir, validar su frustración y luego presentar los hechos con calma: «Entiendo su frustración. Tiene razón en que el plazo se ha extendido. Permítame mostrarle el cronograma con los cambios que se solicitaron para que juntos encontremos la mejor solución.»",
                TipoResultado = TipoResultadoEscenario.Positivo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Excelente! Usaste los 4 pilares: escuchaste activamente, mostraste empatía, fuiste claro con los hechos y asertivo al proponer una solución. El cliente se siente escuchado y puedes avanzar constructivamente."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Responder profesionalmente pero a la defensiva: «Disculpe las molestias, pero debo recordarle que hubo varios cambios de requisitos durante el proyecto que causaron el retraso.»",
                TipoResultado = TipoResultadoEscenario.Neutral, PuntosOtorgados = 10,
                TextoRetroalimentacion = "Tu respuesta es correcta en los hechos, pero al empezar a la defensiva puedes hacer que el cliente se sienta atacado. Hubiera sido mejor validar primero su emoción antes de presentar los hechos."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Decir directamente: «El retraso es por los cambios que usted pidió. No es justo que nos culpe a nosotros.»",
                TipoResultado = TipoResultadoEscenario.Negativo, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Aunque tengas razón en los hechos, responder así daña la relación con el cliente. Faltó empatía y escucha activa. En comunicación profesional, cómo dices algo importa tanto como qué dices."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenarioLiderazgoAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Tu equipo de 5 personas lleva 3 semanas sin cumplir los objetivos semanales. La moral está baja y notas que dos personas llegan tarde con frecuencia. Tu director te ha pedido resultados urgentes.",
            Contexto = "Eres el team lead. Sabes que el equipo está sobrecargado pero también que algunos han perdido la motivación.",
            GuionAudio = "Como líder, debes equilibrar la presión por resultados con el bienestar del equipo. ¿Qué estilo de liderazgo aplicarás?"
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Reunir al equipo para una conversación honesta. Preguntar qué les bloquea, escuchar sus preocupaciones, compartir la presión que tienes, y juntos redefinir prioridades y objetivos alcanzables.",
                TipoResultado = TipoResultadoEscenario.Positivo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Gran liderazgo! Combinaste vulnerabilidad, escucha y participación. Al involucrar al equipo en la solución, generas compromiso genuino y recuperas la moral."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Hablar individualmente con cada persona para entender su situación, y luego reorganizar tareas según las fortalezas de cada uno.",
                TipoResultado = TipoResultadoEscenario.Neutral, PuntosOtorgados = 12,
                TextoRetroalimentacion = "Buen enfoque al hablar individualmente, pero te faltó la conversación grupal. El equipo necesita sentirse unido y parte de la solución, no solo recibir indicaciones individuales."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Enviar un email exigiendo que todos cumplan los objetivos esta semana o habrá consecuencias. Hay que ser firme.",
                TipoResultado = TipoResultadoEscenario.Negativo, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Las amenazas pueden generar resultados a corto plazo, pero destruyen la confianza y la motivación. Un líder que solo presiona sin escuchar pierde a su equipo."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenarioTrabajoEquipoAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Dos miembros clave de tu equipo, Laura y Carlos, han dejado de hablarse después de un desacuerdo en la última reunión. El trabajo se está viendo afectado porque no coordinan tareas que dependen entre sí.",
            Contexto = "Laura cree que Carlos no valora sus ideas. Carlos cree que Laura impone sus opiniones sin escuchar.",
            GuionAudio = "La mediación de conflictos es una habilidad esencial en el trabajo en equipo. ¿Cómo manejarás esta situación?"
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Reunirte primero con cada uno por separado para escuchar su versión, y luego facilitar una conversación conjunta enfocada en intereses comunes y acuerdos de colaboración.",
                TipoResultado = TipoResultadoEscenario.Positivo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Excelente mediación! Escuchar por separado primero evita que se pongan a la defensiva, y la reunión conjunta permite construir acuerdos. Aplicaste perfectamente la resolución de conflictos."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Reunir a ambos inmediatamente y pedirles que resuelvan sus diferencias como profesionales.",
                TipoResultado = TipoResultadoEscenario.Neutral, PuntosOtorgados = 8,
                TextoRetroalimentacion = "Aunque es importante abordar el tema, juntar a dos personas en conflicto sin preparación previa puede escalar la situación. Escuchar individualmente primero hubiera sido más efectivo."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Ignorar el conflicto y redistribuir las tareas para que no tengan que interactuar.",
                TipoResultado = TipoResultadoEscenario.Negativo, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Evitar el conflicto es la 2ª disfunción de Lencioni. Separar a las personas no resuelve el problema y puede crear más tensión en el equipo."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenarioInteligenciaEmocionalAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "En una reunión de equipo, tu jefe critica duramente tu último informe delante de todos: «Este informe es mediocre. Esperaba mucho más de ti.» Sientes que te sube la sangre a la cabeza.",
            Contexto = "Sabes que tu informe era correcto pero quizás no tenía la profundidad que esperaba tu jefe.",
            GuionAudio = "Este es un momento clave para tu inteligencia emocional. Tu amígdala quiere reaccionar. Tu córtex prefrontal necesita 6 segundos para tomar el control."
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Hacer la pausa de 6 segundos internamente, reconocer tu emoción («estoy enfadado»), respirar y responder con calma: «Agradezco el feedback. Me gustaría entender mejor qué esperaba para mejorar el próximo informe. ¿Podemos hablarlo después de la reunión?»",
                TipoResultado = TipoResultadoEscenario.Positivo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Perfecto! Aplicaste la pausa de 6 segundos, el etiquetado emocional y el reencuadre cognitivo. Convertiste una crítica en una oportunidad de aprendizaje sin perder la compostura."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Aceptar la crítica sin decir nada, pero por dentro sentirte resentido.",
                TipoResultado = TipoResultadoEscenario.Neutral, PuntosOtorgados = 5,
                TextoRetroalimentacion = "Controlar la reacción externa está bien, pero suprimir la emoción sin procesarla genera resentimiento. Hubiera sido mejor expresar calmadamente tu perspectiva."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Responder inmediatamente: «El informe está bien hecho. Si usted quería algo diferente, debería haberlo especificado desde el principio.»",
                TipoResultado = TipoResultadoEscenario.Negativo, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Reaccionaste impulsivamente, dominado por la amígdala. Aunque tengas razón, esta respuesta daña tu imagen profesional y la relación con tu jefe."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenarioNetworkingAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Estás en un evento de tu sector. Ves a una persona que trabaja en una empresa donde te encantaría trabajar. Está sola tomando un café. Tienes la oportunidad perfecta de acercarte.",
            Contexto = "Has preparado tu elevator pitch pero estás nervioso. La persona parece accesible.",
            GuionAudio = "El networking se hace en los momentos de oportunidad. Recuerda: generosidad primero, autenticidad siempre. ¿Cómo te acercarás?"
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Acercarte con una sonrisa, comentar algo sobre el evento, mostrar interés genuino en su trabajo preguntándole qué le ha traído al evento, y luego compartir brevemente tu experiencia buscando puntos en común.",
                TipoResultado = TipoResultadoEscenario.Positivo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Perfecto networking! Empezaste con algo natural, mostraste interés genuino antes de hablar de ti, y buscaste conexión auténtica. Así se construyen relaciones profesionales duraderas."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Acercarte y presentarte directamente con tu elevator pitch, explicando quién eres y qué haces.",
                TipoResultado = TipoResultadoEscenario.Neutral, PuntosOtorgados = 10,
                TextoRetroalimentacion = "Está bien que te acercaras, pero lanzar tu pitch directamente puede parecer transaccional. Hubiera sido mejor mostrar interés primero en la otra persona antes de hablar de ti."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "No acercarte porque no quieres parecer desesperado. Ya intentarás conectar por LinkedIn después.",
                TipoResultado = TipoResultadoEscenario.Negativo, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Perder la oportunidad por miedo es el mayor obstáculo del networking. Un mensaje de LinkedIn no tiene ni de lejos el impacto de una conversación cara a cara."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenarioPersuasionAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Quieres proponer cambiar la herramienta de gestión de proyectos del equipo. La actual es ineficiente pero el equipo lleva 2 años usándola. Sabes que habrá resistencia al cambio.",
            Contexto = "Has investigado alternativas y tienes datos que demuestran que la nueva herramienta ahorraría un 30% de tiempo.",
            GuionAudio = "Persuadir a un grupo que resiste el cambio requiere estrategia. ¿Qué principios de Cialdini aplicarás?"
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Preparar una presentación con datos (autoridad), mostrar que otros equipos ya la usan con éxito (prueba social), ofrecer encargarte tú de la migración (reciprocidad), y proponer un período de prueba de 2 semanas sin compromiso.",
                TipoResultado = TipoResultadoEscenario.Positivo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Brillante! Combinaste 3 principios de Cialdini de forma ética: autoridad con datos, prueba social con casos reales, y reciprocidad al ofrecer tu esfuerzo. El período de prueba reduce la resistencia al cambio."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Enviar un email al equipo explicando las ventajas de la nueva herramienta con datos concretos.",
                TipoResultado = TipoResultadoEscenario.Neutral, PuntosOtorgados = 8,
                TextoRetroalimentacion = "Los datos son importantes pero un email puede ser fácilmente ignorado. La persuasión efectiva requiere conexión personal y storytelling, no solo información."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Decir al jefe que imponga el cambio directamente. Es la forma más rápida.",
                TipoResultado = TipoResultadoEscenario.Negativo, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Imponer cambios genera rechazo y sabotaje pasivo. La persuasión ética busca que las personas quieran el cambio, no que lo sufran."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }

    // --- LOGROS ---

    private static async Task SembrarLogrosAsync(AppDbContext contexto)
    {
        List<Logro> logros = new List<Logro>
        {
            new Logro { Slug = "primera-leccion", Titulo = "Primer paso", Descripcion = "Completar tu primera lección", Icono = "🎓", PuntosRequeridos = 0, Condicion = "lecciones_completadas >= 1" },
            new Logro { Slug = "quiz-perfecto", Titulo = "Mente brillante", Descripcion = "Obtener 100% en un quiz", Icono = "💯", PuntosRequeridos = 0, Condicion = "quiz_puntuacion_maxima == true" },
            new Logro { Slug = "racha-3-dias", Titulo = "Constancia inicial", Descripcion = "Mantener una racha de 3 días", Icono = "🔥", PuntosRequeridos = 0, Condicion = "racha_dias >= 3" },
            new Logro { Slug = "racha-7-dias", Titulo = "Semana de fuego", Descripcion = "Mantener una racha de 7 días consecutivos", Icono = "🔥", PuntosRequeridos = 0, Condicion = "racha_dias >= 7" },
            new Logro { Slug = "explorador", Titulo = "Explorador curioso", Descripcion = "Iniciar lecciones en 3 áreas diferentes", Icono = "🧭", PuntosRequeridos = 0, Condicion = "areas_iniciadas >= 3" },
            new Logro { Slug = "todas-las-areas", Titulo = "Renacimiento profesional", Descripcion = "Completar al menos una lección en cada área", Icono = "🌟", PuntosRequeridos = 0, Condicion = "areas_con_leccion >= 6" },
            new Logro { Slug = "maestro-comunicacion", Titulo = "Maestro comunicador", Descripcion = "Completar el nivel 1 de Comunicación Efectiva", Icono = "💬", PuntosRequeridos = 0, Condicion = "nivel_completado_comunicacion >= 1" },
            new Logro { Slug = "primer-roleplay", Titulo = "Actor natural", Descripcion = "Completar tu primer roleplay con la IA", Icono = "🎭", PuntosRequeridos = 0, Condicion = "roleplays_completados >= 1" },
            new Logro { Slug = "coleccionista-50", Titulo = "Coleccionista", Descripcion = "Acumular 50 puntos", Icono = "⭐", PuntosRequeridos = 50, Condicion = "puntos_totales >= 50" },
            new Logro { Slug = "centenario", Titulo = "Centenario", Descripcion = "Acumular 100 puntos", Icono = "🏆", PuntosRequeridos = 100, Condicion = "puntos_totales >= 100" }
        };

        contexto.Set<Logro>().AddRange(logros);
        await contexto.SaveChangesAsync();
    }

    // --- HELPER ---

    private static PreguntaQuiz CrearPregunta(int leccionId, int orden, string textoPregunta,
        string explicacion, (string texto, bool esCorrecta, string retro)[] opciones)
    {
        PreguntaQuiz pregunta = new PreguntaQuiz
        {
            LeccionId = leccionId,
            TextoPregunta = textoPregunta,
            Explicacion = explicacion,
            Orden = orden
        };

        int ordenOpcion = 1;
        foreach ((string texto, bool esCorrecta, string retro) opcion in opciones)
        {
            pregunta.Opciones.Add(new OpcionQuiz
            {
                TextoOpcion = opcion.texto,
                EsCorrecta = opcion.esCorrecta,
                Retroalimentacion = opcion.retro,
                Orden = ordenOpcion++
            });
        }

        return pregunta;
    }
}
