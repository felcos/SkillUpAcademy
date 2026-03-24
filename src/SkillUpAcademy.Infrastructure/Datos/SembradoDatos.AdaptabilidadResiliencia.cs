using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de lecciones para el área Adaptabilidad y Resiliencia (3 niveles).
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesAdaptabilidadResilienciaAsync(AppDbContext contexto)
    {
        // ===== NIVEL 1 — Fundamentos (NivelId=34) =====
        Leccion l1 = new Leccion
        {
            Id = 116, NivelId = 34, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Mentalidad de crecimiento: el poder de creer que puedes mejorar",
            Descripcion = "Descubre la investigación de Carol Dweck sobre mentalidad fija vs. de crecimiento y cómo transforma tu carrera profesional.",
            Contenido = "## Mentalidad fija vs. mentalidad de crecimiento\n\nCarol Dweck, profesora de Stanford, investigó durante 30 años cómo nuestras creencias sobre la inteligencia determinan nuestro éxito.\n\n### Mentalidad fija\nCree que el talento es innato e inmutable. «Soy malo para los números» o «No sirvo para hablar en público». Ante el fracaso, se rinde.\n\n### Mentalidad de crecimiento\nCree que las habilidades se desarrollan con esfuerzo y práctica. «Aún no domino los números, pero puedo aprender». Ante el fracaso, busca la lección.\n\n## Evidencia neurocientífica\n\nLa neuroplasticidad demuestra que el cerebro crea nuevas conexiones neuronales con la práctica deliberada. A los 50 años puedes aprender un idioma nuevo: tu cerebro es plástico, no estático.\n\n## El poder del «todavía»\n\nDweck descubrió que añadir «todavía» cambia la perspectiva: «No sé programar... todavía». Este simple cambio activa circuitos de motivación y persistencia.\n\n## Impacto en el trabajo\n\nEmpresas como Microsoft adoptaron la mentalidad de crecimiento como valor cultural. Satya Nadella transformó la cultura de «sabelotodos» a «aprendetodos» (know-it-alls a learn-it-alls).\n\n## Las 4 señales de mentalidad fija en el trabajo\n\n1. Evitas desafíos nuevos por miedo a fallar\n2. Te sientes amenazado por el éxito de otros\n3. Ignoras el feedback negativo\n4. Abandonas rápido cuando algo se pone difícil",
            PuntosClave = "[\"Carol Dweck identificó dos mentalidades: fija (el talento es innato) y de crecimiento (las habilidades se desarrollan)\",\"La neuroplasticidad demuestra que el cerebro crea conexiones nuevas a cualquier edad\",\"Añadir 'todavía' a las limitaciones activa circuitos de motivación\",\"Microsoft transformó su cultura empresarial adoptando la mentalidad de crecimiento\",\"Las 4 señales de mentalidad fija: evitar desafíos, sentirse amenazado, ignorar feedback y abandonar pronto\"]",
            GuionAudio = "Hoy vamos a hablar sobre una de las investigaciones más transformadoras de la psicología moderna. Carol Dweck descubrió que lo que crees sobre tu propio talento determina hasta dónde llegas. No es tu inteligencia, es tu mentalidad.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = 117, NivelId = 34, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "El ciclo del cambio: de la resistencia a la adaptación",
            Descripcion = "Aprende el modelo de Kübler-Ross aplicado al entorno laboral y cómo navegar cada fase del cambio.",
            Contenido = "## El cambio es la única constante\n\nSegún McKinsey, el 70% de las transformaciones organizacionales fracasan. No por la estrategia, sino porque las personas no logran adaptarse emocionalmente al cambio.\n\n## El modelo de Kübler-Ross aplicado al trabajo\n\nElisabeth Kübler-Ross describió 5 fases del duelo que se aplican a cualquier cambio significativo:\n\n### 1. Negación\n«Esto no nos va a afectar» o «Seguro que es temporal». El cerebro necesita tiempo para procesar la nueva realidad.\n\n### 2. Ira\n«¿Por qué hacen esto?» o «Es injusto». Aparece la frustración cuando la realidad se impone.\n\n### 3. Negociación\n«¿Y si hacemos solo una parte del cambio?» o «Quizás si propongo otra alternativa...». Se buscan atajos.\n\n### 4. Tristeza\nLa fase más difícil. Se extraña cómo eran las cosas. Baja la productividad y la motivación.\n\n### 5. Aceptación\nNo significa estar de acuerdo, sino integrar la nueva realidad y empezar a construir desde ella.\n\n## La curva del cambio de Bridges\n\nWilliam Bridges añadió un concepto clave: toda transición tiene un final (soltar lo viejo), una zona neutral (incertidumbre) y un nuevo comienzo.\n\n## Técnica SARA para acelerar la adaptación\n\n- **S**orpresa → Acepta que es normal sentirte desorientado\n- **A**nálisis → Evalúa objetivamente qué cambia y qué no\n- **R**eacción → Permite las emociones pero no te quedes en ellas\n- **A**ceptación → Busca las oportunidades dentro del cambio",
            PuntosClave = "[\"El 70% de las transformaciones organizacionales fracasan por resistencia al cambio\",\"Las 5 fases de Kübler-Ross: negación, ira, negociación, tristeza y aceptación\",\"La zona neutral de Bridges es donde más apoyo se necesita\",\"La técnica SARA ayuda a acelerar la adaptación: Sorpresa, Análisis, Reacción, Aceptación\",\"Aceptar el cambio no significa estar de acuerdo, sino integrar la nueva realidad\"]",
            GuionAudio = "¿Sabías que el 70% de las transformaciones en las empresas fracasan? No por mala estrategia, sino porque las personas se resisten al cambio. Hoy vamos a entender por qué nos resistimos y cómo superar cada fase.",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 10
        };

        Leccion l3 = new Leccion
        {
            Id = 118, NivelId = 34, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Adaptabilidad y Resiliencia",
            Descripcion = "Pon a prueba tus conocimientos sobre mentalidad de crecimiento y gestión del cambio.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = 119, NivelId = 34, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Reestructuración en tu departamento",
            Descripcion = "Tu empresa anuncia una reestructuración que elimina tu puesto actual. Decide cómo reaccionar.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = 120, NivelId = 34, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Convence a tu equipo de adoptar una nueva herramienta",
            Descripcion = "Tu equipo se resiste a cambiar la herramienta de gestión de proyectos. Practica cómo facilitar la transición.",
            Contenido = "La dirección ha decidido migrar de la herramienta actual a una nueva plataforma. Tu equipo lleva 3 años usando la anterior y varios miembros expresan abiertamente su rechazo. Debes facilitar una reunión para abordar la resistencia y motivar la adopción.",
            GuionAudio = "En este roleplay, yo representaré a los miembros de tu equipo que se resisten al cambio. Tu objetivo es escuchar sus preocupaciones, validar sus emociones y ayudarles a ver las oportunidades. Recuerda: la resistencia al cambio es natural, no personal.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1
        await SembrarEscenasTeoricasAsync(contexto, l1.Id, "mentalidad de crecimiento",
            "El poder de creer que puedes mejorar",
            "Carol Dweck descubrió que lo que crees sobre tu talento determina hasta dónde llegas. Vamos a entender la diferencia entre mentalidad fija y de crecimiento.",
            "Mentalidad fija cree que el talento es innato. Mentalidad de crecimiento cree que se desarrolla con esfuerzo. La neuroplasticidad demuestra que tu cerebro es plástico, no estático.",
            "Esta semana, cuando te encuentres con un desafío, añade la palabra 'todavía': 'No sé hacerlo... todavía'. Ese simple cambio activa tu motivación.");

        // Escenas para lección 2
        await SembrarEscenasTeoricasAsync(contexto, l2.Id, "el ciclo del cambio",
            "De la resistencia a la adaptación",
            "El 70% de las transformaciones organizacionales fracasan porque las personas no logran adaptarse emocionalmente. Hoy aprenderás a navegar cada fase.",
            "Negación, ira, negociación, tristeza y aceptación. Estas son las fases naturales del cambio. La técnica SARA te ayuda a acelerar el proceso.",
            "La próxima vez que enfrentes un cambio en el trabajo, identifica en qué fase estás. Solo reconocerlo ya te ayuda a avanzar más rápido.");

        // Quiz
        await SembrarQuizAdaptabilidadResilienciaAsync(contexto, l3.Id);

        // Escenario
        await SembrarEscenarioAdaptabilidadResilienciaAsync(contexto, l4.Id);

        // ===== NIVEL 2 — Práctica (NivelId=35) =====
        Leccion n2l1 = new Leccion
        {
            Id = 146, NivelId = 35, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Resiliencia según el modelo PERMA de Martin Seligman",
            Descripcion = "Domina los 5 pilares del bienestar psicológico que construyen resiliencia profesional duradera.",
            Contenido = "## Martin Seligman y la psicología positiva\n\nSeligman, padre de la psicología positiva, investigó qué hace que las personas prosperen en lugar de solo sobrevivir. Su modelo PERMA identifica 5 pilares del bienestar:\n\n### P — Emociones Positivas (Positive Emotions)\nNo se trata de ser optimista ingenuo, sino de cultivar deliberadamente emociones como gratitud, curiosidad y orgullo. Barbara Fredrickson demostró que las emociones positivas amplían el repertorio de pensamiento y acción (teoría broaden-and-build).\n\n### E — Compromiso (Engagement)\nEl estado de «flow» descrito por Csikszentmihalyi: estar completamente absorto en una tarea desafiante pero alcanzable. En el trabajo, ocurre cuando tus habilidades coinciden con el nivel de desafío.\n\n### R — Relaciones (Relationships)\nLas conexiones sociales son el predictor #1 de resiliencia. George Vaillant, en el estudio longitudinal de Harvard de 75 años, concluyó: «La felicidad es amor. Punto».\n\n### M — Significado (Meaning)\nConectar tu trabajo con un propósito mayor. Viktor Frankl sobrevivió a Auschwitz y descubrió que quienes encontraban significado en el sufrimiento sobrevivían más.\n\n### A — Logro (Accomplishment)\nLa satisfacción de completar metas. Angela Duckworth demostró que el «grit» (determinación + pasión a largo plazo) predice el éxito mejor que el CI.\n\n## Aplicación práctica: el ejercicio de las 3 cosas buenas\n\nCada noche, escribe 3 cosas buenas que pasaron y por qué. Seligman demostró que este ejercicio, mantenido 1 semana, reduce síntomas depresivos durante 6 meses.",
            PuntosClave = "[\"PERMA: Positive Emotions, Engagement, Relationships, Meaning, Accomplishment\",\"Barbara Fredrickson: las emociones positivas amplían el pensamiento (broaden-and-build)\",\"El estado de flow ocurre cuando habilidad y desafío coinciden\",\"El estudio de Harvard de 75 años: las relaciones son el predictor #1 de bienestar\",\"El ejercicio de 3 cosas buenas reduce síntomas depresivos durante 6 meses\"]",
            GuionAudio = "Martin Seligman revolucionó la psicología al dejar de estudiar lo que va mal y empezar a estudiar lo que hace que las personas prosperen. Su modelo PERMA es tu guía para construir resiliencia real.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 12
        };

        Leccion n2l2 = new Leccion
        {
            Id = 147, NivelId = 35, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Agilidad de aprendizaje: aprende a aprender más rápido",
            Descripcion = "Desarrolla learning agility, la competencia más buscada por las empresas del Fortune 500.",
            Contenido = "## ¿Qué es la agilidad de aprendizaje?\n\nKorn Ferry identificó la learning agility como el predictor #1 de potencial de liderazgo. Es la capacidad de aprender de la experiencia y aplicar ese aprendizaje en situaciones nuevas.\n\n## Las 5 dimensiones de la learning agility\n\n### 1. Agilidad mental\nPensar en problemas desde múltiples ángulos. Cuestionar supuestos. Sentirte cómodo con la ambigüedad y la complejidad.\n\n### 2. Agilidad con personas\nAprender de y a través de otros. Buscar feedback activamente. Adaptar tu comunicación a diferentes audiencias.\n\n### 3. Agilidad con el cambio\nExperimentar constantemente. Tratar cada proyecto como un laboratorio. No temer al fracaso sino al estancamiento.\n\n### 4. Agilidad con resultados\nEntregar resultados en condiciones nuevas o difíciles. Mantener la calma bajo presión. Priorizar con criterio cuando todo parece urgente.\n\n### 5. Autoconocimiento\nConocer tus fortalezas y puntos ciegos. Pedir feedback y actuar en consecuencia. Reflexionar después de cada experiencia significativa.\n\n## El ciclo de aprendizaje de Kolb aplicado al trabajo\n\n1. **Experiencia concreta**: Vives una situación\n2. **Observación reflexiva**: Analizas qué pasó y por qué\n3. **Conceptualización abstracta**: Extraes principios generales\n4. **Experimentación activa**: Aplicas lo aprendido en la siguiente situación\n\n## Técnica del after-action review (AAR)\n\nUsada por el ejército de EE.UU. y adoptada por empresas como Toyota:\n- ¿Qué esperábamos que pasara?\n- ¿Qué pasó realmente?\n- ¿Por qué hubo diferencia?\n- ¿Qué haremos diferente la próxima vez?",
            PuntosClave = "[\"La learning agility es el predictor #1 de potencial de liderazgo según Korn Ferry\",\"5 dimensiones: mental, personas, cambio, resultados y autoconocimiento\",\"El ciclo de Kolb: experiencia, reflexión, conceptualización y experimentación\",\"El after-action review (AAR) sistematiza el aprendizaje de cada experiencia\",\"La agilidad de aprendizaje se entrena, no es un rasgo innato\"]",
            GuionAudio = "Las empresas del Fortune 500 buscan una competencia por encima de todas: la agilidad de aprendizaje. No es cuánto sabes, sino cuán rápido aprendes de cada experiencia y lo aplicas en situaciones nuevas.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 12
        };

        Leccion n2l3 = new Leccion
        {
            Id = 148, NivelId = 35, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Resiliencia y Agilidad de Aprendizaje",
            Descripcion = "Evalúa tu dominio del modelo PERMA y las técnicas de learning agility.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion n2l4 = new Leccion
        {
            Id = 149, NivelId = 35, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Tu proyecto estrella se cancela",
            Descripcion = "Llevas 6 meses liderando un proyecto innovador y la dirección decide cancelarlo por recortes presupuestarios.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 12
        };

        Leccion n2l5 = new Leccion
        {
            Id = 150, NivelId = 35, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Mentoría a un compañero que quiere rendirse",
            Descripcion = "Un colega junior está a punto de abandonar un proyecto difícil. Practica cómo mentorizarle usando técnicas de resiliencia.",
            Contenido = "Tu compañero David lleva 3 semanas atascado en un proyecto técnico complejo. Ha intentado varios enfoques sin éxito y te dice que quiere pedir que le cambien de proyecto. Tú sabes que tiene las capacidades pero le falta resiliencia y estrategia de aprendizaje.",
            GuionAudio = "En este roleplay, yo seré David, el compañero frustrado. Tu objetivo es escuchar su frustración, validarla, y guiarle con técnicas de resiliencia y agilidad de aprendizaje. No se trata de darle la solución técnica, sino de ayudarle a encontrar su camino.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n2l1, n2l2, n2l3, n2l4, n2l5);
        await contexto.SaveChangesAsync();

        // Escenas Nivel 2
        await SembrarEscenasTeoricasAsync(contexto, n2l1.Id, "el modelo PERMA de Seligman",
            "Los 5 pilares de la resiliencia",
            "Martin Seligman dejó de estudiar lo que va mal y empezó a investigar lo que hace que las personas prosperen. Su modelo PERMA es tu mapa de ruta.",
            "Emociones positivas, compromiso, relaciones, significado y logro. El ejercicio de las 3 cosas buenas, mantenido una semana, reduce síntomas depresivos durante 6 meses.",
            "Esta noche, empieza el ejercicio de las 3 cosas buenas: escribe tres cosas positivas del día y por qué ocurrieron. Hazlo durante una semana y nota la diferencia.");

        await SembrarEscenasTeoricasAsync(contexto, n2l2.Id, "agilidad de aprendizaje",
            "Aprende a aprender más rápido",
            "Korn Ferry identificó la learning agility como el predictor número uno de potencial de liderazgo. No es cuánto sabes, sino cuán rápido aprendes.",
            "Cinco dimensiones: mental, personas, cambio, resultados y autoconocimiento. El after-action review del ejército estadounidense sistematiza el aprendizaje de cada experiencia.",
            "Después de tu próximo proyecto o reunión importante, aplica el after-action review: qué esperabas, qué pasó, por qué la diferencia y qué harás diferente.");

        // ===== NIVEL 3 — Dominio (NivelId=36) =====
        Leccion n3l1 = new Leccion
        {
            Id = 176, NivelId = 36, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Liderazgo en entornos VUCA y BANI",
            Descripcion = "Domina los frameworks para liderar en entornos de volatilidad extrema, incertidumbre y complejidad.",
            Contenido = "## De VUCA a BANI: la evolución del caos\n\n### El mundo VUCA (US Army War College, 1987)\n- **V**olatilidad: Los cambios son rápidos e impredecibles\n- **U**ncertainty (Incertidumbre): El pasado no predice el futuro\n- **C**omplejidad: Múltiples variables interconectadas\n- **A**mbigüedad: Las situaciones tienen múltiples interpretaciones\n\n### El mundo BANI (Jamais Cascio, 2020)\nVUCA se quedó corto tras la pandemia. BANI describe mejor la realidad actual:\n- **B**rittle (Frágil): Sistemas aparentemente fuertes que colapsan de repente\n- **A**nxious (Ansioso): La sobreinformación genera parálisis y ansiedad\n- **N**on-linear (No lineal): Pequeñas causas, efectos desproporcionados\n- **I**ncomprehensible: Demasiado complejo para entender completamente\n\n## Respuestas estratégicas a BANI\n\n| BANI | Respuesta |\n|------|----------|\n| Frágil | Resiliencia y redundancia |\n| Ansioso | Empatía y mindfulness |\n| No lineal | Adaptabilidad y experimentación |\n| Incomprensible | Transparencia e intuición |\n\n## El modelo Cynefin de Dave Snowden\n\nNo todos los problemas se resuelven igual:\n- **Simple**: Buenas prácticas → Percibir, categorizar, responder\n- **Complicado**: Análisis experto → Percibir, analizar, responder\n- **Complejo**: Experimentación → Sondear, percibir, responder\n- **Caótico**: Acción inmediata → Actuar, percibir, responder\n\n## Habilidades del líder BANI\n\n1. **Pensamiento sistémico**: Ver conexiones, no solo partes\n2. **Toma de decisiones con información incompleta**: El 70% de certeza es suficiente (regla de Jeff Bezos)\n3. **Comunicación en la incertidumbre**: Ser transparente sobre lo que no sabes\n4. **Creación de equipos adaptativos**: Equipos pequeños, autónomos y con propósito claro",
            PuntosClave = "[\"VUCA: Volatilidad, Incertidumbre, Complejidad, Ambigüedad — el marco original del caos\",\"BANI: Frágil, Ansioso, No lineal, Incomprensible — la evolución post-pandemia\",\"Cynefin diferencia 4 dominios: simple, complicado, complejo y caótico, cada uno con su estrategia\",\"La regla de Bezos: decide con el 70% de la información, no esperes al 100%\",\"El líder BANI necesita pensamiento sistémico, decisión con incertidumbre y comunicación transparente\"]",
            GuionAudio = "El mundo cambió. VUCA ya no alcanza para describir la realidad. Jamais Cascio propuso BANI para explicar por qué los sistemas colapsan de repente y por qué la ansiedad se ha convertido en la emoción dominante. Hoy aprenderás a liderar en este nuevo contexto.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 15
        };

        Leccion n3l2 = new Leccion
        {
            Id = 177, NivelId = 36, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Antifragilidad: más allá de la resiliencia",
            Descripcion = "Aplica el concepto revolucionario de Nassim Taleb para convertir la adversidad en ventaja competitiva.",
            Contenido = "## De frágil a antifrágil\n\nNassim Nicholas Taleb, autor de «Antifrágil», propone una categoría que va más allá de la resiliencia:\n\n- **Frágil**: Se rompe con el estrés (una copa de cristal)\n- **Robusto/Resiliente**: Resiste el estrés sin cambiar (una roca)\n- **Antifrágil**: Se fortalece con el estrés (el sistema inmunológico, los músculos)\n\n## La tríada de Taleb aplicada a la carrera\n\n### Carrera frágil\nDepende de un solo empleador, una sola habilidad o un solo sector. Cualquier cambio la destruye.\n\n### Carrera resiliente\nTiene ahorros, habilidades transferibles y red de contactos. Resiste los golpes.\n\n### Carrera antifrágil\nCada crisis la hace más fuerte. Tiene múltiples fuentes de ingreso, aprende de cada fracaso y busca activamente la incomodidad.\n\n## Estrategia barbell (barra de pesas)\n\nTaleb propone la estrategia barbell: el 90% de tu esfuerzo en lo seguro y estable, el 10% en apuestas de alto riesgo/alta recompensa. En tu carrera:\n- 90%: Tu trabajo estable, habilidades core, red consolidada\n- 10%: Proyectos experimentales, habilidades radicalmente nuevas, sectores emergentes\n\n## Via negativa: crecer restando\n\nEn lugar de añadir más (más cursos, más certificaciones, más proyectos), identifica qué eliminar:\n- Elimina reuniones que no aportan valor\n- Elimina relaciones tóxicas profesionales\n- Elimina tareas que puedes delegar\n- Elimina información innecesaria (infoxicación)\n\n## Los 5 principios de la antifragilidad profesional\n\n1. **Opcionalidad**: Ten siempre más de una opción\n2. **Redundancia**: No dependas de un solo punto de fallo\n3. **Experimentación**: Haz pequeñas apuestas constantemente\n4. **Hormesis**: Exponerte a dosis controladas de estrés te fortalece\n5. **Piel en el juego**: Asume riesgos reales, no teóricos",
            PuntosClave = "[\"Taleb define 3 categorías: frágil (se rompe), resiliente (resiste) y antifrágil (se fortalece con el estrés)\",\"La estrategia barbell: 90% seguro + 10% apuestas de alto riesgo\",\"Via negativa: a veces crecer significa eliminar, no añadir\",\"Los 5 principios: opcionalidad, redundancia, experimentación, hormesis y piel en el juego\",\"Una carrera antifrágil tiene múltiples fuentes de ingreso y busca activamente la incomodidad\"]",
            GuionAudio = "¿Y si la resiliencia no fuera suficiente? Nassim Taleb propone algo revolucionario: no solo resistas los golpes, hazte más fuerte con ellos. Hoy aprenderás a construir una carrera antifrágil.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 15
        };

        Leccion n3l3 = new Leccion
        {
            Id = 178, NivelId = 36, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: VUCA/BANI y Antifragilidad",
            Descripcion = "Evalúa tu dominio de los frameworks avanzados de liderazgo en entornos de incertidumbre.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion n3l4 = new Leccion
        {
            Id = 179, NivelId = 36, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Tu industria se disrumpe por IA",
            Descripcion = "La inteligencia artificial automatiza el 60% de las tareas de tu sector. Decide cómo reposicionar tu carrera.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 15
        };

        Leccion n3l5 = new Leccion
        {
            Id = 180, NivelId = 36, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Lidera a tu equipo en una crisis inesperada",
            Descripcion = "Un cliente estratégico cancela un contrato millonario sin previo aviso. Practica cómo liderar a tu equipo en un entorno caótico.",
            Contenido = "Acabas de recibir la noticia de que vuestro cliente más importante, que representa el 35% de la facturación del departamento, ha cancelado el contrato. Tu equipo de 8 personas depende directamente de ese proyecto. Tienes que comunicar la noticia, gestionar las emociones del equipo y crear un plan de acción en un entorno BANI.",
            GuionAudio = "En este roleplay, yo seré los miembros de tu equipo con diferentes reacciones: uno entrará en pánico, otro se enfadará, otra pedirá soluciones inmediatas. Tu trabajo es liderar en el caos: comunicar con transparencia, gestionar emociones y crear un plan adaptativo. Recuerda Cynefin: en el caos, primero actúa, luego percibe, luego responde.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n3l1, n3l2, n3l3, n3l4, n3l5);
        await contexto.SaveChangesAsync();

        // Escenas Nivel 3
        await SembrarEscenasTeoricasAsync(contexto, n3l1.Id, "liderazgo en entornos VUCA y BANI",
            "Liderar cuando el mundo es caótico",
            "VUCA ya no alcanza. Jamais Cascio propuso BANI: frágil, ansioso, no lineal e incomprensible. Este es el marco para entender la realidad actual.",
            "Cynefin diferencia cuatro dominios: simple, complicado, complejo y caótico. La regla de Bezos dice que el 70% de certeza es suficiente para decidir. No esperes al 100%.",
            "Esta semana, clasifica tus decisiones pendientes con Cynefin. Las simples, resuélvelas ya. Las complejas, experimenta. Las caóticas, actúa primero y ajusta después.");

        await SembrarEscenasTeoricasAsync(contexto, n3l2.Id, "antifragilidad de Nassim Taleb",
            "Más allá de la resiliencia",
            "Nassim Taleb propone una categoría revolucionaria: no solo resistas los golpes, hazte más fuerte con ellos. Frágil, resiliente o antifrágil: tú eliges.",
            "La estrategia barbell: 90% seguro, 10% apuestas arriesgadas. Via negativa: a veces crecer es eliminar. Los 5 principios: opcionalidad, redundancia, experimentación, hormesis y piel en el juego.",
            "Revisa tu carrera con la tríada de Taleb. ¿Eres frágil, resiliente o antifrágil? Aplica la estrategia barbell: dedica un 10% de tu tiempo a algo radicalmente nuevo.");
    }

    /// <summary>
    /// Siembra las preguntas del quiz de Adaptabilidad y Resiliencia (Nivel 1).
    /// </summary>
    private static async Task SembrarQuizAdaptabilidadResilienciaAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "Según Carol Dweck, ¿qué caracteriza a la mentalidad de crecimiento?",
                "La mentalidad de crecimiento cree que las habilidades se desarrollan con esfuerzo y práctica.",
                new[] {
                    ("El talento es innato y no se puede cambiar", false, "Esa es la mentalidad fija, no la de crecimiento."),
                    ("Las habilidades se desarrollan con esfuerzo y práctica", true, "¡Correcto! La mentalidad de crecimiento cree que puedes mejorar con dedicación."),
                    ("Solo las personas inteligentes pueden aprender cosas nuevas", false, "Eso es mentalidad fija. La de crecimiento aplica a todos."),
                    ("El éxito depende exclusivamente de la genética", false, "La mentalidad de crecimiento rechaza el determinismo genético.")
                }),
            CrearPregunta(leccionId, 2, "¿Cuál es la primera fase del modelo de Kübler-Ross aplicado al cambio laboral?",
                "Las 5 fases son: negación, ira, negociación, tristeza y aceptación.",
                new[] {
                    ("Ira", false, "La ira es la segunda fase, cuando la realidad se impone."),
                    ("Negación", true, "¡Correcto! La negación es la primera reacción natural ante el cambio."),
                    ("Tristeza", false, "La tristeza viene después, cuando se extraña cómo eran las cosas."),
                    ("Aceptación", false, "La aceptación es la fase final del proceso.")
                }),
            CrearPregunta(leccionId, 3, "¿Qué porcentaje de las transformaciones organizacionales fracasan según McKinsey?",
                "El 70% fracasan, principalmente por resistencia al cambio de las personas.",
                new[] {
                    ("30%", false, "El porcentaje de fracaso es mucho mayor."),
                    ("50%", false, "Es aún más alto que la mitad."),
                    ("70%", true, "¡Correcto! 7 de cada 10 transformaciones fracasan por la resistencia humana al cambio."),
                    ("90%", false, "Es alto, pero no tanto. El 70% es la cifra de McKinsey.")
                }),
            CrearPregunta(leccionId, 4, "¿Qué técnica propone Dweck para cambiar la perspectiva ante una limitación?",
                "Añadir la palabra 'todavía' activa circuitos de motivación y persistencia.",
                new[] {
                    ("Repetir afirmaciones positivas frente al espejo", false, "Dweck no propone afirmaciones genéricas sino un cambio lingüístico específico."),
                    ("Añadir la palabra 'todavía' a la limitación", true, "¡Correcto! 'No sé hacerlo... todavía' cambia la mentalidad de fija a crecimiento."),
                    ("Ignorar la limitación y seguir adelante", false, "Ignorar no es lo mismo que adoptar mentalidad de crecimiento."),
                    ("Compararte con personas que sí pueden hacerlo", false, "La comparación activa amenaza, no crecimiento.")
                }),
            CrearPregunta(leccionId, 5, "¿Qué significa la 'A' en la técnica SARA para gestionar el cambio?",
                "SARA: Sorpresa, Análisis, Reacción, Aceptación.",
                new[] {
                    ("Acción inmediata", false, "La A final es Aceptación, que implica buscar oportunidades."),
                    ("Análisis y Aceptación", true, "¡Correcto! La primera A es Análisis (evaluar qué cambia) y la segunda es Aceptación (buscar oportunidades)."),
                    ("Adaptación y Ajuste", false, "Las siglas SARA son Sorpresa, Análisis, Reacción, Aceptación."),
                    ("Autorregulación", false, "No es parte del acrónimo SARA.")
                })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    /// <summary>
    /// Siembra el escenario de Adaptabilidad y Resiliencia (Nivel 1).
    /// </summary>
    private static async Task SembrarEscenarioAdaptabilidadResilienciaAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Tu empresa acaba de anunciar una reestructuración. Tu departamento se fusiona con otro y tu puesto actual desaparece. Te ofrecen reubicarte en un área completamente diferente (análisis de datos) que no dominas, o aceptar una indemnización y buscar fuera. Tienes 48 horas para decidir.",
            Contexto = "Llevas 4 años en la empresa con buen desempeño. El área de análisis de datos está creciendo y tiene futuro, pero tú no tienes experiencia técnica en ese campo. Varios compañeros ya están buscando fuera.",
            GuionAudio = "Este escenario pondrá a prueba tu capacidad de adaptación ante un cambio inesperado. Recuerda las fases de Kübler-Ross y la mentalidad de crecimiento de Dweck. ¿Cómo reaccionarás?"
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Aceptar la reubicación con mentalidad de crecimiento: «No sé análisis de datos... todavía. Pediré un plan de formación, buscaré un mentor interno y aplicaré lo que sé de mi área actual. Es una oportunidad para hacerme más versátil.»",
                TipoResultado = TipoResultadoEscenario.Optimo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Excelente! Aplicaste la mentalidad de crecimiento de Dweck con el 'todavía', pediste recursos de aprendizaje y reencuadraste el cambio como oportunidad. Esta es la respuesta de alguien adaptable y resiliente."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Pedir una semana más para evaluar ambas opciones, hablar con el responsable del nuevo área y con un coach de carrera antes de decidir.",
                TipoResultado = TipoResultadoEscenario.Aceptable, PuntosOtorgados = 10,
                TextoRetroalimentacion = "Buen enfoque analítico. Pedir más tiempo y recopilar información es inteligente. Sin embargo, podrías haber mostrado más apertura al cambio y mentalidad de crecimiento desde el primer momento."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Tomar la indemnización inmediatamente. «No voy a empezar de cero en algo que no domino. Prefiero buscar algo similar a lo que ya sé hacer.»",
                TipoResultado = TipoResultadoEscenario.Inadecuado, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Esta reacción muestra mentalidad fija: creer que no puedes aprender algo nuevo. Estás en la fase de negación del cambio. Huir de la incomodidad limita tu crecimiento profesional a largo plazo."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }
}
