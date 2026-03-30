import { useState, useRef, useEffect, useCallback } from 'react';
import { useParams, useSearchParams, useNavigate } from 'react-router-dom';
import { aiApi, lessonsApi, ttsApi, type EventoStreamChat } from '../lib/api';
import { limpiarParaTts } from '../lib/ttsUtils';
import { useConfiguracionTts } from '../hooks/useTts';
import { Send, Sparkles, User, Volume2, VolumeX, CheckCircle } from 'lucide-react';
import AvatarAria from '../components/avatar/AvatarAria';

interface Mensaje {
  rol: 'usuario' | 'asistente';
  contenido: string;
}

export default function ChatPage() {
  const { sesionId: sesionIdParam } = useParams<{ sesionId: string }>();
  const [searchParams] = useSearchParams();
  const leccionId = searchParams.get('leccion') ? Number(searchParams.get('leccion')) : undefined;

  const tipoSesionParam = searchParams.get('tipo');

  const navigate = useNavigate();

  const [sesionId, setSesionId] = useState<string | null>(sesionIdParam ?? null);
  const [mensajes, setMensajes] = useState<Mensaje[]>([]);
  const [input, setInput] = useState('');
  const [sugerencias, setSugerencias] = useState<string[]>([]);
  const [iniciando, setIniciando] = useState(false);
  const [enviando, setEnviando] = useState(false);
  const [completando, setCompletando] = useState(false);
  const [estadoAria, setEstadoAria] = useState<'idle' | 'hablando' | 'pensando' | 'saludando'>('saludando');
  const scrollRef = useRef<HTMLDivElement>(null);
  const abortRef = useRef<AbortController | null>(null);

  // TTS
  const [vozActiva, setVozActiva] = useState(true);
  const [reproduciendo, setReproduciendo] = useState(false);
  const synthRef = useRef(window.speechSynthesis);
  const audioRef = useRef<HTMLAudioElement | null>(null);
  const { data: configTts } = useConfiguracionTts();
  // Ref para acceder al valor actual de vozActiva dentro de callbacks
  const vozActivaRef = useRef(vozActiva);
  useEffect(() => { vozActivaRef.current = vozActiva; }, [vozActiva]);

  // Saludo inicial: 2 segundos saludando, luego idle
  useEffect(() => {
    const timer = setTimeout(() => setEstadoAria('idle'), 2000);
    return () => clearTimeout(timer);
  }, []);

  // Si hay leccionId con tipo de sesion, iniciar automaticamente
  useEffect(() => {
    if (leccionId && tipoSesionParam && !sesionId && !iniciando) {
      iniciarSesion(tipoSesionParam);
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [leccionId, tipoSesionParam]);

  // Auto-scroll
  useEffect(() => {
    scrollRef.current?.scrollTo({ top: scrollRef.current.scrollHeight, behavior: 'smooth' });
  }, [mensajes]);

  // Limpiar audio al desmontar
  useEffect(() => {
    return () => {
      detenerAudio();
    };
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // ==================== UTILIDADES ====================

  /** Renderiza markdown básico a HTML seguro (solo formato, sin scripts). */
  const renderizarMarkdownSimple = (texto: string): string => {
    return texto
      .replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;') // escapar HTML
      .replace(/\*\*\*([^*]+)\*\*\*/g, '<strong><em>$1</em></strong>')
      .replace(/\*\*([^*]+)\*\*/g, '<strong>$1</strong>')
      .replace(/\*([^*]+)\*/g, '<em>$1</em>')
      .replace(/___([^_]+)___/g, '<strong><em>$1</em></strong>')
      .replace(/__([^_]+)__/g, '<strong>$1</strong>')
      .replace(/_([^_]+)_/g, '<em>$1</em>')
      .replace(/`([^`]+)`/g, '<code class="bg-white/10 px-1 rounded text-xs">$1</code>')
      .replace(/^#{1,3}\s+(.+)$/gm, '<strong>$1</strong>')
      .replace(/^\s*[-*+]\s+(.+)$/gm, '• $1')
      .replace(/^\s*\d+\.\s+(.+)$/gm, '‣ $1')
      .replace(/\n/g, '<br/>');
  };

  // ==================== TTS ====================

  const detenerAudio = useCallback(() => {
    synthRef.current.cancel();
    if (audioRef.current) {
      audioRef.current.pause();
      audioRef.current.currentTime = 0;
      audioRef.current = null;
    }
    setReproduciendo(false);
  }, []);

  const reproducirConWebSpeech = useCallback((texto: string, velocidad: number) => {
    synthRef.current.cancel();
    const utterance = new SpeechSynthesisUtterance(texto);
    utterance.lang = 'es-ES';
    utterance.rate = velocidad;
    utterance.onstart = () => { setReproduciendo(true); setEstadoAria('hablando'); };
    utterance.onend = () => { setReproduciendo(false); setEstadoAria('idle'); };
    utterance.onerror = () => { setReproduciendo(false); setEstadoAria('idle'); };
    synthRef.current.speak(utterance);
  }, []);

  const reproducirRespuesta = useCallback(async (texto: string) => {
    if (!vozActivaRef.current || !texto.trim()) return;

    detenerAudio();

    const textoLimpio = limpiarParaTts(texto);
    if (!textoLimpio) return;

    const proveedorPreferido = configTts?.proveedorPreferido ?? 'WebSpeechApi';
    const velocidad = configTts?.velocidadVoz ?? 0.95;

    if (proveedorPreferido === 'WebSpeechApi') {
      reproducirConWebSpeech(textoLimpio, velocidad);
      return;
    }

    try {
      const resultado = await ttsApi.sintetizar(
        textoLimpio,
        configTts?.vozSeleccionada ?? undefined,
        velocidad
      );

      if (resultado.audioUrl) {
        const audio = new Audio(resultado.audioUrl);
        audioRef.current = audio;
        audio.onplay = () => { setReproduciendo(true); setEstadoAria('hablando'); };
        audio.onended = () => { setReproduciendo(false); setEstadoAria('idle'); };
        audio.onerror = () => { reproducirConWebSpeech(textoLimpio, velocidad); };
        await audio.play();
      } else {
        reproducirConWebSpeech(textoLimpio, velocidad);
      }
    } catch {
      reproducirConWebSpeech(textoLimpio, velocidad);
    }
  }, [configTts?.proveedorPreferido, configTts?.vozSeleccionada, configTts?.velocidadVoz, detenerAudio, reproducirConWebSpeech]);

  const alternarVoz = useCallback(() => {
    if (vozActiva) {
      detenerAudio();
    }
    setVozActiva((prev) => !prev);
  }, [vozActiva, detenerAudio]);

  // ==================== SESION ====================

  // Iniciar sesion
  const iniciarSesion = async (tipo: string) => {
    setIniciando(true);
    try {
      const sesion = await aiApi.iniciarSesion(tipo, leccionId);
      setSesionId(sesion.id);
      setMensajes([{ rol: 'asistente', contenido: sesion.mensajeInicial }]);
      // Reproducir mensaje de bienvenida
      reproducirRespuesta(sesion.mensajeInicial);
    } catch {
      setMensajes([{ rol: 'asistente', contenido: 'Error al iniciar la sesion. Intenta de nuevo.' }]);
    } finally {
      setIniciando(false);
    }
  };

  // Ref para capturar la respuesta completa del stream
  const respuestaCompletaRef = useRef('');

  // Enviar mensaje con streaming
  const handleEnviar = useCallback(() => {
    const msg = input.trim();
    if (!msg || !sesionId || enviando) return;

    detenerAudio();
    respuestaCompletaRef.current = '';

    setMensajes((prev) => [...prev, { rol: 'usuario', contenido: msg }]);
    setInput('');
    setSugerencias([]);
    setEnviando(true);
    setEstadoAria('pensando');

    // Añadir mensaje vacio del asistente que se ira llenando
    setMensajes((prev) => [...prev, { rol: 'asistente', contenido: '' }]);

    const abortController = new AbortController();
    abortRef.current = abortController;

    aiApi.enviarMensajeStream(
      sesionId,
      msg,
      (evento: EventoStreamChat) => {
        if (evento.tipo === 'texto' && evento.contenido) {
          setEstadoAria('hablando');
          respuestaCompletaRef.current += evento.contenido;
          setMensajes((prev) => {
            const copia = [...prev];
            const ultimo = copia[copia.length - 1];
            if (ultimo && ultimo.rol === 'asistente') {
              copia[copia.length - 1] = { ...ultimo, contenido: ultimo.contenido + evento.contenido };
            }
            return copia;
          });
        } else if (evento.tipo === 'reemplazo' && evento.contenido) {
          // La salida fue filtrada por seguridad, reemplazar todo el contenido
          respuestaCompletaRef.current = evento.contenido;
          setMensajes((prev) => {
            const copia = [...prev];
            const ultimo = copia[copia.length - 1];
            if (ultimo && ultimo.rol === 'asistente') {
              copia[copia.length - 1] = { ...ultimo, contenido: evento.contenido! };
            }
            return copia;
          });
        } else if (evento.tipo === 'fin') {
          setSugerencias(evento.sugerencias || []);
          setEnviando(false);
          setEstadoAria('idle');
          // Reproducir la respuesta completa con TTS
          reproducirRespuesta(respuestaCompletaRef.current);
        }
      },
      abortController.signal,
    ).catch(() => {
      setMensajes((prev) => {
        const copia = [...prev];
        const ultimo = copia[copia.length - 1];
        if (ultimo && ultimo.rol === 'asistente' && ultimo.contenido === '') {
          copia[copia.length - 1] = { ...ultimo, contenido: 'Error al procesar tu mensaje. Intenta de nuevo.' };
        }
        return copia;
      });
      setEnviando(false);
      setEstadoAria('idle');
    });
  }, [input, sesionId, enviando, detenerAudio, reproducirRespuesta]);

  // Completar lección vinculada al chat (Autoevaluación, Roleplay, etc.)
  const handleCompletarLeccion = useCallback(async () => {
    if (!sesionId || !leccionId || completando) return;

    setCompletando(true);
    detenerAudio();

    try {
      // Cerrar sesión IA
      await aiApi.cerrar(sesionId);
      // Completar la lección
      await lessonsApi.completar(leccionId);
      // Volver atrás
      navigate(-1);
    } catch {
      setMensajes((prev) => [...prev, { rol: 'asistente', contenido: 'Error al completar la lección. Intenta de nuevo.' }]);
    } finally {
      setCompletando(false);
    }
  }, [sesionId, leccionId, completando, detenerAudio, navigate]);

  // Si no hay sesion, mostrar selector
  if (!sesionId) {
    return (
      <div className="max-w-2xl mx-auto px-4 py-16 text-center">
        <div className="mx-auto mb-6">
          <AvatarAria size={64} estado={estadoAria} />
        </div>
        <h1 className="text-2xl font-bold mb-2">Chatea con Aria</h1>
        <p className="text-gray-400 mb-10">Tu instructora IA de habilidades profesionales</p>

        <div className="grid grid-cols-1 sm:grid-cols-3 gap-4">
          {[
            { tipo: 'ConsultaLibre', titulo: 'Consulta libre', desc: 'Pregunta lo que quieras sobre habilidades blandas', icono: '💬' },
            { tipo: 'Roleplay', titulo: 'Roleplay', desc: 'Practica situaciones laborales con Aria', icono: '🎭' },
            { tipo: 'RepasoDeLeccion', titulo: 'Repaso', desc: 'Refuerza lo aprendido en una lección', icono: '📖' },
          ].map((opcion) => (
            <button
              key={opcion.tipo}
              onClick={() => iniciarSesion(opcion.tipo)}
              disabled={iniciando}
              className="bg-[#25254A] rounded-xl p-6 border border-white/5 hover:border-[#3498DB]/40 hover:bg-[#3498DB]/5 transition-all duration-200 text-left disabled:opacity-50 active:scale-[0.97]"
            >
              <span className="text-3xl block mb-3">{opcion.icono}</span>
              <h3 className="font-semibold text-sm mb-1">{opcion.titulo}</h3>
              <p className="text-xs text-gray-500">{opcion.desc}</p>
            </button>
          ))}
        </div>
      </div>
    );
  }

  return (
    <div className="max-w-3xl mx-auto px-4 py-4 flex flex-col" style={{ height: 'calc(100vh - 10rem)' }}>
      {/* Header con boton de voz */}
      <div className="flex items-center justify-end pb-2">
        <button
          onClick={alternarVoz}
          className={`flex items-center gap-1.5 text-xs px-3 py-1.5 rounded-full border transition-all active:scale-[0.95] ${
            vozActiva
              ? 'bg-[#3498DB]/10 border-[#3498DB]/30 text-[#3498DB]'
              : 'bg-white/5 border-white/10 text-gray-500'
          }`}
          title={vozActiva ? 'Silenciar a Aria' : 'Activar voz de Aria'}
        >
          {vozActiva ? <Volume2 size={14} /> : <VolumeX size={14} />}
          {vozActiva ? 'Voz activa' : 'Voz silenciada'}
        </button>
      </div>

      {/* Mensajes */}
      <div ref={scrollRef} className="flex-1 overflow-y-auto space-y-4 py-4">
        {mensajes.map((msg, i) => (
          <div key={i} className={`flex gap-3 ${msg.rol === 'usuario' ? 'flex-row-reverse' : ''}`}>
            {msg.rol === 'asistente' ? (
              <div className="flex-shrink-0">
                <AvatarAria size={32} estado={
                  (enviando && i === mensajes.length - 1) ? estadoAria
                  : (reproduciendo && i === mensajes.length - 1) ? 'hablando'
                  : 'idle'
                } />
              </div>
            ) : (
              <div className="w-8 h-8 rounded-full flex items-center justify-center flex-shrink-0 bg-white/10">
                <User size={16} />
              </div>
            )}
            <div
              className={`max-w-[75%] rounded-2xl px-4 py-3 text-sm leading-relaxed ${
                msg.rol === 'asistente'
                  ? 'bg-[#25254A] border border-white/5 text-gray-200'
                  : 'bg-[#3498DB] text-white'
              }`}
              dangerouslySetInnerHTML={
                msg.rol === 'asistente'
                  ? { __html: renderizarMarkdownSimple(msg.contenido) }
                  : undefined
              }
            >
              {msg.rol === 'usuario' ? msg.contenido : undefined}
            </div>
          </div>
        ))}

        {enviando && (
          <div className="flex gap-3">
            <div className="flex-shrink-0">
              <AvatarAria size={32} estado={estadoAria} />
            </div>
            <div className="bg-[#25254A] border border-white/5 rounded-2xl px-4 py-3">
              <div className="flex gap-1">
                <div className="w-2 h-2 bg-gray-400 rounded-full animate-pulse" style={{ animationDelay: '0ms' }} />
                <div className="w-2 h-2 bg-gray-400 rounded-full animate-pulse" style={{ animationDelay: '200ms' }} />
                <div className="w-2 h-2 bg-gray-400 rounded-full animate-pulse" style={{ animationDelay: '400ms' }} />
              </div>
            </div>
          </div>
        )}
      </div>

      {/* Sugerencias */}
      {sugerencias.length > 0 && (
        <div className="flex gap-2 overflow-x-auto pb-2 pt-1">
          {sugerencias.map((sug, i) => (
            <button
              key={i}
              onClick={() => { setInput(sug); }}
              className="flex-shrink-0 flex items-center gap-1.5 text-xs px-3 py-1.5 rounded-full bg-[#3498DB]/10 border border-[#3498DB]/20 text-[#3498DB] hover:bg-[#3498DB]/20 transition-all active:scale-[0.95]"
            >
              <Sparkles size={12} />
              {sug}
            </button>
          ))}
        </div>
      )}

      {/* Botón completar lección (solo si viene de una lección y hay conversación suficiente) */}
      {leccionId && mensajes.length >= 3 && !enviando && (
        <div className="flex justify-center py-2">
          <button
            onClick={handleCompletarLeccion}
            disabled={completando}
            className="flex items-center gap-2 px-5 py-2.5 rounded-xl bg-green-600 hover:bg-green-700 text-white font-medium transition-all active:scale-[0.97] disabled:opacity-50"
          >
            <CheckCircle size={18} />
            {completando ? 'Completando...' : 'Completar lección'}
          </button>
        </div>
      )}

      {/* Input */}
      <div className="flex gap-3 pt-2 border-t border-white/5">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === 'Enter' && !e.shiftKey && handleEnviar()}
          placeholder="Escribe tu mensaje..."
          className="flex-1 bg-[#25254A] border border-white/10 rounded-xl px-4 py-3 text-white placeholder-gray-500 focus:border-[#3498DB] focus:outline-none focus:ring-2 focus:ring-[#3498DB]/20 transition-all"
        />
        <button
          onClick={handleEnviar}
          disabled={!input.trim() || enviando}
          className="px-4 rounded-xl bg-[#3498DB] hover:bg-[#2980B9] text-white disabled:opacity-30 transition-all active:scale-[0.93]"
        >
          <Send size={18} />
        </button>
      </div>
    </div>
  );
}
