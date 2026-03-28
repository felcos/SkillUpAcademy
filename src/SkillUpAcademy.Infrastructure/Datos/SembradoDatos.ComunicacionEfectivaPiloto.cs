using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado piloto de Comunicación Efectiva: 5 niveles Dreyfus, 32 lecciones, ~25 horas.
/// Reemplaza el seeding antiguo de 3 niveles con el nuevo modelo metodológico.
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesComunicacionPilotoAsync(AppDbContext contexto)
    {
        // ================================================================
        // NIVEL 1 — DESPERTAR (NivelId=1, 6 lecciones, IDs 1001-1006)
        // ================================================================

        Leccion l1001 = new Leccion
        {
            Id = 1001, NivelId = 1, TipoLeccion = TipoLeccion.Autoevaluacion,
            Titulo = "¿Cómo comunicas hoy?",
            Descripcion = "Descubre tu nivel real de comunicación a través de una conversación diagnóstica con Aria.",
            Contenido = "Eres Aria evaluando el nivel de comunicación del usuario. Haz preguntas abiertas sobre su vida laboral:\n1. «Cuéntame una situación reciente donde sentiste que tu mensaje no llegó como querías.»\n2. «¿Cómo reaccionas cuando alguien te interrumpe en una reunión?»\n3. «Describe cómo das feedback a un compañero que cometió un error.»\n4. «¿Qué haces cuando no estás de acuerdo con tu jefe?»\n5. «¿Cómo te preparas para una conversación difícil?»\nDespués de 5-7 preguntas, da un diagnóstico: 2 fortalezas, 2 áreas de mejora, 1 punto ciego. Sé honesta pero constructiva.",
            GuionAudio = "¡Hola! Antes de empezar el curso, quiero conocer cómo te comunicas hoy. No hay respuestas correctas ni incorrectas. Solo quiero entender tu punto de partida para personalizar tu aprendizaje.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 20
        };

        Leccion l1002 = new Leccion
        {
            Id = 1002, NivelId = 1, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "El espejo: los 4 niveles de escucha",
            Descripcion = "Comprende los 4 niveles de escucha según Otto Scharmer y descubre en cuál operas habitualmente.",
            Contenido = "## La mayoría escuchamos en piloto automático\n\nOtto Scharmer (MIT) identificó 4 niveles de escucha que determinan la calidad de nuestras conversaciones:\n\n### Nivel 1: Descarga\nEscuchas solo para confirmar lo que ya crees. Filtras todo por tus opiniones existentes. «Ya lo sabía» es tu frase mental constante.\n\n**Señal:** Asientes pero no cambias de opinión nunca.\n\n### Nivel 2: Factual\nEscuchas los datos y hechos nuevos. Prestas atención a lo que no encaja con lo que sabes. Es el nivel del científico.\n\n**Señal:** Haces preguntas de clarificación: «¿Cuánto? ¿Cuándo? ¿Quién?»\n\n### Nivel 3: Empático\nEscuchas desde el lugar del otro. Sientes lo que la otra persona siente. Ves el mundo a través de sus ojos.\n\n**Señal:** Dices «entiendo por qué te sientes así» y lo dices en serio.\n\n### Nivel 4: Generativo\nEscuchas lo que está emergiendo, lo que todavía no se ha dicho. Estás tan presente que la conversación crea algo nuevo que ninguno de los dos tenía antes.\n\n**Señal:** Al terminar, ambos dicen «no había pensado en eso antes».\n\n## ¿Por qué importa?\n\nLa mayoría operamos en Nivel 1-2 el 90% del tiempo. Los líderes excepcionales operan en Nivel 3-4. La buena noticia: se puede entrenar.\n\n## El experimento del espejo\n\nEsta semana, después de cada conversación importante, pregúntate: «¿En qué nivel estuve?» Solo con esa consciencia, empezarás a subir de nivel.",
            PuntosClave = "[\"Scharmer identifica 4 niveles: Descarga, Factual, Empático y Generativo\",\"La mayoría operamos en nivel 1-2 el 90% del tiempo\",\"El nivel empático requiere ver el mundo desde los ojos del otro\",\"El nivel generativo crea algo nuevo que no existía antes\",\"La consciencia del propio nivel es el primer paso para mejorar\"]",
            GuionAudio = "Hoy vas a descubrir algo que cambiará cómo escuchas para siempre. Otto Scharmer del MIT identificó 4 niveles de escucha, y la mayoría de nosotros nos quedamos atrapados en los dos primeros.",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 35
        };

        Leccion l1003 = new Leccion
        {
            Id = 1003, NivelId = 1, TipoLeccion = TipoLeccion.CasoEstudio,
            Titulo = "El CEO que no escuchaba",
            Descripcion = "Analiza el caso real de un CEO que perdió a su mejor equipo por no escuchar las señales de alerta.",
            Contenido = "Presenta este caso paso a paso:\n\nFASE 1 — Contexto: Carlos, CEO de una startup de 80 personas en Madrid. Facturación creciendo 40% anual. Su CTO, María, lleva 3 años liderando el equipo técnico de 25 personas.\n\nFASE 2 — Señales: María pidió 3 reuniones 1:1 en 2 meses. Carlos las canceló por 'urgencias'. En la tercera, María dijo: 'El equipo está quemado, necesitamos contratar 5 personas y reducir el ritmo de releases.' Carlos respondió: 'Ahora no es momento, estamos en la mejor racha de nuestra historia.'\n\nPregunta al usuario: ¿Qué harías tú en el lugar de Carlos?\n\nFASE 3 — Desenlace: En 6 semanas, María y 4 seniors renunciaron. El equipo técnico se paralizó. Carlos tardó 8 meses en reconstruir. Perdió un contrato de 2M€ por retrasos.\n\nFASE 4 — Análisis: Guía al usuario a identificar: (1) Carlos operaba en Nivel 1 de escucha, (2) confundió urgente con importante, (3) el costo de no escuchar fue 10x mayor que lo que María pedía.\n\nConecta con los 4 niveles de Scharmer de la lección anterior.",
            PuntosClave = "[\"No escuchar las señales de alerta tiene costos exponenciales\",\"Cancelar reuniones 1:1 envía el mensaje de que la persona no importa\",\"El crecimiento rápido no justifica ignorar al equipo\",\"El costo de perder talento siempre supera el costo de escuchar\"]",
            GuionAudio = "Hoy vamos a analizar un caso real que ilustra perfectamente el precio de no escuchar. Te presento a Carlos, un CEO exitoso que lo perdió todo por operar en Nivel 1 de escucha.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 25
        };

        Leccion l1004 = new Leccion
        {
            Id = 1004, NivelId = 1, TipoLeccion = TipoLeccion.PracticaGuiada,
            Titulo = "Tu primera escucha activa",
            Descripcion = "Practica escucha activa en una conversación real con Aria actuando como tu compañera de trabajo.",
            Contenido = "Actúa como Laura, una compañera de trabajo del usuario que está frustrada porque le asignaron un proyecto nuevo sin consultar su carga actual. Laura habla rápido, mezcla emociones con datos, y a veces se contradice.\n\nObjetivo del usuario: practicar escucha activa (parafraseo, preguntas abiertas, validación emocional).\n\nNivel 1 (fácil): Laura cuenta el problema de forma clara.\nNivel 2 (medio): Laura se desvía a quejas sobre el jefe. El usuario debe redirigir con empatía.\nNivel 3 (difícil): Laura dice 'da igual, no importa' cerrándose. El usuario debe usar silencio y preguntas abiertas.\n\nDespués de 4-5 intercambios, sal del personaje y da feedback citando frases exactas del usuario.",
            GuionAudio = "Ahora vamos a practicar. Voy a ser Laura, tu compañera de trabajo que necesita ser escuchada. Tu objetivo es escuchar activamente: parafrasea, haz preguntas abiertas y valida sus emociones.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 30
        };

        Leccion l1005 = new Leccion
        {
            Id = 1005, NivelId = 1, TipoLeccion = TipoLeccion.Reflexion,
            Titulo = "¿Qué descubriste sobre ti?",
            Descripcion = "Reflexiona sobre tu experiencia de escucha activa usando el Ciclo de Gibbs.",
            Contenido = "Guía al usuario por el Ciclo de Gibbs aplicado a su práctica de escucha activa:\n1. Descripción: '¿Qué pasó en la conversación con Laura? Cuéntame los momentos clave.'\n2. Sentimientos: '¿Qué sentiste cuando Laura se cerró? ¿Te frustró o te motivó a intentar otra cosa?'\n3. Evaluación: '¿En qué momentos escuchaste bien? ¿Cuándo caíste en Nivel 1?'\n4. Análisis: '¿Por qué crees que reaccionaste así? ¿Te recuerda a situaciones reales?'\n5. Conclusión: '¿Qué descubriste que no sabías sobre tu forma de escuchar?'\n6. Plan: '¿Qué harás diferente en tu próxima conversación real?'\nHaz UNA pregunta a la vez. Escucha la respuesta antes de avanzar.",
            GuionAudio = "Acabas de practicar escucha activa y ahora es momento de reflexionar. Voy a guiarte por un proceso estructurado para que extraigas el máximo aprendizaje de lo que acabas de experimentar.",
            PuntosRecompensa = 15, Orden = 5, DuracionMinutos = 20
        };

        Leccion l1006 = new Leccion
        {
            Id = 1006, NivelId = 1, TipoLeccion = TipoLeccion.PlanAccion,
            Titulo = "Tu primer compromiso real",
            Descripcion = "Crea un compromiso SMART para practicar escucha activa esta semana en una situación real.",
            Contenido = "Ayuda al usuario a crear un compromiso SMART de escucha activa:\n- Específico: '¿Con quién vas a practicar? ¿En qué tipo de conversación?'\n- Medible: '¿Cómo sabrás que escuchaste activamente? ¿Qué señales buscarás?'\n- Alcanzable: 'Empecemos con UNA conversación. ¿Cuál sería la más fácil para empezar?'\n- Relevante: '¿Por qué esta persona/situación? ¿Qué ganarías al escucharla mejor?'\n- Temporal: '¿Qué día y hora específica? No vale \"esta semana\", necesito día y hora.'\nNo aceptes respuestas vagas. Si dice 'con mi equipo', pregunta '¿con quién específicamente?'",
            GuionAudio = "Ahora viene lo más importante: convertir lo aprendido en acción real. Vamos a crear un compromiso concreto que vas a cumplir esta semana. No un propósito vago, sino algo específico.",
            PuntosRecompensa = 15, Orden = 6, DuracionMinutos = 15
        };

        // ================================================================
        // NIVEL 2 — FUNDAMENTAR (NivelId=2, 7 lecciones, IDs 1007-1013)
        // ================================================================

        Leccion l1007 = new Leccion
        {
            Id = 1007, NivelId = 2, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Comunicación No Violenta: los 4 pasos de Rosenberg",
            Descripcion = "Domina el framework de Marshall Rosenberg para expresar necesidades sin generar defensividad.",
            Contenido = "## El problema: comunicamos juicios, no necesidades\n\nMarshall Rosenberg observó que la mayoría de los conflictos escalan porque expresamos juicios ('Eres irresponsable') en lugar de necesidades ('Necesito que cumplas los plazos porque afecta al equipo').\n\n## Los 4 pasos de la CNV\n\n### 1. Observación (sin juicio)\nDescribe hechos concretos sin evaluar.\n- ❌ «Siempre llegas tarde»\n- ✅ «En las últimas 3 reuniones, llegaste entre 5 y 15 minutos después de la hora»\n\n### 2. Sentimiento (sin culpa)\nExpresa cómo te afecta, sin culpar al otro.\n- ❌ «Me haces sentir ignorado»\n- ✅ «Me siento frustrado cuando esto pasa»\n\n### 3. Necesidad (universal)\nIdentifica la necesidad humana detrás del sentimiento.\n- Respeto, previsibilidad, colaboración, autonomía, reconocimiento...\n- «Necesito previsibilidad para organizar mi día»\n\n### 4. Petición (concreta y negociable)\nPide algo específico y realista.\n- ❌ «Necesito que seas más responsable»\n- ✅ «¿Podrías avisarme con 10 minutos si vas a llegar tarde?»\n\n## La fórmula completa\n«Cuando [observación], me siento [sentimiento] porque necesito [necesidad]. ¿Estarías dispuesto a [petición]?»\n\n## El error más común\nSaltarse los pasos 2 y 3. Sin sentimientos y necesidades, la petición suena como una orden.",
            PuntosClave = "[\"La CNV tiene 4 pasos: Observación, Sentimiento, Necesidad y Petición\",\"Separar observación de juicio es el paso más difícil\",\"Los sentimientos se expresan sin culpar al otro\",\"Las necesidades son universales: respeto, autonomía, reconocimiento\",\"Las peticiones deben ser concretas y negociables\"]",
            GuionAudio = "Marshall Rosenberg cambió la forma en que millones de personas se comunican. Su método de Comunicación No Violenta tiene solo 4 pasos, pero dominarlos transforma tus relaciones profesionales.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 40
        };

        Leccion l1008 = new Leccion
        {
            Id = 1008, NivelId = 2, TipoLeccion = TipoLeccion.CasoEstudio,
            Titulo = "El equipo que se destruyó por un email",
            Descripcion = "Analiza cómo un email mal redactado provocó la ruptura de un equipo de alto rendimiento.",
            Contenido = "Presenta este caso:\n\nFASE 1: Equipo de producto en Barcelona, 12 personas. Ana (líder) envía email a todo el equipo el viernes a las 18:00: 'He revisado los entregables de esta semana y francamente estoy decepcionada. Algunos de vosotros no están a la altura. El lunes hablaremos uno por uno.'\n\nPregunta: ¿Qué problemas ves en este email?\n\nFASE 2: Reacciones del equipo: 3 personas pasaron el fin de semana con ansiedad. 2 actualizaron su CV. El desarrollador senior respondió 'reply-all' con tono agresivo. Se formaron bandos.\n\nPregunta: Si fueras Ana, ¿cómo reescribirías ese email usando CNV?\n\nFASE 3: Solución CNV: Guía al usuario a reescribir usando los 4 pasos de Rosenberg. Muestra la versión mejorada.\n\nFASE 4: Principios extraídos: (1) nunca dar feedback negativo por email a un grupo, (2) el timing importa (viernes 18:00), (3) 'algunos de vosotros' crea paranoia colectiva.",
            PuntosClave = "[\"El feedback negativo grupal por email destruye la seguridad psicológica\",\"El timing de un mensaje importa tanto como su contenido\",\"Acusar sin especificar genera paranoia colectiva\",\"La CNV convierte críticas en peticiones constructivas\"]",
            GuionAudio = "Hoy analizaremos cómo un simple email destruyó un equipo de alto rendimiento en 72 horas. Es un caso perfecto para aplicar lo que aprendimos de Comunicación No Violenta.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 25
        };

        Leccion l1009 = new Leccion
        {
            Id = 1009, NivelId = 2, TipoLeccion = TipoLeccion.PracticaGuiada,
            Titulo = "Expresar necesidades sin atacar",
            Descripcion = "Practica los 4 pasos de CNV con Aria actuando como un colega defensivo.",
            Contenido = "Actúa como Diego, un colega del usuario que siempre se atribuye el mérito del trabajo en equipo en las presentaciones al jefe.\n\nEl usuario debe practicar CNV para abordar la situación.\n\nNivel 1: Diego es receptivo cuando el usuario usa CNV correctamente.\nNivel 2: Diego se pone defensivo: 'Yo solo presento lo que hacemos todos.' El usuario debe mantener la CNV.\nNivel 3: Diego contraataca: 'Si quisieras reconocimiento, deberías hablar más en las reuniones.' El usuario debe no caer en la provocación.\n\nDa feedback específico sobre cada paso de CNV: ¿separó observación de juicio? ¿expresó sentimiento sin culpar? ¿identificó la necesidad? ¿hizo petición concreta?",
            GuionAudio = "Vamos a practicar CNV en una situación real y difícil. Voy a ser Diego, tu colega que se atribuye el mérito de tu trabajo. Tu reto: confrontar la situación sin atacar, usando los 4 pasos.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 35
        };

        Leccion l1010 = new Leccion
        {
            Id = 1010, NivelId = 2, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Diagnóstico situacional",
            Descripcion = "Evalúa tu comprensión de los conceptos de escucha activa y Comunicación No Violenta.",
            PuntosRecompensa = 15, Orden = 4, DuracionMinutos = 15
        };

        Leccion l1011 = new Leccion
        {
            Id = 1011, NivelId = 2, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Dar feedback negativo a tu mejor empleado",
            Descripcion = "Tu mejor empleada ha bajado su rendimiento. Decide cómo abordar la conversación.",
            PuntosRecompensa = 20, Orden = 5, DuracionMinutos = 30
        };

        Leccion l1012 = new Leccion
        {
            Id = 1012, NivelId = 2, TipoLeccion = TipoLeccion.Reflexion,
            Titulo = "Patrones que repites sin darte cuenta",
            Descripcion = "Identifica tus patrones automáticos de comunicación y cómo te limitan.",
            Contenido = "Guía al usuario a identificar sus patrones:\n1. '¿Qué notaste sobre ti en la práctica con Diego? ¿Hubo un momento donde quisiste atacar en vez de usar CNV?'\n2. '¿Reconoces ese impulso en tu vida real? ¿En qué situaciones sueles reaccionar así?'\n3. '¿Hay algún patrón? Por ejemplo: ¿evitas el conflicto, explotas, te cierras, buscas aliados?'\n4. '¿De dónde crees que viene ese patrón? No necesitas ir a tu infancia, piensa en experiencias laborales.'\n5. '¿Qué te costaría cambiar ese patrón? ¿Qué ganarías?'\n6. 'Si pudieras reprogramar UNA respuesta automática, ¿cuál sería?'",
            GuionAudio = "Ahora vamos a hacer algo poderoso: identificar los patrones automáticos que repites cuando te comunicas. Todos los tenemos, y la mayoría no somos conscientes de ellos.",
            PuntosRecompensa = 15, Orden = 6, DuracionMinutos = 25
        };

        Leccion l1013 = new Leccion
        {
            Id = 1013, NivelId = 2, TipoLeccion = TipoLeccion.PlanAccion,
            Titulo = "Conversación pendiente que has evitado",
            Descripcion = "Identifica una conversación que has estado posponiendo y crea un plan para tenerla esta semana.",
            Contenido = "Guía al usuario a preparar una conversación real pendiente:\n1. '¿Hay alguna conversación que has estado evitando? Con un jefe, colega, cliente, empleado...'\n2. '¿Qué es lo peor que podría pasar si la tienes? ¿Y lo mejor?'\n3. 'Vamos a prepararla con CNV: ¿cuál es tu observación sin juicio?'\n4. '¿Qué sentimiento quieres expresar?'\n5. '¿Cuál es tu necesidad real detrás de este tema?'\n6. '¿Qué petición concreta harás?'\n7. '¿Cuándo exactamente la tendrás? Dame día, hora y lugar.'\n8. 'Después de tenerla, vuelve aquí y cuéntame cómo fue.'\nSi el usuario no identifica una conversación, sugiere opciones comunes.",
            GuionAudio = "Todos tenemos alguna conversación pendiente que hemos estado posponiendo. Hoy vamos a dejar de evitarla y prepararnos para tenerla con las herramientas que ya tienes.",
            PuntosRecompensa = 15, Orden = 7, DuracionMinutos = 15
        };

        // ================================================================
        // NIVEL 3 — APLICAR (NivelId=3, 7 lecciones, IDs 1014-1020)
        // ================================================================

        Leccion l1014 = new Leccion
        {
            Id = 1014, NivelId = 3, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Comunicación bajo presión: cuando todo importa",
            Descripcion = "Frameworks para comunicar con claridad y calma cuando la presión es máxima.",
            Contenido = "## Tu cerebro bajo presión\n\nCuando el estrés sube, la amígdala secuestra al córtex prefrontal. Pierdes acceso al pensamiento estratégico y operas en modo lucha-huida. Tu comunicación se vuelve reactiva.\n\n## El framework STOP\n\n### S — Stop (Para)\nAntes de responder, para. Literalmente. Cuenta hasta 3. Este micro-pausa le devuelve el control al córtex prefrontal.\n\n### T — Think (Piensa)\n¿Cuál es mi objetivo en esta conversación? No qué quiero decir, sino qué resultado necesito.\n\n### O — Organize (Organiza)\nEstructura tu mensaje: Conclusión → Razón → Evidencia → Petición.\n\n### P — Proceed (Procede)\nHabla despacio, usa frases cortas, mantén contacto visual.\n\n## Técnicas para situaciones de alta presión\n\n### Reconocer antes de resolver\n«Entiendo que esto es urgente y preocupante» antes de proponer soluciones.\n\n### Separar hechos de interpretaciones\n«Lo que sabemos es X. Lo que aún no sabemos es Y.»\n\n### El poder del 'todavía no'\n«No tengo la respuesta todavía, pero esto es lo que estoy haciendo para conseguirla.»\n\n## Las 3 preguntas del comunicador bajo presión\n1. ¿Qué necesita saber mi audiencia AHORA?\n2. ¿Qué puede esperar?\n3. ¿Cuál es el siguiente paso concreto?",
            PuntosClave = "[\"La amígdala secuestra al córtex prefrontal bajo estrés\",\"Framework STOP: Stop, Think, Organize, Proceed\",\"Reconocer la situación antes de proponer soluciones\",\"Separar hechos de interpretaciones reduce la ansiedad colectiva\",\"3 preguntas: qué necesitan ahora, qué puede esperar, siguiente paso\"]",
            GuionAudio = "Cuando todo sale mal y la presión es máxima, tu comunicación define si la situación mejora o empeora. Hoy aprenderás frameworks probados para mantener la claridad cuando tu cerebro quiere entrar en pánico.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 40
        };

        Leccion l1015 = new Leccion
        {
            Id = 1015, NivelId = 3, TipoLeccion = TipoLeccion.CasoEstudio,
            Titulo = "La crisis de United Airlines (2017)",
            Descripcion = "Analiza la peor crisis de comunicación corporativa de la década y qué lecciones deja.",
            Contenido = "Presenta el caso de United Airlines vuelo 3411 (abril 2017):\n\nFASE 1: El incidente — Un pasajero (Dr. David Dao) fue arrastrado por la fuerza fuera de un avión con overbooking. Videos virales mostraron sangre y gritos.\n\nPregunta: Si fueras el CEO, ¿qué comunicarías en las primeras 2 horas?\n\nFASE 2: La respuesta de Oscar Munoz (CEO) — Primer comunicado: 'Pido disculpas por tener que re-acomodar a estos clientes.' Usó 're-acomodar' en vez de reconocer la violencia. Segundo comunicado interno: llamó al pasajero 'disruptivo y beligerante.'\n\nPregunta: ¿Qué errores de comunicación detectas?\n\nFASE 3: Las consecuencias — $1.4 mil millones perdidos en valor bursátil en 24 horas. Crisis global de imagen.\n\nFASE 4: Lecciones — (1) Nunca minimices, (2) lidera con empatía antes que con legalidad, (3) tu comunicado interno SE FILTRARÁ, (4) la velocidad importa pero la empatía más. Conecta con el framework STOP.",
            PuntosClave = "[\"Minimizar un incidente grave amplifica la crisis\",\"El primer comunicado define la narrativa\",\"Los comunicados internos siempre se filtran\",\"La empatía debe preceder a la posición legal\",\"United perdió $1.4B por comunicar mal en 24 horas\"]",
            GuionAudio = "Abril de 2017. Un pasajero arrastrado fuera de un avión. Videos virales. Y un CEO que respondió con la peor comunicación de crisis de la década. Vamos a analizarlo.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 30
        };

        Leccion l1016 = new Leccion
        {
            Id = 1016, NivelId = 3, TipoLeccion = TipoLeccion.PracticaGuiada,
            Titulo = "El jefe que no quiere escuchar",
            Descripcion = "Practica comunicación ascendente con un jefe impaciente y orientado a resultados.",
            Contenido = "Actúa como Roberto, un director de operaciones con 20 años de experiencia. Características:\n- Interrumpe constantemente\n- Quiere ir al grano: 'Dime qué necesitas en 30 segundos'\n- No le interesan los detalles, solo resultados e impacto económico\n- Si le presentas un problema sin solución, se irrita\n\nEl usuario debe comunicar que el proyecto actual va 3 semanas retrasado y necesita más recursos.\n\nNivel 1: Roberto escucha si el usuario lidera con la conclusión.\nNivel 2: Roberto interrumpe y pregunta '¿Y eso cuánto nos cuesta?' — el usuario debe tener datos.\nNivel 3: Roberto dice 'No hay presupuesto, resuélvelo con lo que tienes' — el usuario debe negociar.\n\nFeedback: evalúa estructura del mensaje, manejo de interrupciones, capacidad de adaptar el estilo al interlocutor.",
            GuionAudio = "Ahora vamos a practicar una de las habilidades más difíciles: comunicar malas noticias a un jefe que no quiere escucharlas. Voy a ser Roberto, tu director. Tienes 30 segundos para captar mi atención.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 40
        };

        Leccion l1017 = new Leccion
        {
            Id = 1017, NivelId = 3, TipoLeccion = TipoLeccion.PracticaGuiada,
            Titulo = "El cliente furioso que tiene razón",
            Descripcion = "Gestiona la conversación con un cliente legítimamente enfadado sin perderlo ni prometer imposibles.",
            Contenido = "Actúa como Elena, directora de compras de una empresa cliente. La última entrega llegó con 2 semanas de retraso y 3 errores críticos. Elena tiene razón al estar enfadada.\n\nComportamiento de Elena:\n- Empieza muy enfadada: 'Esto es inaceptable. Llevamos 6 meses con vosotros y cada entrega es peor.'\n- Si el usuario se disculpa sin plan, sube la presión: 'Disculpas ya he oído muchas.'\n- Si el usuario da excusas, explota: 'No me importan vuestros problemas internos.'\n- Solo se calma si el usuario: (1) valida su frustración, (2) asume responsabilidad, (3) presenta plan concreto con fechas.\n\nNivel 1: Elena se calma con validación emocional + plan.\nNivel 2: Elena pide compensación económica. El usuario debe negociar sin prometer imposibles.\nNivel 3: Elena amenaza con cambiar de proveedor. El usuario debe mantener la relación.\n\nFeedback: evalúa manejo emocional, capacidad de no tomar los ataques como personales, estructuración de plan de acción.",
            GuionAudio = "Esta práctica es especialmente desafiante. Voy a ser Elena, una clienta furiosa. Y lo más difícil: tiene toda la razón para estarlo. Tu reto es gestionar su enfado sin perderla como clienta.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 35
        };

        Leccion l1018 = new Leccion
        {
            Id = 1018, NivelId = 3, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Reunión de crisis: 3 decisiones en cadena",
            Descripcion = "Tu servidor principal cayó, el CEO quiere respuestas y los clientes están llamando. Decide.",
            PuntosRecompensa = 20, Orden = 5, DuracionMinutos = 30
        };

        Leccion l1019 = new Leccion
        {
            Id = 1019, NivelId = 3, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Evaluación de competencia comunicativa",
            Descripcion = "Evalúa tu dominio de los frameworks de comunicación aprendidos hasta ahora.",
            PuntosRecompensa = 15, Orden = 6, DuracionMinutos = 20
        };

        Leccion l1020 = new Leccion
        {
            Id = 1020, NivelId = 3, TipoLeccion = TipoLeccion.Reflexion,
            Titulo = "Tu estilo bajo presión",
            Descripcion = "Reflexiona sobre cómo cambió tu comunicación en las prácticas de alta presión.",
            Contenido = "Combina reflexión (Gibbs) con plan de acción:\n1. '¿Qué práctica te resultó más difícil: el jefe impaciente o la clienta furiosa? ¿Por qué?'\n2. '¿En cuál sentiste que perdiste el control del mensaje? ¿Qué disparó esa pérdida?'\n3. '¿Descubriste algún patrón nuevo sobre cómo reaccionas bajo presión?'\n4. '¿Qué framework (STOP, CNV, estructura conclusión-primero) te resultó más natural?'\n5. '¿Qué situación real de tu trabajo se parece más a estas prácticas?'\n6. PLAN: 'Identifica UNA situación de presión que vivirás esta semana. ¿Qué framework usarás? ¿Cuándo? ¿Con quién?'\nAsegúrate de que el plan sea específico y realista.",
            GuionAudio = "Has pasado por dos prácticas intensas bajo presión. Ahora es momento de reflexionar y crear un plan de acción. Quiero que identifiques qué descubriste sobre ti mismo bajo estrés.",
            PuntosRecompensa = 15, Orden = 7, DuracionMinutos = 25
        };

        // ================================================================
        // NIVEL 4 — INTEGRAR (NivelId=37, 6 lecciones, IDs 1021-1026)
        // ================================================================

        Leccion l1021 = new Leccion
        {
            Id = 1021, NivelId = 37, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Comunicación estratégica e influencia",
            Descripcion = "Domina el arte de comunicar para influir en decisiones organizacionales de alto impacto.",
            Contenido = "## De comunicar información a comunicar influencia\n\nEn los niveles anteriores aprendiste a escuchar y expresarte. Ahora vas a aprender a mover organizaciones con tu comunicación.\n\n## El modelo de Influencia de Cialdini aplicado a la comunicación\n\n### Reciprocidad\nDa antes de pedir. Comparte información valiosa, ofrece ayuda, reconoce el trabajo de otros. Cuando necesites apoyo, lo tendrás.\n\n### Prueba social\nMuestra que otros ya apoyan tu propuesta. «El equipo de producto ya validó esto» tiene más peso que datos solos.\n\n### Autoridad\nCita fuentes, datos y experiencias relevantes. No es presumir, es dar contexto de credibilidad.\n\n### Consistencia\nConecta tu propuesta con compromisos previos de la audiencia. «Esto es consistente con la prioridad Q1 que acordamos.»\n\n## La pirámide de Minto para comunicación ejecutiva\n\nEstructura: Respuesta → Argumentos → Evidencia.\n- Empieza con la conclusión en 30 segundos\n- Da 2-3 razones que la soporten\n- Ten evidencia de backup por si preguntan\n\n## Mapeo de stakeholders\n\nAntes de comunicar una decisión importante:\n1. ¿Quién decide? ¿Quién influye? ¿Quién se ve afectado?\n2. ¿Qué le importa a cada uno? (no lo que tú crees, sino lo que realmente les importa)\n3. ¿Cuál es el mensaje correcto para cada persona?",
            PuntosClave = "[\"Comunicar para influir requiere entender qué mueve a tu audiencia\",\"Los 6 principios de Cialdini se aplican a comunicación organizacional\",\"Pirámide de Minto: conclusión primero, razones después, evidencia como backup\",\"El mapeo de stakeholders previene sorpresas y resistencias\",\"Da antes de pedir: la reciprocidad es la base de la influencia ética\"]",
            GuionAudio = "En este nivel, ya no solo comunicas bien: influyes en decisiones que impactan a toda la organización. Hoy aprenderás los frameworks que usan los comunicadores más efectivos del mundo corporativo.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 40
        };

        Leccion l1022 = new Leccion
        {
            Id = 1022, NivelId = 37, TipoLeccion = TipoLeccion.CasoEstudio,
            Titulo = "Negociación internacional que salvó una empresa",
            Descripcion = "Analiza cómo la comunicación intercultural salvó una alianza estratégica al borde del fracaso.",
            Contenido = "Presenta este caso:\n\nFASE 1: Una empresa española de energía renovable negocia una joint venture con una compañía japonesa. Después de 6 meses de negociación, el equipo español cree que el acuerdo está cerrado. Envían el contrato final.\n\nFASE 2: La respuesta japonesa es 'lo revisaremos con cuidado' (tatemae — lo que se dice en público). El equipo español interpreta esto como positivo. 3 semanas de silencio.\n\nPregunta: ¿Qué crees que está pasando?\n\nFASE 3: Resulta que 'lo revisaremos' significaba que tenían objeciones serias pero no las expresaron directamente (honne — lo que realmente piensan). La directora de desarrollo de negocio viajó a Tokio, pidió una reunión informal (nomikai), y escuchó en nivel 3-4. Descubrió 3 preocupaciones no dichas.\n\nFASE 4: Lecciones — (1) La comunicación directa española choca con la indirecta japonesa, (2) el 'sí' no siempre significa sí en todas las culturas, (3) escuchar lo no dicho es más importante que escuchar lo dicho. Conecta con niveles de escucha de Scharmer.",
            PuntosClave = "[\"En comunicación intercultural, el 'sí' puede significar cosas diferentes\",\"Tatemae vs honne: lo que se dice en público vs lo que realmente se piensa\",\"Los contextos informales revelan lo que los formales ocultan\",\"Escuchar lo no dicho es la habilidad más avanzada de comunicación\"]",
            GuionAudio = "Hoy analizaremos un caso donde una alianza de 50 millones de euros casi se pierde por diferencias culturales en comunicación. La solución no fue hablar más, sino escuchar lo que no se decía.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 30
        };

        Leccion l1023 = new Leccion
        {
            Id = 1023, NivelId = 37, TipoLeccion = TipoLeccion.PracticaGuiada,
            Titulo = "Mediar entre dos equipos en conflicto",
            Descripcion = "Actúa como mediador entre dos equipos que se culpan mutuamente de un fracaso de proyecto.",
            Contenido = "Escenario: El equipo de desarrollo y el equipo de diseño llevan 3 semanas culpándose mutuamente del retraso de un lanzamiento. El usuario debe mediar.\n\nAria alterna entre dos personajes:\n- Marcos (dev lead): 'Diseño nos entrega mockups imposibles de implementar y cambia todo a última hora.'\n- Sara (design lead): 'Desarrollo ignora nuestras especificaciones y entrega cosas que no se parecen al diseño.'\n\nNivel 1: El usuario escucha a ambos lados y parafrasea.\nNivel 2: El usuario identifica necesidades comunes (ambos quieren entregar calidad).\nNivel 3: El usuario propone un proceso de colaboración que satisfaga a ambos.\n\nImportante: Si el usuario toma partido por uno de los dos, el otro se cierra. Debe mantenerse neutral.\n\nFeedback: evalúa neutralidad, capacidad de encontrar terreno común, calidad de la propuesta de proceso.",
            GuionAudio = "Bienvenido a la práctica más compleja hasta ahora: mediar entre dos equipos en conflicto. Voy a interpretar a ambos líderes. Tu trabajo es encontrar terreno común sin tomar partido.",
            PuntosRecompensa = 30, Orden = 3, DuracionMinutos = 45
        };

        Leccion l1024 = new Leccion
        {
            Id = 1024, NivelId = 37, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Reestructuración: comunicar malas noticias",
            Descripcion = "La empresa necesita reducir un 20% la plantilla. Decide cómo comunicarlo al equipo.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 35
        };

        Leccion l1025 = new Leccion
        {
            Id = 1025, NivelId = 37, TipoLeccion = TipoLeccion.PracticaGuiada,
            Titulo = "Persuadir al comité directivo",
            Descripcion = "Presenta una propuesta de inversión al comité directivo con estilos muy diferentes.",
            Contenido = "Escenario: El usuario debe presentar una propuesta de inversión de 500K€ en formación de soft skills (meta!) a un comité de 3 directivos.\n\nAria alterna entre 3 personajes:\n- CEO (visionario): Le importa la cultura y el largo plazo. Responde bien a historias y visión.\n- CFO (analítico): Solo quiere ROI, números y comparativas de mercado.\n- COO (pragmático): Quiere saber quién lo ejecuta, cuándo, y qué pasa si falla.\n\nEl usuario presenta su propuesta y debe adaptar el mensaje a cada interlocutor:\n- Si es demasiado emocional, pierde al CFO\n- Si es solo números, pierde al CEO\n- Si no tiene plan de ejecución, pierde al COO\n\nFeedback: evalúa adaptabilidad comunicativa, uso de datos y narrativa, manejo de objeciones.",
            GuionAudio = "Esta es la práctica de nivel avanzado: persuadir a un comité directivo donde cada persona piensa diferente. Vas a presentar una propuesta de 500K euros. Cada directivo necesita un mensaje diferente.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 40
        };

        Leccion l1026 = new Leccion
        {
            Id = 1026, NivelId = 37, TipoLeccion = TipoLeccion.Reflexion,
            Titulo = "Tu mapa de comunicación actual",
            Descripcion = "Crea un mapa completo de tu comunicación profesional: fortalezas, puntos ciegos y plan de desarrollo.",
            Contenido = "Guía al usuario a crear su mapa de comunicación personal:\n1. 'Haz una lista de tus 5 interacciones comunicativas más frecuentes en el trabajo (reuniones, 1:1, emails, presentaciones, etc.)'\n2. 'Para cada una, ¿en qué nivel de Scharmer sueles operar?'\n3. '¿En cuál de las 5 tu comunicación tiene más impacto? ¿Y en cuál más fricción?'\n4. '¿Qué framework de los aprendidos (CNV, STOP, Minto, Cialdini) aplicarías a cada situación?'\n5. 'Si pudieras mejorar UNA interacción, ¿cuál tendría más efecto cascada en tu trabajo?'\n6. PLAN DE DESARROLLO: 'Define 3 compromisos para las próximas 2 semanas: uno fácil, uno medio, uno difícil.'\nAyuda a que los compromisos sean SMART.",
            GuionAudio = "Has recorrido un camino increíble. Ahora vamos a crear tu mapa de comunicación personal: una foto completa de dónde estás y hacia dónde quieres ir.",
            PuntosRecompensa = 15, Orden = 6, DuracionMinutos = 25
        };

        // ================================================================
        // NIVEL 5 — DOMINAR (NivelId=38, 6 lecciones, IDs 1027-1032)
        // ================================================================

        Leccion l1027 = new Leccion
        {
            Id = 1027, NivelId = 38, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Comunicación de liderazgo transformacional",
            Descripcion = "El nivel más alto: comunicar para transformar culturas, inspirar movimientos y dejar legado.",
            Contenido = "## De comunicar bien a comunicar para transformar\n\nLos líderes transformacionales no solo transmiten información. Crean significado, inspiran acción y transforman culturas.\n\n## Los 4 actos comunicativos del líder transformacional\n\n### 1. Enmarcar (Framing)\nDefinir cómo la organización interpreta la realidad. No es lo que pasa, sino qué significa.\n- Crisis = amenaza o crisis = oportunidad. El líder elige el marco.\n- «No estamos perdiendo cuota de mercado. Estamos descubriendo qué necesita realmente el cliente.»\n\n### 2. Narrar (Storytelling estratégico)\nLas historias crean identidad compartida. Los mejores líderes son narradores.\n- Historia de origen: de dónde venimos\n- Historia de valores: qué nos importa\n- Historia de futuro: hacia dónde vamos\n\n### 3. Dialogar (Escucha generativa)\nCrear espacios donde emerjan ideas que nadie tenía antes. Nivel 4 de Scharmer a escala organizacional.\n- Town halls con preguntas reales, no guionadas\n- Sesiones de 'qué no funciona' sin represalias\n\n### 4. Modelar (Comunicación congruente)\nTus acciones comunican más que tus palabras. Cada decisión es un mensaje.\n- Si dices 'innovación' pero castigas errores, el mensaje real es 'no arriesgues'.\n\n## El caso Nadella\nCuando Satya Nadella llegó a Microsoft en 2014, la cultura era de 'sabelotodos'. Su primer mensaje: «Pasamos de know-it-all a learn-it-all.» Una frase que reenmarcó toda la cultura.",
            PuntosClave = "[\"Los líderes transformacionales crean significado, no solo transmiten información\",\"4 actos: Enmarcar, Narrar, Dialogar y Modelar\",\"El framing define cómo la organización interpreta la realidad\",\"Las acciones comunican más que las palabras\",\"Nadella transformó Microsoft con una frase: de know-it-all a learn-it-all\"]",
            GuionAudio = "Bienvenido al nivel más alto. Aquí ya no hablamos de comunicar bien, sino de comunicar para transformar culturas enteras. Los líderes que cambian organizaciones dominan 4 actos comunicativos.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 35
        };

        Leccion l1028 = new Leccion
        {
            Id = 1028, NivelId = 38, TipoLeccion = TipoLeccion.CasoEstudio,
            Titulo = "Cómo Satya Nadella transformó Microsoft con empatía",
            Descripcion = "Analiza la transformación cultural de Microsoft y el rol de la comunicación empática de Nadella.",
            Contenido = "Presenta el caso de la transformación de Microsoft:\n\nFASE 1: Microsoft 2014 — Cultura de 'stack ranking' donde empleados competían entre sí. Silos entre divisiones. Nokia fue un fracaso de $7.6B. Ballmer gritaba en el escenario.\n\nPregunta: Si fueras el nuevo CEO, ¿cuál sería tu primer mensaje?\n\nFASE 2: El primer email de Nadella a toda la empresa. No habló de estrategia ni productos. Habló de 'learn-it-all vs know-it-all' y de empatía. Citó a Carol Dweck (growth mindset). Muchos directivos veteranos pensaron que era débil.\n\nPregunta: ¿Por qué crees que eligió empatía como primer mensaje en vez de estrategia de negocio?\n\nFASE 3: Los resultados — Microsoft pasó de $300B a $2.5T en capitalización. Satya eliminó stack ranking, creó 'One Microsoft', y convirtió Azure en el motor de crecimiento. Todo empezó con un cambio de narrativa.\n\nFASE 4: Lecciones — (1) La cultura come a la estrategia para desayunar, (2) reenmarcalar la identidad antes de los procesos, (3) la vulnerabilidad del líder da permiso a los demás para aprender, (4) la empatía no es debilidad, es inteligencia estratégica.",
            PuntosClave = "[\"Nadella transformó Microsoft cambiando la narrativa antes que la estrategia\",\"De know-it-all a learn-it-all: un reframing que cambió la cultura\",\"Eliminar stack ranking fue un mensaje comunicativo, no solo de RRHH\",\"La empatía como herramienta estratégica, no como debilidad\"]",
            GuionAudio = "En 2014, Microsoft era un gigante en declive. Un nuevo CEO llegó y en lugar de anunciar productos o recortes, habló de empatía. Todos pensaron que estaba loco. 10 años después, Microsoft vale 8 veces más.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 30
        };

        Leccion l1029 = new Leccion
        {
            Id = 1029, NivelId = 38, TipoLeccion = TipoLeccion.PracticaGuiada,
            Titulo = "Audiencia hostil: convéncelos",
            Descripcion = "Presenta un cambio impopular a un grupo que se opone activamente.",
            Contenido = "Escenario: El usuario debe comunicar al equipo de 50 personas que la empresa migra de oficina presencial a modelo híbrido 3-2. El 70% del equipo prefiere full remoto.\n\nAria actúa como portavoz del grupo opositor:\n- 'Llevamos 2 años rindiendo perfecto en remoto. ¿Por qué arreglar lo que no está roto?'\n- 'Esto es porque la dirección no confía en nosotros.'\n- 'Si imponen esto, perderán a los mejores.'\n\nEl usuario debe:\n1. Reconocer la frustración legítima\n2. Reenmarcalar el cambio (no es desconfianza, es X)\n3. Mostrar que escuchó las necesidades\n4. Ofrecer participación en los detalles (no en la decisión, que ya está tomada)\n5. Ser honesto sobre lo que NO es negociable\n\nSi el usuario miente o manipula, Aria lo detecta y confronta. Si es transparente aunque impopular, el grupo respeta.\n\nFeedback: evalúa autenticidad, manejo de hostilidad grupal, capacidad de mantener firmeza con empatía.",
            GuionAudio = "La práctica definitiva de comunicación: convencer a un grupo que se opone activamente a tu mensaje. No puedes cambiar la decisión, pero puedes cambiar cómo se recibe. Voy a representar a la audiencia hostil.",
            PuntosRecompensa = 30, Orden = 3, DuracionMinutos = 45
        };

        Leccion l1030 = new Leccion
        {
            Id = 1030, NivelId = 38, TipoLeccion = TipoLeccion.PracticaGuiada,
            Titulo = "Mentorear a un comunicador novato",
            Descripcion = "La prueba definitiva: enseña a otro lo que aprendiste. Aria actúa como un junior que necesita tu guía.",
            Contenido = "Aria actúa como Lucía, una analista junior de 24 años en su primer trabajo corporativo. Lucía es inteligente pero:\n- Habla muy rápido y llena silencios\n- Usa jerga técnica que su audiencia no entiende\n- Evita el conflicto a toda costa\n- Se disculpa demasiado: 'Perdona, quizás estoy equivocada, pero...'\n- Tiene una presentación importante al director la próxima semana\n\nEl usuario debe actuar como mentor:\n1. Diagnosticar los problemas de Lucía (sin ser condescendiente)\n2. Enseñarle 1-2 técnicas prácticas (no toda la teoría)\n3. Hacer un roleplay donde Lucía practica y el usuario da feedback\n4. Ayudarla a preparar su presentación\n\nEsta lección evalúa si el usuario puede ENSEÑAR lo aprendido, que es el nivel más alto de dominio.\n\nFeedback: evalúa calidad pedagógica, empatía con la novata, priorización de qué enseñar primero.",
            GuionAudio = "Dicen que la mejor forma de demostrar dominio es enseñar. Hoy vas a ser el mentor. Voy a ser Lucía, una analista junior brillante pero que necesita ayuda urgente con su comunicación.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 35
        };

        Leccion l1031 = new Leccion
        {
            Id = 1031, NivelId = 38, TipoLeccion = TipoLeccion.Capstone,
            Titulo = "Diseña el plan de comunicación de tu equipo",
            Descripcion = "Proyecto integrador: diseña un plan de comunicación real para tu equipo o empresa.",
            Contenido = "Guía al usuario paso a paso para diseñar un plan de comunicación real:\n\nPASO 1 — Diagnóstico: '¿Cuáles son los 3 mayores problemas de comunicación en tu equipo/empresa hoy?'\nPASO 2 — Stakeholders: '¿Quiénes son los actores clave? ¿Qué necesita cada uno?'\nPASO 3 — Canales: '¿Qué canales usáis? ¿Son los correctos para cada tipo de mensaje?'\nPASO 4 — Rituales: 'Propón 3 rituales de comunicación: uno diario, uno semanal, uno mensual.'\nPASO 5 — Cultura: '¿Qué normas de comunicación quieres establecer? (ej: no feedback por email, 1:1 semanales obligatorios)'\nPASO 6 — Métricas: '¿Cómo sabrás que la comunicación mejoró? Define 2-3 indicadores.'\nPASO 7 — Implementación: 'Timeline de 90 días: qué haces la semana 1, el mes 1, el mes 3.'\n\nEvalúa con rúbrica: Profundidad (¿entiende el problema real?), Viabilidad (¿se puede implementar?), Creatividad (¿hay ideas originales?), Impacto (¿esto cambiaría algo de verdad?).\n\nNo des respuestas. Solo haz preguntas que obliguen a pensar más profundo.",
            GuionAudio = "Este es tu proyecto final. Todo lo que has aprendido en las últimas 25 horas converge aquí. Vas a diseñar un plan de comunicación real para tu equipo. Yo te guío con preguntas, pero las respuestas son tuyas.",
            PuntosRecompensa = 50, Orden = 5, DuracionMinutos = 60
        };

        Leccion l1032 = new Leccion
        {
            Id = 1032, NivelId = 38, TipoLeccion = TipoLeccion.Reflexion,
            Titulo = "Tu transformación: de dónde partiste y dónde estás",
            Descripcion = "Reflexión final sobre tu viaje de aprendizaje y compromiso de desarrollo continuo.",
            Contenido = "Guía la reflexión final del curso completo:\n1. 'Vuelve mentalmente a tu primera sesión, la autoevaluación. ¿Qué respondiste sobre cómo comunicabas? ¿Ha cambiado tu perspectiva?'\n2. '¿Cuál fue el momento más revelador de todo el curso? ¿Qué descubriste que no sabías?'\n3. '¿Qué práctica fue la más difícil? ¿Por qué?'\n4. '¿Qué has aplicado ya en tu vida real? ¿Qué resultado tuvo?'\n5. '¿Si tuvieras que elegir UNA habilidad que llevarte de este curso, cuál sería?'\n6. '¿Qué le dirías al tú de hace 25 horas, el que empezó este curso?'\n7. COMPROMISO FINAL: 'Define un compromiso de desarrollo continuo. No para esta semana, sino para los próximos 3 meses. ¿Cómo seguirás practicando?'\n\nTermina con un mensaje motivacional personalizado basado en lo que el usuario compartió durante el curso.",
            GuionAudio = "Llegamos al final de un viaje increíble. 25 horas, 32 lecciones, casos reales, prácticas intensas. Antes de cerrar, quiero que miremos atrás y veamos cuánto has crecido.",
            PuntosRecompensa = 20, Orden = 6, DuracionMinutos = 30
        };

        // Guardar todas las lecciones
        contexto.Set<Leccion>().AddRange(
            l1001, l1002, l1003, l1004, l1005, l1006,
            l1007, l1008, l1009, l1010, l1011, l1012, l1013,
            l1014, l1015, l1016, l1017, l1018, l1019, l1020,
            l1021, l1022, l1023, l1024, l1025, l1026,
            l1027, l1028, l1029, l1030, l1031, l1032
        );
        await contexto.SaveChangesAsync();

        // ================================================================
        // ESCENAS para lecciones de Teoría
        // ================================================================

        await SembrarEscenasDesdeLeccionAsync(contexto, l1002,
            "Hoy vas a descubrir los 4 niveles de escucha de Otto Scharmer. La mayoría nos quedamos atrapados en los dos primeros sin darnos cuenta.",
            "Esta semana, después de cada conversación importante, pregúntate: ¿en qué nivel estuve? Solo con esa consciencia, empezarás a mejorar.");

        await SembrarEscenasDesdeLeccionAsync(contexto, l1007,
            "Marshall Rosenberg cambió cómo millones de personas se comunican. Su método tiene solo 4 pasos, pero dominarlos cambia completamente tus relaciones profesionales.",
            "Practica la fórmula completa esta semana: Cuando [observación], me siento [sentimiento] porque necesito [necesidad]. ¿Estarías dispuesto a [petición]?");

        await SembrarEscenasDesdeLeccionAsync(contexto, l1014,
            "Cuando la presión es máxima, tu cerebro entra en modo lucha-huida y tu comunicación se vuelve reactiva. Hoy aprenderás a mantener la claridad bajo estrés.",
            "Antes de tu próxima reunión difícil, recuerda STOP: Para, Piensa cuál es tu objetivo, Organiza tu mensaje, y Procede despacio.");

        await SembrarEscenasDesdeLeccionAsync(contexto, l1021,
            "Ya comunicas bien. Ahora vas a aprender a influir en decisiones que impactan a toda la organización. Esto requiere otro nivel de estrategia comunicativa.",
            "Antes de tu próxima propuesta importante, mapea a tus stakeholders: quién decide, quién influye, qué le importa a cada uno.");

        await SembrarEscenasDesdeLeccionAsync(contexto, l1027,
            "Los líderes transformacionales no solo transmiten información: crean significado e inspiran movimientos. Hoy aprenderás los 4 actos comunicativos del liderazgo transformacional.",
            "Reflexiona: ¿qué narrativa tiene tu equipo sobre sí mismo? ¿Es la narrativa que quieres? Si no, tienes el poder de cambiarla.");

        // ================================================================
        // QUIZZES
        // ================================================================

        // Quiz Nivel 2 — Diagnóstico situacional (leccionId=1010)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(1010, 1, "¿Cuál es el primer paso de la Comunicación No Violenta de Rosenberg?",
                "El primer paso es la Observación sin juicio: describir hechos concretos.",
                new[] {
                    ("Expresar tus sentimientos", false, "Los sentimientos son el segundo paso"),
                    ("Observación sin juicio", true, "¡Correcto! Separar hechos de interpretaciones es la base de la CNV"),
                    ("Hacer una petición concreta", false, "La petición es el último paso"),
                    ("Identificar tu necesidad", false, "La necesidad es el tercer paso")
                }),
            CrearPregunta(1010, 2, "Según Scharmer, ¿en qué nivel de escucha operas cuando solo confirmas lo que ya crees?",
                "El Nivel 1 (Descarga) filtra todo por opiniones existentes.",
                new[] {
                    ("Nivel 1: Descarga", true, "¡Exacto! En este nivel solo escuchas para confirmar tus creencias"),
                    ("Nivel 2: Factual", false, "El nivel factual presta atención a datos nuevos"),
                    ("Nivel 3: Empático", false, "El empático implica ponerse en el lugar del otro"),
                    ("Nivel 4: Generativo", false, "El generativo crea algo nuevo en la conversación")
                }),
            CrearPregunta(1010, 3, "¿Cuál de estas frases es una observación sin juicio?",
                "Una observación describe hechos medibles sin evaluación ni generalización.",
                new[] {
                    ("Siempre llegas tarde a todo", false, "'Siempre' y 'a todo' son generalizaciones que incluyen juicio"),
                    ("Eres irresponsable con los plazos", false, "'Irresponsable' es un juicio sobre la persona"),
                    ("En las últimas 3 reuniones llegaste 10 minutos tarde", true, "¡Correcto! Hechos concretos, medibles, sin evaluación"),
                    ("No te importan las reuniones del equipo", false, "Esto es una interpretación de las intenciones del otro")
                }),
            CrearPregunta(1010, 4, "¿Qué ratio de feedback positivo vs constructivo recomienda Losada?",
                "El ratio de Losada establece 5 reconocimientos positivos por cada crítica constructiva.",
                new[] {
                    ("1:1 — Un positivo por cada negativo", false, "Este ratio es insuficiente para mantener la motivación"),
                    ("3:1 — Tres positivos por cada negativo", false, "Está cerca pero Losada demostró que se necesitan más"),
                    ("5:1 — Cinco positivos por cada negativo", true, "¡Correcto! 5 reconocimientos por cada crítica constructiva"),
                    ("10:1 — Diez positivos por cada negativo", false, "Esto haría que el feedback constructivo pierda impacto")
                }),
            CrearPregunta(1010, 5, "¿Cuál es la diferencia clave entre escucha empática y generativa?",
                "La empática ve desde los ojos del otro; la generativa crea algo nuevo que no existía.",
                new[] {
                    ("La generativa usa datos y la empática usa emociones", false, "Ambas trascienden los datos puros"),
                    ("La empática siente lo que el otro siente; la generativa crea algo nuevo", true, "¡Exacto! La generativa produce insights que ninguno tenía antes"),
                    ("No hay diferencia práctica, son lo mismo", false, "Son niveles distintos con resultados muy diferentes"),
                    ("La generativa es solo para líderes senior", false, "Cualquiera puede alcanzar el nivel generativo con práctica")
                })
        );

        // Quiz Nivel 3 — Evaluación de competencia comunicativa (leccionId=1019)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(1019, 1, "En el framework STOP, ¿qué significa la O?",
                "O = Organize: estructurar el mensaje antes de hablar.",
                new[] {
                    ("Observe (Observa)", false, "STOP es: Stop, Think, Organize, Proceed"),
                    ("Organize (Organiza)", true, "¡Correcto! Organiza tu mensaje: conclusión, razón, evidencia, petición"),
                    ("Overcome (Supera)", false, "La O de STOP se refiere a Organize"),
                    ("Open (Abre)", false, "STOP no incluye Open")
                }),
            CrearPregunta(1019, 2, "¿Qué debió hacer primero Oscar Munoz (CEO de United) en la crisis del vuelo 3411?",
                "Lo primero en una crisis es reconocer el impacto con empatía, no minimizar.",
                new[] {
                    ("Consultar al departamento legal", false, "Priorizar lo legal sobre lo humano agravó la crisis"),
                    ("Reconocer el impacto con empatía", true, "¡Correcto! La empatía debe preceder a cualquier posición corporativa"),
                    ("Investigar qué pasó exactamente", false, "Importante pero no lo primero que comunicas públicamente"),
                    ("Publicar un comunicado legal neutral", false, "La neutralidad se interpretó como indiferencia")
                }),
            CrearPregunta(1019, 3, "Cuando tu jefe dice '¿Qué necesitas?' y tienes 30 segundos, ¿qué estructura usas?",
                "La pirámide de Minto: conclusión primero, argumentos después.",
                new[] {
                    ("Contexto → Problema → Solución → Petición", false, "Demasiado largo para 30 segundos, el jefe te interrumpirá"),
                    ("Respuesta → Razón principal → Lo que necesitas", true, "¡Correcto! Lidera con la conclusión y luego justifica"),
                    ("Historia personal → Datos → Conclusión", false, "Las historias son para contextos con más tiempo"),
                    ("Datos → Análisis → Recomendación", false, "El enfoque analítico funciona mejor en informes escritos")
                }),
            CrearPregunta(1019, 4, "Un cliente furioso te dice 'Esto es inaceptable'. ¿Cuál es la mejor primera respuesta?",
                "Validar la emoción primero, antes de cualquier explicación o solución.",
                new[] {
                    ("Déjame explicarte qué pasó", false, "Explicar antes de validar suena como excusa"),
                    ("Tienes toda la razón, vamos a solucionarlo", true, "¡Correcto! Valida, asume, luego presenta el plan"),
                    ("No es para tanto, hay solución", false, "Minimizar enfurece más al cliente"),
                    ("Voy a escalar esto a mi manager", false, "Esto transmite que no puedes manejar la situación")
                }),
            CrearPregunta(1019, 5, "¿Cuáles son las 3 preguntas del comunicador bajo presión?",
                "¿Qué necesitan saber AHORA? ¿Qué puede esperar? ¿Cuál es el siguiente paso?",
                new[] {
                    ("¿Quién tiene la culpa? ¿Qué perdimos? ¿Cómo compensamos?", false, "Buscar culpables no es prioritario bajo presión"),
                    ("¿Qué necesitan saber ahora? ¿Qué puede esperar? ¿Siguiente paso?", true, "¡Exacto! Prioriza información, gestiona expectativas, da dirección"),
                    ("¿Qué pasó? ¿Por qué? ¿Cómo evitarlo?", false, "Esas son preguntas de post-mortem, no de comunicación urgente"),
                    ("¿Quién es la audiencia? ¿Qué canal uso? ¿Cuándo comunico?", false, "Son preguntas de planificación, no del momento de presión")
                })
        );

        await contexto.SaveChangesAsync();

        // ================================================================
        // ESCENARIOS
        // ================================================================

        // Escenario: Dar feedback negativo a tu mejor empleado (leccionId=1011)
        Escenario escenario1 = new Escenario
        {
            LeccionId = 1011,
            TextoSituacion = "Sofía es tu mejor empleada desde hace 3 años. Siempre ha superado expectativas. Pero en el último mes, ha llegado tarde a 4 reuniones, entregó 2 informes con errores graves y se muestra distante. No sabes si tiene un problema personal o si simplemente está desmotivada. Hoy tienes tu 1:1 semanal con ella.",
            Contexto = "Oficina privada, reunión 1:1 programada. Sofía acaba de sentarse frente a ti con expresión neutra.",
            GuionAudio = "Esta situación es de las más delicadas: dar feedback constructivo a alguien que siempre ha brillado. Lo que digas en los próximos 5 minutos puede salvar o destruir la relación."
        };
        contexto.Set<Escenario>().Add(escenario1);
        await contexto.SaveChangesAsync();

        contexto.Set<OpcionEscenario>().AddRange(
            new OpcionEscenario
            {
                EscenarioId = escenario1.Id, TextoOpcion = "Ir directo: 'Sofía, tu rendimiento ha bajado. Necesito que vuelvas a tu nivel.'",
                TextoRetroalimentacion = "Ir directo sin empatía pone a Sofía a la defensiva. No descubrirás la causa raíz. Ella se cierra y dice 'lo intentaré' sin convicción. En 2 semanas, la situación empeora.",
                PuntosOtorgados = 5, TipoResultado = TipoResultadoEscenario.Aceptable, Orden = 1
            },
            new OpcionEscenario
            {
                EscenarioId = escenario1.Id, TextoOpcion = "Empezar escuchando: '¿Cómo estás? He notado que las últimas semanas han sido diferentes. ¿Hay algo que quieras compartir?'",
                TextoRetroalimentacion = "¡Excelente! Empezar con escucha empática abre la puerta. Sofía revela que está pasando por un divorcio difícil. Ahora puedes ofrecer flexibilidad temporal manteniendo las expectativas claras. La relación se fortalece.",
                PuntosOtorgados = 20, TipoResultado = TipoResultadoEscenario.Optimo, Orden = 2
            },
            new OpcionEscenario
            {
                EscenarioId = escenario1.Id, TextoOpcion = "No decir nada y esperar que se resuelva solo. Quizás la próxima semana mejore.",
                TextoRetroalimentacion = "Evitar la conversación es la peor opción. Sofía interpreta tu silencio como que no te importa. El rendimiento sigue cayendo. Otros empleados notan el doble estándar y se resienten.",
                PuntosOtorgados = 0, TipoResultado = TipoResultadoEscenario.Inadecuado, Orden = 3
            },
            new OpcionEscenario
            {
                EscenarioId = escenario1.Id, TextoOpcion = "Usar CNV: 'He observado que en las últimas 4 reuniones has llegado tarde y los informes X e Y tenían errores. Me preocupa porque valoro mucho tu trabajo. ¿Podemos hablar de qué está pasando?'",
                TextoRetroalimentacion = "Muy buena opción. Combinas datos concretos con empatía genuina. Sofía aprecia tu honestidad y se abre sobre su situación. Juntos diseñan un plan temporal. Resultado: confianza + plan de acción.",
                PuntosOtorgados = 18, TipoResultado = TipoResultadoEscenario.Optimo, Orden = 4
            }
        );

        // Escenario: Reunión de crisis (leccionId=1018)
        Escenario escenario2 = new Escenario
        {
            LeccionId = 1018,
            TextoSituacion = "Son las 9:00 AM del lunes. El servidor principal de la empresa ha caído hace 30 minutos. Los clientes no pueden acceder al servicio. Tu jefe, el CTO, está de vacaciones y no contesta. El CEO te llama: '¿Qué está pasando y cuándo se arregla?' No tienes toda la información aún.",
            Contexto = "Llamada telefónica con el CEO. El equipo técnico está investigando. Clientes empiezan a escribir en redes sociales.",
            GuionAudio = "Crisis real, presión máxima. El CEO quiere respuestas que todavía no tienes. Los clientes están impacientes. El CTO no contesta. ¿Cómo comunicas cuando no tienes toda la información?"
        };
        contexto.Set<Escenario>().Add(escenario2);
        await contexto.SaveChangesAsync();

        contexto.Set<OpcionEscenario>().AddRange(
            new OpcionEscenario
            {
                EscenarioId = escenario2.Id, TextoOpcion = "Decir: 'Estamos investigando, no sé cuándo se resolverá. Te aviso cuando sepa más.'",
                TextoRetroalimentacion = "Demasiado vago. El CEO necesita más estructura incluso sin certezas. No le das un marco temporal ni acciones concretas. Sube la ansiedad.",
                PuntosOtorgados = 5, TipoResultado = TipoResultadoEscenario.Aceptable, Orden = 1
            },
            new OpcionEscenario
            {
                EscenarioId = escenario2.Id, TextoOpcion = "Usar STOP + las 3 preguntas: 'Lo que sabemos: el servidor cayó a las 8:30, el equipo está diagnosticando. Lo que no sabemos aún: la causa raíz. Siguiente paso: actualización en 30 minutos. Voy a enviar comunicado a clientes diciendo que estamos en ello.'",
                TextoRetroalimentacion = "¡Excelente! Separas hechos de incógnitas, das timeline concreto y propones acción sobre clientes. El CEO se calma porque ve que hay estructura y proactividad.",
                PuntosOtorgados = 20, TipoResultado = TipoResultadoEscenario.Optimo, Orden = 2
            },
            new OpcionEscenario
            {
                EscenarioId = escenario2.Id, TextoOpcion = "Prometer: 'Se resuelve en 1 hora máximo, no te preocupes.'",
                TextoRetroalimentacion = "Sobreprometer cuando no tienes datos es peligroso. Si la hora pasa y no se resuelve, pierdes credibilidad. Nunca prometas lo que no puedes controlar.",
                PuntosOtorgados = 2, TipoResultado = TipoResultadoEscenario.Inadecuado, Orden = 3
            }
        );

        // Escenario: Reestructuración (leccionId=1024)
        Escenario escenario3 = new Escenario
        {
            LeccionId = 1024,
            TextoSituacion = "La empresa necesita reducir un 20% la plantilla por caída de ingresos. Tú eres el director de tu departamento (15 personas). RRHH te pide que comuniques la noticia a tu equipo esta semana. 3 personas de tu equipo serán despedidas. El resto se queda, pero con más carga de trabajo.",
            Contexto = "Sala de reuniones, equipo completo de 15 personas. Todos notan que algo pasa porque los rumores llevan días circulando.",
            GuionAudio = "Comunicar despidos es una de las pruebas más duras de liderazgo. No hay forma de hacerlo fácil, pero hay formas de hacerlo con dignidad, transparencia y humanidad."
        };
        contexto.Set<Escenario>().Add(escenario3);
        await contexto.SaveChangesAsync();

        contexto.Set<OpcionEscenario>().AddRange(
            new OpcionEscenario
            {
                EscenarioId = escenario3.Id, TextoOpcion = "Reunir a todo el equipo y dar la noticia de forma general sin nombrar a los afectados.",
                TextoRetroalimentacion = "Mezclar afectados y no afectados en la misma reunión genera ansiedad colectiva. Los que se quedan piensan '¿seré yo?' durante toda la reunión. Mejor: hablar primero con los afectados en privado.",
                PuntosOtorgados = 8, TipoResultado = TipoResultadoEscenario.Aceptable, Orden = 1
            },
            new OpcionEscenario
            {
                EscenarioId = escenario3.Id, TextoOpcion = "Primero hablar 1:1 con los 3 afectados con empatía y apoyo concreto. Luego reunir al equipo restante para explicar la situación con transparencia.",
                TextoRetroalimentacion = "¡Excelente! Respetar la dignidad de los afectados con conversaciones privadas primero, y luego ser transparente con el equipo restante es el enfoque correcto. Incluye qué apoyo recibirán los que se van y qué cambia para los que se quedan.",
                PuntosOtorgados = 20, TipoResultado = TipoResultadoEscenario.Optimo, Orden = 2
            },
            new OpcionEscenario
            {
                EscenarioId = escenario3.Id, TextoOpcion = "Enviar un email a todo el equipo explicando la reestructuración y que RRHH contactará a los afectados.",
                TextoRetroalimentacion = "Comunicar despidos por email es deshumanizante. Los afectados se enteran de forma impersonal y el resto del equipo pierde la confianza en ti como líder. Las malas noticias se dan cara a cara.",
                PuntosOtorgados = 0, TipoResultado = TipoResultadoEscenario.Inadecuado, Orden = 3
            },
            new OpcionEscenario
            {
                EscenarioId = escenario3.Id, TextoOpcion = "Esperar a que RRHH gestione todo y no intervenir directamente.",
                TextoRetroalimentacion = "Delegar las conversaciones difíciles a RRHH transmite que no te importa tu equipo. Como líder, es tu responsabilidad dar la cara. RRHH apoya, pero tú comunicas.",
                PuntosOtorgados = 2, TipoResultado = TipoResultadoEscenario.Inadecuado, Orden = 4
            }
        );

        await contexto.SaveChangesAsync();
    }
}
