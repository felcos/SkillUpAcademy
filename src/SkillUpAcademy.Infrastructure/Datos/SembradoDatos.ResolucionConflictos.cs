using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de lecciones para el área Resolución de Conflictos (3 niveles).
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesResolucionConflictosAsync(AppDbContext contexto)
    {
        // ===== NIVEL 1 — Fundamentos (NivelId=19) =====
        Leccion l1 = new Leccion
        {
            Id = 91, NivelId = 19, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Tipos de conflictos laborales y sus dinámicas",
            Descripcion = "Identifica los diferentes tipos de conflictos en el trabajo y comprende qué los origina para abordarlos con estrategia.",
            Contenido = "## El conflicto es inevitable, la destrucción no\n\nSegún el informe CPP Global de 2008, los empleados dedican una media de 2,8 horas semanales a gestionar conflictos. Eso son 359.000 millones de dólares anuales en horas pagadas solo en EE.UU. El conflicto no es el problema; la falta de herramientas para gestionarlo sí lo es.\n\n## Clasificación de conflictos laborales\n\n### 1. Conflicto de tarea\nDesacuerdos sobre el contenido del trabajo: cómo hacer algo, qué priorizar, qué enfoque técnico seguir. Cuando es moderado, **mejora** las decisiones del equipo.\n\n### 2. Conflicto de relación\nFricciones personales: antipatía, desconfianza, choque de personalidades. Es el tipo más destructivo porque activa el sistema de amenaza del cerebro.\n\n### 3. Conflicto de proceso\nDisputas sobre cómo organizar el trabajo: quién hace qué, roles, responsabilidades, plazos. Frecuente en equipos nuevos o mal estructurados.\n\n### 4. Conflicto de estatus\nLuchas por reconocimiento, autoridad o influencia. Común cuando hay promociones, reorganizaciones o nuevos líderes.\n\n## La escalada de Glasl\n\nFriedrich Glasl identificó 9 niveles de escalada, agrupados en 3 fases:\n- **Fase 1 (niveles 1-3):** Win-Win posible. Tensión, debate, acciones unilaterales.\n- **Fase 2 (niveles 4-6):** Win-Lose. Coaliciones, pérdida de imagen, amenazas.\n- **Fase 3 (niveles 7-9):** Lose-Lose. Destrucción limitada, fragmentación, guerra total.\n\nLa clave es intervenir en la Fase 1, donde ambas partes aún pueden ganar.",
            PuntosClave = "[\"Los empleados dedican 2,8 horas semanales a conflictos laborales\",\"Hay 4 tipos: tarea, relación, proceso y estatus\",\"El conflicto de tarea moderado mejora las decisiones\",\"La escalada de Glasl tiene 9 niveles en 3 fases: Win-Win, Win-Lose y Lose-Lose\",\"Intervenir en Fase 1 es crítico para resolución constructiva\"]",
            GuionAudio = "Hoy vamos a hablar de algo que todos vivimos pero pocos saben gestionar: los conflictos laborales. No se trata de evitarlos, se trata de entenderlos para transformarlos en oportunidades.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = 92, NivelId = 19, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "El modelo Thomas-Kilmann: 5 estilos de gestión de conflictos",
            Descripcion = "Conoce los 5 estilos de gestión de conflictos y aprende cuándo usar cada uno según la situación.",
            Contenido = "## Dos dimensiones, cinco estilos\n\nKenneth Thomas y Ralph Kilmann crearon el instrumento TKI (Thomas-Kilmann Instrument) en 1974, que mide dos dimensiones:\n- **Asertividad:** grado en que intentas satisfacer tus propios intereses.\n- **Cooperación:** grado en que intentas satisfacer los intereses del otro.\n\n## Los 5 estilos\n\n### 1. Competir (alta asertividad, baja cooperación)\nYo gano, tú pierdes. Útil cuando hay emergencias, principios éticos no negociables, o decisiones impopulares pero necesarias. Peligro: destruye relaciones si se abusa.\n\n### 2. Colaborar (alta asertividad, alta cooperación)\nGanamos los dos. Busca soluciones creativas que satisfagan a ambas partes. Requiere tiempo, confianza y habilidad para explorar intereses profundos. Es el estilo ideal cuando la relación importa y hay margen.\n\n### 3. Comprometer (asertividad media, cooperación media)\nCedemos los dos algo. Rápido y práctico cuando el tiempo apremia. Nadie queda completamente satisfecho, pero es justo. Ideal para conflictos de importancia moderada.\n\n### 4. Evitar (baja asertividad, baja cooperación)\nPosponer o retirarse. Válido cuando el tema es trivial, necesitas tiempo para pensar, o el conflicto se resolverá solo. Peligro: acumulación de tensión si se usa como patrón.\n\n### 5. Acomodar (baja asertividad, alta cooperación)\nCedo yo para que tú ganes. Estratégico cuando la relación es más importante que el tema, cuando estás equivocado, o cuando generas capital de buena voluntad. Peligro: resentimiento si siempre cedes.\n\n## ¿Cuál es tu estilo dominante?\n\nLa mayoría de las personas tiene 1-2 estilos por defecto. Los profesionales con alta inteligencia conflictual dominan los 5 y eligen según la situación.",
            PuntosClave = "[\"TKI mide dos dimensiones: asertividad y cooperación\",\"Competir funciona en emergencias pero destruye relaciones si se abusa\",\"Colaborar es ideal pero requiere tiempo y confianza\",\"Evitar es válido solo para temas triviales o como pausa estratégica\",\"Los mejores profesionales dominan los 5 estilos y eligen según contexto\"]",
            GuionAudio = "Thomas y Kilmann descubrieron que todos tenemos un estilo dominante para enfrentar conflictos. Hoy vas a identificar el tuyo y aprender cuándo cambiar de estrategia.",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 10
        };

        Leccion l3 = new Leccion
        {
            Id = 93, NivelId = 19, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Resolución de Conflictos",
            Descripcion = "Evalúa tus conocimientos sobre tipos de conflictos y el modelo Thomas-Kilmann.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = 94, NivelId = 19, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Mediación entre compañeros enfrentados",
            Descripcion = "Dos colegas de tu equipo llevan semanas sin hablarse tras una discusión por la autoría de un proyecto. Debes mediar.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = 95, NivelId = 19, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Negociación con un colega bloqueante",
            Descripcion = "Un compañero de otro departamento bloquea sistemáticamente tus solicitudes. Practica cómo abordar la situación.",
            Contenido = "Tu compañero del departamento de Calidad rechaza cada entregable que le envías con comentarios vagos como «no cumple los estándares». Sospechas que hay un conflicto no resuelto. Debes tener una conversación directa para desbloquear la situación.",
            GuionAudio = "En este roleplay, yo seré tu compañero del departamento de Calidad. Llevo semanas rechazando tus entregables. Recuerda: identifica el tipo de conflicto, elige tu estilo TKI y busca los intereses reales detrás de mi posición.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1
        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "tipos de conflictos laborales",
            "Tipos de conflictos y la escalada de Glasl",
            "Hoy vamos a clasificar los conflictos laborales. Entender qué tipo de conflicto enfrentas es el primer paso para resolverlo con éxito.",
            "Hay 4 tipos de conflictos laborales: tarea, relación, proceso y estatus. La escalada de Glasl nos muestra que intervenir temprano es clave para que ambas partes ganen.",
            "La próxima vez que detectes tensión en tu equipo, pregúntate: ¿es un conflicto de tarea, relación, proceso o estatus? La respuesta cambia completamente el enfoque.");

        // Escenas para lección 2
        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "modelo Thomas-Kilmann",
            "Los 5 estilos de gestión de conflictos",
            "Thomas y Kilmann demostraron que no existe un único estilo correcto para gestionar conflictos. Los profesionales más efectivos dominan los cinco estilos.",
            "Competir, colaborar, comprometer, evitar y acomodar. Cada estilo tiene su momento ideal según la asertividad y cooperación que requiera la situación.",
            "Identifica tu estilo dominante y practícalo esta semana. Cuando surja un desacuerdo, antes de reaccionar pregúntate: ¿qué estilo es el más adecuado aquí?");

        // Quiz resolución de conflictos
        await SembrarQuizResolucionConflictosAsync(contexto, l3.Id);

        // Escenario resolución de conflictos
        await SembrarEscenarioResolucionConflictosAsync(contexto, l4.Id);

        // ===== NIVEL 2 — Práctica (NivelId=20) =====
        Leccion n2l1 = new Leccion
        {
            Id = 121, NivelId = 20, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Comunicación No Violenta: el método Rosenberg",
            Descripcion = "Domina los 4 pasos de la CNV para transformar conversaciones difíciles en diálogos constructivos.",
            Contenido = "## Marshall Rosenberg y la CNV\n\nMarshall Rosenberg desarrolló la Comunicación No Violenta (CNV) en los años 60, inspirado por Gandhi y Carl Rogers. Su premisa: detrás de cada conflicto hay necesidades insatisfechas. Si identificas esas necesidades, puedes resolverlo.\n\n## Los 4 pasos de la CNV\n\n### 1. Observación (sin evaluación)\nDescribe lo que ocurrió como una cámara de vídeo, sin juicios ni interpretaciones.\n- Mal: «Siempre llegas tarde» (evaluación)\n- Bien: «En las últimas 3 reuniones, llegaste 15 minutos después de la hora acordada» (observación)\n\n### 2. Sentimiento (sin culpa)\nExpresa cómo te sientes usando «yo», no «tú me haces sentir».\n- Mal: «Me haces sentir ignorado» (culpa)\n- Bien: «Me siento frustrado cuando no se respetan los horarios» (sentimiento propio)\n\n### 3. Necesidad (universal)\nIdentifica la necesidad humana detrás del sentimiento. Las necesidades son universales: respeto, autonomía, seguridad, pertenencia, contribución.\n- «Necesito que los acuerdos del equipo se cumplan para poder organizar mi trabajo»\n\n### 4. Petición (concreta y negociable)\nHaz una petición específica, no una exigencia. La diferencia: la petición acepta un «no» y abre diálogo.\n- «¿Estarías dispuesto a avisarme con 10 minutos de antelación si vas a retrasarte?»\n\n## CNV en la práctica corporativa\n\nLa CNV no es ser blando. Empresas como Microsoft, SAP y el Banco Mundial la usan en mediación interna. Un estudio de la Universidad de San Francisco (2014) mostró que equipos entrenados en CNV redujeron los conflictos interpersonales un 43%.",
            PuntosClave = "[\"CNV tiene 4 pasos: Observación, Sentimiento, Necesidad y Petición\",\"Observar sin evaluar es la forma más elevada de inteligencia humana (Krishnamurti)\",\"Las necesidades son universales: respeto, autonomía, seguridad, pertenencia\",\"La petición es negociable; la exigencia no acepta un no\",\"Equipos entrenados en CNV reducen conflictos interpersonales un 43%\"]",
            GuionAudio = "Marshall Rosenberg cambió la forma en que miles de organizaciones gestionan sus conflictos. Hoy aprenderás su método de 4 pasos que transforma conversaciones destructivas en diálogos que construyen.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 12
        };

        Leccion n2l2 = new Leccion
        {
            Id = 122, NivelId = 20, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Negociación integrativa: el método Harvard",
            Descripcion = "Aprende a negociar soluciones donde ambas partes ganan usando el framework de Harvard.",
            Contenido = "## De posiciones a intereses\n\nRoger Fisher y William Ury publicaron «Getting to Yes» en 1981 desde el Harvard Negotiation Project. Su revolución: dejar de negociar posiciones y empezar a negociar intereses.\n\n## Los 4 principios de Harvard\n\n### 1. Separa las personas del problema\nAtaca el problema, nunca a la persona. Cuando dices «tu propuesta es ridícula», atacas a la persona. Cuando dices «esta propuesta no cubre el riesgo X», atacas el problema.\n\n### 2. Concéntrate en intereses, no en posiciones\nPosición: «Quiero un aumento del 20%».\nInterés real: «Necesito sentir que mi contribución es reconocida y tener seguridad financiera».\nEl interés se puede satisfacer de múltiples formas: aumento, bonus, formación, flexibilidad, título.\n\n### 3. Genera opciones de beneficio mutuo\nAntes de decidir, brainstorming de opciones sin juzgar. La creatividad resuelve más conflictos que la presión. Pregunta: «¿Qué opciones se nos ocurren que funcionen para ambos?»\n\n### 4. Usa criterios objetivos\nAncla la negociación en estándares externos: mercado, precedentes, normativa, datos. Así nadie siente que cede por debilidad sino por justicia.\n\n## BATNA: tu poder real\n\nBATNA (Best Alternative To a Negotiated Agreement) es tu mejor alternativa si no hay acuerdo. Quien tiene mejor BATNA tiene más poder. Siempre conoce tu BATNA antes de negociar.\n\n## El ZOPA\n\nZona de Posible Acuerdo: el rango donde ambas partes pueden aceptar. Si tu mínimo es 50K y su máximo es 60K, el ZOPA es 50K-60K.",
            PuntosClave = "[\"Fisher y Ury: negociar intereses, no posiciones\",\"Los 4 principios: personas vs problema, intereses, opciones mutuas, criterios objetivos\",\"BATNA es tu mejor alternativa si no hay acuerdo — define tu poder real\",\"ZOPA es el rango donde ambas partes pueden llegar a un acuerdo\",\"La creatividad resuelve más conflictos que la presión\"]",
            GuionAudio = "El método Harvard transformó la negociación mundial. Hoy aprenderás a separar personas del problema, descubrir intereses ocultos y generar soluciones donde todos ganan.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 12
        };

        Leccion n2l3 = new Leccion
        {
            Id = 123, NivelId = 20, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: CNV y Negociación Integrativa",
            Descripcion = "Evalúa tu dominio de la Comunicación No Violenta y el método Harvard de negociación.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion n2l4 = new Leccion
        {
            Id = 124, NivelId = 20, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Conflicto de recursos entre departamentos",
            Descripcion = "Marketing y Desarrollo se disputan el mismo presupuesto trimestral. Debes facilitar un acuerdo integrativo.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 12
        };

        Leccion n2l5 = new Leccion
        {
            Id = 125, NivelId = 20, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Mediación con CNV en un equipo dividido",
            Descripcion = "Tu equipo está dividido sobre la metodología de trabajo. Usa CNV para facilitar un acuerdo.",
            Contenido = "La mitad de tu equipo quiere mantener Scrum y la otra mitad quiere cambiar a Kanban. Las discusiones se han vuelto personales. Como facilitador, debes usar los 4 pasos de CNV y los principios de Harvard para encontrar una solución integrativa.",
            GuionAudio = "En este roleplay, facilitarás una mediación entre dos facciones de tu equipo. Recuerda: observa sin juzgar, identifica necesidades y busca intereses detrás de las posiciones.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n2l1, n2l2, n2l3, n2l4, n2l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección N2-1
        await SembrarEscenasTeoricasAsync(contexto, n2l1.Id, "comunicación no violenta",
            "El método de 4 pasos de Rosenberg",
            "Marshall Rosenberg nos enseñó que detrás de cada conflicto hay necesidades insatisfechas. Hoy aprenderás a identificarlas y expresarlas sin agresión.",
            "Observación sin juicio, sentimiento sin culpa, necesidad universal y petición negociable. Estos 4 pasos transforman conversaciones destructivas en diálogos constructivos.",
            "Practica esta semana: cuando sientas frustración con un colega, formula tu mensaje con los 4 pasos de CNV antes de hablar.");

        // Escenas para lección N2-2
        await SembrarEscenasTeoricasAsync(contexto, n2l2.Id, "negociación integrativa Harvard",
            "De posiciones a intereses",
            "El método Harvard revolucionó la negociación al demostrar que las mejores soluciones surgen cuando exploras intereses profundos en lugar de defender posiciones rígidas.",
            "Separa personas del problema, enfócate en intereses, genera opciones creativas y usa criterios objetivos. Conoce tu BATNA para negociar con poder real.",
            "Antes de tu próxima negociación, identifica tu BATNA y los posibles intereses de la otra parte. Eso te dará ventaja estratégica.");

        // ===== NIVEL 3 — Dominio (NivelId=21) =====
        Leccion n3l1 = new Leccion
        {
            Id = 151, NivelId = 21, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Conflictos culturales en equipos globales",
            Descripcion = "Navega las diferencias culturales que generan malentendidos y conflictos en equipos internacionales.",
            Contenido = "## La dimensión invisible del conflicto\n\nErin Meyer, profesora de INSEAD y autora de «The Culture Map» (2014), demostró que el 70% de los conflictos en equipos internacionales tienen raíz cultural, no personal.\n\n## Las 8 escalas de Meyer\n\n### 1. Comunicación: bajo contexto vs. alto contexto\n- **Bajo contexto** (EE.UU., Alemania, Países Bajos): se dice todo explícitamente. «No estoy de acuerdo».\n- **Alto contexto** (Japón, Corea, India): se comunica entre líneas. «Es una idea interesante...» puede significar rechazo.\n\n### 2. Confrontación: directa vs. indirecta\n- **Directa** (Francia, Israel, Rusia): el desacuerdo abierto es productivo y esperado.\n- **Indirecta** (Japón, Tailandia, Indonesia): el desacuerdo público causa pérdida de cara.\n\n### 3. Toma de decisiones: consensual vs. top-down\n- **Consensual** (Japón, Suecia, Alemania): decisión lenta, implementación rápida.\n- **Top-down** (India, China, Nigeria): decisión rápida, implementación con ajustes.\n\n### 4. Confianza: basada en tarea vs. basada en relación\n- **Tarea** (EE.UU., Alemania, Dinamarca): confío si entregas bien.\n- **Relación** (China, Brasil, Arabia Saudí): confío si te conozco personalmente.\n\n## Framework ADAPT para conflictos culturales\n\n- **A**sume diferencias (no universalidad)\n- **D**escubre las normas culturales del otro\n- **A**dapta tu estilo (no pidas que se adapten ellos)\n- **P**regunta antes de interpretar\n- **T**estea tus suposiciones con alguien de esa cultura\n\n## Caso real: la reunión silenciosa\nUn equipo alemán-japonés fracasó porque los alemanes interpretaban el silencio japonés como acuerdo, cuando era desacuerdo respetuoso. Solo cuando implementaron el framework ADAPT, los malentendidos se redujeron un 65%.",
            PuntosClave = "[\"El 70% de los conflictos en equipos globales tienen raíz cultural\",\"Erin Meyer identifica 8 escalas culturales que afectan la colaboración\",\"Comunicación bajo vs alto contexto es la fuente principal de malentendidos\",\"Framework ADAPT: Asume diferencias, Descubre normas, Adapta estilo, Pregunta, Testea\",\"El silencio tiene significados opuestos según la cultura\"]",
            GuionAudio = "En un mundo globalizado, los conflictos culturales son la fuente invisible de frustración en equipos internacionales. Hoy aprenderás a descifrar el mapa cultural con el framework de Erin Meyer.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 15
        };

        Leccion n3l2 = new Leccion
        {
            Id = 152, NivelId = 21, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Gestión sistémica de conflictos organizacionales",
            Descripcion = "Diseña sistemas y procesos para prevenir, detectar y resolver conflictos a escala organizacional.",
            Contenido = "## Del conflicto individual al conflicto sistémico\n\nWilliam Ury (co-autor de «Getting to Yes») propuso en «The Third Side» (2000) que las organizaciones necesitan un «tercer lado»: un sistema de personas, procesos y normas que prevenga y gestione conflictos antes de que escalen.\n\n## El Sistema Integrado de Gestión de Conflictos (SIGC)\n\n### Nivel 1: Prevención\n- **Claridad de roles y expectativas**: el 35% de los conflictos surge por ambigüedad de responsabilidades.\n- **Acuerdos de equipo**: normas explícitas sobre comunicación, decisiones y resolución de desacuerdos.\n- **Feedback continuo**: culturas con feedback regular tienen 40% menos conflictos escalados.\n\n### Nivel 2: Detección temprana\n- **Encuestas de clima**: pulso mensual con preguntas sobre colaboración y tensiones.\n- **One-on-ones**: reuniones 1:1 donde el manager pregunta «¿hay alguna fricción que debamos abordar?».\n- **Señales de alerta**: comunicación reducida, silos, reuniones tensas, rotación en equipos específicos.\n\n### Nivel 3: Resolución estructurada\n- **Conversación facilitada**: un tercero neutral guía el diálogo usando CNV y principios Harvard.\n- **Mediación formal**: mediador interno certificado cuando las partes no pueden resolver solas.\n- **Arbitraje**: un decisor con autoridad cuando la mediación falla. Última instancia.\n\n### Nivel 4: Aprendizaje organizacional\n- **Post-mortem de conflictos**: ¿qué lo causó? ¿cómo se detectó? ¿qué sistema falló?\n- **Patrones recurrentes**: si el mismo tipo de conflicto aparece en diferentes equipos, el problema es sistémico.\n- **Actualización de procesos**: cada conflicto resuelto debe mejorar el sistema para el próximo.\n\n## Métricas de salud conflictual\n- Tiempo medio de resolución (objetivo: <2 semanas)\n- % de conflictos resueltos sin escalación (objetivo: >80%)\n- Nivel de confianza inter-departamental (encuesta trimestral)\n- Rotación atribuible a conflictos no resueltos",
            PuntosClave = "[\"Ury propone el tercer lado: sistemas que prevengan y gestionen conflictos\",\"SIGC tiene 4 niveles: prevención, detección, resolución y aprendizaje\",\"El 35% de los conflictos surge por ambigüedad de roles\",\"Culturas con feedback regular tienen 40% menos conflictos escalados\",\"Cada conflicto resuelto debe mejorar el sistema organizacional\"]",
            GuionAudio = "Los conflictos individuales son síntomas. Las causas reales suelen ser sistémicas. Hoy aprenderás a diseñar un sistema organizacional que prevenga, detecte y resuelva conflictos a escala.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 15
        };

        Leccion n3l3 = new Leccion
        {
            Id = 153, NivelId = 21, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Conflictos Culturales y Sistémicos",
            Descripcion = "Demuestra tu dominio sobre conflictos interculturales y gestión sistémica de conflictos.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion n3l4 = new Leccion
        {
            Id = 154, NivelId = 21, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Crisis intercultural en equipo distribuido",
            Descripcion = "Tu equipo global tiene un conflicto entre las sedes de Tokio y Ámsterdam. La comunicación se ha roto. Debes intervenir.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 12
        };

        Leccion n3l5 = new Leccion
        {
            Id = 155, NivelId = 21, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Diseña el sistema de gestión de conflictos",
            Descripcion = "Presenta al comité directivo tu propuesta de SIGC para una empresa con 500 empleados en 4 países.",
            Contenido = "La empresa ha crecido de 50 a 500 empleados en 3 años. Los conflictos entre departamentos y sedes se multiplican. El CEO te ha pedido que diseñes y presentes un Sistema Integrado de Gestión de Conflictos. Debes convencer al comité directivo de invertir en prevención.",
            GuionAudio = "En este roleplay, yo seré el comité directivo. Queremos resultados medibles y un plan realista. Presenta tu SIGC con datos, casos y métricas claras. Recuerda: el conflicto cuesta dinero y prevenir es más barato que resolver.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n3l1, n3l2, n3l3, n3l4, n3l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección N3-1
        await SembrarEscenasTeoricasAsync(contexto, n3l1.Id, "conflictos culturales",
            "El mapa cultural de Erin Meyer",
            "El 70% de los conflictos en equipos internacionales tienen raíz cultural. Hoy aprenderás a descifrar las diferencias que generan malentendidos invisibles.",
            "Comunicación de alto vs bajo contexto, confrontación directa vs indirecta, y confianza basada en tarea vs relación. El framework ADAPT te ayuda a navegar cualquier diferencia cultural.",
            "Antes de tu próxima reunión con un colega de otra cultura, investiga dónde se ubica en las escalas de Meyer. Adapta tu estilo antes de interpretar su comportamiento.");

        // Escenas para lección N3-2
        await SembrarEscenasTeoricasAsync(contexto, n3l2.Id, "gestión sistémica de conflictos",
            "Del síntoma individual al sistema organizacional",
            "Los conflictos repetitivos no son casualidad. Son síntomas de un sistema que necesita rediseño. Hoy aprenderás a construir una organización que gestione conflictos de forma proactiva.",
            "El SIGC tiene 4 niveles: prevención con claridad de roles, detección temprana con pulsos y one-on-ones, resolución estructurada con mediación, y aprendizaje organizacional con post-mortems.",
            "Audita tu equipo: ¿hay roles ambiguos? ¿hay feedback regular? ¿los conflictos se detectan temprano o escalan hasta ser crisis? Identifica el nivel más débil de tu SIGC.");
    }

    private static async Task SembrarQuizResolucionConflictosAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Cuántas horas semanales dedican los empleados a gestionar conflictos según el informe CPP Global?",
                "El informe CPP Global de 2008 reveló que los empleados dedican una media de 2,8 horas semanales.",
                new[] { ("1 hora", false, "Es más del doble. Los empleados dedican 2,8 horas semanales."), ("2,8 horas", true, "¡Correcto! Son 2,8 horas semanales, lo que supone 359.000 millones de dólares anuales solo en EE.UU."), ("5 horas", false, "Es algo menos. Son 2,8 horas semanales."), ("30 minutos", false, "Es mucho más. Los empleados dedican 2,8 horas semanales a conflictos.") }),
            CrearPregunta(leccionId, 2, "Según el modelo Thomas-Kilmann, ¿qué estilo combina alta asertividad y alta cooperación?",
                "Colaborar busca satisfacer los intereses de ambas partes mediante soluciones creativas.",
                new[] { ("Competir", false, "Competir tiene alta asertividad pero baja cooperación."), ("Comprometer", false, "Comprometer tiene niveles medios de ambas dimensiones."), ("Colaborar", true, "¡Correcto! Colaborar maximiza tanto la asertividad como la cooperación para un resultado win-win."), ("Acomodar", false, "Acomodar tiene baja asertividad y alta cooperación.") }),
            CrearPregunta(leccionId, 3, "¿Qué tipo de conflicto laboral es el más destructivo según la investigación?",
                "El conflicto de relación activa el sistema de amenaza del cerebro y destruye la confianza.",
                new[] { ("Conflicto de tarea", false, "El conflicto de tarea moderado puede ser positivo para las decisiones."), ("Conflicto de proceso", false, "Es problemático pero no el más destructivo."), ("Conflicto de relación", true, "¡Correcto! Las fricciones personales activan el sistema de amenaza y son las más difíciles de resolver."), ("Conflicto de estatus", false, "Es disruptivo pero no tanto como las fricciones personales.") }),
            CrearPregunta(leccionId, 4, "En la escalada de Glasl, ¿en qué fase la resolución Win-Win aún es posible?",
                "La Fase 1 (niveles 1-3) permite que ambas partes ganen.",
                new[] { ("Fase 1 (niveles 1-3)", true, "¡Correcto! En la Fase 1, la tensión, el debate y las acciones unilaterales aún permiten una solución donde ambos ganan."), ("Fase 2 (niveles 4-6)", false, "En la Fase 2 ya estamos en Win-Lose: coaliciones y amenazas."), ("Fase 3 (niveles 7-9)", false, "En la Fase 3 ambas partes pierden: es Lose-Lose."), ("En cualquier fase", false, "Desafortunadamente, solo la Fase 1 permite Win-Win.") }),
            CrearPregunta(leccionId, 5, "¿Cuándo es estratégicamente adecuado usar el estilo 'Evitar' según Thomas-Kilmann?",
                "Evitar es válido cuando el tema es trivial o necesitas tiempo para pensar.",
                new[] { ("Siempre, los conflictos se resuelven solos", false, "Evitar como patrón habitual acumula tensión y empeora la situación."), ("Cuando el tema es trivial o necesitas una pausa estratégica", true, "¡Correcto! Evitar es válido para temas menores o cuando necesitas tiempo antes de abordar el conflicto."), ("Cuando la otra persona es tu jefe", false, "El rango jerárquico no determina el estilo; la situación sí."), ("Nunca, hay que enfrentar todo directamente", false, "A veces evitar es la opción más inteligente si el tema no lo merece.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenarioResolucionConflictosAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Dos colegas de tu equipo, Laura y Miguel, llevan tres semanas sin hablarse después de una discusión sobre quién lideró el éxito de un proyecto ante el director. Laura dice que fue su estrategia; Miguel asegura que fue su ejecución técnica. El ambiente del equipo se ha deteriorado: la gente toma bandos y la productividad ha caído un 20%.",
            Contexto = "Ambos tienen razón parcial: Laura diseñó la estrategia y Miguel ejecutó la parte técnica. El director felicitó al equipo sin mencionar nombres, lo que desencadenó la disputa. Eres compañero de ambos, no su manager.",
            GuionAudio = "Este es un conflicto de estatus clásico. Ambos necesitan reconocimiento y la ambigüedad del director lo detonó. ¿Cómo medias sin autoridad jerárquica?"
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Hablar con cada uno por separado para entender su perspectiva, validar que ambos contribuyeron de forma esencial, y luego facilitar una conversación conjunta donde cada uno reconozca la aportación del otro. Sugerir que presenten el caso de éxito juntos al equipo, dando crédito a ambos roles.",
                TipoResultado = TipoResultadoEscenario.Optimo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "Excelente mediación. Escuchaste a ambas partes (validación), identificaste que el conflicto es de estatus (reconocimiento), y propusiste una solución integrativa donde ambos ganan visibilidad. Además, restauras la cohesión del equipo."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Hablar con el manager del equipo para que él asigne el crédito formalmente y resuelva la disputa con su autoridad.",
                TipoResultado = TipoResultadoEscenario.Aceptable, PuntosOtorgados = 8,
                TextoRetroalimentacion = "Involucrar al manager puede resolver el síntoma, pero no aborda la tensión interpersonal. Laura y Miguel necesitan reconocimiento mutuo, no una decisión jerárquica sobre quién hizo más. Además, escalar sin intentar mediar primero puede dañar tu relación con ambos."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "No involucrarte porque no es tu problema. Ya se les pasará con el tiempo.",
                TipoResultado = TipoResultadoEscenario.Inadecuado, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Evitar el conflicto cuando ya afecta a todo el equipo no es neutralidad, es negligencia. La productividad ha caído un 20% y la gente toma bandos. Sin intervención, este conflicto de estatus escalará a un conflicto de relación mucho más difícil de resolver."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }
}
