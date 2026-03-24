using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de lecciones para el área Presentaciones en Público (3 niveles).
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesPresentacionesPublicoAsync(AppDbContext contexto)
    {
        // ===== NIVEL 1 — Fundamentos (NivelId=31) =====
        Leccion l1 = new Leccion
        {
            Id = 111, NivelId = 31, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Estructura de una presentación: apertura, cuerpo y cierre",
            Descripcion = "Domina la estructura clásica de tres actos que garantiza presentaciones claras, memorables y persuasivas.",
            Contenido = "## La regla de oro: apertura-cuerpo-cierre\n\nToda presentación efectiva sigue una estructura de tres actos. Aristóteles la describió hace 2.300 años y sigue funcionando porque se alinea con la forma en que el cerebro procesa información.\n\n## Apertura (10-15% del tiempo)\n\n### El gancho\nTienes 30 segundos para captar la atención. Opciones probadas:\n- **Dato impactante**: «El 75% de las personas tiene más miedo a hablar en público que a morir.»\n- **Pregunta retórica**: «¿Cuántas horas de reuniones has sufrido donde el presentador leía las diapositivas?»\n- **Historia breve**: Una anécdota personal relevante de máximo 60 segundos.\n- **Afirmación provocadora**: «Las diapositivas son el enemigo de las buenas presentaciones.»\n\n### La promesa\nDespués del gancho, dile a la audiencia qué van a ganar: «Al terminar esta sesión, tendrás 3 herramientas concretas para estructurar cualquier presentación en 15 minutos.»\n\n## Cuerpo (75-80% del tiempo)\n\n### La regla de 3\nEl cerebro humano retiene mejor la información en grupos de tres. No presentes 7 puntos — elige 3 y desarróllalos bien.\n\n### Estructura interna de cada punto\n1. **Afirmación**: declara tu idea claramente.\n2. **Evidencia**: dato, ejemplo, caso real o estudio.\n3. **Transición**: conecta con el siguiente punto.\n\n### Señalización\nUsa frases puente: «Mi primer punto es...», «Esto nos lleva a...», «Lo más importante de todo...». La audiencia necesita saber dónde está en todo momento.\n\n## Cierre (10-15% del tiempo)\n\n### Resumen\nRecapitula los 3 puntos principales en una frase cada uno.\n\n### Llamada a la acción\nDile a la audiencia exactamente qué quieres que haga: «Mañana, cuando prepares tu próxima reunión, empieza por el gancho.»\n\n### La última frase\nPlanifica tu frase final. No termines con «bueno, eso es todo» o «¿alguna pregunta?». Cierra con fuerza: una cita, una vuelta al gancho inicial, o una frase memorable.",
            PuntosClave = "[\"Toda presentación tiene 3 partes: apertura (10-15%), cuerpo (75-80%) y cierre (10-15%)\",\"El gancho inicial tiene 30 segundos para captar la atención\",\"La regla de 3: presenta máximo 3 puntos principales\",\"Cada punto necesita afirmación, evidencia y transición\",\"Planifica la última frase — nunca improvises el cierre\"]",
            GuionAudio = "Hoy vas a aprender la estructura que usan los mejores presentadores del mundo. No importa si hablas ante 5 personas o ante 500: apertura, cuerpo y cierre. Tres partes que, si las dominas, transformarán cualquier presentación.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = 112, NivelId = 31, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Superar el miedo escénico: técnicas basadas en ciencia",
            Descripcion = "Aprende técnicas respaldadas por investigación para controlar los nervios y convertir la ansiedad en energía.",
            Contenido = "## El miedo escénico es biológico\n\nHablar en público activa la misma respuesta de lucha-huida que sentían nuestros ancestros ante un depredador. La amígdala interpreta 50 pares de ojos mirándote como una amenaza. La buena noticia: se puede reprogramar.\n\n## Dato clave\nSegún la encuesta de Chapman University, la glosofobia (miedo a hablar en público) afecta al 25.3% de la población. Es el miedo social más común.\n\n## Técnicas basadas en evidencia\n\n### 1. Power Posing (Amy Cuddy, Harvard)\nAdoptar posturas expansivas durante 2 minutos antes de presentar:\n- Manos en la cintura, pies separados (pose de superhéroe)\n- Brazos abiertos ocupando espacio\n\n**Resultado**: Incremento de testosterona (+20%) y reducción de cortisol (-25%). Te sientes más confiado y la audiencia lo percibe.\n\n### 2. Respiración 4-7-8 (Dr. Andrew Weil)\n- Inhala por la nariz durante 4 segundos\n- Mantén durante 7 segundos\n- Exhala por la boca durante 8 segundos\n- Repite 3 veces\n\n**Resultado**: Activa el sistema nervioso parasimpático. Reduce la frecuencia cardíaca en 60 segundos.\n\n### 3. Reencuadre de ansiedad (Alison Wood Brooks, Harvard)\nEn lugar de intentar calmarte, di en voz alta: «Estoy emocionado». Tu cuerpo no distingue entre nervios y emoción: los síntomas son idénticos (corazón acelerado, manos sudorosas). Reencuadrar la etiqueta cambia tu rendimiento.\n\n**Estudio**: Los participantes que dijeron «estoy emocionado» rindieron un 17% mejor que los que dijeron «estoy nervioso».\n\n### 4. Visualización de éxito\nAntes de presentar, cierra los ojos y visualiza:\n- Cómo caminas al frente con confianza\n- Cómo la audiencia asiente\n- Cómo terminas y reciben tu mensaje con entusiasmo\n\nLos atletas olímpicos usan esta técnica. Funciona porque el cerebro no distingue bien entre visualización vívida y experiencia real.\n\n### 5. La regla de los 3 primeros minutos\nLos nervios son más intensos al inicio. Si sobrevives los 3 primeros minutos, el resto fluye. Planifica tu apertura palabra por palabra y practícala hasta que sea automática.",
            PuntosClave = "[\"El miedo escénico afecta al 25.3% de la población — es el miedo social más común\",\"El Power Posing de Amy Cuddy reduce el cortisol un 25% en 2 minutos\",\"La respiración 4-7-8 activa el sistema nervioso parasimpático\",\"Decir 'estoy emocionado' mejora el rendimiento un 17% vs 'estoy nervioso'\",\"Los 3 primeros minutos son los más difíciles — practícalos hasta automatizarlos\"]",
            GuionAudio = "El miedo a hablar en público es el miedo social número uno. Pero no es un defecto: es biología. Y hoy vas a aprender 5 técnicas respaldadas por ciencia para convertir esos nervios en tu mejor aliado.",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 10
        };

        Leccion l3 = new Leccion
        {
            Id = 113, NivelId = 31, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Fundamentos de Presentaciones en Público",
            Descripcion = "Pon a prueba tus conocimientos sobre estructura de presentaciones y técnicas para superar el miedo escénico.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 8
        };

        Leccion l4 = new Leccion
        {
            Id = 114, NivelId = 31, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Tu primera presentación ante la dirección",
            Descripcion = "Tu jefe te pide presentar los resultados trimestrales ante el comité de dirección. Tienes 10 minutos y mucho en juego.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = 115, NivelId = 31, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Pitch de proyecto ante inversores",
            Descripcion = "Practica cómo presentar tu proyecto estrella ante un panel de inversores escépticos usando la estructura apertura-cuerpo-cierre.",
            Contenido = "Tienes 5 minutos para convencer a un grupo de inversores de financiar tu proyecto. Están acostumbrados a escuchar decenas de pitches al día y su atención es limitada. Aplica la estructura de tres actos: un gancho potente, 3 puntos clave con evidencia, y un cierre con llamada a la acción.",
            GuionAudio = "En este roleplay, yo seré una inversora experimentada. He escuchado 20 pitches hoy y estoy cansada. Tienes 5 minutos para captar mi atención y convencerme. Recuerda: gancho, tres puntos, cierre con llamada a la acción. ¡Adelante!",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        await SembrarEscenasDesdeLeccionAsync(contexto, l1,
            "Hoy aprenderás la estructura de tres actos que usan los mejores presentadores. No importa el tema: esta fórmula funciona siempre.",
            "Para tu próxima presentación, empieza por el gancho. Escribe las primeras 3 frases palabra por palabra y practícalas en voz alta.");

        await SembrarEscenasDesdeLeccionAsync(contexto, l2,
            "El miedo a hablar en público es biológico, no un defecto de carácter. Hoy vas a aprender a reprogramar esa respuesta con ciencia.",
            "Antes de tu próxima presentación, haz la pose de superhéroe durante 2 minutos y di en voz alta: estoy emocionado. Notarás la diferencia.");

        await SembrarQuizPresentacionesPublicoAsync(contexto, l3.Id);
        await SembrarEscenarioPresentacionesPublicoAsync(contexto, l4.Id);

        // ===== NIVEL 2 — Práctica (NivelId=32) =====
        Leccion l2n1 = new Leccion
        {
            Id = 141, NivelId = 32, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Storytelling profesional: el framework STAR y el Viaje del Héroe",
            Descripcion = "Domina dos frameworks narrativos que transforman datos aburridos en historias memorables para cualquier audiencia.",
            Contenido = "## Por qué las historias vencen a los datos\n\nSegún un estudio de Stanford, las historias son hasta 22 veces más memorables que los datos solos. Cuando escuchas una historia, tu cerebro libera oxitocina (empatía) y dopamina (atención). Cuando escuchas datos, solo se activan las áreas de procesamiento lingüístico.\n\n## Framework STAR para presentaciones profesionales\n\n### S — Situación\nEstablece el contexto: ¿dónde estábamos? ¿Cuándo? ¿Qué estaba pasando?\n«En Q3 de 2024, nuestro NPS había caído 15 puntos en 3 meses.»\n\n### T — Tarea\n¿Cuál era el reto o el objetivo?\n«Necesitábamos revertir la tendencia antes de perder contratos clave.»\n\n### A — Acción\n¿Qué hiciste específicamente? (usa primera persona)\n«Implementé un programa de voz del cliente con entrevistas semanales y un dashboard en tiempo real.»\n\n### R — Resultado\nCuantifica el impacto siempre que sea posible.\n«En 90 días, el NPS subió 22 puntos y retuvimos el 100% de las cuentas en riesgo.»\n\n## El Viaje del Héroe aplicado a presentaciones\n\nJoseph Campbell identificó un patrón narrativo universal. Aplicado a presentaciones profesionales:\n\n### 1. El mundo ordinario\nDescribe la situación actual de la audiencia. Que se identifiquen.\n«Todos hemos estado en esa presentación donde el presentador lee cada diapositiva...»\n\n### 2. La llamada a la aventura\nPresenta el problema o la oportunidad.\n«¿Y si pudiéramos hacer que cada presentación fuera una experiencia memorable?»\n\n### 3. El mentor y las herramientas\nAquí presentas tu solución, framework o propuesta.\n«Hoy os traigo un sistema de 3 pasos que...»\n\n### 4. Las pruebas\nComparte casos reales, datos, testimonios.\n«Cuando lo aplicamos con el equipo de ventas, los cierres subieron un 34%.»\n\n### 5. El regreso transformado\nLa audiencia vuelve a su mundo con nuevas herramientas.\n«Mañana, en vuestra próxima reunión, podréis aplicar esto inmediatamente.»\n\n## Cuándo usar cada framework\n- **STAR**: entrevistas de trabajo, reportes de resultados, justificación de presupuestos\n- **Viaje del Héroe**: keynotes, presentaciones de visión, pitches de innovación",
            PuntosClave = "[\"Las historias son 22 veces más memorables que los datos solos (Stanford)\",\"STAR: Situación, Tarea, Acción, Resultado — ideal para presentaciones de resultados\",\"El Viaje del Héroe tiene 5 fases aplicables: mundo ordinario, llamada, mentor, pruebas y regreso\",\"STAR para reportes y entrevistas; Viaje del Héroe para keynotes y visión\",\"Las historias liberan oxitocina y dopamina en el cerebro del oyente\"]",
            GuionAudio = "Los datos informan, pero las historias transforman. Hoy dominarás dos frameworks narrativos que usan los mejores presentadores del mundo: el STAR para presentaciones de resultados y el Viaje del Héroe para inspirar audiencias.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 12
        };

        Leccion l2n2 = new Leccion
        {
            Id = 142, NivelId = 32, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Uso efectivo de visuales: el principio de Mayer",
            Descripcion = "Aprende a diseñar apoyos visuales que potencien tu mensaje en lugar de competir con él.",
            Contenido = "## El problema: muerte por PowerPoint\n\nEl 79% de los profesionales admite que las presentaciones con demasiado texto son ineficaces (Prezi, 2023). Sin embargo, seguimos llenando diapositivas de bullets. ¿Por qué?\n\n## El principio de Mayer (Teoría Cognitiva del Aprendizaje Multimedia)\n\nRichard Mayer, psicólogo de UC Santa Barbara, demostró que las personas aprenden mejor con palabras e imágenes juntas que con palabras solas. Pero con condiciones:\n\n### 1. Principio de coherencia\nElimina todo lo que no sea esencial. Cada elemento decorativo que añades reduce la comprensión un 12% (Mayer, 2009).\n\n### 2. Principio de señalización\nUsa resaltados, flechas y títulos claros para guiar la atención. El cerebro no procesa todo a la vez.\n\n### 3. Principio de redundancia\nNO pongas en la diapositiva lo mismo que dices verbalmente. Si la audiencia lee, no te escucha. Si te escucha, no lee. Elige uno.\n\n### 4. Principio de contigüidad espacial\nColoca las palabras cerca de las imágenes que explican. No pongas el texto arriba y la imagen abajo.\n\n### 5. Principio de contigüidad temporal\nPresenta palabras e imágenes simultáneamente, no de forma secuencial.\n\n## La regla 10-20-30 de Guy Kawasaki\n- **10 diapositivas** máximo\n- **20 minutos** de presentación\n- **30 puntos** como tamaño mínimo de fuente\n\nSi no puedes explicar tu idea en 10 diapositivas, no la entiendes lo suficiente.\n\n## Diseño práctico: la regla del 6\n- Máximo 6 palabras por línea\n- Máximo 6 líneas por diapositiva\n- Una idea por diapositiva\n- Usa imágenes de alta calidad, no clipart\n- Contraste alto: texto claro sobre fondo oscuro o viceversa\n\n## El test del estacionamiento\nSi alguien ve tu diapositiva desde el fondo de un estacionamiento y no la entiende en 3 segundos, tiene demasiada información.",
            PuntosClave = "[\"El 79% de profesionales considera ineficaces las presentaciones con demasiado texto\",\"Principio de redundancia de Mayer: no leas lo que está en la diapositiva\",\"Cada elemento decorativo innecesario reduce la comprensión un 12%\",\"Regla 10-20-30 de Kawasaki: 10 slides, 20 minutos, fuente 30pt\",\"Test del estacionamiento: si no se entiende en 3 segundos, sobra información\"]",
            GuionAudio = "Las diapositivas no son tu guión: son el apoyo visual de tu mensaje. Hoy aprenderás los principios científicos de Richard Mayer y las reglas prácticas de diseño que separan una presentación memorable de la muerte por PowerPoint.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 12
        };

        Leccion l2n3 = new Leccion
        {
            Id = 143, NivelId = 32, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Storytelling y Visuales",
            Descripcion = "Evalúa tu dominio del framework STAR, el Viaje del Héroe y los principios de Mayer para apoyos visuales.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 10
        };

        Leccion l2n4 = new Leccion
        {
            Id = 144, NivelId = 32, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Presentación de resultados al comité ejecutivo",
            Descripcion = "Debes presentar los resultados de tu proyecto usando storytelling y visuales efectivos ante un comité con poco tiempo.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 12
        };

        Leccion l2n5 = new Leccion
        {
            Id = 145, NivelId = 32, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Keynote de innovación con storytelling",
            Descripcion = "Practica una presentación tipo keynote usando el Viaje del Héroe para presentar una propuesta de innovación a tu empresa.",
            Contenido = "Tu empresa celebra un evento interno de innovación. Te han dado 7 minutos para presentar tu propuesta ante 200 compañeros y el CEO. Usa el Viaje del Héroe para estructurar tu narrativa y aplica los principios de Mayer en tus visuales.",
            GuionAudio = "En este roleplay seré parte de tu audiencia, incluyendo al CEO. Tienes 7 minutos para cautivar a 200 personas con tu propuesta de innovación. Recuerda: storytelling con el Viaje del Héroe y visuales que apoyen sin distraer. ¡Es tu momento!",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(l2n1, l2n2, l2n3, l2n4, l2n5);
        await contexto.SaveChangesAsync();

        await SembrarEscenasDesdeLeccionAsync(contexto, l2n1,
            "Los datos informan pero las historias transforman. Hoy dominarás dos frameworks narrativos que usan los mejores presentadores del mundo.",
            "En tu próxima presentación, elige un resultado importante y cuéntalo con STAR. Verás cómo la audiencia conecta mucho más que con una tabla de números.");

        await SembrarEscenasDesdeLeccionAsync(contexto, l2n2,
            "El 79% de los profesionales considera ineficaces las presentaciones con demasiado texto. Hoy aprenderás la ciencia detrás del diseño visual efectivo.",
            "Revisa tu última presentación y aplica el test del estacionamiento: si una diapositiva no se entiende en 3 segundos desde lejos, simplifica.");

        // ===== NIVEL 3 — Dominio (NivelId=33) =====
        Leccion l3n1 = new Leccion
        {
            Id = 171, NivelId = 33, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Presentaciones TED-style: la regla de 18 minutos y la regla de 3",
            Descripcion = "Aprende los principios de las charlas TED más vistas y cómo aplicarlos a cualquier presentación profesional.",
            Contenido = "## Por qué TED funciona\n\nLas charlas TED más vistas comparten patrones específicos. Chris Anderson, curador de TED, y Carmine Gallo, autor de 'Talk Like TED', identificaron las claves:\n\n## La regla de 18 minutos\n\nTED limita todas las charlas a 18 minutos. No es arbitrario:\n- **Carga cognitiva**: A los 18 minutos, el cerebro empieza a experimentar fatiga informacional (Dr. Paul King, neurocientífico de Texas Christian University).\n- **Disciplina**: Obligarte a condensar tu mensaje te fuerza a ser claro. Como dijo Mark Twain: «No tuve tiempo de escribir una carta corta, así que escribí una larga.»\n- **Tensión creativa**: La restricción genera creatividad. Si tienes 60 minutos, rellenas. Si tienes 18, destidas.\n\n## La regla de 3 de Carmine Gallo\n\nGallo analizó 500+ charlas TED y descubrió que las más exitosas tienen exactamente 3 mensajes principales:\n\n### Estructura TED óptima\n1. **Apertura emocional** (2-3 min): Historia personal o dato sorprendente\n2. **3 pilares** (12-13 min): ~4 min por pilar con evidencia\n3. **Cierre inspirador** (2-3 min): Vuelta a la apertura + llamada a la acción\n\n## Los 3 pilares de Gallo\n\n### 1. Emoción\nLas 10 charlas TED más vistas empiezan con una historia personal. Simon Sinek empieza su charla con los hermanos Wright. Brené Brown con su crisis profesional. La emoción es la puerta.\n\n### 2. Novedad\nEl cerebro se activa ante lo inesperado. Presenta un dato contraintuitivo, un enfoque nuevo o un momento «jaw-dropping». Bill Gates soltó mosquitos en la audiencia para hablar de malaria.\n\n### 3. Memorabilidad\nUsa lo que Gallo llama «el momento santo» (holy smokes moment): un instante que la audiencia recordará y compartirá. Steve Jobs sacó el MacBook Air de un sobre de papel. Es teatral, pero funciona.\n\n## El framework de la idea que merece ser compartida\nTED pide a cada speaker que defina su «idea worth spreading» en una frase de máximo 15 palabras. Si no puedes hacerlo, tu mensaje no está claro.\n\nEjercicios:\n- «Mi idea es que [tu idea en 15 palabras o menos]»\n- «Después de mi presentación, quiero que la audiencia [acción concreta]»\n- «Si solo pudieran recordar una cosa, sería [una frase]»",
            PuntosClave = "[\"TED limita a 18 minutos porque el cerebro experimenta fatiga informacional después\",\"Las mejores charlas TED tienen exactamente 3 mensajes principales (Carmine Gallo)\",\"Los 3 pilares TED: emoción, novedad y memorabilidad\",\"Define tu idea en 15 palabras o menos — si no puedes, no está clara\",\"La estructura óptima: apertura emocional (2-3 min), 3 pilares (12-13 min), cierre inspirador (2-3 min)\"]",
            GuionAudio = "Las charlas TED más vistas no son casualidad. Siguen patrones específicos que puedes aplicar a cualquier presentación. Hoy aprenderás la regla de 18 minutos, la regla de 3 de Carmine Gallo y cómo crear ese momento memorable que tu audiencia no olvidará.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 15
        };

        Leccion l3n2 = new Leccion
        {
            Id = 172, NivelId = 33, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Improvisación y manejo de Q&A bajo presión",
            Descripcion = "Domina técnicas de improvisación profesional y manejo de preguntas difíciles para mantener la credibilidad y el control.",
            Contenido = "## La paradoja de la improvisación\n\nLos mejores improvisadores no improvisan: tienen frameworks preparados para lo inesperado. La improvisación profesional es preparación flexible.\n\n## El framework PREP para respuestas improvisadas\n\n### P — Point (Punto)\nEmpieza con tu conclusión: «Mi posición es que deberíamos expandir al mercado europeo.»\n\n### R — Reason (Razón)\nDa tu argumento principal: «Porque el mercado europeo tiene un 40% menos de competencia y márgenes un 15% superiores.»\n\n### E — Example (Ejemplo)\nAñade evidencia concreta: «Nuestro piloto en Alemania generó 200K€ en 90 días sin inversión en marketing.»\n\n### P — Point (Punto de nuevo)\nVuelve a tu conclusión: «Por eso creo firmemente que Europa es nuestra prioridad de expansión.»\n\n## Manejo de preguntas difíciles\n\n### La técnica del puente\nReconoce la pregunta → Construye un puente → Lleva a tu mensaje clave.\n«Esa es una pregunta interesante. Lo que realmente importa aquí es...»\n\n### La técnica del embudo\nSi la pregunta es vaga, estrecha: «¿Se refiere específicamente a los resultados del Q2 o al pronóstico anual?»\n\n### La técnica de la honestidad\nSi no sabes la respuesta: «No tengo ese dato ahora mismo. Me comprometo a enviárselo por email antes del viernes.» Nunca inventes. Tu credibilidad vale más que una respuesta.\n\n### La técnica del espejo\nRepite la pregunta reformulada para ganar tiempo y asegurar comprensión: «Si entiendo bien, usted pregunta si...»\n\n## Preguntas hostiles\n\n### Regla 1: No te lo tomes personal\nLa persona ataca la idea, no a ti. Respira.\n\n### Regla 2: Valida antes de responder\n«Entiendo su preocupación y es legítima. Permítame explicar cómo hemos abordado exactamente ese riesgo.»\n\n### Regla 3: El 3-1-3\nSi te interrumpen: deja que hablen 3 segundos, levanta suavemente la mano, y retoma en 3 segundos. Nunca entres en un duelo verbal.\n\n## Improvisación aplicada: la técnica «Sí, y...»\nDel teatro de improvisación: acepta lo que te dan y construye sobre ello.\n- «Tiene razón en que los costes son altos. Y precisamente por eso hemos diseñado un modelo de implementación por fases que reduce el riesgo financiero un 60%.»\n\n## Preparación para Q&A\nAntes de cualquier presentación, anticipa las 10 preguntas más probables y las 5 más incómodas. Prepara respuestas PREP para cada una. El 80% de las preguntas son predecibles.",
            PuntosClave = "[\"Framework PREP: Punto, Razón, Ejemplo, Punto — para respuestas estructuradas sobre la marcha\",\"El 80% de las preguntas son predecibles — prepara respuestas PREP para las 10 más probables\",\"Si no sabes la respuesta, di la verdad y comprométete a responder después\",\"La técnica del puente: reconoce, construye, lleva a tu mensaje clave\",\"Técnica 'Sí, y...' del teatro de improvisación: acepta y construye sobre lo que te dan\"]",
            GuionAudio = "Las preguntas difíciles son donde se gana o se pierde la credibilidad. No es lo que sabes, sino cómo manejas lo que no sabes. Hoy dominarás el framework PREP para improvisar con estructura y técnicas profesionales para manejar cualquier pregunta, incluidas las hostiles.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 15
        };

        Leccion l3n3 = new Leccion
        {
            Id = 173, NivelId = 33, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Presentaciones TED e Improvisación",
            Descripcion = "Evalúa tu dominio de las técnicas TED-style, el framework PREP y el manejo de Q&A bajo presión.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 12
        };

        Leccion l3n4 = new Leccion
        {
            Id = 174, NivelId = 33, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Q&A hostil después de tu presentación estratégica",
            Descripcion = "Acabas de presentar un plan de reestructuración y el director financiero cuestiona agresivamente tus cifras delante de todos.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 15
        };

        Leccion l3n5 = new Leccion
        {
            Id = 175, NivelId = 33, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Charla TED ante audiencia en vivo",
            Descripcion = "Practica una charla TED-style de 5 minutos con preguntas improvisadas al final, aplicando todas las técnicas avanzadas.",
            Contenido = "Es la conferencia anual de tu industria. Te han invitado como speaker y tienes 5 minutos para presentar una idea innovadora seguidos de 3 minutos de preguntas. Aplica la estructura TED: apertura emocional, 3 pilares con evidencia, cierre inspirador. Después, maneja las preguntas con el framework PREP y las técnicas de puente y espejo.",
            GuionAudio = "En este roleplay simularemos una charla TED con audiencia real. Yo seré un moderador exigente y parte de la audiencia hará preguntas desafiantes. Tienes 5 minutos de presentación y 3 de Q&A. Aplica todo lo aprendido: estructura TED, la regla de 3, el momento memorable, y manejo PREP de preguntas. ¡Este es tu momento cumbre!",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(l3n1, l3n2, l3n3, l3n4, l3n5);
        await contexto.SaveChangesAsync();

        await SembrarEscenasDesdeLeccionAsync(contexto, l3n1,
            "Las charlas TED más vistas siguen patrones muy específicos. Hoy vas a conocer las reglas que usan los mejores speakers del mundo y cómo aplicarlas a tu realidad profesional.",
            "Define tu próxima presentación en una frase de 15 palabras. Si no puedes, simplifica hasta que puedas. Esa es tu idea que merece ser compartida.");

        await SembrarEscenasDesdeLeccionAsync(contexto, l3n2,
            "Las preguntas difíciles son donde realmente se mide un presentador. Hoy aprenderás frameworks para improvisar con estructura y mantener tu credibilidad bajo presión.",
            "Antes de tu próxima presentación, escribe las 5 preguntas más difíciles que podrían hacerte y prepara respuestas PREP para cada una. El 80% de las preguntas son predecibles.");
    }

    private static async Task SembrarQuizPresentacionesPublicoAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Cuánto tiempo tienes para captar la atención de la audiencia en la apertura?",
                "Los estudios muestran que los primeros 30 segundos son decisivos para captar o perder la atención.",
                new[] { ("5 minutos", false, "Es mucho menos. La audiencia decide en los primeros 30 segundos si vale la pena escucharte."), ("30 segundos", true, "¡Correcto! Tienes 30 segundos para enganchar a la audiencia con un dato, una pregunta o una historia."), ("2 minutos", false, "La atención se decide mucho antes: en los primeros 30 segundos."), ("No importa el tiempo", false, "Sí importa, y mucho. 30 segundos es todo lo que tienes para el gancho.") }),
            CrearPregunta(leccionId, 2, "¿Cuántos puntos principales debe tener el cuerpo de una presentación según la regla de 3?",
                "El cerebro humano retiene mejor la información en grupos de tres puntos.",
                new[] { ("1 punto profundo", false, "Un solo punto puede ser insuficiente para una presentación completa."), ("3 puntos", true, "¡Correcto! La regla de 3 aprovecha cómo el cerebro agrupa y retiene información."), ("5 puntos", false, "Cinco puntos saturan la memoria de trabajo. El cerebro prefiere 3."), ("Tantos como sea necesario", false, "Más no es mejor. La regla de 3 es óptima para la retención.") }),
            CrearPregunta(leccionId, 3, "Según la investigación de Amy Cuddy, ¿qué efecto tiene el Power Posing durante 2 minutos?",
                "Las posturas de poder reducen el cortisol un 25% e incrementan la testosterona un 20%.",
                new[] { ("Aumenta la inteligencia", false, "El Power Posing afecta a las hormonas del estrés y la confianza, no a la inteligencia."), ("Reduce cortisol un 25% y aumenta testosterona un 20%", true, "¡Correcto! La postura expansiva cambia tu bioquímica: menos estrés y más confianza."), ("Solo es un placebo", false, "Hay evidencia hormonal medible de los efectos del Power Posing."), ("Mejora la memoria", false, "El efecto principal es hormonal: reduce el estrés y aumenta la confianza.") }),
            CrearPregunta(leccionId, 4, "¿Qué técnica propone Alison Wood Brooks para manejar los nervios antes de presentar?",
                "Reencuadrar la ansiedad diciendo 'estoy emocionado' mejora el rendimiento un 17%.",
                new[] { ("Respirar profundamente y decir 'estoy calmado'", false, "Intentar calmarte va contra la activación fisiológica. Es más efectivo reencuadrar."), ("Decir en voz alta 'estoy emocionado'", true, "¡Correcto! El reencuadre de ansiedad convierte los nervios en entusiasmo y mejora el rendimiento un 17%."), ("Meditar 30 minutos antes", false, "La meditación ayuda, pero la técnica específica de Brooks es el reencuadre verbal."), ("Imaginar a la audiencia desnuda", false, "Este mito popular no tiene respaldo científico y puede ser contraproducente.") }),
            CrearPregunta(leccionId, 5, "¿Cómo debe terminar una presentación efectiva?",
                "El cierre debe ser planificado: resumen, llamada a la acción y frase final memorable.",
                new[] { ("Con '¿Alguna pregunta?'", false, "Las preguntas son importantes, pero no son un cierre. El cierre debe ser planificado y memorable."), ("Diciendo 'bueno, eso es todo'", false, "Eso transmite inseguridad. El cierre debe ser tu momento más fuerte."), ("Con un resumen, llamada a la acción y frase final memorable", true, "¡Correcto! Planifica tu cierre: recapitula, di qué quieres que hagan, y cierra con fuerza."), ("Simplemente dejando de hablar", false, "Un cierre abrupto desperdicia todo el impacto construido durante la presentación.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenarioPresentacionesPublicoAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Es martes por la mañana y tu jefe te dice que el director general quiere ver los resultados trimestrales del proyecto en una reunión con el comité de dirección. Tienes 10 minutos de presentación. El comité incluye al CFO (que solo quiere números), la directora de RRHH (que quiere saber el impacto en el equipo) y el CEO (que quiere visión estratégica). Notas que tus manos tiemblan ligeramente al preparar las diapositivas.",
            Contexto = "Nunca has presentado ante el comité de dirección. Tu proyecto ha tenido buenos resultados (15% de mejora en eficiencia) pero también un sobrecoste del 8%. Tienes 2 horas para prepararte.",
            GuionAudio = "Este escenario pondrá a prueba todo lo que has aprendido sobre estructura de presentaciones y manejo del miedo escénico. Recuerda: apertura con gancho, 3 puntos clave, cierre con llamada a la acción. Y antes de entrar, usa tus técnicas contra los nervios."
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Hacer la pose de superhéroe 2 minutos antes de entrar y decir 'estoy emocionado'. Luego presentar con estructura clara: abrir con el dato de impacto (15% mejora), desarrollar 3 puntos (resultados financieros para el CFO, impacto en equipo para RRHH, visión estratégica para el CEO), abordar proactivamente el sobrecoste con un plan de mitigación, y cerrar con los próximos pasos concretos.",
                TipoResultado = TipoResultadoEscenario.Optimo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Excelente! Aplicaste las técnicas de manejo de nervios, estructuraste tu presentación pensando en cada audiencia, abordaste el punto débil proactivamente (el sobrecoste) y cerraste con acción. Eso es presentar como un profesional."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Preparar diapositivas detalladas con todos los datos del proyecto y leerlas durante la presentación para no olvidar nada. Incluir el sobrecoste solo si alguien pregunta.",
                TipoResultado = TipoResultadoEscenario.Aceptable, PuntosOtorgados = 8,
                TextoRetroalimentacion = "Preparar los datos está bien, pero leer las diapositivas pierde a la audiencia. Y ocultar el sobrecoste es arriesgado: si lo descubren en Q&A, tu credibilidad cae. Es mejor abordar los puntos débiles proactivamente y con un plan."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Pedir a tu jefe que presente en tu lugar porque tú no tienes experiencia con el comité de dirección y los nervios podrían traicionarte.",
                TipoResultado = TipoResultadoEscenario.Inadecuado, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Evitar la situación elimina los nervios pero también elimina tu oportunidad de crecimiento y visibilidad. El miedo escénico se supera con técnicas y práctica, no evitándolo. Las técnicas de Power Posing y reencuadre existen exactamente para este momento."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }
}
