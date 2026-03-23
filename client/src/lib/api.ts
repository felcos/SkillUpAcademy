const API_BASE = '/api/v1';

interface RequestOptions {
  method?: string;
  body?: unknown;
  headers?: Record<string, string>;
}

class ApiError extends Error {
  constructor(public status: number, message: string) {
    super(message);
    this.name = 'ApiError';
  }
}

function getToken(): string | null {
  return localStorage.getItem('token');
}

async function request<T>(endpoint: string, options: RequestOptions = {}): Promise<T> {
  const { method = 'GET', body, headers = {} } = options;

  const token = getToken();
  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }

  if (body) {
    headers['Content-Type'] = 'application/json';
  }

  const response = await fetch(`${API_BASE}${endpoint}`, {
    method,
    headers,
    body: body ? JSON.stringify(body) : undefined,
  });

  if (response.status === 204) {
    return undefined as T;
  }

  const data = await response.json();

  if (!response.ok) {
    throw new ApiError(response.status, data.mensaje || data.message || 'Error del servidor');
  }

  return data as T;
}

// ============ AUTH ============
export interface PeticionRegistro {
  nombre: string;
  apellidos: string;
  email: string;
  contrasena: string;
  confirmarContrasena: string;
}

export interface PeticionLogin {
  email: string;
  contrasena: string;
}

export interface RespuestaLogin {
  token: string;
  refreshToken: string;
  expiracion: string;
  usuario: PerfilUsuario;
}

export interface PerfilUsuario {
  id: string;
  nombre: string;
  apellidos: string;
  email: string;
  puntosTotales: number;
  rachaDias: number;
  fechaRegistro: string;
}

export const authApi = {
  registrar: (datos: PeticionRegistro) =>
    request<RespuestaLogin>('/auth/register', { method: 'POST', body: datos }),
  login: (datos: PeticionLogin) =>
    request<RespuestaLogin>('/auth/login', { method: 'POST', body: datos }),
  perfil: () => request<PerfilUsuario>('/auth/me'),
  logout: () => request<void>('/auth/logout', { method: 'POST' }),
};

// ============ SKILLS ============
export interface AreaHabilidad {
  id: number;
  slug: string;
  titulo: string;
  subtitulo: string;
  icono: string;
  colorPrimario: string;
  progreso?: ProgresoResumen;
}

export interface ProgresoResumen {
  leccionesCompletadas: number;
  leccionesTotales: number;
  porcentaje: number;
  nivelActual: number;
}

export interface AreaDetalle extends AreaHabilidad {
  descripcion: string;
  colorAcento: string;
  niveles: Nivel[];
}

export interface Nivel {
  id: number;
  numeroNivel: number;
  nombre: string;
  descripcion: string;
  puntosDesbloqueo: number;
  desbloqueado: boolean;
  leccionesCompletadas: number;
  leccionesTotales: number;
}

export interface NivelDetalle {
  id: number;
  numeroNivel: number;
  nombre: string;
  descripcion: string;
  desbloqueado: boolean;
  lecciones: LeccionResumen[];
}

export interface LeccionResumen {
  id: number;
  titulo: string;
  tipoLeccion: string;
  duracionMinutos: number;
  puntosRecompensa: number;
  estado: string;
  puntuacion?: number;
}

export const skillsApi = {
  listar: () => request<AreaHabilidad[]>('/skills'),
  detalle: (slug: string) => request<AreaDetalle>(`/skills/${slug}`),
  niveles: (slug: string) => request<Nivel[]>(`/skills/${slug}/levels`),
  nivel: (slug: string, numero: number) =>
    request<NivelDetalle>(`/skills/${slug}/levels/${numero}`),
};

// ============ LESSONS ============
export interface LeccionDetalle {
  id: number;
  titulo: string;
  descripcion: string;
  contenido: string;
  puntosClave: string[];
  guionAudio: string;
  tipoLeccion: string;
  duracionMinutos: number;
  puntosRecompensa: number;
}

export interface Escena {
  id: number;
  orden: number;
  tipoContenidoVisual: string;
  tituloEscena: string;
  guionAria: string;
  contenidoVisual: string;
  metadatosVisuales: string;
  transicionEntrada: string;
  layout: string;
  duracionSegundos: number;
  esPausaReflexiva: boolean;
  segundosPausa: number;
  recursos: RecursoVisual[];
}

export interface RecursoVisual {
  id: number;
  tipoRecurso: string;
  nombre: string;
  url: string;
  textoAlternativo: string;
}

export const lessonsApi = {
  detalle: (id: number) => request<LeccionDetalle>(`/lessons/${id}`),
  escenas: (id: number) => request<Escena[]>(`/lessons/${id}/scenes`),
  iniciar: (id: number) => request<void>(`/lessons/${id}/start`, { method: 'POST' }),
  completar: (id: number) => request<unknown>(`/lessons/${id}/complete`, { method: 'POST' }),
};

// ============ QUIZ ============
export interface PreguntaQuiz {
  id: number;
  textoPregunta: string;
  orden: number;
  opciones: OpcionQuiz[];
}

export interface OpcionQuiz {
  id: number;
  textoOpcion: string;
  orden: number;
}

export interface ResultadoQuiz {
  preguntasTotales: number;
  respuestasCorrectas: number;
  puntuacion: number;
  puntosObtenidos: number;
  aprobado: boolean;
  puntuacionMinima: number;
}

export const quizApi = {
  preguntas: (leccionId: number) =>
    request<PreguntaQuiz[]>(`/lessons/${leccionId}/quiz`),
  enviar: (leccionId: number, respuestas: { preguntaId: number; opcionSeleccionadaId: number }[]) =>
    request<ResultadoQuiz>(`/lessons/${leccionId}/quiz/submit`, {
      method: 'POST',
      body: { respuestas },
    }),
};

// ============ SCENARIO ============
export interface EscenarioDto {
  id: number;
  textoSituacion: string;
  contexto: string;
  guionAudio: string;
  opciones: OpcionEscenario[];
}

export interface OpcionEscenario {
  id: number;
  textoOpcion: string;
  orden: number;
}

export interface ResultadoEscenario {
  tipoResultado: string;
  textoRetroalimentacion: string;
  puntosOtorgados: number;
}

export const scenarioApi = {
  obtener: (leccionId: number) =>
    request<EscenarioDto>(`/lessons/${leccionId}/scenario`),
  elegir: (leccionId: number, opcionId: number) =>
    request<ResultadoEscenario>(`/lessons/${leccionId}/scenario/choose`, {
      method: 'POST',
      body: { opcionEscenarioId: opcionId },
    }),
};

// ============ AI CHAT ============
export interface SesionIA {
  id: string;
  tipoSesion: string;
  leccionId?: number;
  fechaInicio: string;
  mensajeInicial: string;
}

export interface MensajeIA {
  id: number;
  rol: string;
  contenido: string;
  fechaEnvio: string;
}

export interface RespuestaMensajeIA {
  respuesta: string;
  urlAudio?: string;
  fueMarcado: boolean;
  tokensUsados: number;
  sugerencias: string[];
}

/** Callback para eventos SSE del chat streaming. */
export interface EventoStreamChat {
  tipo: 'texto' | 'reemplazo' | 'fin';
  contenido?: string;
  fueMarcado?: boolean;
  tokensUsados?: number;
  sugerencias?: string[];
}

export const aiApi = {
  iniciarSesion: (tipoSesion: string, leccionId?: number) =>
    request<SesionIA>('/ai/session/start', {
      method: 'POST',
      body: { tipoSesion, leccionId },
    }),
  enviarMensaje: (sesionId: string, mensaje: string) =>
    request<RespuestaMensajeIA>(`/ai/session/${sesionId}/message`, {
      method: 'POST',
      body: { mensaje },
    }),
  /** Envía un mensaje y recibe la respuesta como stream SSE. */
  enviarMensajeStream: async (
    sesionId: string,
    mensaje: string,
    onEvento: (evento: EventoStreamChat) => void,
    signal?: AbortSignal,
  ): Promise<void> => {
    const token = getToken();
    const headers: Record<string, string> = {
      'Content-Type': 'application/json',
    };
    if (token) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    const response = await fetch(`${API_BASE}/ai/session/${sesionId}/message/stream`, {
      method: 'POST',
      headers,
      body: JSON.stringify({ mensaje }),
      signal,
    });

    if (!response.ok) {
      const data = await response.json().catch(() => ({}));
      throw new ApiError(response.status, data.mensaje || data.message || 'Error del servidor');
    }

    const reader = response.body?.getReader();
    if (!reader) {
      throw new Error('ReadableStream no disponible');
    }

    const decoder = new TextDecoder();
    let buffer = '';

    try {
      while (true) {
        const { done, value } = await reader.read();
        if (done) break;

        buffer += decoder.decode(value, { stream: true });
        const lineas = buffer.split('\n\n');
        buffer = lineas.pop() || '';

        for (const linea of lineas) {
          const limpia = linea.trim();
          if (!limpia.startsWith('data: ')) continue;
          const json = limpia.substring(6);
          try {
            const evento: EventoStreamChat = JSON.parse(json);
            onEvento(evento);
          } catch {
            // Ignorar líneas que no sean JSON válido
          }
        }
      }

      // Procesar buffer restante
      if (buffer.trim().startsWith('data: ')) {
        const json = buffer.trim().substring(6);
        try {
          const evento: EventoStreamChat = JSON.parse(json);
          onEvento(evento);
        } catch {
          // Ignorar
        }
      }
    } finally {
      reader.releaseLock();
    }
  },
  historial: (sesionId: string) =>
    request<MensajeIA[]>(`/ai/session/${sesionId}/history`),
  cerrar: (sesionId: string) =>
    request<void>(`/ai/session/${sesionId}/end`, { method: 'POST' }),
};

// ============ PROGRESS ============
export interface Dashboard {
  puntosTotales: number;
  rachaDias: number;
  leccionesCompletadas: number;
  leccionesTotales: number;
  tiempoTotalMinutos: number;
  progresoPorArea: ProgresoArea[];
}

export interface ProgresoArea {
  areaSlug: string;
  areaTitulo: string;
  areaIcono: string;
  porcentaje: number;
  leccionesCompletadas: number;
  leccionesTotales: number;
}

export interface LogroDto {
  id: number;
  slug: string;
  titulo: string;
  descripcion: string;
  icono: string;
  desbloqueado: boolean;
  fechaDesbloqueo?: string;
}

export const progressApi = {
  dashboard: () => request<Dashboard>('/progress/dashboard'),
  logros: () => request<LogroDto[]>('/progress/achievements'),
};

export { ApiError };
