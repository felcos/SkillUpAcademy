using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de lecciones para el área Negociación (3 niveles).
/// </summary>
public static partial class SembradoDatos
{
    private static async Task SembrarLeccionesNegociacionAsync(AppDbContext contexto)
    {
        // ===== NIVEL 1 — Fundamentos (NivelId=28) =====
        Leccion l1 = new Leccion
        {
            Id = 106, NivelId = 28, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Método Harvard: negocia por principios, no por posiciones",
            Descripcion = "Domina el framework de negociación más influyente del mundo: separa persona del problema, enfócate en intereses y genera opciones de beneficio mutuo.",
            Contenido = "## El Proyecto de Negociación de Harvard\n\nEn 1981, Roger Fisher y William Ury publicaron «Getting to Yes», revolucionando la forma de negociar. Su tesis central: la negociación posicional (yo quiero X, tú quieres Y) destruye relaciones y genera acuerdos mediocres.\n\n## Los 4 principios del Método Harvard\n\n### 1. Separa la persona del problema\nAtaca el problema, no a la persona. Un negociador que se siente atacado personalmente deja de buscar soluciones. Ejemplo: en lugar de «Tu propuesta es ridícula», di «Necesito entender cómo llegaste a esa cifra».\n\n### 2. Enfócate en intereses, no en posiciones\nUna posición es lo que dices que quieres. Un interés es por qué lo quieres. Dos hermanas pelean por una naranja (posición). Una quiere el zumo, la otra la cáscara para un pastel (intereses). Ambas pueden obtener el 100%.\n\n### 3. Genera opciones de beneficio mutuo\nAntes de decidir, inventa opciones. El brainstorming creativo multiplica las posibilidades. La regla: separa el acto de inventar del acto de decidir.\n\n### 4. Insiste en criterios objetivos\nBasa el acuerdo en estándares independientes: valor de mercado, precedentes, opinión de expertos. Así nadie cede ante presión, sino ante la razón.\n\n## BATNA: tu mayor fuente de poder\n\nBATNA (Best Alternative To a Negotiated Agreement) es tu mejor alternativa si la negociación fracasa. Quien tiene mejor BATNA tiene más poder. Antes de cualquier negociación: ¿qué hago si no llegamos a un acuerdo?\n\n## ZOPA: la zona de acuerdo posible\n\nZOPA (Zone of Possible Agreement) es el rango entre el mínimo aceptable de cada parte. Si tu mínimo es 50.000€ y el máximo del otro es 60.000€, la ZOPA es 50.000-60.000€. Sin ZOPA, no hay acuerdo posible.",
            PuntosClave = "[\"El Método Harvard se basa en 4 principios: separar persona del problema, enfocarse en intereses, generar opciones y usar criterios objetivos\",\"BATNA es tu mejor alternativa si la negociación fracasa — quien tiene mejor BATNA tiene más poder\",\"ZOPA es la zona de acuerdo posible entre los mínimos y máximos de ambas partes\",\"Las posiciones son lo que pides; los intereses son por qué lo pides\",\"Negociar por principios genera acuerdos más duraderos que negociar por posiciones\"]",
            GuionAudio = "Hoy vamos a aprender el método de negociación más utilizado en el mundo: el Método Harvard. Este framework cambió la forma de negociar porque demostró que las mejores negociaciones no son batallas de voluntades, sino ejercicios de resolución conjunta de problemas.",
            PuntosRecompensa = 10, Orden = 1, DuracionMinutos = 10
        };

        Leccion l2 = new Leccion
        {
            Id = 107, NivelId = 28, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Negociación posicional vs. integrativa: elige tu estrategia",
            Descripcion = "Comprende cuándo competir y cuándo colaborar, y por qué la negociación integrativa genera más valor para todos.",
            Contenido = "## Dos paradigmas de negociación\n\n### Negociación posicional (distributiva)\nUna tarta fija que se reparte. Lo que gano yo, lo pierdes tú. Funciona en transacciones únicas donde no importa la relación futura.\n\n**Tácticas comunes:** anclar alto, hacer concesiones pequeñas, usar el tiempo como presión, poli bueno/poli malo.\n\n### Negociación integrativa (colaborativa)\nExpandir la tarta antes de repartirla. Buscar formas de crear más valor para todos. Ideal cuando la relación importa y hay múltiples variables.\n\n**Tácticas clave:** compartir información selectivamente, hacer preguntas exploratorias, ofrecer paquetes de opciones, intercambiar concesiones de distinto valor.\n\n## La matriz de resultados\n\n| | Yo gano | Yo pierdo |\n|---|---|---|\n| **Tú ganas** | Win-Win (integrativa) | Acomodación |\n| **Tú pierdes** | Win-Lose (posicional) | Lose-Lose |\n\n## Cuándo usar cada una\n\n**Posicional:** compraventa de coche usado, negociación con proveedor que no volverás a ver, subasta.\n\n**Integrativa:** negociación salarial (quieres seguir en la empresa), alianzas estratégicas, contratos de largo plazo, negociaciones entre departamentos.\n\n## El error más común\n\nTratar negociaciones integrativas como posicionales. Si anclas agresivamente en una negociación salarial, puedes ganar 5.000€ más pero perder confianza, proyectos y oportunidades durante años.\n\n## El dilema del negociador\n\nPara crear valor necesitas compartir información. Pero compartir información te hace vulnerable si el otro compite. La solución: reciprocidad gradual. Comparte un dato pequeño y observa si el otro reciproca.",
            PuntosClave = "[\"La negociación posicional reparte una tarta fija; la integrativa la expande\",\"La negociación integrativa funciona mejor cuando la relación importa y hay múltiples variables\",\"El error más común es usar tácticas posicionales en contextos que requieren colaboración\",\"La reciprocidad gradual resuelve el dilema entre compartir información y protegerse\",\"Win-Win no significa ceder: significa crear más valor para ambas partes\"]",
            GuionAudio = "¿Negociar es ganar o es crear? Hoy vamos a comparar dos enfoques radicalmente distintos: la negociación posicional, donde uno gana y otro pierde, y la integrativa, donde ambas partes pueden salir ganando. Entender cuándo usar cada una es lo que separa a un negociador novato de uno experto.",
            PuntosRecompensa = 10, Orden = 2, DuracionMinutos = 10
        };

        Leccion l3 = new Leccion
        {
            Id = 108, NivelId = 28, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Fundamentos de Negociación",
            Descripcion = "Pon a prueba tus conocimientos sobre el Método Harvard y los tipos de negociación.",
            PuntosRecompensa = 15, Orden = 3, DuracionMinutos = 5
        };

        Leccion l4 = new Leccion
        {
            Id = 109, NivelId = 28, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: La renovación del contrato con el proveedor",
            Descripcion = "Tu proveedor clave quiere subir precios un 20%. Aplica el Método Harvard para encontrar una solución que funcione para ambas partes.",
            PuntosRecompensa = 20, Orden = 4, DuracionMinutos = 10
        };

        Leccion l5 = new Leccion
        {
            Id = 110, NivelId = 28, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Negocia el presupuesto de tu proyecto",
            Descripcion = "Tu director quiere recortar un 30% del presupuesto de tu proyecto. Practica cómo negociar usando intereses y opciones creativas.",
            Contenido = "Tu director financiero acaba de comunicarte que necesita recortar un 30% del presupuesto de tu proyecto. Tú sabes que con ese recorte no puedes entregar los resultados comprometidos. Debes negociar para proteger los entregables clave sin perder la relación con finanzas.",
            GuionAudio = "En este roleplay seré tu director financiero. Tengo presión para recortar costes y tu proyecto es uno de los candidatos. Convénceme de que recortar un 30% sería un error, pero ofréceme alternativas realistas. Usa lo que aprendiste sobre BATNA, ZOPA e intereses.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 10
        };

        contexto.Set<Leccion>().AddRange(l1, l2, l3, l4, l5);
        await contexto.SaveChangesAsync();

        // Escenas teóricas Nivel 1
        await SembrarEscenasDesdeLeccionAsync(contexto, l1,
            "Hoy aprenderás el framework de negociación más importante del mundo. Después de esta lección, nunca volverás a negociar igual.",
            "Antes de tu próxima negociación, identifica tu BATNA y calcula la ZOPA. Con esos dos datos, entrarás con una ventaja enorme.");

        await SembrarEscenasDesdeLeccionAsync(contexto, l2,
            "¿Negociar es un juego de suma cero o se puede expandir la tarta? Hoy descubrirás que la respuesta depende del contexto y que elegir mal tu estrategia tiene consecuencias graves.",
            "La próxima vez que negocies, pregúntate: ¿quiero ganar esta batalla o construir una relación? La respuesta te dirá qué estrategia usar.");

        // Quiz y escenario Nivel 1
        await SembrarQuizNegociacionAsync(contexto, l3.Id);
        await SembrarEscenarioNegociacionAsync(contexto, l4.Id);

        // ===== NIVEL 2 — Práctica (NivelId=29) =====
        Leccion n2l1 = new Leccion
        {
            Id = 136, NivelId = 29, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Tácticas de anclaje y contraanclaje",
            Descripcion = "Domina la técnica más poderosa de la negociación: quién pone el primer número controla la conversación.",
            Contenido = "## El poder del ancla\n\nEl anclaje es un sesgo cognitivo demostrado por Tversky y Kahneman: el primer número sobre la mesa influye desproporcionadamente en el resultado final. En un estudio, agentes inmobiliarios profesionales variaron sus tasaciones hasta un 12% dependiendo del precio de lista que les mostraron.\n\n## Cuándo anclar primero\n\nAncla primero cuando:\n- Tienes buena información sobre el rango justo\n- Quieres controlar el marco de la negociación\n- La otra parte es menos experta\n\nNo ancles primero cuando:\n- Tienes poca información sobre lo que el otro aceptaría\n- Hay mucha asimetría de información\n- Podrías anclar demasiado bajo y dejar valor sobre la mesa\n\n## Técnicas de anclaje efectivo\n\n### Ancla agresiva pero justificable\nPide más de lo que esperas, pero ten una justificación creíble. «Basándome en el estudio de mercado de Mercer, el rango para este puesto es 65.000-80.000€».\n\n### Ancla precisa\nUn estudio de Columbia Business School demostró que anclas precisas (67.350€) son más efectivas que anclas redondas (67.000€) porque sugieren que has hecho tu investigación.\n\n### Ancla con rango\nOfrecer un rango (65.000-75.000€) es percibido como más colaborativo. El truco: tu número ideal debe ser el límite inferior del rango.\n\n## Técnicas de contraanclaje\n\n### Ignora el ancla\nNo reacciones al número. Reformula: «Entiendo tu propuesta. Déjame compartir mi perspectiva basada en estos datos».\n\n### Reancla con datos\nPresenta tu propio ancla respaldada por criterios objetivos: benchmarks de mercado, precedentes, datos verificables.\n\n### Descompón el ancla\nSi alguien pide 100.000€ por un servicio, pregunta: «¿Puedes desglosarme cómo llegas a esa cifra?». Al descomponer, los números inflados se hacen evidentes.\n\n### La pausa estratégica\nAntes de responder a un ancla, haz silencio durante 5-7 segundos. Esto comunica que estás evaluando seriamente y no aceptarás automáticamente.",
            PuntosClave = "[\"El anclaje es un sesgo cognitivo: el primer número influye desproporcionadamente en el resultado\",\"Ancla primero cuando tienes buena información sobre el rango justo\",\"Las anclas precisas (67.350€) son más efectivas que las redondas (67.000€)\",\"Para contraanclar: ignora, reancla con datos o descompón la cifra del otro\",\"La pausa de 5-7 segundos tras recibir un ancla comunica que no aceptarás automáticamente\"]",
            GuionAudio = "Hoy vamos a dominar la técnica más poderosa de cualquier negociación: el anclaje. Quien pone el primer número sobre la mesa tiene una ventaja enorme, y los estudios de Tversky y Kahneman lo demuestran con datos aplastantes.",
            PuntosRecompensa = 15, Orden = 1, DuracionMinutos = 12
        };

        Leccion n2l2 = new Leccion
        {
            Id = 137, NivelId = 29, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Negociación salarial: frameworks para maximizar tu compensación",
            Descripcion = "Aprende los frameworks probados para negociar tu salario, bonus y condiciones con confianza y datos.",
            Contenido = "## Por qué la mayoría no negocia su salario\n\nSegún un estudio de Salary.com, el 57% de los profesionales no negocia su oferta salarial. Quienes lo hacen obtienen de media un 7-10% más. En una carrera de 30 años, esa diferencia inicial se traduce en más de 500.000€.\n\n## El framework de 5 pasos para negociación salarial\n\n### Paso 1: Investiga tu valor de mercado\nUsa Glassdoor, LinkedIn Salary, informes de Robert Half y Hays. Cruza al menos 3 fuentes. Conoce el rango para tu puesto, sector, ubicación y experiencia.\n\n### Paso 2: Define tu paquete completo\nEl salario es solo una parte. Negocia el paquete: salario base, bonus, equity, teletrabajo, formación, vacaciones, seguro médico, horario flexible. Si el salario tiene techo, amplía la conversación a otras variables.\n\n### Paso 3: Deja que el otro diga el primer número (excepción al anclaje)\nEn negociación salarial, dejar que la empresa revele su rango primero te da información valiosa. Si te presionan: «Prefiero entender el rango del puesto antes de hablar de cifras».\n\n### Paso 4: Presenta tu caso con datos\nNo digas «quiero más dinero». Di: «Según Glassdoor y Robert Half, el rango de mercado para este puesto en Madrid es 55.000-70.000€. Con mis 8 años de experiencia y certificación PMP, me posiciono en el percentil 75».\n\n### Paso 5: Gestiona el silencio y las objeciones\nDespués de presentar tu cifra, calla. El silencio es tu aliado. Si dicen «no hay presupuesto», pregunta: «¿Qué tendría que pasar para que sea posible en 6 meses?».\n\n## Técnicas avanzadas\n\n### El espejo\nRepite las últimas 3 palabras del otro como pregunta. «No podemos ir más allá de 52.000€.» → «¿Más allá de 52.000€?». Esto les obliga a explicar y frecuentemente a flexibilizar.\n\n### El etiquetado (Chris Voss)\n«Parece que el presupuesto está muy ajustado este año.» Etiquetar la emoción del otro genera confianza y abre la puerta a soluciones creativas.\n\n### La pregunta calibrada\n«¿Cómo puedo ayudarte a justificar este paquete internamente?» Transforma la negociación en colaboración.",
            PuntosClave = "[\"El 57% de los profesionales no negocia su salario — quienes lo hacen ganan de media un 7-10% más\",\"Investiga tu valor en al menos 3 fuentes antes de negociar\",\"Negocia el paquete completo: salario, bonus, teletrabajo, formación, no solo el número base\",\"El espejo y el etiquetado de Chris Voss son técnicas poderosas para obtener información\",\"Después de presentar tu cifra, el silencio es tu mejor aliado\"]",
            GuionAudio = "¿Sabías que no negociar tu salario puede costarte más de medio millón de euros a lo largo de tu carrera? Hoy vamos a aprender un framework de 5 pasos para que nunca más dejes dinero sobre la mesa.",
            PuntosRecompensa = 15, Orden = 2, DuracionMinutos = 12
        };

        Leccion n2l3 = new Leccion
        {
            Id = 138, NivelId = 29, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Tácticas de Negociación",
            Descripcion = "Evalúa tu dominio de las tácticas de anclaje, contraanclaje y negociación salarial.",
            PuntosRecompensa = 20, Orden = 3, DuracionMinutos = 7
        };

        Leccion n2l4 = new Leccion
        {
            Id = 139, NivelId = 29, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Tu primera negociación salarial",
            Descripcion = "Te han ofrecido un puesto con un salario por debajo de tu expectativa. Decide cómo responder usando los frameworks aprendidos.",
            PuntosRecompensa = 25, Orden = 4, DuracionMinutos = 12
        };

        Leccion n2l5 = new Leccion
        {
            Id = 140, NivelId = 29, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Contraanclaje con un cliente agresivo",
            Descripcion = "Un cliente ha anclado un precio un 40% por debajo de tu tarifa. Practica técnicas de contraanclaje manteniendo la relación.",
            Contenido = "Un cliente potencial te ha dicho que su presupuesto máximo es 30.000€ para un proyecto que normalmente cotizas en 50.000€. Sabes que tiene presupuesto mayor porque ha contratado servicios similares por 45.000€ con otros proveedores. Debes contraanclar sin perder la oportunidad.",
            GuionAudio = "En este roleplay seré un cliente que intenta anclar muy bajo. Tu trabajo es no reaccionar al ancla, reanclar con datos y explorar opciones creativas. Recuerda: la pausa estratégica y la descomposición del ancla son tus mejores herramientas.",
            PuntosRecompensa = 25, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n2l1, n2l2, n2l3, n2l4, n2l5);
        await contexto.SaveChangesAsync();

        // Escenas teóricas Nivel 2
        await SembrarEscenasDesdeLeccionAsync(contexto, n2l1,
            "Hoy dominarás la técnica más estudiada de la negociación: el anclaje. Tversky y Kahneman demostraron que el primer número influye en todo lo que viene después.",
            "En tu próxima negociación, prepara un ancla precisa con una justificación sólida antes de sentarte a la mesa.");

        await SembrarEscenasDesdeLeccionAsync(contexto, n2l2,
            "No negociar tu salario te puede costar más de 500.000 euros a lo largo de tu carrera. Hoy aprenderás un framework de 5 pasos para no dejar dinero sobre la mesa.",
            "Antes de tu próxima revisión salarial, investiga tu valor de mercado en Glassdoor, LinkedIn Salary y al menos un informe sectorial.");

        // ===== NIVEL 3 — Dominio (NivelId=30) =====
        Leccion n3l1 = new Leccion
        {
            Id = 166, NivelId = 30, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Negociación multicultural: el mapa de Erin Meyer",
            Descripcion = "Domina las diferencias culturales que determinan el éxito o fracaso de negociaciones internacionales.",
            Contenido = "## Por qué fracasan las negociaciones internacionales\n\nErin Meyer, profesora de INSEAD, investigó las dimensiones culturales que afectan la negociación. Su libro «The Culture Map» revela que lo que funciona en Madrid puede ser desastroso en Tokio o Riad.\n\n## Las 8 dimensiones de Erin Meyer aplicadas a la negociación\n\n### 1. Comunicación: bajo contexto vs. alto contexto\n- **Bajo contexto** (EEUU, Alemania, Holanda): sé directo, di exactamente lo que quieres.\n- **Alto contexto** (Japón, China, Arabia Saudí): lee entre líneas. «Lo consideraremos» puede significar «no».\n\n### 2. Confianza: basada en tareas vs. basada en relaciones\n- **Tareas** (EEUU, Alemania): la confianza se gana cumpliendo. Ve al grano.\n- **Relaciones** (China, Brasil, Arabia Saudí): invierte en cenas, conversación personal y tiempo antes de hablar de negocios. Intentar cerrar en la primera reunión es un error fatal.\n\n### 3. Confrontación: confrontar vs. evitar\n- **Confrontar** (Francia, Israel, Rusia): el desacuerdo abierto es señal de compromiso.\n- **Evitar** (Japón, Indonesia, Tailandia): el desacuerdo directo causa pérdida de «cara». Usa intermediarios.\n\n### 4. Toma de decisiones: top-down vs. consenso\n- **Top-down** (EEUU, China, India): negocia con el decisor. Una persona dice sí y se ejecuta rápido.\n- **Consenso** (Japón, Suecia, Alemania): el proceso es largo pero la implementación es inmediata. Ringi-sho japonés: la propuesta circula hasta que todos firman.\n\n## Errores fatales por cultura\n\n| Error | Cultura afectada |\n|---|---|\n| Ir directo al precio en la primera reunión | China, Japón, Arabia Saudí |\n| No llevar tarjetas de visita | Japón, Corea del Sur |\n| Rechazar comida o bebida | Oriente Medio, Asia |\n| Mostrar la suela del zapato | Países árabes |\n| Apretar demasiado la mano | Japón |\n\n## Framework GLOBE para preparar una negociación internacional\n\n1. **Investiga** la cultura usando The Culture Map de Meyer\n2. **Adapta** tu estilo de comunicación (directo vs. indirecto)\n3. **Invierte** en relación si la cultura lo requiere\n4. **Identifica** quién decide realmente (puede no ser quien está en la mesa)\n5. **Respeta** los tiempos: algunas culturas necesitan múltiples reuniones",
            PuntosClave = "[\"Erin Meyer identifica 8 dimensiones culturales que afectan la negociación\",\"En culturas de alto contexto (Japón, China) hay que leer entre líneas: un sí puede ser un no\",\"En culturas relacionales, intentar cerrar en la primera reunión es un error fatal\",\"En Japón y Suecia las decisiones son por consenso: el proceso es lento pero la implementación es inmediata\",\"El framework GLOBE: Investiga, Adapta, Invierte en relación, Identifica al decisor y Respeta los tiempos\"]",
            GuionAudio = "¿Sabías que lo que funciona perfectamente en una negociación en Madrid puede ser un desastre en Tokio? Hoy vamos a explorar el mapa cultural de Erin Meyer y cómo las diferencias culturales determinan quién gana y quién pierde en negociaciones internacionales.",
            PuntosRecompensa = 20, Orden = 1, DuracionMinutos = 15
        };

        Leccion n3l2 = new Leccion
        {
            Id = 167, NivelId = 30, TipoLeccion = TipoLeccion.Teoria,
            Titulo = "Negociación de alto stake: M&A, contratos clave y acuerdos millonarios",
            Descripcion = "Aprende las técnicas que usan los negociadores de élite en operaciones de fusiones, adquisiciones y contratos de alto impacto.",
            Contenido = "## Qué cambia en negociaciones de alto stake\n\nCuando los números tienen 6 o más ceros, todo se intensifica: la presión emocional, la complejidad de los actores, el número de variables y las consecuencias de un error. Los principios básicos siguen aplicando, pero necesitas herramientas adicionales.\n\n## El framework de negociación de alto stake\n\n### 1. Preparación exhaustiva (80% del éxito)\nEn M&A, la due diligence no es solo financiera. Mapea:\n- **Stakeholders ocultos**: ¿quién influye realmente en la decisión? El CEO puede no ser el decisor real.\n- **Motivaciones no financieras**: legado, ego, miedo al fracaso, presión del consejo.\n- **Deal breakers**: identifica los puntos que harán que la otra parte se levante de la mesa.\n- **BATNA de ambas partes**: en M&A, tu BATNA puede ser otra adquisición o crecimiento orgánico.\n\n### 2. Estructura de la negociación\n\n**Negociación por paquetes, no por puntos aislados:**\nEn un contrato complejo, nunca negocies precio, plazo, garantías y penalizaciones por separado. Presenta paquetes completos donde las concesiones en un área se compensan en otra.\n\n**Múltiples ofertas simultáneas (MESOs):**\nPresenta 3 opciones equivalentes en valor para ti pero distintas en estructura. Esto revela las prioridades del otro sin que tú preguntes directamente.\n\n### 3. Gestión emocional bajo presión extrema\n\n**La regla del balcón (Ury):**\nCuando la tensión sube, imagina que te levantas y vas a un balcón desde el que observas la negociación desde arriba. Esta distancia mental te permite responder en lugar de reaccionar.\n\n**Tácticas de presión comunes en alto stake:**\n- «Tómalo o déjalo» (ultimátum) → Responde: «Entiendo la urgencia. ¿Puedes ayudarme a entender qué impulsa ese plazo?»\n- «Tenemos otra oferta mejor» → Responde: «Me alegro de que tengan opciones. Centrémonos en el valor único que nosotros aportamos».\n- Cambio de negociador → No repitas concesiones. «Según lo acordado con [nombre], partimos de estos puntos».\n\n### 4. Cláusulas de protección\n\nEn contratos de alto stake, incluye siempre:\n- **Earn-out**: parte del pago vinculado a resultados futuros. Reduce riesgo para el comprador.\n- **Cláusula de no competencia**: protege la inversión.\n- **Mecanismo de resolución de disputas**: arbitraje vs. tribunales, jurisdicción, idioma.\n- **Cláusula MAC** (Material Adverse Change): permite renegociar si las condiciones cambian drásticamente.\n\n### 5. El cierre\n\nEn alto stake, el cierre no es un momento sino un proceso. Usa el **cierre condicional**: «Si podemos acordar X en estos términos, estoy listo para firmar el resto del paquete hoy».",
            PuntosClave = "[\"En negociaciones de alto stake, la preparación representa el 80% del éxito\",\"Negocia por paquetes completos, nunca punto por punto aislado\",\"MESOs (múltiples ofertas simultáneas) revelan las prioridades del otro sin preguntar\",\"La regla del balcón de Ury: distancia mental para responder en lugar de reaccionar\",\"Cláusulas clave en alto stake: earn-out, no competencia, MAC y mecanismo de disputas\"]",
            GuionAudio = "Cuando hay millones sobre la mesa, las reglas cambian. Hoy vamos a explorar las técnicas que usan los negociadores de élite en fusiones, adquisiciones y contratos que pueden definir el futuro de una empresa.",
            PuntosRecompensa = 20, Orden = 2, DuracionMinutos = 15
        };

        Leccion n3l3 = new Leccion
        {
            Id = 168, NivelId = 30, TipoLeccion = TipoLeccion.Quiz,
            Titulo = "Quiz: Negociación Avanzada",
            Descripcion = "Evalúa tu dominio de la negociación multicultural y de alto stake.",
            PuntosRecompensa = 25, Orden = 3, DuracionMinutos = 8
        };

        Leccion n3l4 = new Leccion
        {
            Id = 169, NivelId = 30, TipoLeccion = TipoLeccion.Escenario,
            Titulo = "Escenario: Negociación de alianza estratégica con empresa japonesa",
            Descripcion = "Tu empresa quiere cerrar una alianza con una compañía japonesa. Navega las diferencias culturales para llegar a un acuerdo.",
            PuntosRecompensa = 30, Orden = 4, DuracionMinutos = 15
        };

        Leccion n3l5 = new Leccion
        {
            Id = 170, NivelId = 30, TipoLeccion = TipoLeccion.Roleplay,
            Titulo = "Roleplay: Cierre de adquisición bajo presión",
            Descripcion = "Estás en la fase final de una adquisición por 12M€. El vendedor ha cambiado de negociador y usa tácticas de presión.",
            Contenido = "Llevas 3 meses negociando la adquisición de una empresa tecnológica por 12 millones de euros. En la reunión final, el vendedor aparece con un nuevo abogado que cuestiona los términos ya acordados y plantea un ultimátum: o aceptas sus nuevas condiciones hoy o la operación se cancela. Sabes que tu BATNA es fuerte (hay otra empresa objetivo) pero esta adquisición tiene sinergias únicas.",
            GuionAudio = "Este es el roleplay más desafiante del curso. Seré el nuevo abogado del vendedor que intenta renegociar todo en el último momento. Mantén la calma, usa la regla del balcón, no repitas concesiones ya acordadas y recuerda que tu BATNA te da poder.",
            PuntosRecompensa = 30, Orden = 5, DuracionMinutos = 15
        };

        contexto.Set<Leccion>().AddRange(n3l1, n3l2, n3l3, n3l4, n3l5);
        await contexto.SaveChangesAsync();

        // Escenas teóricas Nivel 3
        await SembrarEscenasDesdeLeccionAsync(contexto, n3l1,
            "Lo que funciona en Madrid puede fracasar en Tokio. Hoy exploraremos las dimensiones culturales que determinan el éxito en negociaciones internacionales.",
            "Antes de tu próxima negociación internacional, consulta The Culture Map de Erin Meyer y adapta tu estilo a las dimensiones clave de esa cultura.");

        await SembrarEscenasDesdeLeccionAsync(contexto, n3l2,
            "En operaciones de M&A y contratos millonarios, la preparación es el 80% del éxito. Hoy aprenderás las herramientas que usan los negociadores de élite.",
            "En tu próxima negociación compleja, prepara 3 paquetes de oferta equivalentes en valor pero distintos en estructura. Las prioridades del otro se revelarán solas.");
    }

    /// <summary>
    /// Siembra las preguntas del quiz de Negociación (Nivel 1).
    /// </summary>
    private static async Task SembrarQuizNegociacionAsync(AppDbContext contexto, int leccionId)
    {
        List<PreguntaQuiz> preguntas = new List<PreguntaQuiz>
        {
            CrearPregunta(leccionId, 1, "¿Qué significa BATNA en el Método Harvard?",
                "BATNA es la mejor alternativa a un acuerdo negociado — tu plan B si la negociación fracasa.",
                new[] { ("Best Agreement Through Negotiation Approach", false, "BATNA no se refiere a un enfoque de negociación, sino a tu alternativa si no hay acuerdo."), ("Best Alternative To a Negotiated Agreement", true, "¡Correcto! BATNA es tu mejor alternativa si la negociación no prospera. Es tu mayor fuente de poder."), ("Basic Assessment of Total Negotiation Assets", false, "BATNA no es una evaluación de activos sino tu mejor alternativa fuera del acuerdo."), ("Bilateral Agreement for Trade and Negotiation Advancement", false, "BATNA es un concepto de Harvard, no un tipo de acuerdo bilateral.") }),
            CrearPregunta(leccionId, 2, "¿Cuál es la diferencia clave entre negociación posicional e integrativa?",
                "La posicional reparte una tarta fija; la integrativa busca expandirla creando más valor.",
                new[] { ("La posicional es más rápida", false, "La velocidad no es la diferencia fundamental entre ambos enfoques."), ("La integrativa solo funciona entre amigos", false, "La integrativa funciona en cualquier contexto donde la relación importe y haya múltiples variables."), ("La posicional reparte valor fijo; la integrativa crea más valor", true, "¡Correcto! La integrativa busca expandir la tarta antes de repartirla, beneficiando a ambas partes."), ("No hay diferencia real, son sinónimos", false, "Son enfoques fundamentalmente distintos con tácticas y resultados muy diferentes.") }),
            CrearPregunta(leccionId, 3, "Según el Método Harvard, ¿en qué debes enfocarte durante una negociación?",
                "El segundo principio de Harvard dice: enfócate en intereses, no en posiciones.",
                new[] { ("En ganar a toda costa", false, "El Método Harvard busca acuerdos mutuamente beneficiosos, no victorias unilaterales."), ("En las posiciones de cada parte", false, "Las posiciones son superficiales. Los intereses subyacentes son lo que realmente importa."), ("En los intereses subyacentes de cada parte", true, "¡Correcto! Los intereses revelan el 'por qué' detrás de lo que cada parte pide."), ("En ceder rápidamente para mantener la relación", false, "Ceder no es negociar. El Método Harvard busca satisfacer intereses de ambas partes.") }),
            CrearPregunta(leccionId, 4, "¿Qué es la ZOPA?",
                "ZOPA es la zona de acuerdo posible entre los mínimos y máximos aceptables de cada parte.",
                new[] { ("La zona de presión en una negociación", false, "ZOPA no se refiere a presión sino al rango donde el acuerdo es posible."), ("El rango de acuerdo posible entre ambas partes", true, "¡Correcto! Si tu mínimo es 50.000€ y el máximo del otro es 60.000€, la ZOPA es ese rango."), ("Una técnica de cierre rápido", false, "ZOPA no es una técnica sino un concepto analítico para evaluar si el acuerdo es viable."), ("El mejor resultado posible para ti", false, "ZOPA es el rango compartido, no tu resultado ideal individual.") }),
            CrearPregunta(leccionId, 5, "¿Cuál de estos es un principio del Método Harvard?",
                "Los 4 principios son: separar persona del problema, enfocarse en intereses, generar opciones de beneficio mutuo y usar criterios objetivos.",
                new[] { ("Anclar siempre con el número más alto posible", false, "El anclaje es una táctica posicional, no un principio de Harvard."), ("Insistir en criterios objetivos para basar el acuerdo", true, "¡Correcto! Usar estándares independientes como valor de mercado o precedentes es el cuarto principio de Harvard."), ("No revelar nunca tu BATNA", false, "Proteger tu BATNA es prudente, pero no es uno de los 4 principios."), ("Hacer concesiones rápidas para generar buena voluntad", false, "Harvard no aboga por concesiones rápidas sino por explorar intereses y generar opciones.") })
        };

        contexto.Set<PreguntaQuiz>().AddRange(preguntas);
        await contexto.SaveChangesAsync();
    }

    /// <summary>
    /// Siembra el escenario de Negociación (Nivel 1).
    /// </summary>
    private static async Task SembrarEscenarioNegociacionAsync(AppDbContext contexto, int leccionId)
    {
        Escenario escenario = new Escenario
        {
            LeccionId = leccionId,
            TextoSituacion = "Eres el responsable de compras de tu empresa. Tu proveedor principal de software lleva 5 años contigo y quiere renovar el contrato con un aumento del 20% (de 100.000€ a 120.000€ anuales). Argumenta que los costes de desarrollo han subido y que el mercado paga más. Tu presupuesto máximo aprobado es 110.000€, pero sabes que necesitas añadir un módulo nuevo que costaría 15.000€ aparte.",
            Contexto = "El proveedor es fiable y cambiar tendría un coste de migración estimado en 80.000€. Sin embargo, has recibido una propuesta de un competidor por 95.000€ anuales que incluye el módulo nuevo, aunque con menor soporte. Tu BATNA es aceptar al competidor.",
            GuionAudio = "Este escenario pondrá a prueba tu capacidad de aplicar el Método Harvard. Tienes un BATNA claro, hay una ZOPA que debes calcular y las posibilidades de acuerdo integrativo son reales si separas la persona del problema y exploras intereses."
        };

        contexto.Set<Escenario>().Add(escenario);
        await contexto.SaveChangesAsync();

        List<OpcionEscenario> opciones = new List<OpcionEscenario>
        {
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 1,
                TextoOpcion = "Reconocer el valor de la relación de 5 años, compartir que tienes alternativas sin amenazar, y proponer un paquete: mantener el precio en 108.000€ pero incluir el módulo nuevo y extender el contrato a 3 años. Así el proveedor gana estabilidad y tú consigues el módulo sin coste extra.",
                TipoResultado = TipoResultadoEscenario.Optimo, PuntosOtorgados = 20,
                TextoRetroalimentacion = "¡Excelente negociación integrativa! Separaste persona del problema, exploraste intereses mutuos (estabilidad para él, módulo para ti), generaste opciones creativas y te mantuviste dentro de tu presupuesto. El paquete de 3 años le da previsibilidad al proveedor, lo que compensa el precio menor."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 2,
                TextoOpcion = "Decir que tu presupuesto máximo es 110.000€, que no puedes ir más allá, y pedirle que incluya el módulo nuevo como gesto de buena voluntad por los 5 años de relación.",
                TipoResultado = TipoResultadoEscenario.Aceptable, PuntosOtorgados = 10,
                TextoRetroalimentacion = "Resultado aceptable pero no óptimo. Te mantuviste en tu presupuesto, pero la negociación fue posicional: pusiste un número y pediste un favor. No exploraste los intereses del proveedor ni ofreciste nada a cambio del módulo. Podías haber obtenido más creando opciones de beneficio mutuo."
            },
            new OpcionEscenario
            {
                EscenarioId = escenario.Id, Orden = 3,
                TextoOpcion = "Decirle directamente que tienes una oferta de un competidor por 95.000€ con el módulo incluido, y que si no iguala esa oferta, cambiarás de proveedor.",
                TipoResultado = TipoResultadoEscenario.Inadecuado, PuntosOtorgados = 0,
                TextoRetroalimentacion = "Esta es una táctica posicional agresiva que puede destruir una relación de 5 años. Amenazar con la competencia pone al proveedor a la defensiva y convierte la negociación en un juego de poder. Además, revelaste tu BATNA completamente, perdiendo poder negociador. El Método Harvard propone mencionar alternativas sin amenazar y enfocarse en criterios objetivos."
            }
        };

        contexto.Set<OpcionEscenario>().AddRange(opciones);
        await contexto.SaveChangesAsync();
    }
}
