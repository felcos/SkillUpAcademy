using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de lecciones para el área Gestión del Tiempo (3 niveles).
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesGestionTiempoAsync(AppDbContext contexto)
    {
        // ===== NIVEL 1 — Fundamentos (NivelId=25) =====
        Leccion l1 = new Leccion
        {
            Id = 101, NivelId = 25, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "La Matriz de Eisenhower: urgente vs. importante",
            Descripcion = "Aprende a clasificar tareas según su urgencia e importancia para tomar mejores decisiones sobre en qué invertir tu tiempo.",
            Contenido = "## El error más común: confundir urgente con importante\n\nDwight D. Eisenhower, presidente de EE.UU. y comandante supremo aliado en la II Guerra Mundial, dijo: «Lo que es importante rara vez es urgente, y lo que es urgente rara vez es importante.» Esta frase es la base de uno de los frameworks de productividad más poderosos.\n\n## La Matriz de 4 cuadrantes\n\n### Cuadrante 1 — Urgente e Importante (HACER)\nCrisis, plazos de entrega inminentes, problemas críticos. Ejemplo: un servidor caído en producción o una propuesta que vence mañana. Debes actuar ahora.\n\n### Cuadrante 2 — Importante pero No Urgente (PLANIFICAR)\nFormación, relaciones estratégicas, planificación a largo plazo, ejercicio. Este es el cuadrante del crecimiento. Las personas altamente productivas pasan el 60-70% de su tiempo aquí.\n\n### Cuadrante 3 — Urgente pero No Importante (DELEGAR)\nInterrupciones, la mayoría de emails, reuniones innecesarias, llamadas no planificadas. Parecen urgentes pero no aportan valor real. Delega o minimiza.\n\n### Cuadrante 4 — Ni Urgente Ni Importante (ELIMINAR)\nRedes sociales sin propósito, tareas por inercia, perfeccionismo en detalles irrelevantes. Elimina sin remordimiento.\n\n## Datos reveladores\n\nSegún un estudio de McKinsey, los ejecutivos dedican el 28% de su jornada a email (C3) y solo el 14% a trabajo estratégico (C2). Las personas que invierten conscientemente en C2 reportan un 40% menos de estrés y un 25% más de satisfacción laboral.\n\n## Cómo aplicarla en 3 pasos\n\n1. **Lista completa**: Escribe todas tus tareas pendientes sin filtrar.\n2. **Clasifica**: Asigna cada tarea a uno de los 4 cuadrantes.\n3. **Actúa según el cuadrante**: Hacer (C1), Planificar con fecha (C2), Delegar (C3), Eliminar (C4).",
            PuntosClave = "[\"Urgente e importante no son sinónimos: la mayoría confunde ambos conceptos\",\"El Cuadrante 2 (importante no urgente) es donde ocurre el crecimiento real\",\"Los ejecutivos dedican solo el 14% al trabajo estratégico según McKinsey\",\"Clasificar tareas en 4 cuadrantes reduce el estrés un 40%\",\"Eliminar el Cuadrante 4 libera tiempo para lo que realmente importa\"]",
            GuionAudio = "Hoy vamos a hablar de uno de los frameworks más poderosos para gestionar tu tiempo: la Matriz de Eisenhower. La gran mayoría de profesionales confunde lo urgente con lo importante, y eso les lleva a estar siempre apagando fuegos. Vamos a cambiar eso.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = 102, NivelId = 25, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Técnica Pomodoro y timeboxing: estructura tu jornada",
            Descripcion = "Domina las técnicas de bloques de tiempo para maximizar tu concentración y reducir la procrastinación.",
            Contenido = "## El cerebro no está diseñado para la concentración sostenida\n\nLa neurociencia muestra que la atención plena dura entre 20 y 45 minutos. Después, el rendimiento cae en picado. Por eso trabajar 8 horas seguidas es una ilusión: realmente produces unas 4-5 horas efectivas.\n\n## La Técnica Pomodoro\n\nCreada por Francesco Cirillo en los años 80, usa un temporizador de cocina (pomodoro en italiano):\n\n### El ciclo básico\n1. **Elige UNA tarea** concreta.\n2. **Pon el temporizador a 25 minutos** y trabaja sin interrupciones.\n3. **Descanso corto de 5 minutos**: levántate, estira, bebe agua.\n4. **Cada 4 pomodoros**, descanso largo de 15-30 minutos.\n\n### Reglas fundamentales\n- Un pomodoro es **indivisible**: si te interrumpen, el pomodoro se invalida y reinicia.\n- Si terminas la tarea antes de los 25 minutos, usa el tiempo restante para revisar o mejorar.\n- Registra cuántos pomodoros te lleva cada tipo de tarea. Esto mejora drásticamente tu estimación.\n\n## Timeboxing: pomodoro a escala\n\nEl timeboxing aplica el mismo principio a bloques más grandes:\n\n### Cómo funciona\n- Asigna a cada tarea un **bloque fijo de tiempo** en tu calendario (30 min, 1h, 2h).\n- Cuando el bloque termina, **paras aunque no hayas acabado**.\n- Esto crea urgencia artificial (Cuadrante 1) para tareas importantes (Cuadrante 2).\n\n### Datos de efectividad\nElon Musk y Bill Gates usan timeboxing en bloques de 5 minutos. Un estudio de la Harvard Business Review mostró que el timeboxing es la técnica de productividad #1 entre 100 evaluadas. Los profesionales que lo usan completan un 20% más de tareas que los que trabajan con listas convencionales.",
            PuntosClave = "[\"La atención plena dura entre 20 y 45 minutos según la neurociencia\",\"La técnica Pomodoro usa ciclos de 25 minutos de trabajo + 5 de descanso\",\"Un pomodoro es indivisible: si te interrumpen, se reinicia\",\"El timeboxing crea urgencia artificial para tareas importantes\",\"Harvard Business Review lo posiciona como la técnica #1 de productividad\"]",
            GuionAudio = "¿Sabías que tu cerebro solo puede concentrarse de verdad durante 20 a 45 minutos? La técnica Pomodoro y el timeboxing trabajan con tu biología, no contra ella. Hoy vas a aprender a estructurar tu jornada para ser mucho más productivo.",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 10
        };

        Leccion l3 = new Leccion
        {
            Id = 103, NivelId = 25, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Gestión del Tiempo — Fundamentos",
            Descripcion = "Evalúa tus conocimientos sobre la Matriz de Eisenhower, Pomodoro y timeboxing.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = 104, NivelId = 25, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: El lunes caótico",
            Descripcion = "Llegas el lunes con 20 tareas pendientes, 3 reuniones y un email urgente del director. ¿Cómo priorizas?",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = 105, NivelId = 25, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Negociar plazos con tu jefe",
            Descripcion = "Tu jefe te pide un informe para hoy pero ya tienes la agenda llena. Practica cómo negociar plazos de forma profesional.",
            Contenido = "Tu jefe se acerca a tu mesa a las 10 de la mañana y te dice que necesita un informe de análisis de mercado para las 6 de la tarde. Ya tienes 3 tareas críticas planificadas con timeboxing. No puedes hacerlo todo. Debes negociar prioridades usando la Matriz de Eisenhower como argumento.",
            GuionAudio = "En este roleplay, yo seré tu jefe. Te voy a pedir algo urgente cuando ya tienes la agenda comprometida. Tu objetivo es negociar un plazo realista sin dañar la relación. Recuerda: usa la Matriz de Eisenhower para argumentar.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1
        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "gestión del tiempo",
            "La Matriz de Eisenhower",
            "Hoy aprenderás a distinguir lo urgente de lo importante. Esta confusión es la causa número uno de estrés laboral y baja productividad.",
            "La Matriz tiene 4 cuadrantes: Hacer (urgente e importante), Planificar (importante no urgente), Delegar (urgente no importante) y Eliminar. Las personas productivas pasan el 60-70% en el Cuadrante 2.",
            "Esta semana, antes de empezar cualquier tarea, pregúntate: ¿es urgente, importante, ambas o ninguna? Clasificar antes de actuar te cambiará la vida profesional.");

        // Escenas para lección 2
        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "técnicas de concentración",
            "Pomodoro y Timeboxing",
            "Tu cerebro solo se concentra de verdad entre 20 y 45 minutos. Vamos a trabajar con tu biología, no contra ella.",
            "La técnica Pomodoro usa ciclos de 25 minutos de trabajo más 5 de descanso. El timeboxing asigna bloques fijos a cada tarea en el calendario. Harvard Business Review lo posiciona como la técnica número 1 de productividad.",
            "Mañana prueba esto: pon un temporizador de 25 minutos, trabaja en una sola tarea y no toques el móvil. Después de 4 ciclos verás cuánto más has avanzado.");

        // Quiz gestión del tiempo
        await SembrarQuizGestionTiempoAsync(contexto, l3.Id);

        // Escenario gestión del tiempo
        await SembrarEscenarioGestionTiempoAsync(contexto, l4.Id);

        // ===== NIVEL 2 — Práctica (NivelId=26) =====
        Leccion n2l1 = new Leccion
        {
            Id = 131, NivelId = 26, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "La Ley de Parkinson: el trabajo se expande al tiempo disponible",
            Descripcion = "Descubre por qué las tareas tardan más de lo necesario y cómo usar plazos artificiales para ser más eficiente.",
            Contenido = "## La ley que nadie te enseñó\n\nEn 1955, Cyril Northcote Parkinson publicó en The Economist una observación brillante: «El trabajo se expande hasta llenar el tiempo disponible para su realización.» Si tienes una semana para escribir un email, tardarás una semana. Si tienes 15 minutos, lo harás en 15 minutos, y probablemente igual de bien.\n\n## La evidencia científica\n\nUn estudio de la Universidad de Stanford demostró que los trabajadores que tienen plazos ajustados pero realistas producen un 30% más que quienes tienen plazos holgados. La razón es neurológica: los plazos activan la dopamina y el córtex prefrontal, mejorando la concentración.\n\n## Corolarios de Parkinson\n\n### Ley de los datos\nLos datos se expanden para llenar el almacenamiento disponible. Aplica a tu bandeja de entrada: sin límites, acumulas sin parar.\n\n### Ley de la trivialidad (Bikeshedding)\nUn comité dedicará más tiempo a discutir el color del cobertizo de bicicletas que el presupuesto de una central nuclear. Las decisiones triviales consumen tiempo desproporcionado porque todos se sienten competentes para opinar.\n\n## Cómo combatir la Ley de Parkinson\n\n### 1. Plazos artificiales agresivos\nSi crees que una tarea llevará 2 horas, asigna 1 hora 15 minutos. El 80% de las veces lo conseguirás.\n\n### 2. Regla del 80/20 (Principio de Pareto)\nEl 80% del valor viene del 20% del esfuerzo. Identifica ese 20% y empieza por ahí.\n\n### 3. Definir \"suficientemente bueno\"\nAntes de empezar, define qué criterios mínimos debe cumplir el resultado. Cuando los cumpla, para.\n\n### 4. Trabajo en sprints\nBloquea 90 minutos máximo por tarea compleja. Al terminar el bloque, evalúa: ¿está suficientemente bueno? Si sí, pasa a lo siguiente.",
            PuntosClave = "[\"El trabajo se expande hasta llenar el tiempo disponible — Ley de Parkinson 1955\",\"Plazos ajustados aumentan la productividad un 30% según Stanford\",\"El bikeshedding hace que gastemos más tiempo en decisiones triviales\",\"Asignar un 60-70% del tiempo que crees necesitar combate la expansión\",\"Definir 'suficientemente bueno' antes de empezar evita el perfeccionismo\"]",
            GuionAudio = "¿Alguna vez has notado que una tarea que debería llevar una hora acaba ocupando toda la mañana? Eso tiene nombre: Ley de Parkinson. Y hoy vas a aprender a usarla a tu favor en lugar de ser su víctima.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 10
        };

        Leccion n2l2 = new Leccion
        {
            Id = 132, NivelId = 26, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Batching y Deep Work: la ciencia de la concentración profunda",
            Descripcion = "Aprende el método de Cal Newport para producir trabajo de alto valor y la técnica de agrupar tareas similares.",
            Contenido = "## El coste oculto del multitasking\n\nCada vez que cambias de tarea, tu cerebro necesita 23 minutos para recuperar la concentración plena (estudio de la Universidad de California, Irvine). Si revisas el email cada 15 minutos, nunca llegas a concentración profunda.\n\n## Deep Work: el concepto de Cal Newport\n\nEn su libro «Deep Work: Rules for Focused Success in a Distracted World» (2016), Cal Newport define:\n\n### Deep Work (Trabajo profundo)\nActividades profesionales realizadas en estado de concentración sin distracciones que llevan tus capacidades cognitivas al límite. Generan nuevo valor, mejoran tus habilidades y son difíciles de replicar.\n\n### Shallow Work (Trabajo superficial)\nTareas logísticas que no requieren esfuerzo cognitivo y se pueden hacer distraído: emails, reuniones rutinarias, formularios.\n\n## Las 4 filosofías de Deep Work\n\n### 1. Monástica\nEliminar casi todas las distracciones permanentemente. Ejemplo: un novelista que no tiene email.\n\n### 2. Bimodal\nAlternar períodos largos de Deep Work (días o semanas) con períodos normales. Ejemplo: un profesor que investiga en verano.\n\n### 3. Rítmica\nBloques fijos diarios de Deep Work (3-4 horas siempre a la misma hora). La más práctica para profesionales.\n\n### 4. Periodística\nEncajar Deep Work en cualquier hueco libre. Solo funciona para expertos con mucha práctica.\n\n## Batching: agrupar para no fragmentar\n\nEl batching consiste en agrupar tareas similares en un solo bloque:\n\n- **Email**: revisarlo 3 veces al día (9:00, 13:00, 17:00), no cada 5 minutos.\n- **Reuniones**: concentrarlas en 2 días (martes y jueves), dejando lunes, miércoles y viernes para Deep Work.\n- **Llamadas**: un bloque de 30 minutos para hacer todas seguidas.\n- **Tareas administrativas**: un bloque semanal el viernes por la tarde.\n\n## El resultado\n\nNewport cita que un trabajador del conocimiento que practica Deep Work 4 horas diarias produce más que uno que trabaja 8 horas en modo fragmentado. La calidad del trabajo producido en Deep Work es exponencialmente superior.",
            PuntosClave = "[\"Cambiar de tarea cuesta 23 minutos de recuperación de concentración\",\"Deep Work es trabajo cognitivamente exigente sin distracciones — genera valor real\",\"La filosofía rítmica (bloques diarios fijos) es la más práctica para profesionales\",\"Batching agrupa tareas similares para evitar el coste de cambio de contexto\",\"4 horas de Deep Work producen más que 8 horas de trabajo fragmentado\"]",
            GuionAudio = "Cada vez que miras el móvil mientras trabajas, tu cerebro necesita 23 minutos para volver a concentrarse. Hoy vamos a hablar de Deep Work y batching: dos estrategias que multiplicarán la calidad de tu trabajo.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 12
        };

        Leccion n2l3 = new Leccion
        {
            Id = 133, NivelId = 26, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Gestión del Tiempo — Práctica",
            Descripcion = "Evalúa tus conocimientos sobre la Ley de Parkinson, Deep Work y batching.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 5
        };

        Leccion n2l4 = new Leccion
        {
            Id = 134, NivelId = 26, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: La semana de entregas múltiples",
            Descripcion = "Tienes 3 proyectos con deadline la misma semana. ¿Cómo organizas tu tiempo?",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 12
        };

        Leccion n2l5 = new Leccion
        {
            Id = 135, NivelId = 26, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Proteger tu bloque de Deep Work",
            Descripcion = "Un colega quiere interrumpir tu bloque de concentración para una reunión improvisada. Gestiona la situación.",
            Contenido = "Estás en tu bloque de Deep Work programado de 9:00 a 12:00. A las 9:30, tu compañero Marcos se acerca y te dice que necesita tu ayuda urgente con una presentación para la tarde. Sabes que tu proyecto tiene deadline mañana y este bloque es tu única ventana de concentración. Debes gestionar la interrupción protegiendo tu productividad sin dañar la relación.",
            GuionAudio = "En este roleplay, yo seré tu compañero Marcos. Voy a intentar interrumpir tu bloque de Deep Work. Tu reto es proteger tu tiempo de concentración sin ser descortés y ofreciéndole una alternativa viable.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 12
        };

        contexto.Set<Leccion>().AddRange(n2l1, n2l2, n2l3, n2l4, n2l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1 nivel 2
        await SembrarEscenasTeoricasAsync(contexto, n2l1.Id, "la Ley de Parkinson",
            "El trabajo se expande al tiempo disponible",
            "En 1955, Parkinson descubrió algo que cambia la forma de gestionar el tiempo: si tienes una semana para un email, tardarás una semana. Hoy vas a aprender a usar esto a tu favor.",
            "Los plazos ajustados aumentan la productividad un 30%. La clave es asignar un 60-70% del tiempo que crees necesitar y definir qué es suficientemente bueno antes de empezar.",
            "Prueba esto hoy: elige una tarea que creas que lleva 2 horas, asígnale 1 hora 15 y define el resultado mínimo aceptable antes de empezar. Te sorprenderá el resultado.");

        // Escenas para lección 2 nivel 2
        await SembrarEscenasTeoricasAsync(contexto, n2l2.Id, "concentración profunda",
            "Deep Work y Batching",
            "Cada cambio de tarea te cuesta 23 minutos de concentración. El multitasking es un mito: tu cerebro no puede procesar dos tareas cognitivas a la vez.",
            "Cal Newport demostró que 4 horas de Deep Work producen más que 8 horas fragmentadas. El batching agrupa tareas similares para minimizar el coste de cambio de contexto.",
            "Esta semana, bloquea 3 horas en tu calendario para Deep Work. Silencia el móvil, cierra el email y trabaja en tu tarea más importante. Mide cuánto produces comparado con un día normal.");

        // ===== NIVEL 3 — Dominio (NivelId=27) =====
        Leccion n3l1 = new Leccion
        {
            Id = 161, NivelId = 27, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Planificación estratégica trimestral con OKRs",
            Descripcion = "Aprende a usar Objectives and Key Results para alinear tu gestión del tiempo con metas de alto impacto.",
            Contenido = "## De la táctica diaria a la estrategia trimestral\n\nLa Matriz de Eisenhower y Pomodoro gestionan el día a día. Pero sin una visión estratégica, puedes ser muy eficiente haciendo las cosas equivocadas. Los OKRs (Objectives and Key Results) conectan tu tiempo diario con objetivos transformadores.\n\n## Qué son los OKRs\n\nCreados por Andy Grove en Intel y popularizados por Google, los OKRs tienen dos componentes:\n\n### Objective (Objetivo)\nQualitativo, inspirador, ambicioso. Responde a: ¿qué quiero lograr?\n- Ejemplo: «Convertirme en referente de gestión de proyectos en mi empresa»\n\n### Key Results (Resultados Clave)\nCuantitativos, medibles, con plazo. Responden a: ¿cómo sé que lo estoy logrando?\n- KR1: Completar la certificación PMP antes del 30 de junio\n- KR2: Liderar 3 proyectos cross-funcionales este trimestre\n- KR3: Reducir los retrasos en entregas del 25% al 5%\n\n## La planificación trimestral en 5 pasos\n\n### 1. Revisión del trimestre anterior (2 horas)\nAnaliza: ¿qué OKRs cumpliste? ¿Cuáles no? ¿Por qué? Sin este análisis, repites errores.\n\n### 2. Definir 3-5 objetivos trimestrales\nMenos es más. Si todo es prioritario, nada lo es. Google recomienda máximo 5 objetivos con 3-4 KRs cada uno.\n\n### 3. Desglose mensual\nCada mes debe tener hitos claros que alimenten los KRs trimestrales.\n\n### 4. Planificación semanal (30 minutos cada domingo)\nRevisa tus OKRs y pregúntate: ¿qué acciones de esta semana me acercan a mis Key Results?\n\n### 5. Check-in quincenal\nCada 2 semanas, puntúa tus KRs de 0 a 1.0. Google considera 0.6-0.7 un buen resultado (significa que el objetivo era ambicioso).\n\n## El sistema completo\n\nOKRs trimestrales → Hitos mensuales → Planificación semanal (Cuadrante 2 de Eisenhower) → Timeboxing diario → Pomodoros para ejecución. Cada nivel alimenta al siguiente.",
            PuntosClave = "[\"Los OKRs conectan la gestión diaria del tiempo con objetivos estratégicos transformadores\",\"Un Objective es qualitativo e inspirador; los Key Results son cuantitativos y medibles\",\"Google recomienda máximo 5 objetivos con 3-4 Key Results cada uno por trimestre\",\"La planificación semanal de 30 minutos conecta OKRs con acciones concretas\",\"Un KR puntuado 0.6-0.7 indica objetivos ambiciosos bien ejecutados\"]",
            GuionAudio = "Puedes dominar Pomodoro y Eisenhower, pero si no tienes una dirección estratégica, serás muy eficiente haciendo las cosas equivocadas. Hoy vamos a conectar tu gestión del tiempo con objetivos que realmente transformen tu carrera.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 12
        };

        Leccion n3l2 = new Leccion
        {
            Id = 162, NivelId = 27, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Delegación efectiva y automatización inteligente",
            Descripcion = "Aprende a multiplicar tu tiempo delegando correctamente y automatizando tareas repetitivas.",
            Contenido = "## El techo de la productividad individual\n\nPor mucho que optimices tu tiempo, tienes un límite: 24 horas al día. La delegación y la automatización son los únicos multiplicadores reales de productividad.\n\n## Los 5 niveles de delegación\n\nMichael Hyatt define 5 niveles según el grado de autonomía:\n\n### Nivel 1 — Haz exactamente lo que te digo\nPara tareas nuevas o personas sin experiencia. Instrucciones paso a paso.\n\n### Nivel 2 — Investiga y dame opciones\nLa persona investiga pero tú decides. Desarrolla su criterio.\n\n### Nivel 3 — Investiga, recomienda una opción y espera mi aprobación\nLa persona ya tiene criterio para recomendar. Tú solo validas.\n\n### Nivel 4 — Decide y actúa, pero infórmame\nConfianza alta. La persona actúa y te mantiene informado.\n\n### Nivel 5 — Decide y actúa, no necesitas informarme\nDelegación total. Solo intervenes si hay problemas.\n\n## El framework SMART para delegar\n\n- **Specific**: Qué resultado exacto esperas.\n- **Measurable**: Cómo sabrás que está bien hecho.\n- **Achievable**: ¿La persona tiene los recursos y habilidades?\n- **Relevant**: ¿Por qué importa esta tarea?\n- **Time-bound**: Deadline claro.\n\n## Automatización: el multiplicador silencioso\n\n### Regla de las 3 veces\nSi haces algo manualmente 3 veces, automatízalo. El coste de automatizar se paga solo.\n\n### Qué automatizar\n- **Emails repetitivos**: Plantillas y respuestas automáticas con reglas.\n- **Informes periódicos**: Dashboards que se actualizan solos.\n- **Agendamiento**: Herramientas como Calendly eliminan los 8-10 emails de ida y vuelta.\n- **Flujos de aprobación**: Workflows automáticos en lugar de cadenas de email.\n\n### La matriz Esfuerzo-Frecuencia\nAntes de automatizar, evalúa: ¿cuánto esfuerzo requiere la automatización vs. cuántas veces haré esta tarea? Si la frecuencia es alta y el esfuerzo de automatización es bajo, hazlo ya.\n\n## El resultado combinado\n\nUn profesional que delega en nivel 4-5 y automatiza tareas repetitivas puede multiplicar su productividad por 3-5x, liberando tiempo para el trabajo estratégico del Cuadrante 2.",
            PuntosClave = "[\"La delegación y automatización son los únicos multiplicadores reales de productividad\",\"Michael Hyatt define 5 niveles de delegación según el grado de autonomía\",\"El framework SMART asegura que la delegación sea clara y efectiva\",\"Si haces algo manualmente 3 veces, es candidato a automatización\",\"Delegar en nivel 4-5 y automatizar puede multiplicar la productividad 3-5x\"]",
            GuionAudio = "Hay un techo para la productividad individual: 24 horas al día. La delegación efectiva y la automatización inteligente son los únicos caminos para romper ese techo. Hoy aprenderás a multiplicar tu tiempo.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 15
        };

        Leccion n3l3 = new Leccion
        {
            Id = 163, NivelId = 27, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Gestión del Tiempo — Dominio",
            Descripcion = "Evalúa tus conocimientos sobre OKRs, delegación efectiva y automatización.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 5
        };

        Leccion n3l4 = new Leccion
        {
            Id = 164, NivelId = 27, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Reestructurar el departamento",
            Descripcion = "Te nombran responsable de un departamento ineficiente. Diseña una estrategia de gestión del tiempo para todo el equipo.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 15
        };

        Leccion n3l5 = new Leccion
        {
            Id = 165, NivelId = 27, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Presentar tu plan de OKRs al comité de dirección",
            Descripcion = "Presenta y defiende tus OKRs trimestrales ante un comité de dirección exigente.",
            Contenido = "El comité de dirección te ha pedido que presentes tus OKRs para el próximo trimestre. Hay 3 directores: uno enfocado en resultados financieros, otro en innovación y otro en eficiencia operativa. Debes presentar 3 objetivos con sus Key Results, defender tu planificación temporal y explicar cómo vas a delegar y automatizar para cumplirlos.",
            GuionAudio = "En este roleplay, yo seré los directores del comité. Vas a presentar tus OKRs trimestrales y te haré preguntas difíciles. Debes demostrar que tu planificación estratégica es sólida y que sabes gestionar el tiempo a nivel de equipo, no solo individual.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n3l1, n3l2, n3l3, n3l4, n3l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1 nivel 3
        await SembrarEscenasTeoricasAsync(contexto, n3l1.Id, "planificación estratégica con OKRs",
            "Planificación trimestral con OKRs",
            "Hoy vamos a subir de nivel. De gestionar el día a día pasamos a la planificación estratégica trimestral. Sin dirección, la eficiencia no sirve de nada.",
            "Los OKRs conectan tu tiempo diario con objetivos transformadores. Un Objective es inspirador, los Key Results son medibles. Google recomienda máximo 5 objetivos por trimestre. La planificación semanal de 30 minutos es el puente entre estrategia y ejecución.",
            "Define tu primer OKR personal esta semana: un objetivo ambicioso con 3 resultados clave medibles. Luego pregúntate cada lunes: ¿qué haré esta semana para avanzar en mis Key Results?");

        // Escenas para lección 2 nivel 3
        await SembrarEscenasTeoricasAsync(contexto, n3l2.Id, "delegación y automatización",
            "Multiplicar tu tiempo",
            "Hay un techo para tu productividad individual: 24 horas. Hoy vamos a romper ese techo con delegación efectiva y automatización inteligente.",
            "Michael Hyatt define 5 niveles de delegación. La clave es usar el framework SMART para cada tarea delegada. Si haces algo 3 veces manualmente, automatízalo. La combinación de delegación nivel 4-5 más automatización puede multiplicar tu productividad hasta 5 veces.",
            "Identifica 3 tareas que haces cada semana y que podrías delegar o automatizar. Empieza por la más frecuente. En un mes habrás recuperado horas de tu semana para trabajo estratégico.");
    }

    /// <summary>
    /// Quiz de Gestión del Tiempo — Nivel 1 (5 preguntas sobre Eisenhower, Pomodoro y timeboxing).
    /// </summary>
    private static async Task SembrarQuizGestionTiempoAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "En la Matriz de Eisenhower, ¿en qué cuadrante deberías pasar la mayor parte de tu tiempo?",
                "El Cuadrante 2 (importante pero no urgente) es donde ocurre el crecimiento real. Las personas altamente productivas dedican el 60-70% de su tiempo aquí.",
                new[] { ("Cuadrante 1 — Urgente e Importante", false, "El C1 son crisis que hay que resolver, pero vivir ahí genera estrés crónico."), ("Cuadrante 2 — Importante pero No Urgente", true, "¡Correcto! Formación, planificación y relaciones estratégicas: aquí es donde se construye el éxito a largo plazo."), ("Cuadrante 3 — Urgente pero No Importante", false, "El C3 son interrupciones que parecen urgentes pero no aportan valor real."), ("Cuadrante 4 — Ni Urgente Ni Importante", false, "El C4 son distracciones que deberías eliminar directamente.") }),
            CrearPregunta(leccionId, 2, "¿Cuánto dura un pomodoro estándar?",
                "Francesco Cirillo definió el pomodoro como un bloque de 25 minutos de trabajo concentrado seguido de 5 minutos de descanso.",
                new[] { ("15 minutos", false, "Es un poco más. El pomodoro estándar dura 25 minutos."), ("25 minutos", true, "¡Correcto! 25 minutos de trabajo concentrado seguidos de 5 minutos de descanso."), ("45 minutos", false, "Es más corto. 25 minutos es el estándar, aunque hay variantes."), ("60 minutos", false, "Demasiado largo. El cerebro pierde concentración plena después de 25-45 minutos.") }),
            CrearPregunta(leccionId, 3, "Según McKinsey, ¿qué porcentaje de su jornada dedican los ejecutivos al email?",
                "Los ejecutivos dedican el 28% de su jornada al email, que generalmente cae en el Cuadrante 3 de Eisenhower.",
                new[] { ("10%", false, "Es mucho más. El email consume una parte desproporcionada de la jornada ejecutiva."), ("28%", true, "¡Correcto! Casi un tercio de la jornada en una tarea que rara vez es estratégica."), ("45%", false, "No es tanto, pero el 28% ya es una cifra preocupante."), ("5%", false, "Ojalá fuera tan poco. Los datos muestran un 28%.") }),
            CrearPregunta(leccionId, 4, "¿Qué ocurre si te interrumpen durante un pomodoro?",
                "Un pomodoro es indivisible. Si te interrumpen, el pomodoro se invalida y debes reiniciarlo desde cero.",
                new[] { ("Pausas el temporizador y continúas después", false, "No. Un pomodoro roto pierde su efectividad. Hay que reiniciarlo."), ("El pomodoro se invalida y se reinicia", true, "¡Correcto! Esta regla estricta entrena tu capacidad de proteger los bloques de concentración."), ("Restas el tiempo de interrupción", false, "No hay cálculos parciales. El pomodoro es una unidad indivisible."), ("No pasa nada, sigues donde estabas", false, "La regla es clara: un pomodoro interrumpido no cuenta. Esto motiva a proteger tu tiempo.") }),
            CrearPregunta(leccionId, 5, "¿Qué técnica de productividad posiciona Harvard Business Review como la #1 entre 100 evaluadas?",
                "El timeboxing, que asigna bloques fijos de tiempo a tareas en el calendario, fue posicionado como la técnica más efectiva.",
                new[] { ("La lista de tareas tradicional", false, "Las listas son útiles pero no tan efectivas como asignar tiempos fijos."), ("El método GTD de David Allen", false, "GTD es un gran sistema pero no ocupó el primer puesto en este estudio."), ("El timeboxing", true, "¡Correcto! Asignar bloques fijos en el calendario supera a todas las demás técnicas evaluadas."), ("La regla de los 2 minutos", false, "Es un truco útil pero no es la técnica más efectiva según este estudio.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    /// <summary>
    /// Escenario de Gestión del Tiempo — Nivel 1 (el lunes caótico con 3 opciones).
    /// </summary>
    private static async Task SembrarEscenarioGestionTiempoAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Es lunes a las 8:30. Llegas a la oficina y encuentras: 47 emails sin leer, 3 reuniones programadas (9:30, 11:00 y 15:00), un mensaje de tu director pidiendo un informe de avance para las 12:00, y una lista de 8 tareas pendientes de la semana pasada. Tu compañera Ana te pide ayuda urgente con una presentación para las 10:00.",
            Contexto = "Eres responsable de un proyecto que tiene entrega el viernes. El informe que pide tu director es sobre ese proyecto. Las 8 tareas pendientes incluyen 3 del Cuadrante 2 (formación, planificación trimestral y mejora de procesos) y 5 operativas.",
            GuionAudio = "Este escenario pondrá a prueba tu capacidad de priorizar bajo presión. Recuerda la Matriz de Eisenhower y el timeboxing. No puedes hacerlo todo: la clave es elegir bien."
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Antes de abrir el email, dedicas 15 minutos a clasificar todo con la Matriz de Eisenhower. El informe del director es C1: lo timeboxeas de 8:45 a 10:30. Le dices a Ana que puedes ayudarla a las 10:30 (30 min). Bloqueas de 13:00 a 15:00 para las 3 tareas de C2. Las tareas operativas van en huecos entre reuniones. Los emails los revisas a las 12:30 y 17:00.",
                TipoResultado = TipoResultadoEscenario.Optimo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Excelente! Aplicaste perfectamente la Matriz de Eisenhower y el timeboxing. Priorizaste el informe urgente-importante, negociaste con Ana sin rechazarla, protegiste tiempo para el Cuadrante 2 y controlaste el email en lugar de dejar que te controle a ti."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Ayudas primero a Ana porque es urgente, luego trabajas en el informe del director, y después intentas hacer las demás tareas según vayan surgiendo durante el día.",
                TipoResultado = TipoResultadoEscenario.Aceptable, PuntosOtorgados = 10,
                TextoRetroalimentacion = "Ayudar a Ana es solidario, pero su urgencia no es tu prioridad más importante. Al no planificar con la matriz, el informe del director puede retrasarse y las tareas de Cuadrante 2 quedarán para otro día. Reaccionar sin clasificar reduce tu efectividad."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Empiezas leyendo los 47 emails para tener una visión completa de lo que ha pasado durante el fin de semana. Después decides qué hacer.",
                TipoResultado = TipoResultadoEscenario.Inadecuado, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Empezar por los 47 emails es caer en la trampa del Cuadrante 3. Cuando termines de leerlos (30-45 minutos mínimo), habrás perdido tu ventana más productiva del día y el informe del director estará en riesgo. El email debe ser una herramienta, no el dueño de tu mañana."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }
}
