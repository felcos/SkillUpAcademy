using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de lecciones Nivel 2 (Práctica) para las 6 áreas.
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesNivel2Async(AppDbContext contexto)
    {
        // Nivel 2 IDs: Comunicación=2, Liderazgo=5, TrabajoEquipo=8, IntEmocional=11, Networking=14, Persuasión=17
        // Lecciones N2 empiezan en ID 31 (N1 usa 1-30)
        int leccionId = 31;

        // ===== COMUNICACIÓN EFECTIVA — Nivel 2 (NivelId=2) =====
        Leccion comN2L1 = new Leccion
        {
            Id = leccionId++, NivelId = 2, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Comunicación no verbal en entornos profesionales",
            Descripcion = "Domina los mensajes silenciosos que envías con tu cuerpo, gestos y postura.",
            Contenido = "## El 93% del mensaje es no verbal\n\nAlbert Mehrabian demostró que en la comunicación emocional, solo el 7% del impacto viene de las palabras, el 38% del tono de voz y el 55% del lenguaje corporal.\n\n## Señales clave en el trabajo\n\n### Postura de poder\nHombros atrás, barbilla paralela al suelo, manos visibles. Transmite confianza sin decir una palabra.\n\n### Microexpresiones\nDuran menos de medio segundo pero revelan emociones reales. Aprende a leer la sorpresa, el desprecio y la alegría genuina.\n\n### Proxémica profesional\nLa distancia física comunica: zona íntima (0-45cm), personal (45cm-1.2m), social (1.2-3.6m). En reuniones, respeta la zona social.\n\n### Contacto visual\nMantén contacto 60-70% del tiempo. Menos parece evasivo, más puede resultar intimidante.",
            PuntosClave = "[\"El 93% de la comunicación emocional es no verbal\",\"La postura de poder transmite confianza automáticamente\",\"Las microexpresiones revelan emociones en menos de medio segundo\",\"El contacto visual óptimo es del 60-70% del tiempo\"]",
            GuionAudio = "Hoy vamos a descifrar el lenguaje silencioso del cuerpo. Porque en el trabajo, lo que no dices habla más fuerte que tus palabras.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 10
        };

        Leccion comN2L2 = new Leccion
        {
            Id = leccionId++, NivelId = 2, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Feedback constructivo: el arte de la crítica que transforma",
            Descripcion = "Aprende a dar y recibir retroalimentación que impulse el crecimiento.",
            Contenido = "## Por qué tememos el feedback\n\nDar feedback activa el sistema de amenaza del receptor. La clave es hacerlo de forma que active el sistema de recompensa: crecimiento, no castigo.\n\n## El modelo SBI (Situación-Comportamiento-Impacto)\n\n### Situación\n«En la reunión del martes con el cliente...»\n\n### Comportamiento\n«...noté que interrumpiste tres veces a María...»\n\n### Impacto\n«...lo que hizo que perdiera el hilo de su propuesta y el cliente se confundiera.»\n\n## Feedback positivo: el refuerzo olvidado\nPor cada crítica constructiva, necesitas 5 reconocimientos positivos (ratio de Losada). No es adulación, es neuro-ciencia.\n\n## Recibir feedback como un profesional\n1. Escucha sin defenderte\n2. Agradece (genuinamente)\n3. Pide ejemplos concretos\n4. Decide qué implementar\n5. Haz seguimiento",
            PuntosClave = "[\"El modelo SBI estructura el feedback: Situación + Comportamiento + Impacto\",\"El ratio de Losada: 5 positivos por cada 1 constructivo\",\"Recibir feedback requiere escuchar sin defenderse\",\"El feedback efectivo activa el sistema de recompensa, no el de amenaza\"]",
            GuionAudio = "El feedback es un regalo cuando se da bien. Hoy aprenderás el modelo SBI y por qué necesitas cinco elogios por cada crítica.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 10
        };

        Leccion comN2L3 = new Leccion
        {
            Id = leccionId++, NivelId = 2, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Comunicación Avanzada",
            Descripcion = "Evalúa tu dominio de la comunicación no verbal y el feedback constructivo.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion comN2L4 = new Leccion
        {
            Id = leccionId++, NivelId = 2, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Feedback al compañero que siempre llega tarde",
            Descripcion = "Tu compañero de equipo lleva tres semanas llegando tarde a las reuniones. Necesitas abordar la situación.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 10
        };

        Leccion comN2L5 = new Leccion
        {
            Id = leccionId++, NivelId = 2, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Negociación salarial",
            Descripcion = "Practica cómo comunicar tu valor y negociar un aumento con tu manager.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(comN2L1, comN2L2, comN2L3, comN2L4, comN2L5);

        // ===== LIDERAZGO — Nivel 2 (NivelId=5) =====
        Leccion lidN2L1 = new Leccion
        {
            Id = leccionId++, NivelId = 5, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Liderazgo situacional: adapta tu estilo",
            Descripcion = "No hay un solo estilo de liderazgo. Aprende cuándo dirigir, entrenar, apoyar o delegar.",
            Contenido = "## El modelo de Hersey y Blanchard\n\nEl liderazgo efectivo depende de la madurez del colaborador. No lideras igual a un junior que a un senior.\n\n## Los 4 estilos\n\n### S1: Dirigir\nAlta tarea, baja relación. Para personas nuevas con poca experiencia. Da instrucciones claras y supervisa de cerca.\n\n### S2: Entrenar\nAlta tarea, alta relación. Para personas con algo de experiencia pero que necesitan guía. Explica el porqué, pide opinión.\n\n### S3: Apoyar\nBaja tarea, alta relación. Para personas competentes pero inseguras. Da confianza, facilita decisiones.\n\n### S4: Delegar\nBaja tarea, baja relación. Para personas expertas y motivadas. Confía, da autonomía, pide resultados.\n\n## La trampa del líder\nMuchos líderes se quedan en S1 o S4 para siempre. El verdadero liderazgo es diagnosticar y adaptar continuamente.",
            PuntosClave = "[\"El liderazgo situacional tiene 4 estilos: Dirigir, Entrenar, Apoyar y Delegar\",\"El estilo depende de la madurez del colaborador\",\"No hay un estilo universalmente mejor\",\"La trampa común es usar siempre el mismo estilo\"]",
            GuionAudio = "¿Lideras igual a todos? Hoy aprenderás por qué los mejores líderes cambian de estilo según la persona y el momento.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 10
        };

        Leccion lidN2L2 = new Leccion
        {
            Id = leccionId++, NivelId = 5, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Gestión de conflictos en equipos",
            Descripcion = "Transforma los conflictos de destructivos a productivos con técnicas probadas.",
            Contenido = "## El conflicto no es malo\n\nPatrick Lencioni identifica la ausencia de conflicto como una disfunción de equipo. Los equipos que evitan el conflicto toman peores decisiones.\n\n## Los 5 estilos de manejo de conflictos (Thomas-Kilmann)\n\n### Competir\nYo gano, tú pierdes. Útil en emergencias o cuando hay principios no negociables.\n\n### Colaborar\nGanamos los dos. Ideal cuando hay tiempo y ambas partes pueden ganar. Requiere creatividad.\n\n### Comprometer\nCedemos los dos. Rápido y justo, pero ninguno queda completamente satisfecho.\n\n### Evitar\nNadie gana. Solo válido cuando el tema es trivial o necesitas tiempo para pensar.\n\n### Acomodar\nTú ganas, yo cedo. Útil para preservar relaciones o cuando el otro tiene razón.\n\n## El framework DESC para conversaciones difíciles\n**D**escribe la situación, **E**xpresa cómo te afecta, **S**ugiere una solución, **C**onsecuencias positivas.",
            PuntosClave = "[\"La ausencia de conflicto es una disfunción de equipo\",\"Thomas-Kilmann: 5 estilos según cooperación y asertividad\",\"Colaborar es ideal pero requiere tiempo y creatividad\",\"DESC: Describir, Expresar, Sugerir, Consecuencias\"]",
            GuionAudio = "El conflicto bien gestionado produce mejores decisiones. Hoy aprenderás 5 estilos y cuándo usar cada uno.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 10
        };

        Leccion lidN2L3 = new Leccion
        {
            Id = leccionId++, NivelId = 5, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Liderazgo Situacional",
            Descripcion = "Demuestra tu conocimiento sobre estilos de liderazgo y gestión de conflictos.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion lidN2L4 = new Leccion
        {
            Id = leccionId++, NivelId = 5, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Dos miembros del equipo en conflicto",
            Descripcion = "Dos desarrolladores senior no se ponen de acuerdo sobre la arquitectura del proyecto. Como líder, debes intervenir.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 10
        };

        Leccion lidN2L5 = new Leccion
        {
            Id = leccionId++, NivelId = 5, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Tu primer 1-on-1 como manager",
            Descripcion = "Practica cómo llevar una reunión uno a uno efectiva con un nuevo miembro del equipo.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(lidN2L1, lidN2L2, lidN2L3, lidN2L4, lidN2L5);

        // ===== TRABAJO EN EQUIPO — Nivel 2 (NivelId=8) =====
        Leccion teqN2L1 = new Leccion
        {
            Id = leccionId++, NivelId = 8, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Dinámicas de equipo de alto rendimiento",
            Descripcion = "Descubre qué diferencia a un grupo de trabajo de un equipo de alto rendimiento.",
            Contenido = "## El modelo de Tuckman\n\nTodos los equipos pasan por 4 fases:\n\n### Forming (Formación)\nLos miembros son educados, cautelosos. Buscan entender las reglas.\n\n### Storming (Tormenta)\nSurgen conflictos, luchas de poder. Es la fase más difícil pero más necesaria.\n\n### Norming (Normalización)\nSe establecen acuerdos, roles claros, confianza. El equipo empieza a fluir.\n\n### Performing (Desempeño)\nEl equipo es autónomo, productivo, se autocorrige. No todos los equipos llegan aquí.\n\n## Las 5 disfunciones de un equipo (Lencioni)\n1. **Ausencia de confianza** — no se muestran vulnerables\n2. **Miedo al conflicto** — armonía artificial\n3. **Falta de compromiso** — ambigüedad en decisiones\n4. **Evasión de responsabilidad** — no se exigen entre sí\n5. **Desatención a resultados** — ego sobre equipo\n\n## Seguridad psicológica (Google Project Aristotle)\nEl factor #1 de los equipos exitosos de Google: sentirse seguro para tomar riesgos.",
            PuntosClave = "[\"Modelo Tuckman: Forming → Storming → Norming → Performing\",\"La fase de tormenta es necesaria para el crecimiento\",\"Las 5 disfunciones de Lencioni comienzan con falta de confianza\",\"La seguridad psicológica es el factor #1 de equipos exitosos\"]",
            GuionAudio = "¿Por qué algunos equipos brillan y otros solo sobreviven? Hoy exploramos los modelos que explican la diferencia.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 10
        };

        Leccion teqN2L2 = new Leccion
        {
            Id = leccionId++, NivelId = 8, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Colaboración remota efectiva",
            Descripcion = "Estrategias probadas para que tu equipo distribuido funcione como si estuviera en la misma oficina.",
            Contenido = "## El reto del trabajo remoto\n\nLos equipos remotos pierden las interacciones informales que construyen confianza. Sin intención, la cultura se erosiona.\n\n## Prácticas que funcionan\n\n### Comunicación asíncrona primero\nNo todo necesita una reunión. Documenta decisiones, usa hilos en chat, escribe antes de llamar.\n\n### Rituales de conexión\n- Daily standup (15 min máx)\n- Café virtual semanal (sin agenda de trabajo)\n- Demo Friday (muestra lo que hiciste)\n\n### Documentación como cultura\nSi no está escrito, no existe. Decisiones, procesos, contexto — todo documentado.\n\n### Zonas horarias y respeto\nDefine ventanas de overlap. Respeta el tiempo off. Nunca asumas que alguien está disponible.\n\n## Herramientas ≠ Solución\nNinguna herramienta arregla mala comunicación. Primero los hábitos, luego las herramientas.",
            PuntosClave = "[\"Comunicación asíncrona debe ser la norma, no la excepción\",\"Los rituales de conexión reemplazan las interacciones informales\",\"Si no está documentado, no existe\",\"Las herramientas no arreglan mala comunicación\"]",
            GuionAudio = "El trabajo remoto no es solo estar en casa con una laptop. Requiere prácticas deliberadas para mantener al equipo conectado.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 10
        };

        Leccion teqN2L3 = new Leccion
        {
            Id = leccionId++, NivelId = 8, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Equipos de Alto Rendimiento",
            Descripcion = "Evalúa tu conocimiento sobre dinámicas de equipo y colaboración remota.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion teqN2L4 = new Leccion
        {
            Id = leccionId++, NivelId = 8, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: El miembro del equipo que no colabora",
            Descripcion = "Un compañero trabaja solo, no comparte información y bloquea al equipo. ¿Cómo abordas la situación?",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 10
        };

        Leccion teqN2L5 = new Leccion
        {
            Id = leccionId++, NivelId = 8, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Facilitador de retrospectiva",
            Descripcion = "Facilita una retrospectiva de sprint donde el equipo está desmotivado por un lanzamiento fallido.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(teqN2L1, teqN2L2, teqN2L3, teqN2L4, teqN2L5);

        // ===== INTELIGENCIA EMOCIONAL — Nivel 2 (NivelId=11) =====
        Leccion ieN2L1 = new Leccion
        {
            Id = leccionId++, NivelId = 11, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Regulación emocional bajo presión",
            Descripcion = "Técnicas para mantener la calma y tomar buenas decisiones cuando todo se complica.",
            Contenido = "## El secuestro amigdalar\n\nCuando sientes una amenaza (un email agresivo, un deadline imposible), tu amígdala reacciona antes que tu corteza prefrontal. Pasas de pensar a reaccionar.\n\n## Técnicas de regulación inmediata\n\n### Respiración 4-7-8\nInhala 4 segundos, retén 7, exhala 8. Activa el sistema parasimpático en 60 segundos.\n\n### Etiquetado emocional\nNombra lo que sientes: «Estoy frustrado porque...». Solo nombrar la emoción reduce su intensidad un 50% (estudios de UCLA).\n\n### Distanciamiento temporal\n«Voy a responder a esto en 30 minutos.» No reacciones en caliente.\n\n### Reevaluación cognitiva\nCambia la narrativa: de «Esto es un desastre» a «Esto es un desafío que puedo resolver».\n\n## La ventana de tolerancia\nCada persona tiene un rango donde funciona bien. Fuera de esa ventana, entras en hiper-activación (ansiedad) o hipo-activación (desconexión). Conoce tu ventana.",
            PuntosClave = "[\"El secuestro amigdalar hace que reacciones antes de pensar\",\"Respiración 4-7-8 calma el sistema nervioso en 60 segundos\",\"Nombrar la emoción reduce su intensidad un 50%\",\"La ventana de tolerancia define tu rango de funcionamiento óptimo\"]",
            GuionAudio = "Cuando sientes que vas a explotar en una reunión, tu amígdala te secuestró. Hoy aprenderás a recuperar el control en segundos.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 10
        };

        Leccion ieN2L2 = new Leccion
        {
            Id = leccionId++, NivelId = 11, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Empatía avanzada: leer emociones en otros",
            Descripcion = "Desarrolla la capacidad de percibir y responder a las emociones de tus colegas.",
            Contenido = "## Empatía cognitiva vs. empatía emocional\n\n**Cognitiva**: entiendes intelectualmente lo que siente el otro. «Comprendo que estés frustrado.»\n**Emocional**: sientes lo que el otro siente. Útil pero agotante si no se gestiona.\n\nEn el trabajo necesitas ambas, dosificadas.\n\n## Señales que la mayoría ignora\n\n### Cambios de energía\nSi alguien que siempre participa se calla, algo pasa. Si alguien que es tranquilo se agita, algo le afecta.\n\n### El tono detrás del texto\nEn mensajes escritos, busca cambios: respuestas más cortas, ausencia de emojis habituales, «Ok.» en lugar de «¡Ok, perfecto!»\n\n### Incongruencias\nCuando alguien dice «Estoy bien» pero su cuerpo dice otra cosa, créele al cuerpo.\n\n## Respuesta empática efectiva\n1. Valida: «Tiene sentido que te sientas así»\n2. No arregles (aún): «¿Quieres que hablemos o prefieres una solución?»\n3. Ofrece apoyo concreto: «¿Cómo puedo ayudarte?»",
            PuntosClave = "[\"Empatía cognitiva = entender; empatía emocional = sentir\",\"Los cambios de energía son señales tempranas de problemas\",\"En comunicación escrita, cambios sutiles revelan mucho\",\"Validar antes de resolver: preguntar qué necesita el otro\"]",
            GuionAudio = "La empatía no es solo sentir, es percibir. Hoy desarrollarás tu radar emocional para leer lo que otros no dicen.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 10
        };

        Leccion ieN2L3 = new Leccion
        {
            Id = leccionId++, NivelId = 11, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Inteligencia Emocional Aplicada",
            Descripcion = "Pon a prueba tu capacidad para regular emociones y leer a los demás.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion ieN2L4 = new Leccion
        {
            Id = leccionId++, NivelId = 11, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: El colega al borde del burnout",
            Descripcion = "Un compañero muestra señales de agotamiento extremo. ¿Cómo abordas la conversación?",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 10
        };

        Leccion ieN2L5 = new Leccion
        {
            Id = leccionId++, NivelId = 11, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Dar malas noticias con empatía",
            Descripcion = "Practica cómo comunicar a tu equipo que se cancela un proyecto en el que han trabajado meses.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(ieN2L1, ieN2L2, ieN2L3, ieN2L4, ieN2L5);

        // ===== NETWORKING — Nivel 2 (NivelId=14) =====
        Leccion netN2L1 = new Leccion
        {
            Id = leccionId++, NivelId = 14, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Networking estratégico: calidad sobre cantidad",
            Descripcion = "Construye una red profesional intencional que genere oportunidades reales.",
            Contenido = "## Networking no es coleccionar contactos\n\nTener 500+ contactos en LinkedIn no es networking. Tener 20 relaciones genuinas donde puedes pedir ayuda y ofrecer valor: eso es networking.\n\n## El modelo de red estratégica\n\n### Tu círculo interno (5-10 personas)\nMentores, aliados cercanos, peers de confianza. Inviertes tiempo regular en estas relaciones.\n\n### Tu círculo extendido (30-50 personas)\nContactos de industria, ex-compañeros, conocidos relevantes. Contacto trimestral.\n\n### Tu red latente (100+ personas)\nContactos débiles que pueden activarse. El estudio de Granovetter demostró que los vínculos débiles generan más oportunidades que los fuertes.\n\n## La regla del 5-1\nPor cada favor que pidas, ofrece al menos 5 actos de valor: compartir artículos, hacer introducciones, dar feedback, recomendar.\n\n## Follow-up: donde muere el networking\nEl 80% de las conexiones muere por falta de seguimiento. Envía un mensaje dentro de 48h después de conocer a alguien.",
            PuntosClave = "[\"Networking de calidad supera al de cantidad\",\"Los vínculos débiles generan más oportunidades (Granovetter)\",\"Regla 5-1: ofrece 5 veces antes de pedir 1\",\"El follow-up en 48h es crucial para mantener conexiones\"]",
            GuionAudio = "¿Cuántos de tus 500 contactos de LinkedIn te responderían en 24 horas? Hoy aprenderás a construir una red que realmente funciona.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 10
        };

        Leccion netN2L2 = new Leccion
        {
            Id = leccionId++, NivelId = 14, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "El arte de la conversación profesional",
            Descripcion = "Domina las técnicas para iniciar, mantener y cerrar conversaciones valiosas en eventos y reuniones.",
            Contenido = "## El miedo a la primera frase\n\nLa mayoría evita iniciar conversaciones por miedo al rechazo. La realidad: el 90% de las personas en un evento también quieren hablar con alguien.\n\n## Técnicas de apertura\n\n### La pregunta contextual\n«¿Qué te trae a este evento?» Simple, universal, abre la puerta.\n\n### La observación compartida\n«La presentación del keynote fue intensa, ¿qué te pareció?» Crea un punto en común.\n\n### El cumplido específico\n«Vi tu artículo sobre X, me pareció muy acertado el punto sobre Y.» Demuestra interés genuino.\n\n## Mantener la conversación: FORD\n- **F**amily (contexto personal ligero)\n- **O**ccupation (trabajo, proyectos)\n- **R**ecreation (intereses, hobbies)\n- **D**reams (metas, aspiraciones)\n\n## Cerrar con elegancia\n«Ha sido genial hablar contigo. ¿Te parece si nos conectamos en LinkedIn para seguir la conversación?»",
            PuntosClave = "[\"El 90% de las personas en eventos también quieren conversar\",\"FORD: Family, Occupation, Recreation, Dreams\",\"Los cumplidos específicos son más efectivos que los genéricos\",\"Cerrar proponiendo un siguiente paso concreto\"]",
            GuionAudio = "Iniciar una conversación con un desconocido puede ser intimidante. Pero con las técnicas correctas, se vuelve natural.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 10
        };

        Leccion netN2L3 = new Leccion
        {
            Id = leccionId++, NivelId = 14, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Networking Estratégico",
            Descripcion = "Demuestra tu conocimiento sobre networking profesional.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion netN2L4 = new Leccion
        {
            Id = leccionId++, NivelId = 14, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: El evento de networking",
            Descripcion = "Estás en una conferencia y ves a alguien que trabaja en la empresa de tus sueños. ¿Cómo te acercas?",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 10
        };

        Leccion netN2L5 = new Leccion
        {
            Id = leccionId++, NivelId = 14, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Pedir una introducción profesional",
            Descripcion = "Practica cómo pedirle a un contacto que te presente a alguien importante de su red.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(netN2L1, netN2L2, netN2L3, netN2L4, netN2L5);

        // ===== PERSUASIÓN — Nivel 2 (NivelId=17) =====
        Leccion perN2L1 = new Leccion
        {
            Id = leccionId++, NivelId = 17, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Los 6 principios de influencia de Cialdini",
            Descripcion = "Domina los principios psicológicos que hacen que la gente diga que sí.",
            Contenido = "## Robert Cialdini y la ciencia de la persuasión\n\nCialdini identificó 6 principios universales que activan respuestas automáticas de aceptación.\n\n## Los 6 principios\n\n### 1. Reciprocidad\nCuando alguien te da algo, sientes la obligación de devolver. Da valor primero.\n\n### 2. Compromiso y coherencia\nLas personas quieren ser consistentes con lo que ya dijeron o hicieron. Empieza con compromisos pequeños.\n\n### 3. Prueba social\n«Si otros lo hacen, debe ser correcto.» Muestra testimonios, casos de éxito, números.\n\n### 4. Autoridad\nLas personas siguen a expertos. Establece tu credibilidad antes de persuadir.\n\n### 5. Simpatía\nCompramos de quien nos cae bien. Busca similitudes, da cumplidos genuinos, coopera.\n\n### 6. Escasez\n«Quedan 3 plazas.» Lo limitado se percibe como más valioso.\n\n## Uso ético\nEstos principios son herramientas, no armas. La persuasión ética busca beneficio mutuo, no manipulación.",
            PuntosClave = "[\"Cialdini: Reciprocidad, Compromiso, Prueba social, Autoridad, Simpatía, Escasez\",\"La reciprocidad es el principio más poderoso: da valor primero\",\"La prueba social funciona porque seguimos al grupo\",\"Persuasión ética = beneficio mutuo, no manipulación\"]",
            GuionAudio = "Robert Cialdini descubrió seis botones psicológicos que activan el sí. Hoy los aprenderás para usarlos con ética.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 10
        };

        Leccion perN2L2 = new Leccion
        {
            Id = leccionId++, NivelId = 17, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Storytelling persuasivo: vende ideas con historias",
            Descripcion = "Aprende a estructurar narrativas que conecten emocionalmente y muevan a la acción.",
            Contenido = "## Por qué funcionan las historias\n\nCuando escuchas datos, se activan 2 áreas del cerebro. Cuando escuchas una historia, se activan 7. Las historias sincronizan el cerebro del narrador con el del oyente.\n\n## La estructura del héroe corporativo\n\n### 1. El contexto\n«Teníamos un problema: nuestros clientes abandonaban el carrito en un 78%.»\n\n### 2. El desafío\n«Probamos 3 soluciones y ninguna funcionó. El equipo estaba frustrado.»\n\n### 3. El momento de cambio\n«Entonces Ana del equipo de UX sugirió algo que nadie había considerado...»\n\n### 4. La resolución\n«En 3 meses, el abandono bajó al 34%. Y aprendimos que escuchar al usuario es más importante que nuestras suposiciones.»\n\n### 5. La lección aplicable\n«Esta experiencia nos enseñó que la solución casi siempre está más cerca de lo que pensamos.»\n\n## La regla de 1\nUna historia, un dato, un héroe, un mensaje. La audiencia recuerda una cosa. Elige cuál.",
            PuntosClave = "[\"Las historias activan 7 áreas del cerebro vs 2 de los datos\",\"Estructura: Contexto → Desafío → Cambio → Resolución → Lección\",\"La regla de 1: un mensaje claro por historia\",\"Las historias sincronizan cerebros entre narrador y oyente\"]",
            GuionAudio = "Los datos convencen, pero las historias mueven. Hoy aprenderás a contar historias que vendan tus ideas.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 10
        };

        Leccion perN2L3 = new Leccion
        {
            Id = leccionId++, NivelId = 17, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Persuasión e Influencia",
            Descripcion = "Evalúa tu dominio de los principios de Cialdini y el storytelling persuasivo.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion perN2L4 = new Leccion
        {
            Id = leccionId++, NivelId = 17, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Convencer al director de aprobar tu presupuesto",
            Descripcion = "Tu proyecto necesita $50K adicionales. El director es escéptico. ¿Cómo lo convences?",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 10
        };

        Leccion perN2L5 = new Leccion
        {
            Id = leccionId++, NivelId = 17, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Pitch de producto en 3 minutos",
            Descripcion = "Practica un elevator pitch persuasivo para un producto nuevo ante inversores.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(perN2L1, perN2L2, perN2L3, perN2L4, perN2L5);

        await contexto.SaveChangesAsync();

        // ===== ESCENAS TEÓRICAS NIVEL 2 =====

        // Escenas Comunicación N2
        await SembrarEscenasDesdeLeccionAsync(contexto, comN2L1,
            "Hoy llevaremos la comunicación al siguiente nivel: aprenderás a leer y controlar los mensajes que envías sin decir una palabra.",
            "Esta semana, observa tu postura en las reuniones. Mantén las manos visibles y el contacto visual al 60-70%.");

        await SembrarEscenasDesdeLeccionAsync(contexto, comN2L2,
            "Dar feedback es un arte que transforma equipos. Hoy aprenderás una fórmula probada para que tu retroalimentación impulse el crecimiento.",
            "Practica el modelo SBI esta semana: describe la situación, señala el comportamiento y explica el impacto concreto.");

        // Escenas Liderazgo N2
        await SembrarEscenasDesdeLeccionAsync(contexto, lidN2L1,
            "No existe un estilo de liderazgo perfecto. Hoy descubrirás por qué los mejores líderes cambian de enfoque según la persona y el contexto.",
            "Identifica a alguien de tu equipo y piensa qué estilo necesita hoy. ¿Dirigir, entrenar, apoyar o delegar?");

        await SembrarEscenasDesdeLeccionAsync(contexto, lidN2L2,
            "Los equipos que evitan el conflicto toman peores decisiones. Hoy aprenderás a transformar fricciones en resultados productivos.",
            "La próxima vez que surja un desacuerdo, usa el framework DESC: Describe, Expresa, Sugiere y plantea Consecuencias positivas.");

        // Escenas Trabajo en Equipo N2
        await SembrarEscenasDesdeLeccionAsync(contexto, teqN2L1,
            "¿Qué separa a un equipo promedio de uno extraordinario? Hoy exploraremos los modelos que explican por qué algunos equipos brillan.",
            "Evalúa en qué fase está tu equipo según Tuckman y piensa qué acción concreta lo llevaría a la siguiente fase.");

        await SembrarEscenasDesdeLeccionAsync(contexto, teqN2L2,
            "El trabajo remoto no es solo estar en casa con una laptop. Hoy aprenderás prácticas deliberadas para mantener al equipo conectado y productivo.",
            "Implementa un ritual de conexión semanal con tu equipo: un café virtual sin agenda de trabajo donde simplemente conversen.");

        // Escenas Inteligencia Emocional N2
        await SembrarEscenasDesdeLeccionAsync(contexto, ieN2L1,
            "Cuando tu amígdala se activa, pierdes la capacidad de pensar con claridad. Hoy aprenderás técnicas para recuperar el control en situaciones de alta presión.",
            "Practica la respiración 4-7-8 antes de tu próxima reunión difícil: inhala 4 segundos, retén 7 y exhala 8.");

        await SembrarEscenasDesdeLeccionAsync(contexto, ieN2L2,
            "La empatía no es solo sentir lo que otros sienten, es percibir lo que no dicen. Hoy afinarás tu capacidad de leer emociones en los demás.",
            "Esta semana, antes de ofrecer soluciones, pregunta: ¿quieres que hablemos o prefieres que busquemos una solución?");

        // Escenas Networking N2
        await SembrarEscenasDesdeLeccionAsync(contexto, netN2L1,
            "¿Cuántos de tus contactos te responderían en 24 horas? Hoy aprenderás a construir una red intencional que genere oportunidades reales.",
            "Envía un mensaje de valor a alguien de tu red esta semana: comparte un artículo, haz una recomendación o conecta a dos personas.");

        await SembrarEscenasDesdeLeccionAsync(contexto, netN2L2,
            "Iniciar una conversación con un desconocido puede ser intimidante, pero con las técnicas correctas se vuelve natural.",
            "En tu próximo evento profesional, usa una pregunta contextual para abrir conversación y cierra proponiendo conectar en LinkedIn.");

        // Escenas Persuasión N2
        await SembrarEscenasDesdeLeccionAsync(contexto, perN2L1,
            "Robert Cialdini descubrió seis principios que activan respuestas automáticas de aceptación. Hoy los dominarás para usarlos con ética.",
            "Esta semana, aplica la reciprocidad: ofrece algo de valor a un colega antes de pedirle ayuda con tu proyecto.");

        await SembrarEscenasDesdeLeccionAsync(contexto, perN2L2,
            "Los datos convencen, pero las historias mueven. Hoy aprenderás a estructurar narrativas que conecten emocionalmente y muevan a la acción.",
            "Prepara una historia de tu experiencia profesional usando la estructura del héroe corporativo y úsala en tu próxima presentación.");

        // ===== QUIZZES NIVEL 2 =====
        await SembrarQuizzesNivel2Async(contexto);

        // ===== ESCENARIOS NIVEL 2 =====
        await SembrarEscenariosNivel2Async(contexto);
    }

    private static async Task SembrarQuizzesNivel2Async(AppDbContext contexto)
    {
        // Quiz Comunicación N2 (leccionId=33)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(33, 1, "Según Mehrabian, ¿qué porcentaje de la comunicación emocional es no verbal?",
                "Albert Mehrabian demostró que el 93% del impacto viene del tono y lenguaje corporal.",
                new[] {
                    ("55%", false, "Ese es solo el porcentaje del lenguaje corporal"),
                    ("93%", true, "¡Correcto! 55% cuerpo + 38% tono = 93% no verbal"),
                    ("50%", false, "El porcentaje real es significativamente mayor"),
                    ("75%", false, "Es aún mayor que eso")
                }),
            CrearPregunta(33, 2, "¿Qué es el modelo SBI para dar feedback?",
                "SBI = Situación + Comportamiento + Impacto.",
                new[] {
                    ("Sistema-Base-Información", false, "SBI no corresponde a esas siglas"),
                    ("Situación-Comportamiento-Impacto", true, "¡Exacto! Describe la situación, el comportamiento observado y su impacto"),
                    ("Simple-Breve-Inmediato", false, "Aunque son buenas prácticas, no es el modelo SBI"),
                    ("Sujeto-Beneficio-Instrucción", false, "Esas no son las siglas correctas")
                }),
            CrearPregunta(33, 3, "¿Cuál es el ratio de Losada para feedback?",
                "Se necesitan 5 comentarios positivos por cada 1 constructivo.",
                new[] {
                    ("3 positivos por 1 negativo", false, "El ratio es mayor"),
                    ("5 positivos por 1 constructivo", true, "¡Correcto! 5:1 es el ratio que mantiene relaciones productivas"),
                    ("1 a 1", false, "Ese ratio no genera suficiente confianza"),
                    ("10 a 1", false, "Eso sería excesivo y poco auténtico")
                }),
            CrearPregunta(33, 4, "¿Cuál es el porcentaje óptimo de contacto visual en una conversación?",
                "El contacto visual óptimo es del 60-70% del tiempo.",
                new[] {
                    ("90-100%", false, "Eso resultaría intimidante"),
                    ("30-40%", false, "Muy poco, puede parecer evasivo"),
                    ("60-70%", true, "¡Correcto! Suficiente para mostrar interés sin intimidar"),
                    ("50%", false, "Un poco bajo, lo ideal es algo más")
                }),
            CrearPregunta(33, 5, "¿Qué paso NO es parte de recibir feedback como profesional?",
                "Defenderse no es parte del proceso; lo correcto es escuchar sin defenderse.",
                new[] {
                    ("Escuchar sin defenderte", false, "Este sí es un paso correcto"),
                    ("Agradecer genuinamente", false, "Este sí es un paso correcto"),
                    ("Justificar inmediatamente tu acción", true, "¡Correcto! Justificarse es lo opuesto a recibir feedback bien"),
                    ("Pedir ejemplos concretos", false, "Este sí es un paso correcto")
                })
        );

        // Quiz Liderazgo N2 (leccionId=38)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(38, 1, "En el liderazgo situacional, ¿qué estilo usarías con un empleado nuevo sin experiencia?",
                "Para personas nuevas con poca experiencia se usa S1: Dirigir.",
                new[] {
                    ("Delegar", false, "Delegar es para personas expertas y motivadas"),
                    ("Dirigir", true, "¡Correcto! Alta tarea, baja relación para guiar al nuevo"),
                    ("Apoyar", false, "Apoyar es para personas competentes pero inseguras"),
                    ("Entrenar", false, "Entrenar es para personas con algo de experiencia")
                }),
            CrearPregunta(38, 2, "Según Thomas-Kilmann, ¿cuándo es válido el estilo de evitar conflictos?",
                "Evitar solo es válido cuando el tema es trivial o necesitas tiempo para pensar.",
                new[] {
                    ("Siempre, el conflicto es negativo", false, "La ausencia de conflicto es una disfunción"),
                    ("Nunca, hay que enfrentar todo", false, "A veces evitar es estratégico"),
                    ("Cuando el tema es trivial o necesitas tiempo", true, "¡Correcto! Es una estrategia temporal válida"),
                    ("Cuando no tienes autoridad", false, "La autoridad no determina cuándo evitar")
                }),
            CrearPregunta(38, 3, "¿Qué significa la D en el framework DESC?",
                "DESC = Describir, Expresar, Sugerir, Consecuencias.",
                new[] {
                    ("Decidir", false, "La D es por Describir la situación objetivamente"),
                    ("Describir", true, "¡Correcto! Describe la situación de forma objetiva"),
                    ("Dialogar", false, "DESC empieza con una descripción, no un diálogo"),
                    ("Diagnosticar", false, "No se trata de diagnosticar sino de describir")
                }),
            CrearPregunta(38, 4, "¿Qué identifica Lencioni como la primera disfunción de un equipo?",
                "La base de todas las disfunciones es la ausencia de confianza.",
                new[] {
                    ("Miedo al conflicto", false, "Es la segunda disfunción, consecuencia de la primera"),
                    ("Ausencia de confianza", true, "¡Correcto! Sin confianza, todas las demás disfunciones aparecen"),
                    ("Falta de compromiso", false, "Es la tercera disfunción"),
                    ("Evasión de responsabilidad", false, "Es la cuarta disfunción")
                }),
            CrearPregunta(38, 5, "¿Cuál es la trampa más común del líder según el liderazgo situacional?",
                "Muchos líderes usan siempre el mismo estilo sin adaptarse.",
                new[] {
                    ("Delegar demasiado pronto", false, "Es un error pero no la trampa más común"),
                    ("Usar siempre el mismo estilo", true, "¡Correcto! El liderazgo efectivo requiere adaptación continua"),
                    ("Cambiar de estilo muy rápido", false, "Lo problemático es no cambiar nunca"),
                    ("No tener estilo definido", false, "Lo problemático es tener solo uno")
                })
        );

        // Quiz Trabajo en Equipo N2 (leccionId=43)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(43, 1, "¿Cuál es la fase más difícil del modelo de Tuckman?",
                "Storming es la fase de conflictos y luchas de poder.",
                new[] {
                    ("Forming", false, "Es la fase inicial, incómoda pero no la más difícil"),
                    ("Storming", true, "¡Correcto! Es donde surgen los conflictos necesarios"),
                    ("Norming", false, "En esta fase el equipo ya está mejorando"),
                    ("Performing", false, "Es la fase de máximo rendimiento")
                }),
            CrearPregunta(43, 2, "Según Google Project Aristotle, ¿cuál es el factor #1 de los equipos exitosos?",
                "La seguridad psicológica es el factor más importante.",
                new[] {
                    ("Talento individual", false, "El talento importa pero no es el factor #1"),
                    ("Seguridad psicológica", true, "¡Correcto! Sentirse seguro para tomar riesgos"),
                    ("Buen salario", false, "La compensación no fue el factor determinante"),
                    ("Liderazgo fuerte", false, "Importante, pero no el factor #1")
                }),
            CrearPregunta(43, 3, "¿Qué debe ser la norma en equipos remotos?",
                "La comunicación asíncrona debe ser la norma.",
                new[] {
                    ("Videollamadas constantes", false, "Produce fatiga y no respeta zonas horarias"),
                    ("Comunicación asíncrona", true, "¡Correcto! No todo necesita una reunión en tiempo real"),
                    ("Mensajes instantáneos", false, "Los mensajes instantáneos son síncronos por naturaleza"),
                    ("Emails formales", false, "Los emails no son el canal más eficiente")
                }),
            CrearPregunta(43, 4, "¿Cuántas fases tiene el modelo de Tuckman?",
                "Forming, Storming, Norming, Performing = 4 fases.",
                new[] {
                    ("3", false, "Son más de 3 fases"),
                    ("4", true, "¡Correcto! Forming, Storming, Norming, Performing"),
                    ("5", false, "A veces se añade Adjourning, pero el modelo clásico tiene 4"),
                    ("6", false, "Son menos de 6")
                }),
            CrearPregunta(43, 5, "«Si no está escrito, no existe» se refiere a...",
                "En equipos remotos, la documentación es cultura.",
                new[] {
                    ("Que no se deben tomar decisiones verbales", false, "Se pueden tomar, pero deben documentarse"),
                    ("La importancia de la documentación en equipos remotos", true, "¡Correcto! Documentar es esencial para equipos distribuidos"),
                    ("Que los contratos deben ser escritos", false, "Se refiere a la comunicación interna del equipo"),
                    ("Que hay que escribir más emails", false, "No se trata de emails sino de documentación")
                })
        );

        // Quiz Inteligencia Emocional N2 (leccionId=48)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(48, 1, "¿Qué es el secuestro amigdalar?",
                "La amígdala reacciona antes que la corteza prefrontal ante amenazas.",
                new[] {
                    ("Una técnica de meditación", false, "Es un fenómeno neurológico, no una técnica"),
                    ("Cuando la amígdala reacciona antes que el pensamiento racional", true, "¡Correcto! Pasas de pensar a reaccionar automáticamente"),
                    ("Un tipo de ansiedad crónica", false, "Es una reacción momentánea, no crónica"),
                    ("La pérdida de memoria bajo estrés", false, "No se refiere a la memoria sino a la reactividad")
                }),
            CrearPregunta(48, 2, "¿Cuánto reduce la intensidad emocional el simple hecho de nombrar la emoción?",
                "Estudios de UCLA muestran una reducción del 50%.",
                new[] {
                    ("10%", false, "El efecto es mucho mayor"),
                    ("25%", false, "El etiquetado emocional tiene un efecto más fuerte"),
                    ("50%", true, "¡Correcto! Nombrar la emoción reduce su intensidad a la mitad"),
                    ("75%", false, "El efecto es significativo pero no tanto")
                }),
            CrearPregunta(48, 3, "¿Cuál es la diferencia entre empatía cognitiva y emocional?",
                "Cognitiva = entender; Emocional = sentir lo que el otro siente.",
                new[] {
                    ("No hay diferencia", false, "Son tipos distintos de empatía"),
                    ("Cognitiva es entender, emocional es sentir", true, "¡Correcto! Ambas son necesarias en el trabajo"),
                    ("Cognitiva es más importante", false, "Ambas son necesarias, ninguna es superior"),
                    ("Emocional es racional, cognitiva es instintiva", false, "Es al revés")
                }),
            CrearPregunta(48, 4, "¿Qué técnica de regulación emocional se basa en inhalar 4s, retener 7s, exhalar 8s?",
                "La respiración 4-7-8 activa el sistema parasimpático.",
                new[] {
                    ("Mindfulness", false, "Mindfulness es una práctica más amplia"),
                    ("Respiración 4-7-8", true, "¡Correcto! Calma el sistema nervioso en 60 segundos"),
                    ("Respiración diafragmática", false, "Es una técnica diferente"),
                    ("Meditación trascendental", false, "Es una práctica completamente diferente")
                }),
            CrearPregunta(48, 5, "Ante alguien estresado, ¿qué deberías hacer PRIMERO?",
                "Validar antes de resolver: preguntar qué necesita.",
                new[] {
                    ("Ofrecer una solución inmediata", false, "Resolver antes de validar puede ser contraproducente"),
                    ("Validar su emoción", true, "¡Correcto! 'Tiene sentido que te sientas así' antes de resolver"),
                    ("Cambiar de tema para distraerle", false, "Ignorar la emoción no ayuda"),
                    ("Contar una experiencia similar tuya", false, "Primero valida, luego puedes compartir")
                })
        );

        // Quiz Networking N2 (leccionId=53)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(53, 1, "¿Qué demostró el estudio de Granovetter sobre vínculos?",
                "Los vínculos débiles generan más oportunidades que los fuertes.",
                new[] {
                    ("Los amigos cercanos son la mejor fuente de empleos", false, "Granovetter demostró lo contrario"),
                    ("Los vínculos débiles generan más oportunidades", true, "¡Correcto! Los conocidos abren puertas a redes diferentes"),
                    ("LinkedIn es la mejor herramienta de networking", false, "El estudio no evalúa herramientas específicas"),
                    ("La cantidad de contactos determina el éxito", false, "Es sobre la fuerza del vínculo, no la cantidad")
                }),
            CrearPregunta(53, 2, "¿Cuál es la regla 5-1 del networking?",
                "Ofrece 5 actos de valor por cada favor que pidas.",
                new[] {
                    ("5 contactos nuevos por 1 reunión", false, "No se trata de cantidad de contactos"),
                    ("5 actos de valor por cada favor que pidas", true, "¡Correcto! Da antes de pedir"),
                    ("5 minutos de conversación por persona", false, "No es una regla de tiempo"),
                    ("5 eventos por mes", false, "No es una regla de asistencia")
                }),
            CrearPregunta(53, 3, "¿Qué significa FORD en el contexto de conversaciones?",
                "FORD = Family, Occupation, Recreation, Dreams.",
                new[] {
                    ("Foco, Objetivo, Resultado, Decisión", false, "Esas no son las siglas correctas"),
                    ("Family, Occupation, Recreation, Dreams", true, "¡Correcto! Temas para mantener conversaciones fluidas"),
                    ("Fuerza, Oportunidad, Riesgo, Dirección", false, "FORD no son siglas de análisis estratégico"),
                    ("Formal, Open, Relaxed, Direct", false, "No son estilos de comunicación")
                }),
            CrearPregunta(53, 4, "¿En cuánto tiempo debes hacer follow-up después de conocer a alguien?",
                "El follow-up debe ser dentro de 48 horas.",
                new[] {
                    ("1 semana", false, "Es demasiado tiempo, la conexión se enfría"),
                    ("48 horas", true, "¡Correcto! Dentro de 2 días para mantener la conexión viva"),
                    ("1 mes", false, "Para entonces ya te habrán olvidado"),
                    ("Inmediatamente en el evento", false, "Es mejor hacerlo después con un mensaje personalizado")
                }),
            CrearPregunta(53, 5, "¿Cuántas personas debería tener tu círculo interno de networking?",
                "El círculo interno tiene 5-10 relaciones de confianza.",
                new[] {
                    ("1-2 personas", false, "Es un círculo muy reducido"),
                    ("5-10 personas", true, "¡Correcto! Relaciones profundas donde inviertes tiempo regular"),
                    ("50+ personas", false, "Ese es el tamaño del círculo extendido"),
                    ("No hay límite", false, "Las relaciones profundas requieren tiempo limitado")
                })
        );

        // Quiz Persuasión N2 (leccionId=58)
        contexto.Set<PreguntaQuiz>().AddRange(
            CrearPregunta(58, 1, "¿Cuántos principios de influencia identificó Cialdini?",
                "Cialdini identificó 6 principios de influencia.",
                new[] {
                    ("4", false, "Son más de 4 principios"),
                    ("6", true, "¡Correcto! Reciprocidad, Compromiso, Prueba social, Autoridad, Simpatía, Escasez"),
                    ("8", false, "Son menos de 8"),
                    ("10", false, "El modelo original tiene 6 principios")
                }),
            CrearPregunta(58, 2, "¿Cuántas áreas del cerebro activa una historia vs. datos?",
                "Las historias activan 7 áreas vs 2 de los datos.",
                new[] {
                    ("3 vs 1", false, "La diferencia es mayor"),
                    ("7 vs 2", true, "¡Correcto! Las historias sincronizan múltiples áreas cerebrales"),
                    ("5 vs 3", false, "La diferencia real es más notable"),
                    ("2 vs 2", false, "Las historias activan muchas más áreas")
                }),
            CrearPregunta(58, 3, "¿Cuál es la regla de 1 del storytelling?",
                "Una historia, un dato, un héroe, un mensaje.",
                new[] {
                    ("Contar solo 1 historia por presentación", false, "Se refiere a un mensaje por historia"),
                    ("Un mensaje claro por historia", true, "¡Correcto! La audiencia recuerda una cosa, elige cuál"),
                    ("Hablar máximo 1 minuto", false, "No es una regla de tiempo"),
                    ("Usar 1 dato estadístico", false, "Es sobre el mensaje, no los datos")
                }),
            CrearPregunta(58, 4, "¿Cuál es el principio de persuasión más poderoso según Cialdini?",
                "La reciprocidad es el principio más universal y poderoso.",
                new[] {
                    ("Escasez", false, "Es poderoso pero no el más universal"),
                    ("Reciprocidad", true, "¡Correcto! Dar valor primero activa la obligación de corresponder"),
                    ("Autoridad", false, "Depende del contexto"),
                    ("Prueba social", false, "Es muy efectivo pero la reciprocidad es más poderosa")
                }),
            CrearPregunta(58, 5, "¿Qué diferencia la persuasión ética de la manipulación?",
                "La persuasión ética busca beneficio mutuo.",
                new[] {
                    ("El volumen de ventas", false, "No se trata de resultados comerciales"),
                    ("El beneficio mutuo", true, "¡Correcto! Persuadir éticamente busca que ambas partes ganen"),
                    ("El uso de datos", false, "Ambas pueden usar datos"),
                    ("Nada, son lo mismo", false, "Son fundamentalmente diferentes en intención")
                })
        );

        await contexto.SaveChangesAsync();

        // ===== ESCENARIOS NIVEL 2 =====
    }

    private static async Task SembrarEscenariosNivel2Async(AppDbContext contexto)
    {
        // Escenario Comunicación N2 (leccionId=34)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 34,
            TextoSituacion = "Tu compañero Carlos lleva 3 semanas llegando 10-15 minutos tarde a las reuniones del equipo. Esto retrasa al grupo y genera frustración. Necesitas hablar con él.",
            Contexto = "Carlos es buen trabajador y se lleva bien con todos. No sabes si hay un motivo personal detrás de los retrasos.",
            GuionAudio = "Carlos es parte importante del equipo, pero sus retrasos están afectando la dinámica grupal.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Usar el modelo SBI: 'Carlos, en las últimas reuniones he notado que llegas 10-15 minutos tarde, lo que hace que el equipo pierda tiempo y tenga que repetir información. ¿Hay algo que pueda ayudar?'", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Excelente uso del modelo SBI. Describes la situación y comportamiento objetivamente, expresas el impacto y abres espacio para que Carlos explique. Empático y directo.", PuntosOtorgados = 25, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Mencionarlo casualmente: 'Oye Carlos, intenta llegar un poco antes a las reuniones, ¿vale?'", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "Es directo pero demasiado suave. No comunica el impacto real en el equipo y es fácil de ignorar. El modelo SBI sería más efectivo.", PuntosOtorgados = 10, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Quejarte con el manager para que él hable con Carlos", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Escalar sin intentar resolver directamente daña la confianza y la relación. Siempre intenta la conversación directa primero.", PuntosOtorgados = 0, Orden = 3 }
            }
        });

        // Escenario Liderazgo N2 (leccionId=39)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 39,
            TextoSituacion = "Eres tech lead y dos desarrolladores senior, Ana y David, están en desacuerdo sobre la arquitectura del nuevo módulo. Ana quiere microservicios y David prefiere monolito modular. Las discusiones se están volviendo personales.",
            Contexto = "Ambos tienen experiencia válida. El deadline es en 6 semanas y el equipo está dividido.",
            GuionAudio = "Este es un conflicto técnico que se está volviendo personal. Como líder, necesitas intervenir.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Reunirte con ambos, usar el framework DESC, reconocer la validez de ambas posturas y facilitar un análisis objetivo con criterios claros (complejidad, deadline, escalabilidad) para llegar a una decisión basada en datos", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Perfecto manejo. Reconoces ambas posturas (evitas tomar partido), usas criterios objetivos y facilitas una decisión informada. Esto resuelve el conflicto técnico y preserva la relación.", PuntosOtorgados = 25, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Decidir tú mismo qué arquitectura usar para terminar la discusión rápido", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "A veces el líder debe decidir, pero sin proceso colaborativo generas resentimiento. El perdedor sentirá que no fue escuchado.", PuntosOtorgados = 10, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Dejar que lo resuelvan entre ellos, ya son seniors", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Evitar el conflicto cuando se está volviendo personal es una disfunción de liderazgo. El equipo te necesita como facilitador.", PuntosOtorgados = 0, Orden = 3 }
            }
        });

        // Escenario Trabajo en Equipo N2 (leccionId=44)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 44,
            TextoSituacion = "Luis, un miembro del equipo, trabaja de forma aislada. No comparte su progreso en standups, no documenta su código y cuando otros necesitan información sobre su módulo, dice 'ya lo arreglo yo'. El equipo está frustrado porque dependen de su trabajo.",
            Contexto = "Luis es técnicamente excelente pero su forma de trabajar bloquea al equipo.",
            GuionAudio = "Tienes un problema de silos. Luis es brillante pero trabaja como si fuera un equipo de una persona.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Tener una conversación privada con Luis, explicar cómo su estilo de trabajo impacta al equipo con ejemplos concretos, y proponer acuerdos específicos: PR reviews obligatorios, standups con detalle técnico, documentación mínima", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Abordas el problema directamente con empatía, usando evidencia concreta y proponiendo soluciones accionables. Esto respeta a Luis y resuelve el bloqueo del equipo.", PuntosOtorgados = 25, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Establecer reglas generales de documentación y PR review para todo el equipo sin hablar con Luis directamente", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "Las reglas generales ayudan pero evitas la conversación directa. Luis puede no entender que el cambio es por su comportamiento específico.", PuntosOtorgados = 10, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Redistribuir el trabajo para no depender de Luis", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Esto evita el problema real y castiga al equipo con más carga. No resuelve la disfunción y aísla más a Luis.", PuntosOtorgados = 0, Orden = 3 }
            }
        });

        // Escenario Inteligencia Emocional N2 (leccionId=49)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 49,
            TextoSituacion = "Tu colega María, normalmente alegre y productiva, lleva dos semanas callada en reuniones, respondiendo con monosílabos y trabajando hasta muy tarde. Sospechas que está al borde del burnout.",
            Contexto = "No eres su manager, pero sí un colega cercano. Otros han notado el cambio pero nadie ha hablado con ella.",
            GuionAudio = "María necesita apoyo pero abordar el burnout de un colega requiere delicadeza y empatía genuina.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Invitarla a tomar un café a solas, expresar que has notado que parece cansada, validar que es normal sentirse abrumada, y preguntar '¿quieres hablar o prefieres que simplemente te ayude con algo?'", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Perfecto. Espacio privado, observación sin juicio, validación emocional y oferta de ayuda sin presionar. Le das control sobre qué tipo de apoyo necesita.", PuntosOtorgados = 25, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Decirle frente al equipo: 'María, ¿estás bien? Te veo muy cansada últimamente'", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Exponer la vulnerabilidad de alguien en público puede avergonzarla y empeorar la situación. Estos temas siempre en privado.", PuntosOtorgados = 0, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Enviarle un mensaje diciendo que si necesita algo, cuentas contigo", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "Es un gesto amable pero genérico. Un mensaje es fácil de ignorar. La conversación cara a cara muestra más compromiso y genera más confianza.", PuntosOtorgados = 10, Orden = 3 }
            }
        });

        // Escenario Networking N2 (leccionId=54)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 54,
            TextoSituacion = "Estás en una conferencia de tecnología y ves a la CTO de una empresa donde te encantaría trabajar. Está sola tomando café entre sesiones. Solo tienes unos minutos antes de la siguiente charla.",
            Contexto = "Has leído artículos suyos y conoces su trabajo, pero nunca se han visto.",
            GuionAudio = "Esta es una oportunidad única. ¿Cómo te acercas a alguien importante sin parecer interesado solo por el cargo?",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Acercarte con un cumplido específico: 'Hola, leí tu artículo sobre escalabilidad en sistemas distribuidos y me pareció brillante el enfoque de X. ¿Cómo llegaste a esa conclusión?' Luego, al despedirte, pedir conectar en LinkedIn.", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Excelente. Demuestras interés genuino con un detalle específico, generas conversación valiosa y cierras con un next step concreto. Networking de calidad.", PuntosOtorgados = 25, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Presentarte y decirle directamente que te encantaría trabajar en su empresa", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "Es directo y honesto, pero centras la conversación en lo que tú necesitas. Primero ofrece valor (muestra interés en su trabajo), luego surgirá la oportunidad.", PuntosOtorgados = 10, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "No acercarte para no parecer oportunista", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Evitar la oportunidad por miedo es lo opuesto al networking estratégico. La mayoría de las personas en conferencias esperan conectar.", PuntosOtorgados = 0, Orden = 3 }
            }
        });

        // Escenario Persuasión N2 (leccionId=59)
        contexto.Set<Escenario>().Add(new Escenario
        {
            LeccionId = 59,
            TextoSituacion = "Tu proyecto necesita $50,000 adicionales para implementar una funcionalidad que crees mejorará la retención de usuarios un 20%. El director financiero es muy conservador y ha rechazado las últimas 3 solicitudes de presupuesto de otros equipos.",
            Contexto = "Tienes datos de un piloto limitado que sugiere el impacto, pero no es concluyente al 100%.",
            GuionAudio = "Convencer a un director financiero escéptico requiere datos, storytelling y los principios de Cialdini.",
            Opciones = new List<OpcionEscenario>
            {
                new OpcionEscenario { TextoOpcion = "Preparar un caso con datos del piloto (prueba social), mostrar el ROI esperado vs. costo de inacción (escasez de oportunidad), presentar testimonios de usuarios del piloto, y proponer una inversión gradual: $15K iniciales con métricas claras para desbloquear el resto", TipoResultado = TipoResultadoEscenario.Optimo, TextoRetroalimentacion = "Magistral. Usas prueba social, escasez, autoridad (datos) y reduces el riesgo con inversión gradual. Además, le das control al director con métricas de seguimiento.", PuntosOtorgados = 25, Orden = 1 },
                new OpcionEscenario { TextoOpcion = "Presentar solo los datos del piloto y el ROI proyectado de forma directa", TipoResultado = TipoResultadoEscenario.Aceptable, TextoRetroalimentacion = "Los datos son necesarios pero no suficientes para alguien escéptico. Falta storytelling, reducción de riesgo y principios de persuasión.", PuntosOtorgados = 10, Orden = 2 },
                new OpcionEscenario { TextoOpcion = "Pedir al CEO que presione al director financiero", TipoResultado = TipoResultadoEscenario.Inadecuado, TextoRetroalimentacion = "Escalar sin persuadir genera enemigos y demuestra incapacidad de influir. El director financiero se opondría aún más en el futuro.", PuntosOtorgados = 0, Orden = 3 }
            }
        });

        await contexto.SaveChangesAsync();
    }
}
