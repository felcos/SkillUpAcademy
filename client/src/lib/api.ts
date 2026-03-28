const API_BASE = '/api/v1';

interface RequestOptions {
  method?: string;
  body?: unknown;
  headers?: Record<string, string>;
}

class ApiError extends Error {
  status: number;
  constructor(status: number, message: string) {
    super(message);
    this.status = status;
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
    // El backend devuelve { error: { code, message, details } }
    const mensaje = data.error?.message || data.mensaje || data.message || 'Error del servidor';
    throw new ApiError(response.status, mensaje);
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
  tokenAcceso: string;
  tokenRenovacion: string;
  expiraEnSegundos: number;
  usuario: PerfilUsuario;
}

export interface PerfilUsuario {
  id: string;
  email: string;
  nombre: string;
  apellidos: string;
  urlAvatar: string | null;
  puntosTotales: number;
  rachaDias: number;
  audioHabilitado: boolean;
  idiomaPreferido: string;
  roles: string[];
  esAdmin: boolean;
  vozPreferida: string | null;
  velocidadVoz: number;
  proveedorTtsPreferido: string;
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
  elegir: (leccionId: number, escenarioId: number, opcionId: number) =>
    request<ResultadoEscenario>(`/lessons/${leccionId}/scenario/choose`, {
      method: 'POST',
      body: { escenarioId, opcionId },
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

// ============ PLAN DE ACCION ============
export interface PlanAccionDto {
  id: number;
  leccionId: number;
  tituloLeccion: string;
  compromiso: string;
  contextoAplicacion: string;
  fechaObjetivo: string;
  completado: boolean;
  reflexionResultado?: string;
  fechaCreacion: string;
  fechaCompletado?: string;
}

export const planAccionApi = {
  listar: () => request<PlanAccionDto[]>('/plan-accion'),
  crear: (datos: { leccionId: number; compromiso: string; contextoAplicacion: string; fechaObjetivo: string }) =>
    request<PlanAccionDto>('/plan-accion', { method: 'POST', body: datos }),
  completar: (id: number, reflexionResultado: string) =>
    request<void>(`/plan-accion/${id}/completar`, { method: 'PUT', body: { reflexionResultado } }),
};

// ============ PROGRESS ============
export interface Dashboard {
  puntosTotales: number;
  rachaDias: number;
  leccionesCompletadas: number;
  leccionesTotales: number;
  resumenAreas: ResumenArea[];
  logrosRecientes: LogroReciente[];
  siguienteLeccionRecomendada: LeccionRecomendada | null;
}

export interface ResumenArea {
  slug: string;
  titulo: string;
  icono: string | null;
  colorPrimario: string | null;
  porcentaje: number;
  nivelActual: number;
}

export interface LogroReciente {
  titulo: string;
  icono: string | null;
  fechaDesbloqueo: string;
}

export interface LeccionRecomendada {
  id: number;
  titulo: string;
  areaHabilidad: string;
  tipoLeccion: string;
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

// === Admin ===

export interface ActividadDiaria {
  fecha: string;
  usuariosActivos: number;
  leccionesCompletadas: number;
  mensajesIA: number;
}

export interface ResumenAdmin {
  totalUsuarios: number;
  usuariosActivos7Dias: number;
  totalSesionesIA: number;
  totalMensajesIA: number;
  totalLeccionesCompletadas: number;
  totalQuizzesCompletados: number;
  promedioProgreso: number;
  actividadUltimos30Dias: ActividadDiaria[];
}

export interface UsuarioAdmin {
  id: string;
  email: string;
  nombreCompleto: string;
  fechaRegistro: string;
  ultimoAcceso: string | null;
  leccionesCompletadas: number;
  logrosDesbloqueados: number;
  progresoGeneral: number;
  estaBloqueadoIA: boolean;
}

export interface RespuestaUsuariosAdmin {
  usuarios: UsuarioAdmin[];
  total: number;
  pagina: number;
  tamano: number;
}

export interface AreaEstadistica {
  nombreArea: string;
  vecesCompletada: number;
  promedioCalificacion: number;
}

export interface EstadisticasContenido {
  totalAreas: number;
  totalNiveles: number;
  totalLecciones: number;
  totalPreguntas: number;
  totalEscenarios: number;
  totalEscenas: number;
  areasPorCompletados: AreaEstadistica[];
}

export const adminApi = {
  obtenerResumen: () => request<ResumenAdmin>('/admin/resumen'),
  obtenerUsuarios: (pagina: number, tamano: number) =>
    request<RespuestaUsuariosAdmin>(`/admin/usuarios?pagina=${pagina}&tamano=${tamano}`),
  obtenerEstadisticasContenido: () =>
    request<EstadisticasContenido>('/admin/estadisticas-contenido'),
  alternarBloqueoIA: (id: string) =>
    request<{ estaBloqueadoIA: boolean }>(`/admin/usuarios/${id}/alternar-bloqueo-ia`, { method: 'POST' }),
};

// ============ TTS ============
export interface VozDisponible {
  idVoz: string;
  nombre: string;
  idioma: string;
  genero: string;
  proveedor: string;
  descripcionPreview: string | null;
}

export interface ProveedorTtsPublico {
  tipo: string;
  nombreVisible: string;
  descripcion: string | null;
}

export interface ConfiguracionTtsUsuario {
  proveedores: ProveedorTtsPublico[];
  voces: VozDisponible[];
  vozSeleccionada: string | null;
  velocidadVoz: number;
  proveedorPreferido: string;
}

export interface ConfiguracionProveedorTts {
  id: number;
  tipo: string;
  nombreVisible: string;
  descripcion: string | null;
  habilitado: boolean;
  tieneApiKey: boolean;
  region: string | null;
  vozPorDefecto: string;
  orden: number;
  cantidadVoces: number;
  fechaCreacion: string;
  fechaActualizacion: string | null;
}

export const ttsApi = {
  voces: () => request<VozDisponible[]>('/tts/voces'),
  configuracion: () => request<ConfiguracionTtsUsuario>('/tts/configuracion'),
  actualizarPreferencias: (datos: { vozPreferida?: string; velocidadVoz?: number; proveedorPreferido?: string }) =>
    request<ConfiguracionTtsUsuario>('/tts/preferencias', { method: 'PUT', body: datos }),
  sintetizar: async (texto: string, voz?: string, velocidad?: number): Promise<{ audioUrl: string | null; usarWebSpeech: boolean }> => {
    const token = getToken();
    const headers: Record<string, string> = { 'Content-Type': 'application/json' };
    if (token) headers['Authorization'] = `Bearer ${token}`;

    const response = await fetch(`${API_BASE}/tts/sintetizar`, {
      method: 'POST',
      headers,
      body: JSON.stringify({ texto, voz, velocidad }),
    });

    if (!response.ok) {
      const data = await response.json().catch(() => ({}));
      throw new ApiError(response.status, data.error?.message || 'Error de TTS');
    }

    const contentType = response.headers.get('content-type');
    if (contentType?.includes('audio/')) {
      const blob = await response.blob();
      return { audioUrl: URL.createObjectURL(blob), usarWebSpeech: false };
    }

    // Si no es audio, es JSON indicando fallback a Web Speech
    return { audioUrl: null, usarWebSpeech: true };
  },
  previewVoz: (proveedor: string, idVoz: string) => {
    const token = getToken();
    return fetch(`${API_BASE}/tts/preview/${proveedor}/${idVoz}`, {
      headers: token ? { 'Authorization': `Bearer ${token}` } : {},
    });
  },
};

// ============ ADMIN TTS ============
export const adminTtsApi = {
  obtenerProveedores: () => request<ConfiguracionProveedorTts[]>('/admin/tts/proveedores'),
  obtenerProveedor: (tipo: string) => request<ConfiguracionProveedorTts>(`/admin/tts/proveedores/${tipo}`),
  actualizarProveedor: (tipo: string, datos: {
    nombreVisible?: string;
    descripcion?: string;
    habilitado?: boolean;
    apiKey?: string;
    region?: string;
    vozPorDefecto?: string;
    orden?: number;
  }) => request<ConfiguracionProveedorTts>(`/admin/tts/proveedores/${tipo}`, { method: 'PUT', body: datos }),
  alternarProveedor: (tipo: string) =>
    request<{ habilitado: boolean }>(`/admin/tts/proveedores/${tipo}/alternar`, { method: 'POST' }),
};

// ============ ADMIN IA ============
export interface ConfiguracionProveedorIA {
  id: number;
  tipo: string;
  nombreVisible: string;
  descripcion: string | null;
  habilitado: boolean;
  esActivo: boolean;
  tieneApiKey: boolean;
  urlBase: string;
  modeloChat: string;
  maxTokens: number;
  temperatura: number;
}

export const adminIaApi = {
  obtenerProveedores: () => request<ConfiguracionProveedorIA[]>('/admin/ia/proveedores'),
  obtenerProveedor: (tipo: string) => request<ConfiguracionProveedorIA>(`/admin/ia/proveedores/${tipo}`),
  actualizarProveedor: (tipo: string, datos: {
    nombreVisible?: string;
    descripcion?: string;
    habilitado?: boolean;
    apiKey?: string;
    urlBase?: string;
    modeloChat?: string;
    maxTokens?: number;
    temperatura?: number;
  }) => request<ConfiguracionProveedorIA>(`/admin/ia/proveedores/${tipo}`, { method: 'PUT', body: datos }),
  alternarProveedor: (tipo: string) =>
    request<{ habilitado: boolean }>(`/admin/ia/proveedores/${tipo}/alternar`, { method: 'POST' }),
  activarProveedor: (tipo: string) =>
    request<ConfiguracionProveedorIA>(`/admin/ia/proveedores/${tipo}/activar`, { method: 'POST' }),
};

export { ApiError };
