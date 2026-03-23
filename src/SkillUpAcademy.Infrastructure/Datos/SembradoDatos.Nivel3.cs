using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de lecciones Nivel 3 (Dominio) para las 6 áreas.
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesNivel3Async(AppDbContext contexto)
    {
        // Nivel 3 IDs: Comunicación=3, Liderazgo=6, TrabajoEquipo=9, IntEmocional=12, Networking=15, Persuasión=18
        // Lecciones N3 empiezan en ID 61 (N1 usa 1-30, N2 usa 31-60)
        int leccionId = 61;

        // ===== COMUNICACIÓN EFECTIVA — Nivel 3 (NivelId=3) =====
        Leccion comN3L1 = new Leccion
        {
            Id = leccionId++, NivelId = 3, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Comunicación ejecutiva: presenta como un CEO",
            Descripcion = "Domina el arte de comunicar visión, estrategia y decisiones difíciles a nivel directivo.",
            Contenido = "## El principio de la pirámide de Minto\n\nBarbara Minto (McKinsey) creó el framework más usado en comunicación ejecutiva: empieza por la conclusión, luego los argumentos que la sostienen, y finalmente la evidencia.\n\n## Inversión de la narrativa\n\nLos juniors narran así: «Investigamos X, encontramos Y, por lo tanto Z.»\nLos ejecutivos comunican así: «La recomendación es Z. Las razones son A, B y C.»\n\n## Las 3 reglas de comunicación ejecutiva\n\n### 1. Lidera con la respuesta\nDi la conclusión en los primeros 30 segundos. Si te cortan, ya dijiste lo importante.\n\n### 2. Anticipa las preguntas\nPrepara slides de backup para las 5 preguntas más probables. Nunca digas «no lo sé» a algo predecible.\n\n### 3. Cuantifica todo\nNo digas «mejoró mucho». Di «mejoró un 34% en 3 meses, generando $120K adicionales».\n\n## Comunicar malas noticias\nSandwich inverso: Situación actual → Impacto → Plan de acción → Recursos necesarios. Sin endulzar, sin alarmar.",
            PuntosClave = "[\"Pirámide de Minto: conclusión primero, argumentos después, evidencia al final\",\"Los ejecutivos lideran con la respuesta, no con el proceso\",\"Anticipa las 5 preguntas más probables\",\"Cuantifica siempre: números, porcentajes, plazos\"]",
            GuionAudio = "Los líderes más efectivos comunican con precisión quirúrgica. Hoy aprenderás a presentar como un CEO usando la pirámide de Minto.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 12
        };

        Leccion comN3L2 = new Leccion
        {
            Id = leccionId++, NivelId = 3, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Comunicación de crisis: cuando todo sale mal",
            Descripcion = "Protocolos para comunicar durante emergencias, incidentes y crisis organizacionales.",
            Contenido = "## Las primeras 24 horas definen todo\n\nEn una crisis, la narrativa se forma en las primeras horas. Si no la controlas tú, alguien más lo hará.\n\n## El framework HEARD\n\n### H - Hear (Escucha)\nAntes de comunicar, entiende qué pasó realmente. Habla con los afectados.\n\n### E - Empathize (Empatiza)\nReconoce el impacto. «Entendemos que esto afecta directamente a...»\n\n### A - Apologize (Discúlpate si corresponde)\nSi hay responsabilidad, asúmela. Las excusas erosionan más que el error.\n\n### R - Resolve (Resuelve)\nPresenta el plan de acción con fechas. No prometas lo que no puedes cumplir.\n\n### D - Diagnose (Diagnostica)\nComparte qué harás para que no vuelva a pasar.\n\n## Errores fatales en crisis\n- Minimizar: «No es para tanto» → destruye confianza\n- Culpar: «Fue culpa de X» → pierde credibilidad\n- Silencio: no comunicar genera especulación\n- Sobreprometer: «Nunca más pasará» → si pasa, pierdes todo",
            PuntosClave = "[\"Las primeras 24 horas de una crisis definen la narrativa\",\"Framework HEARD: Hear, Empathize, Apologize, Resolve, Diagnose\",\"Asumir responsabilidad genera más confianza que las excusas\",\"El silencio durante una crisis genera especulación\"]",
            GuionAudio = "Cuando todo sale mal, tu comunicación puede salvarte o hundirte. Hoy aprendes el protocolo HEARD para gestionar crisis.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 12
        };

        Leccion comN3L3 = new Leccion
        {
            Id = leccionId++, NivelId = 3, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Comunicación de Alto Nivel",
            Descripcion = "Demuestra tu dominio de la comunicación ejecutiva y de crisis.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion comN3L4 = new Leccion
        {
            Id = leccionId++, NivelId = 3, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Caída del sistema en producción",
            Descripcion = "El sistema cayó afectando a 10,000 usuarios. El CEO quiere un informe en 30 minutos. ¿Cómo comunicas?",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 12
        };

        Leccion comN3L5 = new Leccion
        {
            Id = leccionId++, NivelId = 3, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Presentación al board de directores",
            Descripcion = "Presenta la estrategia del próximo año a un board exigente que cuestionará cada punto.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(comN3L1, comN3L2, comN3L3, comN3L4, comN3L5);

        // ===== LIDERAZGO — Nivel 3 (NivelId=6) =====
        Leccion lidN3L1 = new Leccion
        {
            Id = leccionId++, NivelId = 6, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Liderazgo transformacional: inspira cambio profundo",
            Descripcion = "Ve más allá de gestionar tareas: transforma personas y culturas organizacionales.",
            Contenido = "## De transaccional a transformacional\n\nEl líder transaccional intercambia: «Haces esto, recibes aquello.»\nEl líder transformacional inspira: «Esto es lo que podemos ser.»\n\n## Los 4 pilares de Bass\n\n### Influencia idealizada\nSé el modelo que tu equipo quiere seguir. Tus acciones deben alinearse con tus palabras.\n\n### Motivación inspiracional\nCrea una visión compartida que dé sentido al trabajo diario. La gente no quiere tareas, quiere propósito.\n\n### Estimulación intelectual\nDesafía suposiciones, fomenta la innovación, permite el error como aprendizaje.\n\n### Consideración individualizada\nCada persona del equipo tiene necesidades, motivaciones y metas únicas. Conócelas y apóyalas.\n\n## La paradoja del líder transformacional\nNo impones tu visión: facilitas que el equipo la co-cree. Cuanto más empoderas, más influencia genuina tienes.\n\n## Indicador clave\n¿Tu equipo toma buenas decisiones cuando tú no estás? Si la respuesta es sí, eres un líder transformacional.",
            PuntosClave = "[\"Transaccional = intercambio; Transformacional = inspiración\",\"4 pilares de Bass: Influencia, Motivación, Estimulación, Consideración\",\"El líder transformacional facilita, no impone\",\"Indicador clave: el equipo funciona bien sin ti\"]",
            GuionAudio = "Los mejores líderes no crean seguidores, crean otros líderes. Hoy exploramos el liderazgo que transforma culturas.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 12
        };

        Leccion lidN3L2 = new Leccion
        {
            Id = leccionId++, NivelId = 6, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Liderazgo en la incertidumbre: decidir sin información completa",
            Descripcion = "Frameworks para tomar decisiones de alto impacto cuando los datos son ambiguos.",
            Contenido = "## La niebla de guerra organizacional\n\nEn el mundo real, nunca tendrás el 100% de la información. Los líderes que esperan certeza total quedan paralizados.\n\n## El framework 70/30 de Jeff Bezos\nCuando tengas el 70% de la información que querrías, decide. Si esperas al 90%, probablemente sea demasiado tarde.\n\n## Decisiones reversibles vs. irreversibles\n\n### Tipo 1 (irreversibles)\nPocas. Requieren análisis profundo, múltiples perspectivas, deliberación. Ejemplo: vender la empresa.\n\n### Tipo 2 (reversibles)\nLa mayoría. Decide rápido, itera, ajusta. Ejemplo: cambiar el pricing de un feature.\n\n## Pre-mortem: evita desastres antes de que ocurran\nAntes de ejecutar una decisión importante, pregunta al equipo: «Imaginemos que dentro de 6 meses esto fue un fracaso total. ¿Por qué fracasó?» Esto activa el pensamiento crítico sin la presión de oponerse.\n\n## El líder como jardinero\nNo puedes controlar el clima (mercado). Pero puedes preparar el suelo (cultura), plantar semillas (iniciativas) y podar lo que no crece (proyectos fallidos).",
            PuntosClave = "[\"Framework 70/30: decide con el 70% de la información\",\"Distinguir decisiones reversibles de irreversibles es clave\",\"Pre-mortem: imaginar el fracaso antes de ejecutar\",\"La mayoría de decisiones son reversibles: decide rápido e itera\"]",
            GuionAudio = "Los grandes líderes no esperan la certeza, la crean. Hoy aprenderás a tomar decisiones sin información completa.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 12
        };

        Leccion lidN3L3 = new Leccion
        {
            Id = leccionId++, NivelId = 6, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Liderazgo Avanzado",
            Descripcion = "Evalúa tu comprensión del liderazgo transformacional y la toma de decisiones.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion lidN3L4 = new Leccion
        {
            Id = leccionId++, NivelId = 6, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Reestructuración del equipo",
            Descripcion = "La empresa necesita reducir tu equipo de 10 a 7 personas. Debes decidir quién se va y comunicarlo.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 12
        };

        Leccion lidN3L5 = new Leccion
        {
            Id = leccionId++, NivelId = 6, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Mentor de un futuro líder",
            Descripcion = "Un miembro del equipo quiere crecer a posición de liderazgo. Guíalo como mentor.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(lidN3L1, lidN3L2, lidN3L3, lidN3L4, lidN3L5);

        // ===== TRABAJO EN EQUIPO — Nivel 3 (NivelId=9) =====
        Leccion teqN3L1 = new Leccion
        {
            Id = leccionId++, NivelId = 9, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Equipos cross-funcionales: liderazgo sin autoridad",
            Descripcion = "Aprende a coordinar equipos donde no eres el jefe pero necesitas resultados.",
            Contenido = "## El reto: influir sin autoridad\n\nEn organizaciones modernas, los proyectos cruzan departamentos. No tienes poder formal sobre marketing, legal o finanzas, pero necesitas su colaboración.\n\n## Las 3 monedas del liderazgo sin autoridad\n\n### Moneda de relación\nConfianza acumulada. Se construye con cumplir promesas, ser transparente y ayudar sin esperar retorno.\n\n### Moneda de expertise\nConocimiento que otros valoran. Si eres la persona que sabe cómo resolver X, te buscarán.\n\n### Moneda de visión\nCapacidad de mostrar el panorama completo. Conecta el trabajo de cada persona con el resultado final.\n\n## Facilitar vs. Dirigir\nEn equipos cross-funcionales, tu rol es:\n- Alinear objetivos (todos entienden el porqué)\n- Remover obstáculos (especialmente entre departamentos)\n- Crear visibilidad (que cada área vea el progreso del conjunto)\n- Gestionar dependencias (quién necesita qué de quién)\n\n## La reunión más importante\nEl kickoff. Define alcance, roles, calendario y canales en una sola sesión. Sin kickoff claro, el proyecto nace muerto.",
            PuntosClave = "[\"Liderar sin autoridad requiere relación, expertise y visión\",\"Facilitar ≠ dirigir: alinea, remueve obstáculos, crea visibilidad\",\"El kickoff es la reunión más importante del proyecto\",\"La confianza se construye cumpliendo promesas consistentemente\"]",
            GuionAudio = "¿Cómo lideras cuando no eres el jefe? Hoy descubrirás las tres monedas del liderazgo sin autoridad formal.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 12
        };

        Leccion teqN3L2 = new Leccion
        {
            Id = leccionId++, NivelId = 9, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Cultura de equipo: diseña el ambiente donde la gente quiere estar",
            Descripcion = "Crea y mantiene una cultura de equipo que atraiga talento y genere resultados excepcionales.",
            Contenido = "## La cultura come estrategia para el desayuno\n\nPeter Drucker lo dijo claro. Puedes tener la mejor estrategia, pero si tu cultura es tóxica, no llegarás lejos.\n\n## Los 5 elementos de una cultura excepcional\n\n### 1. Valores vividos (no colgados en la pared)\nCada decisión se filtra por los valores. Si dices «innovación» pero castigas el error, tu cultura real es miedo.\n\n### 2. Rituales con propósito\nNo reuniones por inercia. Cada ritual debe tener un porqué: demos para celebrar progreso, retros para aprender, coffees para conectar.\n\n### 3. Feedback como norma\nEn culturas sanas, el feedback fluye en todas direcciones. No es un evento anual, es una conversación continua.\n\n### 4. Celebración intencional\nCelebra no solo resultados sino comportamientos que reflejan los valores. «Juan ayudó a Lucía con su módulo aunque no le tocaba» → reconocimiento público.\n\n### 5. Onboarding como inversión\nLas primeras 2 semanas definen si alguien se queda 2 meses o 2 años. Invierte en que los nuevos entiendan la cultura, no solo los procesos.",
            PuntosClave = "[\"La cultura se demuestra en decisiones, no en posters\",\"Los rituales deben tener propósito claro\",\"El feedback es conversación continua, no evento anual\",\"El onboarding define la retención a largo plazo\"]",
            GuionAudio = "La cultura de un equipo no se decreta, se diseña. Hoy aprenderás los 5 elementos que crean equipos donde la gente quiere estar.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 12
        };

        Leccion teqN3L3 = new Leccion
        {
            Id = leccionId++, NivelId = 9, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Liderazgo de Equipos Avanzado",
            Descripcion = "Evalúa tu dominio en liderazgo cross-funcional y diseño de cultura.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion teqN3L4 = new Leccion
        {
            Id = leccionId++, NivelId = 9, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Fusión de dos equipos con culturas opuestas",
            Descripcion = "Tras una adquisición, debes integrar tu equipo ágil con uno muy jerárquico. Las fricciones son evidentes.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 12
        };

        Leccion teqN3L5 = new Leccion
        {
            Id = leccionId++, NivelId = 9, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Diseñar el offsite del equipo",
            Descripcion = "Tu equipo necesita reconectarse tras un año difícil. Planifica y facilita un offsite transformador.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(teqN3L1, teqN3L2, teqN3L3, teqN3L4, teqN3L5);

        // ===== INTELIGENCIA EMOCIONAL — Nivel 3 (NivelId=12) =====
        Leccion ieN3L1 = new Leccion
        {
            Id = leccionId++, NivelId = 12, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Inteligencia emocional del líder: gestiona el clima emocional",
            Descripcion = "Como líder, tu estado emocional se contagia. Aprende a gestionar el clima emocional del equipo.",
            Contenido = "## El efecto espejo del líder\n\nEstudios de Daniel Goleman demuestran que las emociones del líder se propagan al equipo en minutos. Si llegas estresado, tu equipo se estresará. Si llegas sereno, se calmarán.\n\n## Contagio emocional consciente\n\n### Lee la sala\nAntes de hablar, observa la energía del grupo. ¿Están agotados, frustrados, energizados? Adapta tu tono.\n\n### Regula antes de entrar\nTómate 2 minutos antes de cada reunión para centrarte. Tu estado emocional es tu herramienta más poderosa.\n\n### Nombra lo invisible\n«Noto que hay tensión después de los cambios de la semana pasada. Es normal.» Nombrar la emoción colectiva la desactiva.\n\n## El líder emocionalmente inteligente\n- No finge positidad tóxica\n- Normaliza las emociones difíciles\n- Modela vulnerabilidad apropiada\n- Celebra genuinamente\n- Pide ayuda cuando la necesita\n\n## La trampa del estoicismo\nNo confundas fortaleza con insensibilidad. Los equipos respetan más al líder que dice «Esto también me afecta» que al que finge que nada le toca.",
            PuntosClave = "[\"Las emociones del líder se contagian al equipo en minutos\",\"Nombrar la emoción colectiva la desactiva\",\"Regularse antes de entrar a reuniones es esencial\",\"Vulnerabilidad apropiada genera más respeto que el estoicismo\"]",
            GuionAudio = "Tu estado emocional es la herramienta de liderazgo más poderosa que tienes. Hoy aprenderás a usarla con intención.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 12
        };

        Leccion ieN3L2 = new Leccion
        {
            Id = leccionId++, NivelId = 12, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Conversaciones difíciles con maestría emocional",
            Descripcion = "Domina el arte de abordar temas incómodos: despidos, bajo rendimiento, conflictos interpersonales.",
            Contenido = "## Por qué evitamos las conversaciones difíciles\n\nNuestro cerebro equipara el rechazo social con el dolor físico (estudios de Eisenberger). Evitar la conversación es instintivo, tenerla es una habilidad.\n\n## El modelo COIN para conversaciones difíciles\n\n### C - Context (Contexto)\nEstablece el marco: «Quiero hablar sobre algo importante para ambos.»\n\n### O - Observation (Observación)\nDescribe hechos, no juicios: «He notado que los últimos 3 entregables llegaron después del deadline.»\n\n### I - Impact (Impacto)\nConecta con las consecuencias: «Esto ha retrasado al equipo y generado re-trabajo.»\n\n### N - Next steps (Próximos pasos)\nCo-crea la solución: «¿Qué podemos hacer juntos para cambiar esto?»\n\n## Gestionar la reacción defensiva\nCuando alguien se pone a la defensiva:\n1. Pausa (no contra-ataques)\n2. Valida la emoción: «Entiendo que es difícil escuchar esto»\n3. Redirige al futuro: «No busco culpar, busco mejorar juntos»\n\n## Después de la conversación\nHaz follow-up en 1-2 semanas. Si mejoró, reconócelo. Si no, repite con más firmeza.",
            PuntosClave = "[\"El cerebro equipara rechazo social con dolor físico\",\"Modelo COIN: Contexto, Observación, Impacto, Próximos pasos\",\"Ante defensividad: pausa, valida y redirige al futuro\",\"El follow-up después de la conversación es imprescindible\"]",
            GuionAudio = "Las conversaciones que evitas son las que más necesitas tener. Hoy aprenderás el modelo COIN para abordarlas con maestría.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 12
        };

        Leccion ieN3L3 = new Leccion
        {
            Id = leccionId++, NivelId = 12, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Maestría Emocional",
            Descripcion = "Evalúa tu dominio de la inteligencia emocional a nivel de liderazgo.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion ieN3L4 = new Leccion
        {
            Id = leccionId++, NivelId = 12, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Despedir a un amigo del equipo",
            Descripcion = "La empresa decide prescindir de alguien que es buen amigo tuyo y del equipo. Tú debes comunicarlo.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 12
        };

        Leccion ieN3L5 = new Leccion
        {
            Id = leccionId++, NivelId = 12, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Conversación de rendimiento con empleado senior",
            Descripcion = "Un empleado con 8 años en la empresa ha bajado su rendimiento drásticamente. Necesitas abordar la situación.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(ieN3L1, ieN3L2, ieN3L3, ieN3L4, ieN3L5);

        // ===== NETWORKING — Nivel 3 (NivelId=15) =====
        Leccion netN3L1 = new Leccion
        {
            Id = leccionId++, NivelId = 15, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Personal branding: conviértete en referente de tu industria",
            Descripcion = "Construye una marca personal que atraiga oportunidades sin que las busques.",
            Contenido = "## De networking a brand networking\n\nEn los primeros niveles aprendiste a conectar. Ahora aprenderás a que las conexiones vengan a ti.\n\n## Los 4 pilares del personal branding\n\n### 1. Nicho de expertise\nNo seas «bueno en muchas cosas». Sé el referente en una. «Elena es la persona para todo lo de accesibilidad web.»\n\n### 2. Contenido consistente\nPublica regularmente: artículos, posts, charlas, mentorías. No necesitas ser viral, necesitas ser constante.\n\n### 3. Visibilidad estratégica\nNo estés en todas las plataformas. Elige 1-2 donde está tu audiencia: LinkedIn para B2B, Twitter/X para tech, conferencias para liderazgo.\n\n### 4. Generosidad pública\nComparte conocimiento, haz introducciones, ayuda en público. La generosidad visible construye reputación.\n\n## La regla del 1% de mejora\nNo necesitas ser el mejor del mundo. Necesitas saber el 1% más que tu audiencia sobre un tema específico.\n\n## El efecto compuesto\nUn post al día × 365 días = autoridad. No esperes resultados en semanas. Es un juego de años.",
            PuntosClave = "[\"Personal branding = que las oportunidades vengan a ti\",\"Elige un nicho específico: mejor ser referente en uno que mediocre en muchos\",\"Consistencia > viralidad\",\"Generosidad pública construye reputación más rápido que la autopromoción\"]",
            GuionAudio = "Los profesionales más exitosos no buscan oportunidades: las atraen. Hoy diseñarás tu marca personal.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 12
        };

        Leccion netN3L2 = new Leccion
        {
            Id = leccionId++, NivelId = 15, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Mentoría y sponsorship: las relaciones que aceleran carreras",
            Descripcion = "Entiende la diferencia entre mentor y sponsor, y cómo cultivar ambas relaciones.",
            Contenido = "## Mentor vs. Sponsor: no son lo mismo\n\n**Mentor**: te da consejos, comparte experiencia, te guía en decisiones. Habla contigo.\n**Sponsor**: te recomienda para oportunidades, te defiende en las reuniones donde no estás. Habla de ti.\n\n## Necesitas ambos\nLos mentores te hacen mejor. Los sponsors te hacen visible. Sin mentor, creces lento. Sin sponsor, creces en la sombra.\n\n## Cómo encontrar un mentor\n1. No pidas «¿serías mi mentor?» (demasiada presión)\n2. Pide consejo específico: «¿Puedo preguntarte sobre X? Valoro tu experiencia en ese tema.»\n3. Si la relación fluye, se formaliza sola\n4. Respeta su tiempo: agenda corta, acción después\n\n## Cómo ganarte un sponsor\n1. Haz trabajo excelente y visible\n2. Comunica tus aspiraciones a personas con poder\n3. Facilita el trabajo de potenciales sponsors\n4. Sé alguien que no les avergüence recomendar\n\n## Sé mentor de otros\nLa mejor forma de consolidar lo que sabes es enseñarlo. Además, tus mentees se convierten en tu red futura.",
            PuntosClave = "[\"Mentor habla contigo, Sponsor habla de ti\",\"No pidas mentorship directamente: pide consejo específico\",\"Para ganar sponsors: excelencia visible + comunicar aspiraciones\",\"Ser mentor de otros consolida tu conocimiento y amplía tu red\"]",
            GuionAudio = "Hay dos tipos de relaciones que aceleran carreras: mentores y sponsors. Hoy aprenderás cómo cultivar ambas.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 12
        };

        Leccion netN3L3 = new Leccion
        {
            Id = leccionId++, NivelId = 15, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Networking Avanzado y Personal Branding",
            Descripcion = "Evalúa tu dominio de personal branding, mentoría y sponsorship.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion netN3L4 = new Leccion
        {
            Id = leccionId++, NivelId = 15, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Te ofrecen ser speaker en una conferencia",
            Descripcion = "Te invitan a dar una charla de 20 minutos en una conferencia importante. Debes preparar tu propuesta de tema.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 12
        };

        Leccion netN3L5 = new Leccion
        {
            Id = leccionId++, NivelId = 15, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Sesión de mentoría como mentor",
            Descripcion = "Un junior te pide consejo sobre cómo crecer profesionalmente. Guíalo como mentor.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(netN3L1, netN3L2, netN3L3, netN3L4, netN3L5);

        // ===== PERSUASIÓN — Nivel 3 (NivelId=18) =====
        Leccion perN3L1 = new Leccion
        {
            Id = leccionId++, NivelId = 18, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Negociación avanzada: el framework de Harvard",
            Descripcion = "Domina la negociación principista: separa personas de problemas, negocia intereses no posiciones.",
            Contenido = "## Getting to Yes: el método Harvard\n\nFisher y Ury crearon el framework de negociación más usado del mundo. Su premisa: negociar duro con el problema, suave con la persona.\n\n## Los 4 principios\n\n### 1. Separa personas del problema\nAtaca el problema, no a la persona. «El timeline es un desafío» vs. «Tú siempre pides plazos imposibles.»\n\n### 2. Enfócate en intereses, no posiciones\nPosición: «Necesito un 20% de aumento.» Interés detrás: reconocimiento, crecimiento, seguridad financiera. Negocia el interés.\n\n### 3. Inventa opciones de beneficio mutuo\nAntes de negociar, genera 10+ opciones creativas. Más opciones = más posibilidades de acuerdo.\n\n### 4. Insiste en criterios objetivos\nBasa la negociación en estándares externos: mercado, benchmarks, precedentes. Elimina el «porque yo digo».\n\n## BATNA: tu arma secreta\nBest Alternative To a Negotiated Agreement. Si no llegas a acuerdo, ¿cuál es tu mejor alternativa? Conocer tu BATNA te da poder real.\n\n## ZOPA: la zona mágica\nZone Of Possible Agreement. Donde tu mínimo aceptable se solapa con el máximo del otro. Si no hay ZOPA, no hay trato posible.",
            PuntosClave = "[\"Harvard: duro con el problema, suave con la persona\",\"Negocia intereses (por qué lo quiere), no posiciones (qué pide)\",\"BATNA: conocer tu mejor alternativa te da poder\",\"ZOPA: si no hay zona de acuerdo posible, no fuerces el trato\"]",
            GuionAudio = "La negociación no es ganar o perder. Es encontrar la zona donde ambos ganan. Hoy aprendes el método de Harvard.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 12
        };

        Leccion perN3L2 = new Leccion
        {
            Id = leccionId++, NivelId = 18, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Influencia organizacional: mueve ideas en sistemas complejos",
            Descripcion = "Aprende a navegar la política organizacional y mover iniciativas en empresas grandes.",
            Contenido = "## La política no es mala, es inevitable\n\nDonde hay más de dos personas, hay política. Ignorarla es ingenuidad. Dominarla éticamente es liderazgo.\n\n## El mapa de stakeholders\n\n### Champions (Aliados)\nCreen en tu idea y tienen influencia. Cuídalos, mantenlos informados, dales crédito.\n\n### Bloqueadores\nSe oponen por razones legítimas (riesgo, recursos) o políticas (territorio, ego). Entiende su motivo real.\n\n### Observadores\nNo están a favor ni en contra. Son tu oportunidad: gánalos con datos y beneficios claros para ellos.\n\n## Cómo mover ideas grandes\n\n### 1. Pre-alignment (alineamiento previo)\nNunca presentes una idea importante por primera vez en una reunión grande. Habla antes con los stakeholders clave en privado.\n\n### 2. Piloto como prueba\nNo pidas permiso para el proyecto completo. Pide un piloto pequeño con métricas claras. Los resultados hablan.\n\n### 3. Cede autoría\nSi tu idea necesita que alguien poderoso la adopte como suya para avanzar, déjalo. El resultado importa más que el crédito.\n\n### 4. Coaliciones\nUna persona con una idea es ignorada. Cinco personas alineadas con la misma idea son una fuerza.",
            PuntosClave = "[\"La política organizacional es inevitable: domínala éticamente\",\"Pre-alignment: alinea en privado antes de presentar en público\",\"Piloto pequeño + métricas claras > pedir permiso para todo\",\"El crédito importa menos que el resultado\"]",
            GuionAudio = "Las mejores ideas mueren en empresas grandes por no saber navegar el sistema. Hoy aprendes a mover iniciativas con influencia estratégica.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 12
        };

        Leccion perN3L3 = new Leccion
        {
            Id = leccionId++, NivelId = 18, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Negociación e Influencia Organizacional",
            Descripcion = "Demuestra tu dominio del método Harvard y la influencia en organizaciones.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion perN3L4 = new Leccion
        {
            Id = leccionId++, NivelId = 18, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Negociación con proveedor que quiere subir precios un 40%",
            Descripcion = "Tu proveedor principal anuncia un aumento del 40%. Tu empresa no puede absorberlo pero cambiar de proveedor tomaría 6 meses.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 12
        };

        Leccion perN3L5 = new Leccion
        {
            Id = leccionId++, NivelId = 18, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Convencer a la organización de cambiar de tecnología",
            Descripcion = "Quieres migrar el stack tecnológico. Practica cómo ganar aliados y mover la iniciativa.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(perN3L1, perN3L2, perN3L3, perN3L4, perN3L5);

        await contexto.SaveChangesAsync();

        // ===== QUIZZES Y ESCENARIOS NIVEL 3 =====
        await SembrarQuizzesNivel3Async(contexto);
        await SembrarEscenariosNivel3Async(contexto);
    }

    private static async Task SembrarQuizzesNivel3Async(AppDbContext contexto)
    {
        // Quiz Comunicación N3 (leccionId=63)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(63, 1, "¿Cuál es el principio central de la pirámide de Minto?",
                "Empezar por la conclusión, luego argumentos, luego evidencia.",
                new[] {
                    ("Empezar con la conclusión", true, "¡Correcto! Di lo importante primero, detalla después"),
                    ("Empezar con los datos", false, "Eso es el enfoque junior: proceso antes que conclusión"),
                    ("Empezar con una historia", false, "El storytelling tiene su lugar pero Minto es estructura lógica"),
                    ("Empezar con una pregunta", false, "La pirámide de Minto lidera con la respuesta")
                }),
            CrearPregunta(63, 2, "¿Qué significa la H en el framework HEARD para crisis?",
                "H = Hear (Escucha). Entiende qué pasó antes de comunicar.",
                new[] {
                    ("Help (Ayuda)", false, "La H es por escuchar primero"),
                    ("Hear (Escucha)", true, "¡Correcto! Antes de hablar, entiende la situación"),
                    ("Honesty (Honestidad)", false, "La honestidad es importante pero no es la H de HEARD"),
                    ("Hurry (Prisa)", false, "Actuar rápido sí, pero la H es por escuchar")
                }),
            CrearPregunta(63, 3, "¿Cuál es el error más fatal en comunicación de crisis?",
                "El silencio genera especulación que es peor que la realidad.",
                new[] {
                    ("Ser demasiado transparente", false, "La transparencia suele ser positiva en crisis"),
                    ("El silencio", true, "¡Correcto! No comunicar deja que otros controlen la narrativa"),
                    ("Hablar demasiado rápido", false, "Es mejor comunicar rápido que no comunicar"),
                    ("Pedir disculpas", false, "Disculparse cuando corresponde genera confianza")
                }),
            CrearPregunta(63, 4, "¿En cuántos segundos debes decir tu conclusión según comunicación ejecutiva?",
                "Los primeros 30 segundos son críticos.",
                new[] {
                    ("10 segundos", false, "Es un poco rápido, 30 es el estándar"),
                    ("30 segundos", true, "¡Correcto! Si te cortan, ya dijiste lo importante"),
                    ("2 minutos", false, "Demasiado tiempo, el ejecutivo ya se distrajo"),
                    ("5 minutos", false, "En 5 minutos debería estar toda la presentación, no solo la conclusión")
                }),
            CrearPregunta(63, 5, "¿Qué es el 'sandwich inverso' para dar malas noticias?",
                "Situación → Impacto → Plan de acción → Recursos necesarios.",
                new[] {
                    ("Buena noticia → Mala noticia → Buena noticia", false, "Ese es el sandwich normal, que es inefectivo"),
                    ("Situación → Impacto → Plan → Recursos", true, "¡Correcto! Directo, sin endulzar ni alarmar"),
                    ("Mala noticia → Disculpa → Solución", false, "Incompleto: falta el plan concreto y recursos"),
                    ("Contexto → Causa → Culpable → Solución", false, "Buscar culpables erosiona la confianza")
                })
        );

        // Quiz Liderazgo N3 (leccionId=68)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(68, 1, "¿Qué diferencia al líder transformacional del transaccional?",
                "Transformacional inspira, transaccional intercambia.",
                new[] {
                    ("El transformacional es más estricto", false, "No se trata de strictness sino de inspiración"),
                    ("El transformacional inspira, el transaccional intercambia", true, "¡Correcto! Propósito vs. premio/castigo"),
                    ("No hay diferencia real", false, "Son estilos fundamentalmente diferentes"),
                    ("El transaccional es mejor para crisis", false, "Ambos tienen sus contextos, la diferencia es el enfoque")
                }),
            CrearPregunta(68, 2, "¿Qué es el framework 70/30 de Jeff Bezos?",
                "Decide con el 70% de la información que querrías tener.",
                new[] {
                    ("70% trabajo, 30% descanso", false, "No es sobre balance de vida"),
                    ("Decide con el 70% de la información", true, "¡Correcto! Esperar al 90% suele ser demasiado tarde"),
                    ("70% de probabilidad de éxito", false, "No es sobre probabilidades de éxito"),
                    ("70% del equipo de acuerdo", false, "No es sobre consenso")
                }),
            CrearPregunta(68, 3, "¿Qué es un pre-mortem?",
                "Imaginar que el proyecto fracasó y analizar por qué.",
                new[] {
                    ("Reunión antes de lanzar un producto", false, "Es un ejercicio mental, no una reunión de lanzamiento"),
                    ("Imaginar el fracaso para prevenirlo", true, "¡Correcto! Activa pensamiento crítico sin la presión de oponerse"),
                    ("Análisis post-proyecto", false, "Eso es un post-mortem, no un pre-mortem"),
                    ("Evaluación de riesgos formal", false, "Es más creativo e informal que una evaluación de riesgos")
                }),
            CrearPregunta(68, 4, "¿Cuál es el indicador clave de un líder transformacional?",
                "El equipo toma buenas decisiones sin el líder presente.",
                new[] {
                    ("El equipo siempre le consulta", false, "Eso indica dependencia, no transformación"),
                    ("El equipo funciona bien sin él", true, "¡Correcto! Ha creado autonomía y capacidad"),
                    ("El equipo lo admira", false, "La admiración no es el indicador principal"),
                    ("Las métricas mejoran", false, "Las métricas son consecuencia, no el indicador")
                }),
            CrearPregunta(68, 5, "Según Bass, ¿cuáles son los 4 pilares del liderazgo transformacional?",
                "Influencia idealizada, Motivación inspiracional, Estimulación intelectual, Consideración individualizada.",
                new[] {
                    ("Visión, Estrategia, Ejecución, Medición", false, "Esos son pilares de gestión, no de transformación"),
                    ("Influencia, Motivación, Estimulación, Consideración", true, "¡Correcto! Los 4 pilares de Bass"),
                    ("Carisma, Inteligencia, Decisión, Empatía", false, "No son los pilares formales del modelo"),
                    ("Comunicación, Delegación, Feedback, Coaching", false, "Son habilidades de gestión, no los pilares de Bass")
                })
        );

        // Quiz Trabajo en Equipo N3 (leccionId=73)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(73, 1, "¿Cuáles son las 3 monedas del liderazgo sin autoridad?",
                "Relación, Expertise y Visión.",
                new[] {
                    ("Poder, Influencia, Carisma", false, "El liderazgo sin autoridad no depende del poder formal"),
                    ("Relación, Expertise, Visión", true, "¡Correcto! Confianza, conocimiento y panorama completo"),
                    ("Dinero, Título, Contactos", false, "Esos son recursos formales, no monedas de influencia"),
                    ("Comunicación, Empatía, Decisión", false, "Son habilidades pero no las 3 monedas del framework")
                }),
            CrearPregunta(73, 2, "Según Peter Drucker, ¿qué come la estrategia para el desayuno?",
                "La cultura organizacional.",
                new[] {
                    ("La tecnología", false, "Drucker hablaba de cultura, no de tecnología"),
                    ("La cultura", true, "¡Correcto! Sin buena cultura, la mejor estrategia fracasa"),
                    ("La competencia", false, "La frase es sobre factores internos"),
                    ("El presupuesto", false, "El dinero importa pero la cultura es el factor clave")
                }),
            CrearPregunta(73, 3, "¿Cuál es la reunión más importante en un proyecto cross-funcional?",
                "El kickoff donde se definen alcance, roles y calendario.",
                new[] {
                    ("La daily standup", false, "Es importante pero no la más importante"),
                    ("El kickoff", true, "¡Correcto! Sin un kickoff claro, el proyecto nace muerto"),
                    ("La retrospectiva final", false, "Es útil pero el proyecto ya terminó"),
                    ("La reunión semanal de status", false, "El seguimiento importa pero el kickoff es fundamental")
                }),
            CrearPregunta(73, 4, "¿Qué definen las primeras 2 semanas de un nuevo empleado según el onboarding?",
                "Si se queda 2 meses o 2 años.",
                new[] {
                    ("Su productividad del primer trimestre", false, "El impacto va mucho más allá de la productividad inicial"),
                    ("Si se queda 2 meses o 2 años", true, "¡Correcto! El onboarding define la retención a largo plazo"),
                    ("Su relación con el manager", false, "Es un factor pero no el único"),
                    ("Nada significativo", false, "Las primeras semanas son determinantes")
                }),
            CrearPregunta(73, 5, "Si dices que valoras la innovación pero castigas el error, ¿cuál es tu cultura real?",
                "Miedo. La cultura real se demuestra en acciones, no en palabras.",
                new[] {
                    ("Innovación con disciplina", false, "Si castigas el error, no hay innovación real"),
                    ("Miedo", true, "¡Correcto! La cultura real se ve en las consecuencias, no en los posters"),
                    ("Excelencia", false, "Castigar el error no produce excelencia sino parálisis"),
                    ("Perfeccionismo sano", false, "Castigar errores es perfeccionismo tóxico, no sano")
                })
        );

        // Quiz Inteligencia Emocional N3 (leccionId=78)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(78, 1, "Según Goleman, ¿qué pasa con las emociones del líder en el equipo?",
                "Se contagian al equipo en minutos.",
                new[] {
                    ("No afectan al equipo", false, "Las emociones del líder son altamente contagiosas"),
                    ("Se contagian en minutos", true, "¡Correcto! El contagio emocional del líder es inmediato"),
                    ("Solo afectan si las verbaliza", false, "El contagio es automático, no requiere verbalización"),
                    ("Solo afectan a personas sensibles", false, "Afecta a todo el equipo")
                }),
            CrearPregunta(78, 2, "¿Qué es el modelo COIN para conversaciones difíciles?",
                "Context, Observation, Impact, Next steps.",
                new[] {
                    ("Contexto, Observación, Impacto, Próximos pasos", true, "¡Correcto! Un framework claro para temas incómodos"),
                    ("Comunicar, Observar, Implementar, Negociar", false, "Esas no son las siglas del modelo COIN"),
                    ("Causa, Origen, Investigación, Norma", false, "COIN no es un modelo de investigación"),
                    ("Conflicto, Opciones, Intervención, Negociación", false, "No son las siglas correctas")
                }),
            CrearPregunta(78, 3, "Cuando alguien se pone a la defensiva en una conversación difícil, ¿qué debes hacer?",
                "Pausa, valida la emoción, redirige al futuro.",
                new[] {
                    ("Subir el tono para imponer autoridad", false, "Escalar solo aumenta la defensividad"),
                    ("Pausa, valida la emoción, redirige al futuro", true, "¡Correcto! Desescalar con empatía y enfoque en soluciones"),
                    ("Cambiar de tema", false, "Evitar el tema no resuelve el problema"),
                    ("Terminar la conversación", false, "Abandonar deja el conflicto sin resolver")
                }),
            CrearPregunta(78, 4, "¿Por qué la vulnerabilidad apropiada genera más respeto que el estoicismo?",
                "Porque humaniza al líder y da permiso al equipo para ser auténtico.",
                new[] {
                    ("Porque muestra debilidad que genera compasión", false, "No es debilidad, es autenticidad"),
                    ("Porque humaniza y da permiso para ser auténtico", true, "¡Correcto! El equipo se siente seguro para ser genuino"),
                    ("Porque es una técnica de manipulación", false, "La vulnerabilidad genuina no es manipulación"),
                    ("No genera más respeto", false, "Los estudios demuestran que sí")
                }),
            CrearPregunta(78, 5, "¿Qué debe hacer un líder antes de entrar a cada reunión?",
                "Tomarse 2 minutos para centrarse y regular su estado emocional.",
                new[] {
                    ("Revisar la agenda", false, "Importante, pero la regulación emocional es prioritaria"),
                    ("Regular su estado emocional", true, "¡Correcto! Tu estado se contagia, así que prepáralo"),
                    ("Preparar sus argumentos", false, "Los argumentos importan pero tu energía importa más"),
                    ("Nada especial", false, "La preparación emocional es fundamental")
                })
        );

        // Quiz Networking N3 (leccionId=83)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(83, 1, "¿Cuál es la diferencia entre un mentor y un sponsor?",
                "Mentor habla contigo, Sponsor habla de ti ante otros.",
                new[] {
                    ("No hay diferencia", false, "Son roles fundamentalmente distintos"),
                    ("El mentor habla contigo, el sponsor habla de ti", true, "¡Correcto! El sponsor te recomienda cuando no estás"),
                    ("El mentor es más senior", false, "Ambos pueden ser senior, la diferencia es el rol"),
                    ("El sponsor es informal", false, "El sponsor puede ser formal o informal")
                }),
            CrearPregunta(83, 2, "¿Cuál es la regla del 1% en personal branding?",
                "Solo necesitas saber el 1% más que tu audiencia sobre un tema.",
                new[] {
                    ("Publicar el 1% de tu trabajo", false, "No es sobre cuánto publicas"),
                    ("Saber el 1% más que tu audiencia", true, "¡Correcto! No necesitas ser el mejor del mundo"),
                    ("Alcanzar al 1% top de tu industria", false, "El personal branding no requiere ser elite"),
                    ("Dedicar el 1% de tu tiempo", false, "Requiere más del 1% de tu tiempo")
                }),
            CrearPregunta(83, 3, "¿Cómo NO deberías pedir mentorship?",
                "Preguntando directamente '¿serías mi mentor?' genera demasiada presión.",
                new[] {
                    ("Pidiendo consejo específico", false, "Esa es la forma correcta"),
                    ("Preguntando '¿serías mi mentor?'", true, "¡Correcto! Demasiada presión. Empieza con preguntas específicas"),
                    ("Mostrando interés en su expertise", false, "Eso es un buen primer paso"),
                    ("Ofreciendo valor a cambio", false, "Ofrecer valor es siempre positivo")
                }),
            CrearPregunta(83, 4, "¿Qué es más importante en personal branding: consistencia o viralidad?",
                "La consistencia supera a la viralidad en el largo plazo.",
                new[] {
                    ("Viralidad", false, "Un post viral sin consistencia se olvida rápido"),
                    ("Consistencia", true, "¡Correcto! Un post al día × 365 días = autoridad"),
                    ("Ambas por igual", false, "La consistencia es claramente más importante"),
                    ("Ninguna, importa la calidad", false, "La calidad importa pero sin consistencia no hay impacto")
                }),
            CrearPregunta(83, 5, "¿Cómo se gana uno un sponsor?",
                "Trabajo excelente y visible + comunicar aspiraciones.",
                new[] {
                    ("Pidiendo directamente que te recomiende", false, "Es demasiado directo, necesitas ganártelo"),
                    ("Excelencia visible + comunicar aspiraciones", true, "¡Correcto! Haz que sea fácil y natural recomendarte"),
                    ("Siendo amigo personal del jefe", false, "El amiguismo no es sponsorship"),
                    ("Cambiando de empresa frecuentemente", false, "La rotación no genera sponsors")
                })
        );

        // Quiz Persuasión N3 (leccionId=88)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(88, 1, "¿Qué es el BATNA en negociación?",
                "Best Alternative To a Negotiated Agreement — tu mejor alternativa si no hay acuerdo.",
                new[] {
                    ("La mejor oferta del otro lado", false, "BATNA es TU mejor alternativa, no la del otro"),
                    ("Tu mejor alternativa si no hay acuerdo", true, "¡Correcto! Conocer tu BATNA te da poder real"),
                    ("El punto medio de la negociación", false, "Eso sería el compromiso, no el BATNA"),
                    ("El presupuesto máximo", false, "BATNA es tu plan B, no tu presupuesto")
                }),
            CrearPregunta(88, 2, "Según el método Harvard, ¿qué debes negociar: posiciones o intereses?",
                "Intereses. Las posiciones son lo que piden, los intereses son por qué lo piden.",
                new[] {
                    ("Posiciones", false, "Las posiciones son rígidas y generan conflicto"),
                    ("Intereses", true, "¡Correcto! Los intereses permiten soluciones creativas"),
                    ("Ambos por igual", false, "El método Harvard prioriza claramente los intereses"),
                    ("Ni uno ni otro: hechos", false, "Los hechos importan pero los intereses son la clave")
                }),
            CrearPregunta(88, 3, "¿Qué es pre-alignment en influencia organizacional?",
                "Alinear stakeholders en privado antes de presentar en público.",
                new[] {
                    ("Preparar slides antes de la reunión", false, "Es sobre relaciones, no sobre materiales"),
                    ("Alinear stakeholders en privado antes de presentar", true, "¡Correcto! Nunca presentes ideas importantes por primera vez en grupo"),
                    ("Conseguir aprobación del CEO primero", false, "No necesariamente el CEO, sino los stakeholders clave"),
                    ("Hacer un análisis previo de datos", false, "Los datos importan pero pre-alignment es sobre personas")
                }),
            CrearPregunta(88, 4, "¿Qué es el ZOPA en negociación?",
                "Zone Of Possible Agreement — donde tu mínimo se solapa con su máximo.",
                new[] {
                    ("Zona de presión acumulada", false, "ZOPA no se refiere a presión"),
                    ("Zone Of Possible Agreement", true, "¡Correcto! Si no hay solapamiento, no hay trato posible"),
                    ("La zona de confort del negociador", false, "No tiene que ver con zonas de confort"),
                    ("El objetivo de la negociación", false, "Es el rango donde el acuerdo es posible")
                }),
            CrearPregunta(88, 5, "¿Por qué ceder autoría de una idea puede ser estratégicamente inteligente?",
                "Porque el resultado importa más que el crédito personal.",
                new[] {
                    ("Nunca es inteligente ceder autoría", false, "A veces es la única forma de que una buena idea avance"),
                    ("Porque el resultado importa más que el crédito", true, "¡Correcto! Si la idea avanza, todos ganan"),
                    ("Porque genera lástima", false, "No se trata de emociones sino de estrategia"),
                    ("Porque así evitas responsabilidad si falla", false, "No es para evitar responsabilidad sino para facilitar adopción")
                })
        );

        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenariosNivel3Async(AppDbContext contexto)
    {
        // Escenario Comunicación N3 (leccionId=64)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 64,
            TextoSituacion = "El sistema de producción se cayó hace 2 horas afectando a 10,000 usuarios. El CEO quiere un informe en 30 minutos para comunicar externamente. El equipo técnico aún no tiene la causa raíz.",
            Contexto = "Eres el director de tecnología. Hay presión de comunicados en redes sociales y el equipo de soporte está desbordado.",
            GuionAudio = "Crisis en tiempo real. El CEO necesita información y el mundo está mirando.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Usar el framework HEARD: informar al CEO con hechos conocidos (qué pasó, cuántos afectados, estado actual), reconocer el impacto, presentar plan de acción (equipo trabajando, ETA de restauración), y sugerir comunicado externo transparente con compromiso de actualización cada hora", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Magistral gestión de crisis. Comunicas con hechos sin especular, asumes responsabilidad, presentas plan y propones transparencia. El framework HEARD en acción.", PuntosOtorgados = 30, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Decirle al CEO que espere a tener la causa raíz antes de comunicar nada", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "Esperar a tener toda la información es comprensible, pero en una crisis el silencio genera especulación. Es mejor comunicar lo que sabes ahora y actualizar después.", PuntosOtorgados = 15, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Minimizar: 'Son solo 10 mil usuarios de millones, no es para tanto'", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Minimizar en crisis destruye credibilidad. Para esos 10,000 usuarios, es 100% de su experiencia la que está afectada.", PuntosOtorgados = 0, Orden = 3 }
            }
        });

        // Escenario Liderazgo N3 (leccionId=69)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 69,
            TextoSituacion = "La empresa necesita reducir costos y tu equipo de 10 debe pasar a 7. RRHH te pide los nombres esta semana. Tienes que decidir quién sale y comunicarlo al equipo.",
            Contexto = "Todos han contribuido. Hay un senior con bajo rendimiento reciente pero 5 años de lealtad, un junior brillante con 3 meses, y el resto con rendimiento similar.",
            GuionAudio = "La decisión más difícil del liderazgo: elegir quién se va cuando todos han dado algo.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Usar criterios objetivos (rendimiento, criticidad del rol, costo) para la decisión; hablar individualmente con los afectados con honestidad y empatía usando COIN; ofrecer apoyo en la transición; comunicar al equipo restante con transparencia sobre el porqué y el plan futuro", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Liderazgo transformacional en su momento más difícil. Criterios justos, comunicación empática individual, y transparencia con el equipo. Esto preserva la confianza y la dignidad de todos.", PuntosOtorgados = 30, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Elegir a los 3 con menor antigüedad para ser justo (FIFO)", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "La antigüedad como criterio único es simple pero puede hacerte perder talento clave. El junior brillante de 3 meses podría ser más valioso que un senior desmotivado.", PuntosOtorgados = 15, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Pedir voluntarios para la salida", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Los mejores se irán primero (tienen más opciones) y te quedarás con los menos empleables. Además, creas semanas de ansiedad colectiva.", PuntosOtorgados = 0, Orden = 3 }
            }
        });

        // Escenario Trabajo en Equipo N3 (leccionId=74)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 74,
            TextoSituacion = "Tu empresa adquirió una startup. Debes integrar su equipo de 5 (ágil, informal, sin procesos) con tu equipo de 8 (estructurado, procesos claros, jerárquico). Las primeras semanas han sido tensas.",
            Contexto = "Ambos equipos tienen talento pero las formas de trabajar son opuestas. Ya hubo 2 discusiones fuertes en reuniones.",
            GuionAudio = "Fusionar culturas es uno de los retos más complejos. No gana una: hay que crear algo nuevo.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Organizar un workshop de 2 días donde ambos equipos co-crean los valores, rituales y procesos del nuevo equipo combinado. Cada lado aporta lo mejor: la agilidad de la startup y la estructura del equipo original", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Excelente. Co-crear la nueva cultura da ownership a ambos lados. No impones ni la tuya ni la de ellos: crean algo nuevo juntos. Esto resuelve la raíz del conflicto.", PuntosOtorgados = 30, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Imponer tus procesos actuales ya que tu equipo es el mayoritario", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Imponer cultura genera resentimiento y fugas de talento. Los mejores de la startup se irán porque no se sienten valorados.", PuntosOtorgados = 0, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Dejar que con el tiempo se adapten naturalmente", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "La integración cultural no ocurre por inercia. Sin intervención deliberada, los silos se refuerzan y el conflicto escala.", PuntosOtorgados = 10, Orden = 3 }
            }
        });

        // Escenario Inteligencia Emocional N3 (leccionId=79)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 79,
            TextoSituacion = "Debes despedir a Pedro, que además de ser un buen empleado con 3 años en la empresa, es amigo personal tuyo fuera del trabajo. La decisión viene de arriba y no es negociable.",
            Contexto = "Pedro no se lo espera. Su rendimiento es bueno pero su puesto se elimina por reestructuración.",
            GuionAudio = "Esta es la conversación más difícil que tendrás como líder. Tu amigo confía en ti.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Reunirte en privado, usar COIN, ser directo desde el primer minuto ('Pedro, lamento tener que darte esta noticia'), explicar la razón sin rodeos, validar sus emociones, ofrecer apoyo concreto (referencia, tiempo de transición) y después hablar como amigo fuera del trabajo", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Profesional y humano. Separas los roles (manager y amigo), eres directo pero empático, y ofreces apoyo tangible. La amistad puede sobrevivir si manejas esto con dignidad.", PuntosOtorgados = 30, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Pedirle a RRHH que dé la noticia para no dañar la amistad", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Delegar esta conversación es cobardía y Pedro lo percibirá como traición. Precisamente porque son amigos, merece escucharlo de ti.", PuntosOtorgados = 0, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Invitarlo a comer y suavizar la noticia con contexto extenso sobre la reestructuración", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "La intención es buena pero dar la noticia en un restaurante es incómodo (no puede reaccionar libremente) y rodear el tema genera ansiedad. Mejor directo y en privado.", PuntosOtorgados = 10, Orden = 3 }
            }
        });

        // Escenario Networking N3 (leccionId=84)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 84,
            TextoSituacion = "Te invitan a dar una charla de 20 minutos en una conferencia importante de tu industria. Hay 500 asistentes y entre ellos potenciales clientes, empleadores y colaboradores. Debes elegir tu tema y enfoque.",
            Contexto = "Tu expertise es en desarrollo de productos digitales. Nunca has dado una charla ante tanta gente.",
            GuionAudio = "Esta es tu oportunidad de personal branding en vivo. 20 minutos para posicionarte como referente.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Elegir un tema nicho donde eres referente, estructurar la charla como historia (un problema real que resolviste), incluir datos concretos, una lección aplicable, y cerrar con una call-to-action clara (conectar en LinkedIn, descargar un recurso)", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Perfecto enfoque de personal branding. Nicho específico, storytelling con datos, valor aplicable y CTA clara. La audiencia recordará tu nombre y tu expertise.", PuntosOtorgados = 30, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Hacer una presentación genérica sobre tendencias de la industria con muchas slides", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "Las presentaciones genéricas no te diferencian. Hablar de tendencias que todos conocen no te posiciona como experto. La historia personal es más memorable.", PuntosOtorgados = 10, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Rechazar la invitación porque no te sientes preparado", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Las oportunidades de visibilidad no se repiten fácilmente. El síndrome del impostor es normal pero no debe paralizarte. La preparación vence al miedo.", PuntosOtorgados = 0, Orden = 3 }
            }
        });

        // Escenario Persuasión N3 (leccionId=89)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 89,
            TextoSituacion = "Tu proveedor principal de infraestructura cloud anuncia un aumento del 40% en sus tarifas. Cambiar de proveedor tomaría 6 meses y $200K en migración. Tu contrato actual vence en 2 meses.",
            Contexto = "El proveedor sabe que eres un cliente importante (top 5% de facturación) pero también sabe que migrar es costoso para ti.",
            GuionAudio = "Negociación de alto impacto. Ambos tienen información sobre las limitaciones del otro.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Preparar tu BATNA (cotizar 2 proveedores alternativos), identificar tu ZOPA (aceptarías hasta 15% de aumento), negociar intereses no posiciones (proponer contrato largo a cambio de mejor tarifa, compromiso de volumen, caso de estudio público), y usar datos de mercado como criterio objetivo", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Método Harvard en acción. BATNA preparado, ZOPA identificada, negociación de intereses con opciones creativas y criterios objetivos. Esto maximiza tu poder y crea valor mutuo.", PuntosOtorgados = 30, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Aceptar el 40% para evitar el riesgo de migración", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Aceptar sin negociar es rendirse. El proveedor esperaba una negociación. Probablemente aceptarían mucho menos que el 40%.", PuntosOtorgados = 0, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Amenazar con irte inmediatamente si no bajan el precio", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "La amenaza puede funcionar pero es una posición, no un interés. Además, si no puedes cumplirla (6 meses de migración), pierdes credibilidad. Mejor negociar con BATNA real.", PuntosOtorgados = 10, Orden = 3 }
            }
        });

        await contexto.SaveChangesAsync();
    }
}
