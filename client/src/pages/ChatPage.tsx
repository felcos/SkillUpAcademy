import { useState, useRef, useEffect } from 'react';
import { useParams, useSearchParams } from 'react-router-dom';
import { useMutation } from '@tanstack/react-query';
import { aiApi, type RespuestaMensajeIA } from '../lib/api';
import { Send, Sparkles, Bot, User } from 'lucide-react';

interface Mensaje {
  rol: 'usuario' | 'asistente';
  contenido: string;
}

export default function ChatPage() {
  const { sesionId: sesionIdParam } = useParams<{ sesionId: string }>();
  const [searchParams] = useSearchParams();
  const leccionId = searchParams.get('leccion') ? Number(searchParams.get('leccion')) : undefined;

  const [sesionId, setSesionId] = useState<string | null>(sesionIdParam ?? null);
  const [mensajes, setMensajes] = useState<Mensaje[]>([]);
  const [input, setInput] = useState('');
  const [sugerencias, setSugerencias] = useState<string[]>([]);
  const [iniciando, setIniciando] = useState(false);
  const scrollRef = useRef<HTMLDivElement>(null);

  // Auto-scroll
  useEffect(() => {
    scrollRef.current?.scrollTo({ top: scrollRef.current.scrollHeight, behavior: 'smooth' });
  }, [mensajes]);

  // Iniciar sesión
  const iniciarSesion = async (tipo: string) => {
    setIniciando(true);
    try {
      const sesion = await aiApi.iniciarSesion(tipo, leccionId);
      setSesionId(sesion.id);
      setMensajes([{ rol: 'asistente', contenido: sesion.mensajeInicial }]);
    } catch {
      setMensajes([{ rol: 'asistente', contenido: 'Error al iniciar la sesión. Intenta de nuevo.' }]);
    } finally {
      setIniciando(false);
    }
  };

  // Enviar mensaje
  const enviarMut = useMutation({
    mutationFn: (mensaje: string) => aiApi.enviarMensaje(sesionId!, mensaje),
    onSuccess: (data: RespuestaMensajeIA) => {
      setMensajes((prev) => [...prev, { rol: 'asistente', contenido: data.respuesta }]);
      setSugerencias(data.sugerencias || []);
    },
    onError: () => {
      setMensajes((prev) => [...prev, { rol: 'asistente', contenido: 'Error al procesar tu mensaje. Intenta de nuevo.' }]);
    },
  });

  const handleEnviar = () => {
    const msg = input.trim();
    if (!msg || !sesionId || enviarMut.isPending) return;
    setMensajes((prev) => [...prev, { rol: 'usuario', contenido: msg }]);
    setInput('');
    setSugerencias([]);
    enviarMut.mutate(msg);
  };

  // Si no hay sesión, mostrar selector
  if (!sesionId) {
    return (
      <div className="max-w-2xl mx-auto px-4 py-16 text-center">
        <div className="w-16 h-16 rounded-full bg-gradient-to-br from-[#3498DB] to-[#9B59B6] flex items-center justify-center mx-auto mb-6 text-2xl font-bold">
          A
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
              className="bg-[#25254A] rounded-xl p-6 border border-white/5 hover:border-[#3498DB]/40 hover:bg-[#3498DB]/5 transition-all text-left disabled:opacity-50"
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
      {/* Mensajes */}
      <div ref={scrollRef} className="flex-1 overflow-y-auto space-y-4 py-4">
        {mensajes.map((msg, i) => (
          <div key={i} className={`flex gap-3 ${msg.rol === 'usuario' ? 'flex-row-reverse' : ''}`}>
            <div className={`w-8 h-8 rounded-full flex items-center justify-center flex-shrink-0 ${
              msg.rol === 'asistente'
                ? 'bg-gradient-to-br from-[#3498DB] to-[#9B59B6]'
                : 'bg-white/10'
            }`}>
              {msg.rol === 'asistente' ? <Bot size={16} /> : <User size={16} />}
            </div>
            <div className={`max-w-[75%] rounded-2xl px-4 py-3 text-sm leading-relaxed ${
              msg.rol === 'asistente'
                ? 'bg-[#25254A] border border-white/5 text-gray-200'
                : 'bg-[#3498DB] text-white'
            }`}>
              {msg.contenido}
            </div>
          </div>
        ))}

        {enviarMut.isPending && (
          <div className="flex gap-3">
            <div className="w-8 h-8 rounded-full bg-gradient-to-br from-[#3498DB] to-[#9B59B6] flex items-center justify-center flex-shrink-0">
              <Bot size={16} />
            </div>
            <div className="bg-[#25254A] border border-white/5 rounded-2xl px-4 py-3">
              <div className="flex gap-1">
                <div className="w-2 h-2 bg-gray-500 rounded-full animate-bounce" style={{ animationDelay: '0ms' }} />
                <div className="w-2 h-2 bg-gray-500 rounded-full animate-bounce" style={{ animationDelay: '150ms' }} />
                <div className="w-2 h-2 bg-gray-500 rounded-full animate-bounce" style={{ animationDelay: '300ms' }} />
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
              className="flex-shrink-0 flex items-center gap-1.5 text-xs px-3 py-1.5 rounded-full bg-[#3498DB]/10 border border-[#3498DB]/20 text-[#3498DB] hover:bg-[#3498DB]/20 transition-colors"
            >
              <Sparkles size={12} />
              {sug}
            </button>
          ))}
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
          className="flex-1 bg-[#25254A] border border-white/10 rounded-xl px-4 py-3 text-white placeholder-gray-500 focus:border-[#3498DB] focus:outline-none transition-colors"
        />
        <button
          onClick={handleEnviar}
          disabled={!input.trim() || enviarMut.isPending}
          className="px-4 rounded-xl bg-[#3498DB] hover:bg-[#2980B9] text-white disabled:opacity-30 transition-colors"
        >
          <Send size={18} />
        </button>
      </div>
    </div>
  );
}
