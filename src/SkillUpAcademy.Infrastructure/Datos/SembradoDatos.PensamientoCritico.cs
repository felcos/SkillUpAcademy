using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de lecciones para el área Pensamiento Crítico (3 niveles).
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesPensamientoCriticoAsync(AppDbContext contexto)
    {
        // ===== NIVEL 1 — Fundamentos (NivelId=22) =====

        Leccion l1 = new Leccion
        {
            Id = 96, NivelId = 22, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Falacias lógicas: los errores que todos cometemos",
            Descripcion = "Aprende a identificar las falacias lógicas más comunes que distorsionan argumentos en el trabajo y la vida cotidiana.",
            Contenido = "## ¿Qué es una falacia lógica?\n\nUna falacia es un error en el razonamiento que hace que un argumento parezca válido cuando no lo es. Aristóteles fue el primero en catalogarlas hace más de 2.300 años, y siguen siendo igual de relevantes.\n\n## Las 6 falacias más comunes en el entorno profesional\n\n### 1. Ad Hominem (Ataque a la persona)\nEn lugar de refutar el argumento, se ataca a quien lo dice. «No le hagas caso a Pedro, él no tiene MBA.» El argumento de Pedro puede ser válido independientemente de su título.\n\n### 2. Strawman (Hombre de paja)\nDistorsionar el argumento del otro para refutar algo que nunca dijo. Si dices «deberíamos considerar trabajo remoto» y te responden «¿quieres que nadie venga nunca a la oficina?», te están construyendo un hombre de paja.\n\n### 3. Falsa dicotomía\nPresentar solo dos opciones cuando hay más. «O implementamos esta tecnología o la empresa fracasará.» La realidad rara vez es binaria.\n\n### 4. Apelación a la autoridad\n«El CEO lo dijo, así que debe ser cierto.» La autoridad no convierte un argumento en válido. Los datos y la lógica sí.\n\n### 5. Pendiente resbaladiza\n«Si permitimos trabajo remoto un día, pronto nadie vendrá y la cultura desaparecerá.» Asumir que un paso lleva inevitablemente a una catástrofe sin evidencia.\n\n### 6. Correlación no implica causalidad\n«Desde que cambiamos el logo, las ventas subieron.» Que dos cosas ocurran juntas no significa que una cause la otra.",
            PuntosClave = "[\"Una falacia es un error de razonamiento que hace parecer válido un argumento inválido\",\"Ad hominem ataca a la persona en lugar del argumento\",\"Strawman distorsiona el argumento del otro para refutarlo fácilmente\",\"La falsa dicotomía presenta solo dos opciones cuando hay más\",\"Correlación no implica causalidad: que dos cosas coincidan no significa que una cause la otra\"]",
            GuionAudio = "Hoy vamos a hablar de falacias lógicas, esos errores de razonamiento que todos cometemos y que, si no los detectas, pueden llevarte a tomar decisiones equivocadas. Aristóteles las identificó hace más de dos mil años y siguen apareciendo cada día en reuniones, correos y presentaciones.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 8
        };

        Leccion l2 = new Leccion
        {
            Id = 97, NivelId = 22, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Taxonomía de Bloom: los 6 niveles del pensamiento",
            Descripcion = "Descubre los niveles cognitivos que van desde memorizar datos hasta crear soluciones originales.",
            Contenido = "## La pirámide del pensamiento\n\nBenjamin Bloom publicó en 1956 una taxonomía que clasifica el pensamiento en 6 niveles de complejidad creciente. La versión revisada por Anderson y Krathwohl (2001) es la que usamos hoy.\n\n## Los 6 niveles (de menor a mayor complejidad)\n\n### 1. Recordar\nRecuperar información de la memoria. «¿Cuáles son las 4P del marketing?» Es el nivel más básico: datos, fechas, definiciones.\n\n### 2. Comprender\nExplicar ideas con tus propias palabras. No es repetir, es demostrar que entiendes el significado. «Explica por qué la inflación afecta a los salarios reales.»\n\n### 3. Aplicar\nUsar conocimiento en situaciones nuevas. «Usa la matriz DAFO para analizar tu empresa.» Aquí pasas de la teoría a la práctica.\n\n### 4. Analizar\nDescomponer información en partes y encontrar relaciones. «¿Qué factores contribuyeron al fracaso del proyecto?» Separas causa de efecto, hechos de opiniones.\n\n### 5. Evaluar\nEmitir juicios fundamentados con criterios claros. «¿Es viable esta propuesta? ¿Qué evidencia la respalda?» Aquí el pensamiento crítico brilla.\n\n### 6. Crear\nGenerar ideas, productos o soluciones originales combinando elementos. «Diseña una estrategia de retención de talento para tu empresa.» Es el nivel más alto del pensamiento.\n\n## ¿Por qué importa esto?\n\nLa mayoría de los profesionales operan en los niveles 1-3. Los que llegan a los niveles 4-6 son los que toman mejores decisiones, innovan y lideran.",
            PuntosClave = "[\"La Taxonomía de Bloom tiene 6 niveles: Recordar, Comprender, Aplicar, Analizar, Evaluar y Crear\",\"La mayoría de profesionales operan en los niveles 1-3 sin llegar al pensamiento crítico real\",\"El nivel 4 (Analizar) es donde empieza el pensamiento crítico: separar causa de efecto, hechos de opiniones\",\"El nivel 6 (Crear) es el más alto: generar soluciones originales combinando elementos\"]",
            GuionAudio = "Benjamin Bloom clasificó el pensamiento humano en seis niveles. La mayoría de nosotros nos quedamos en los tres primeros: recordar, comprender y aplicar. Hoy vas a descubrir cómo llegar a los niveles superiores donde se toman las mejores decisiones.",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 10
        };

        Leccion l3 = new Leccion
        {
            Id = 98, NivelId = 22, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Pensamiento Crítico",
            Descripcion = "Pon a prueba tus conocimientos sobre falacias lógicas y niveles de pensamiento.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = 99, NivelId = 22, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: La propuesta llena de falacias",
            Descripcion = "Tu compañero presenta una propuesta con argumentos falaces. Decide cómo responder profesionalmente.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = 100, NivelId = 22, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Defiende tu análisis ante el comité",
            Descripcion = "Practica cómo defender un análisis basado en evidencia ante un comité que cuestiona tus conclusiones.",
            Contenido = "Eres analista de datos y has presentado un informe que contradice la opinión del director comercial. El comité quiere que justifiques tus conclusiones con evidencia sólida y respondas a las objeciones.",
            GuionAudio = "En este roleplay, yo seré el comité directivo. Vamos a cuestionar cada una de tus conclusiones. Tu misión es defender tu análisis con lógica, datos y sin caer en falacias. Recuerda: separar hechos de opiniones es tu mejor arma.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1
        await SembrarEscenasDesdeLeccionAsync(contexto, l1,
            "Hoy vamos a aprender a detectar los trucos que nuestro cerebro usa para hacernos creer que un argumento es válido cuando no lo es. Esto cambiará tu forma de evaluar propuestas y decisiones.",
            "Esta semana, presta atención a las reuniones y anota cada falacia que detectes. Te sorprenderá lo frecuentes que son.");

        // Escenas para lección 2
        await SembrarEscenasDesdeLeccionAsync(contexto, l2,
            "Benjamin Bloom clasificó el pensamiento en seis niveles de complejidad. Saber en qué nivel estás te permite subir conscientemente al siguiente.",
            "La próxima vez que tomes una decisión, pregúntate en qué nivel de Bloom estás operando. Si solo estás recordando o aplicando, intenta subir a analizar o evaluar.");

        // Quiz pensamiento crítico
        await SembrarQuizPensamientoCriticoAsync(contexto, l3.Id);

        // Escenario pensamiento crítico
        await SembrarEscenarioPensamientoCriticoAsync(contexto, l4.Id);

        // ===== NIVEL 2 — Práctica (NivelId=23) =====

        Leccion n2l1 = new Leccion
        {
            Id = 126, NivelId = 23, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Sesgo de confirmación y sesgos cognitivos",
            Descripcion = "Descubre cómo tu cerebro te engaña para confirmar lo que ya crees, según la investigación de Daniel Kahneman.",
            Contenido = "## Tu cerebro te miente (y no lo sabes)\n\nDaniel Kahneman, premio Nobel de Economía, demostró que tenemos dos sistemas de pensamiento:\n\n### Sistema 1: Rápido e intuitivo\nResponde automáticamente, usa atajos mentales (heurísticas). Es eficiente pero propenso a errores sistemáticos.\n\n### Sistema 2: Lento y deliberado\nAnaliza, calcula, razona. Es preciso pero consume mucha energía, así que lo usamos poco.\n\n## Los 5 sesgos cognitivos más peligrosos en el trabajo\n\n### 1. Sesgo de confirmación\nBuscas información que confirme lo que ya crees e ignoras la que lo contradice. Si crees que un proyecto va a fracasar, solo verás señales de fracaso.\n\n### 2. Efecto ancla\nLa primera información que recibes influye desproporcionadamente. Si te dicen que el presupuesto anterior fue 100.000€, tu estimación girará alrededor de esa cifra.\n\n### 3. Sesgo de disponibilidad\nJuzgas la probabilidad de algo por lo fácil que es recordar ejemplos. Después de un accidente aéreo, sobreestimas el riesgo de volar.\n\n### 4. Efecto Dunning-Kruger\nLos menos competentes sobreestiman su habilidad, y los más competentes la subestiman. La ignorancia genera más confianza que el conocimiento.\n\n### 5. Sesgo del coste hundido\nSeguir invirtiendo en algo solo porque ya invertiste mucho. «Llevamos 2 años en este proyecto, no podemos abandonarlo ahora.» Sí puedes, y a veces debes.\n\n## Técnica anti-sesgo: Pre-mortem\nAntes de tomar una decisión, imagina que ha fracasado. «Es un año después y el proyecto fue un desastre. ¿Qué salió mal?» Esto activa el Sistema 2 y neutraliza el sesgo de confirmación.",
            PuntosClave = "[\"Kahneman identificó dos sistemas: el rápido e intuitivo (Sistema 1) y el lento y deliberado (Sistema 2)\",\"El sesgo de confirmación te hace buscar solo información que confirme tus creencias\",\"El efecto Dunning-Kruger hace que los menos competentes sobreestimen su habilidad\",\"El sesgo del coste hundido te atrapa en proyectos fallidos solo porque ya invertiste\",\"La técnica pre-mortem neutraliza sesgos imaginando el fracaso antes de decidir\"]",
            GuionAudio = "Daniel Kahneman ganó el Nobel demostrando que nuestro cerebro nos engaña de formas sistemáticas y predecibles. Hoy vas a aprender a detectar esos sesgos para que no saboteen tus decisiones profesionales.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 12
        };

        Leccion n2l2 = new Leccion
        {
            Id = 127, NivelId = 23, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Pensamiento de primer principio: razona desde cero",
            Descripcion = "Aprende la técnica que usan Elon Musk y Aristóteles para resolver problemas complejos descomponiendo hasta las verdades fundamentales.",
            Contenido = "## ¿Qué es el pensamiento de primer principio?\n\nAristóteles lo definió como «las primeras bases a partir de las cuales se conoce una cosa». Elon Musk lo popularizó: en lugar de razonar por analogía («así se ha hecho siempre»), descompones el problema hasta sus verdades fundamentales y reconstruyes desde ahí.\n\n## Razonar por analogía vs. primer principio\n\n### Por analogía (lo habitual)\n«Las baterías cuestan 600$/kWh porque siempre han costado eso. Los coches eléctricos serán siempre caros.»\n\n### Por primer principio (lo transformador)\n«¿De qué están hechas las baterías? Cobalto, níquel, litio, aluminio. ¿Cuánto cuestan esos materiales en el mercado? 80$/kWh. Entonces, ¿por qué no podemos fabricarlas por menos de 600$?» Así nació Tesla.\n\n## El método en 3 pasos\n\n### Paso 1: Identifica las suposiciones\n¿Qué estás dando por hecho? Escribe cada suposición que hagas sobre el problema.\n\n### Paso 2: Descompón hasta las verdades fundamentales\nPregunta «¿por qué?» repetidamente (técnica de los 5 porqués de Toyota) hasta llegar a hechos verificables.\n\n### Paso 3: Reconstruye desde las verdades\nCon las verdades fundamentales como base, construye una solución nueva sin cargar con suposiciones del pasado.\n\n## Ejemplo práctico: reducir costes\n**Analogía:** «Nuestros competidores gastan un 30% en marketing, nosotros también.»\n**Primer principio:** «¿Cuál es el objetivo del marketing? Atraer clientes. ¿Cuáles son todas las formas posibles de atraer clientes? ¿Cuál tiene mejor coste por adquisición?»",
            PuntosClave = "[\"El pensamiento de primer principio descompone problemas hasta verdades fundamentales verificables\",\"Razonar por analogía repite el pasado; razonar por primer principio permite innovar\",\"Los 3 pasos: identificar suposiciones, descomponer hasta verdades y reconstruir desde ellas\",\"La técnica de los 5 porqués de Toyota ayuda a llegar a las verdades fundamentales\",\"Elon Musk aplicó este método para reducir el coste de baterías de 600 a 80$/kWh\"]",
            GuionAudio = "¿Sabías que Aristóteles y Elon Musk comparten la misma técnica de pensamiento? Se llama razonamiento de primer principio y consiste en no aceptar nada como verdad hasta descomponerlo en sus partes fundamentales. Hoy vas a aprender a usarla.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 12
        };

        Leccion n2l3 = new Leccion
        {
            Id = 128, NivelId = 23, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Sesgos y Primer Principio",
            Descripcion = "Evalúa tu dominio de los sesgos cognitivos y el pensamiento de primer principio.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion n2l4 = new Leccion
        {
            Id = 129, NivelId = 23, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: El proyecto que nadie quiere cancelar",
            Descripcion = "Un proyecto lleva 18 meses de retraso pero nadie quiere cancelarlo. Identifica los sesgos y decide.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 12
        };

        Leccion n2l5 = new Leccion
        {
            Id = 130, NivelId = 23, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Desafía las suposiciones del equipo",
            Descripcion = "Practica cómo cuestionar las suposiciones de tu equipo usando pensamiento de primer principio sin generar resistencia.",
            Contenido = "Tu equipo está diseñando un nuevo proceso de onboarding para clientes. Todos asumen que debe durar 3 meses porque «siempre ha sido así». Tu misión es aplicar pensamiento de primer principio para cuestionar esas suposiciones y proponer un enfoque radicalmente diferente.",
            GuionAudio = "En este roleplay, serás el facilitador de una sesión de brainstorming. Tu equipo tiene muchas suposiciones arraigadas. Usa los 5 porqués y el pensamiento de primer principio para llegar a las verdades fundamentales. Recuerda: cuestiona con curiosidad, no con agresividad.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n2l1, n2l2, n2l3, n2l4, n2l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1 N2
        await SembrarEscenasDesdeLeccionAsync(contexto, n2l1,
            "Daniel Kahneman demostró que nuestro cerebro opera con dos sistemas y que el rápido nos lleva a errores predecibles. Hoy aprenderás a detectar los cinco sesgos más peligrosos en el trabajo.",
            "Antes de tu próxima decisión importante, haz un pre-mortem: imagina que salió mal y pregúntate qué falló. Activarás tu Sistema 2 y neutralizarás tus sesgos.");

        // Escenas para lección 2 N2
        await SembrarEscenasDesdeLeccionAsync(contexto, n2l2,
            "La mayoría razonamos por analogía: así se ha hecho siempre. Hoy aprenderás a descomponer problemas hasta sus verdades fundamentales y reconstruir soluciones desde cero.",
            "Elige un proceso de tu trabajo que lleve años sin cambiar y aplica los 3 pasos del primer principio. Te sorprenderá lo que descubras.");

        // ===== NIVEL 3 — Dominio (NivelId=24) =====

        Leccion n3l1 = new Leccion
        {
            Id = 156, NivelId = 24, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Análisis de sistemas complejos",
            Descripcion = "Aprende a pensar en sistemas: bucles de retroalimentación, efectos de segundo orden y puntos de apalancamiento.",
            Contenido = "## Pensamiento sistémico: ver el bosque completo\n\nPeter Senge, en «La Quinta Disciplina», demostró que la mayoría de los problemas organizacionales no se resuelven atacando los síntomas, sino entendiendo el sistema completo.\n\n## Conceptos clave del pensamiento sistémico\n\n### Bucles de retroalimentación\n**Bucle de refuerzo (+):** Un efecto que se amplifica a sí mismo. Más clientes → más ingresos → más inversión en marketing → más clientes. Puede ser virtuoso o vicioso.\n\n**Bucle de equilibrio (-):** Un efecto que se autorregula. Más producción → más inventario → menos producción. Son los termostatos del sistema.\n\n### Retrasos temporales\nLos efectos no son instantáneos. Contratas a alguien hoy pero su impacto real llega en 3-6 meses. Los retrasos causan sobrecompensación: «contratamos demasiado porque los resultados no llegaban rápido».\n\n### Efectos de segundo orden\nSon las consecuencias de las consecuencias. Ejemplo: automatizas atención al cliente (primer orden: reduces costes). Segundo orden: los clientes se frustran con bots. Tercer orden: pierdes clientes premium que pagaban más.\n\n### Puntos de apalancamiento (Donella Meadows)\nLos 12 puntos de intervención en un sistema, del menos al más efectivo:\n- Cambiar constantes y parámetros (débil)\n- Cambiar las reglas del sistema (medio)\n- Cambiar los objetivos del sistema (fuerte)\n- Cambiar el paradigma del sistema (transformador)\n\n## Herramienta: Diagrama de bucles causales\nDibuja las variables, conecta con flechas (+ refuerza, - equilibra) y busca los bucles. Donde encuentres un bucle de refuerzo vicioso, ahí está tu problema real.",
            PuntosClave = "[\"El pensamiento sistémico ve el bosque completo, no solo árboles individuales\",\"Los bucles de refuerzo amplifican efectos; los de equilibrio los regulan\",\"Los retrasos temporales causan sobrecompensación en las decisiones\",\"Los efectos de segundo orden son las consecuencias de las consecuencias\",\"Los puntos de apalancamiento de Meadows van desde cambiar parámetros hasta cambiar paradigmas\"]",
            GuionAudio = "Peter Senge demostró que los mejores pensadores no resuelven problemas aislados: entienden sistemas completos. Hoy vas a aprender a ver bucles de retroalimentación, detectar efectos de segundo orden y encontrar los puntos de apalancamiento donde un pequeño cambio genera un gran impacto.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 15
        };

        Leccion n3l2 = new Leccion
        {
            Id = 157, NivelId = 24, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Metacognición y pensamiento de segundo orden",
            Descripcion = "Aprende a pensar sobre tu propio pensamiento y a anticipar las consecuencias de las consecuencias.",
            Contenido = "## Metacognición: pensar sobre pensar\n\nLa metacognición es la capacidad de observar y regular tu propio proceso de pensamiento. John Flavell la definió en 1979 como «el conocimiento sobre los propios procesos cognitivos».\n\n## Los 3 componentes de la metacognición\n\n### 1. Conocimiento metacognitivo\nSaber qué sabes y qué no sabes. Sócrates lo resumió: «Solo sé que no sé nada.» Los mejores profesionales tienen un mapa claro de su ignorancia.\n\n### 2. Regulación metacognitiva\nPlanificar tu pensamiento antes de actuar, monitorearlo durante la ejecución y evaluarlo después. Es como tener un director de orquesta en tu mente.\n\n### 3. Experiencia metacognitiva\nLa sensación de «algo no encaja aquí» o «esto lo entiendo perfectamente». Aprender a confiar en estas señales internas mejora tu toma de decisiones.\n\n## Pensamiento de segundo orden\n\nHoward Marks, inversor legendario, distingue entre:\n\n### Pensamiento de primer orden\n«Esta empresa tiene buenos resultados → compro acciones.» Es superficial y lo hace todo el mundo.\n\n### Pensamiento de segundo orden\n«Esta empresa tiene buenos resultados → todos comprarán → el precio ya está inflado → no compro.» Pregúntate: «¿Y luego qué?» repetidamente.\n\n## Framework para decisiones complejas\n\n### 1. Invierte la pregunta\nEn lugar de «¿cómo puedo tener éxito?», pregunta «¿cómo puedo asegurarme de fracasar?» y evita eso. Charlie Munger llama a esto «inversión».\n\n### 2. Busca la evidencia contraria\nActivamente busca razones por las que estás equivocado. Si no encuentras ninguna, probablemente no has buscado bien.\n\n### 3. Probabilidades, no certezas\nPiensa en rangos de probabilidad: «Hay un 70% de probabilidad de que esto funcione, un 20% de resultado mediocre y un 10% de fracaso total.»\n\n### 4. Documenta tu razonamiento\nEscribe tus razones ANTES de decidir. Después, compara con el resultado. Es la única forma de mejorar tu calibración.",
            PuntosClave = "[\"La metacognición es pensar sobre tu propio pensamiento: saber qué sabes y qué no\",\"Regulación metacognitiva: planifica, monitorea y evalúa tu proceso de pensamiento\",\"El pensamiento de segundo orden pregunta '¿y luego qué?' repetidamente\",\"Charlie Munger recomienda invertir la pregunta: pensar cómo fracasar para evitarlo\",\"Documentar tu razonamiento antes de decidir es la única forma de mejorar tu calibración\"]",
            GuionAudio = "El nivel más alto del pensamiento crítico es pensar sobre cómo piensas. La metacognición te permite detectar errores en tu propio razonamiento antes de que te lleven a malas decisiones. Hoy también aprenderás el pensamiento de segundo orden de Howard Marks: preguntar siempre «¿y luego qué?».",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 15
        };

        Leccion n3l3 = new Leccion
        {
            Id = 158, NivelId = 24, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Sistemas y Metacognición",
            Descripcion = "Evalúa tu dominio del pensamiento sistémico, la metacognición y el pensamiento de segundo orden.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion n3l4 = new Leccion
        {
            Id = 159, NivelId = 24, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: La decisión estratégica con efectos en cadena",
            Descripcion = "Tu empresa quiere automatizar el soporte al cliente. Analiza los efectos de segundo y tercer orden antes de decidir.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 15
        };

        Leccion n3l5 = new Leccion
        {
            Id = 160, NivelId = 24, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Facilitador de análisis sistémico",
            Descripcion = "Facilita una sesión donde el equipo directivo debe analizar un problema complejo usando pensamiento sistémico.",
            Contenido = "La empresa está perdiendo talento senior a pesar de haber subido salarios un 15%. El CEO quiere subir otro 10% pero tú sospechas que el problema es sistémico. Debes facilitar un análisis de bucles causales con el equipo directivo para encontrar el verdadero punto de apalancamiento.",
            GuionAudio = "En este roleplay, serás el facilitador de una sesión de análisis sistémico con el equipo directivo. El CEO quiere una solución rápida, pero tú sabes que hay que entender el sistema completo. Usa diagramas de bucles causales, busca efectos de segundo orden y encuentra el punto de apalancamiento real.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n3l1, n3l2, n3l3, n3l4, n3l5);
        await contexto.SaveChangesAsync();

        // Escenas para lección 1 N3
        await SembrarEscenasDesdeLeccionAsync(contexto, n3l1,
            "Peter Senge demostró que los problemas complejos no se resuelven atacando síntomas. Hoy aprenderás a ver sistemas completos con sus bucles de retroalimentación y puntos de apalancamiento.",
            "Elige un problema recurrente en tu trabajo y dibuja un diagrama de bucles causales. Busca dónde se retroalimenta el problema y ahí estará tu punto de apalancamiento.");

        // Escenas para lección 2 N3
        await SembrarEscenasDesdeLeccionAsync(contexto, n3l2,
            "El nivel más alto del pensamiento crítico es observar tu propio proceso mental. Hoy aprenderás metacognición y el pensamiento de segundo orden que distingue a los mejores decisores.",
            "Antes de tu próxima decisión importante, escribe tus razones en un papel. Después del resultado, compara. Es la única forma de calibrar tu juicio a largo plazo.");
    }

    private static async Task SembrarQuizPensamientoCriticoAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Qué falacia comete alguien que ataca a la persona en lugar de refutar su argumento?",
                "La falacia Ad Hominem ataca al mensajero para invalidar el mensaje.",
                new[] { ("Strawman", false, "El strawman distorsiona el argumento, no ataca a la persona."), ("Ad Hominem", true, "¡Correcto! Ad Hominem significa 'contra el hombre': atacar a quien argumenta en vez de refutar lo que dice."), ("Falsa dicotomía", false, "La falsa dicotomía presenta solo dos opciones cuando hay más."), ("Pendiente resbaladiza", false, "La pendiente resbaladiza asume que un paso lleva inevitablemente a una catástrofe.") }),
            CrearPregunta(leccionId, 2, "Según la Taxonomía de Bloom, ¿en qué nivel empieza realmente el pensamiento crítico?",
                "El nivel 4 (Analizar) es donde separamos causa de efecto y hechos de opiniones.",
                new[] { ("Recordar (nivel 1)", false, "Recordar es simplemente recuperar datos de la memoria."), ("Comprender (nivel 2)", false, "Comprender es explicar ideas, pero aún no es pensamiento crítico."), ("Analizar (nivel 4)", true, "¡Correcto! Analizar implica descomponer información, encontrar relaciones y separar hechos de opiniones."), ("Crear (nivel 6)", false, "Crear es el nivel más alto, pero el pensamiento crítico comienza en Analizar.") }),
            CrearPregunta(leccionId, 3, "Si alguien dice «o adoptamos IA ahora o la empresa morirá», ¿qué falacia está usando?",
                "Presentar solo dos opciones extremas cuando existen alternativas intermedias es una falsa dicotomía.",
                new[] { ("Ad Hominem", false, "No está atacando a ninguna persona."), ("Correlación sin causalidad", false, "No está confundiendo correlación con causa."), ("Falsa dicotomía", true, "¡Correcto! Presenta solo dos opciones extremas ignorando alternativas como adopción gradual, piloto limitado, etc."), ("Apelación a la autoridad", false, "No está citando a ninguna autoridad para justificar su posición.") }),
            CrearPregunta(leccionId, 4, "¿Cuál es la diferencia entre correlación y causalidad?",
                "Que dos eventos coincidan no significa que uno cause el otro.",
                new[] { ("Son sinónimos", false, "No lo son. Correlación es coincidencia; causalidad es relación causa-efecto demostrable."), ("La correlación demuestra que un evento causa otro", false, "No, la correlación solo muestra que coinciden en el tiempo."), ("La causalidad requiere demostrar que un evento provoca directamente otro", true, "¡Correcto! Para demostrar causalidad necesitas evidencia de que A provoca B, no solo que coincidan."), ("La causalidad es más débil que la correlación", false, "Es al revés: la causalidad es una relación más fuerte y difícil de demostrar.") }),
            CrearPregunta(leccionId, 5, "¿Qué nivel de la Taxonomía de Bloom implica generar soluciones originales combinando elementos?",
                "Crear es el nivel más alto: producir algo nuevo y original.",
                new[] { ("Evaluar (nivel 5)", false, "Evaluar es emitir juicios fundamentados, no crear algo nuevo."), ("Aplicar (nivel 3)", false, "Aplicar es usar conocimiento en situaciones nuevas, pero siguiendo modelos existentes."), ("Analizar (nivel 4)", false, "Analizar descompone información pero no genera soluciones originales."), ("Crear (nivel 6)", true, "¡Correcto! Crear es el nivel más alto: generar ideas, productos o soluciones originales que no existían antes.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    private static async Task SembrarEscenarioPensamientoCriticoAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Estás en una reunión donde tu compañero Carlos presenta una propuesta para cambiar de proveedor tecnológico. Durante su presentación dice: «Nuestro proveedor actual es terrible. Además, Laura, que lleva solo 6 meses aquí, opina que deberíamos quedarnos, pero ella no entiende cómo funcionamos. Si no cambiamos ahora, en 2 años estaremos fuera del mercado. El CEO de Google usa este proveedor, así que claramente es la mejor opción.»",
            Contexto = "Eres el responsable de evaluar propuestas técnicas. Has detectado al menos 3 falacias en el argumento de Carlos, pero necesitas responder de forma que corrija el razonamiento sin humillarlo ni generar conflicto.",
            GuionAudio = "Este escenario pondrá a prueba tu capacidad de detectar falacias lógicas en tiempo real y responder de forma constructiva. Recuerda: el objetivo no es ganar el debate, sino mejorar la calidad del razonamiento del equipo."
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Agradecer la propuesta y señalar constructivamente las falacias: «Carlos, gracias por investigar alternativas. Me gustaría que analicemos esto con más rigor. Primero, la opinión de Laura es válida independientemente de su antigüedad — evaluemos sus argumentos. Segundo, que Google use ese proveedor no significa que sea ideal para nosotros. Y tercero, ¿tenemos datos que muestren que sin cambiar estaremos fuera del mercado en 2 años? Propongo que hagamos un análisis comparativo con criterios objetivos.»",
                TipoResultado = TipoResultadoEscenario.Optimo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Excelente! Identificaste las 3 falacias principales (ad hominem contra Laura, apelación a autoridad con Google, pendiente resbaladiza con el plazo de 2 años) y las señalaste de forma respetuosa proponiendo un enfoque basado en evidencia."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Pedir más datos sin señalar las falacias directamente: «Interesante propuesta, Carlos. ¿Podrías traer un análisis comparativo con métricas objetivas para la próxima reunión? Así podremos tomar una decisión informada.»",
                TipoResultado = TipoResultadoEscenario.Aceptable, PuntosOtorgados = 10,
                TextoRetroalimentacion = "Buena respuesta al pedir datos objetivos, pero perdiste la oportunidad de educar al equipo sobre las falacias. Carlos y el equipo seguirán usando argumentos falaces en el futuro si nadie los señala constructivamente."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Señalar bruscamente los errores: «Carlos, tu argumento tiene al menos tres falacias lógicas. Atacas a Laura en vez de refutar su argumento, apelas a la autoridad de Google y usas una pendiente resbaladiza. No podemos tomar decisiones así.»",
                TipoResultado = TipoResultadoEscenario.Inadecuado, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Aunque identificaste correctamente las falacias, la forma de comunicarlo fue agresiva y humillante. Esto genera defensividad y daña la relación. El pensamiento crítico debe aplicarse con inteligencia emocional: corregir el razonamiento sin atacar a la persona."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }
}
