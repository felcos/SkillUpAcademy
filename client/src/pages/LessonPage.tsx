import { useState, useEffect, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useQuery, useMutation } from '@tanstack/react-query';
import { lessonsApi, type Escena } from '../lib/api';
import { ChevronLeft, ChevronRight, Volume2, VolumeX, Pause, Play } from 'lucide-react';

export default function LessonPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const leccionId = Number(id);
  const [escenaActual, setEscenaActual] = useState(0);
  const [ttsActivo, setTtsActivo] = useState(true);
  const [hablando, setHablando] = useState(false);
  const synthRef = useRef(window.speechSynthesis);

  const { data: leccion } = useQuery({
    queryKey: ['leccion', leccionId],
    queryFn: () => lessonsApi.detalle(leccionId),
    enabled: !!leccionId,
  });

  const { data: escenas } = useQuery({
    queryKey: ['escenas', leccionId],
    queryFn: () => lessonsApi.escenas(leccionId),
    enabled: !!leccionId,
  });

  const iniciarMut = useMutation({
    mutationFn: () => lessonsApi.iniciar(leccionId),
  });

  const completarMut = useMutation({
    mutationFn: () => lessonsApi.completar(leccionId),
    onSuccess: () => navigate(-1),
  });

  useEffect(() => {
    if (leccionId) iniciarMut.mutate();
  }, [leccionId]);

  const escena = escenas?.[escenaActual];

  // TTS
  useEffect(() => {
    if (!escena?.guionAria || !ttsActivo) return;
    synthRef.current.cancel();
    const utterance = new SpeechSynthesisUtterance(escena.guionAria);
    utterance.lang = 'es-ES';
    utterance.rate = 0.95;
    utterance.onstart = () => setHablando(true);
    utterance.onend = () => setHablando(false);
    synthRef.current.speak(utterance);
    return () => { synthRef.current.cancel(); };
  }, [escenaActual, escena?.guionAria, ttsActivo]);

  const siguiente = () => {
    if (escenas && escenaActual < escenas.length - 1) {
      setEscenaActual(escenaActual + 1);
    }
  };

  const anterior = () => {
    if (escenaActual > 0) {
      setEscenaActual(escenaActual - 1);
    }
  };

  const esUltima = escenas ? escenaActual === escenas.length - 1 : false;

  if (!leccion || !escenas) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  return (
    <div className="max-w-4xl mx-auto px-4 py-8">
      {/* Header */}
      <div className="flex items-center justify-between mb-6">
        <div>
          <h1 className="text-2xl font-bold">{leccion.titulo}</h1>
          <p className="text-gray-400 text-sm">{leccion.descripcion}</p>
        </div>
        <button
          onClick={() => setTtsActivo(!ttsActivo)}
          className="p-2 rounded-lg bg-white/5 hover:bg-white/10 transition-colors"
          title={ttsActivo ? 'Silenciar' : 'Activar voz'}
        >
          {ttsActivo ? <Volume2 size={20} /> : <VolumeX size={20} />}
        </button>
      </div>

      {/* Barra progreso */}
      <div className="flex gap-1 mb-8">
        {escenas.map((_, i) => (
          <div
            key={i}
            className={`h-1 flex-1 rounded-full transition-all ${
              i <= escenaActual ? 'bg-[#3498DB]' : 'bg-white/10'
            }`}
          />
        ))}
      </div>

      {/* Escena */}
      {escena && <EscenaVisual escena={escena} hablando={hablando} />}

      {/* Controles */}
      <div className="flex items-center justify-between mt-8">
        <button
          onClick={anterior}
          disabled={escenaActual === 0}
          className="flex items-center gap-2 px-4 py-2 rounded-lg bg-white/5 hover:bg-white/10 disabled:opacity-30 disabled:cursor-not-allowed transition-colors"
        >
          <ChevronLeft size={18} />
          Anterior
        </button>

        <span className="text-sm text-gray-500">
          {escenaActual + 1} / {escenas.length}
        </span>

        {esUltima ? (
          <button
            onClick={() => completarMut.mutate()}
            disabled={completarMut.isPending}
            className="px-6 py-2 rounded-lg bg-green-600 hover:bg-green-700 text-white font-medium transition-colors"
          >
            {completarMut.isPending ? 'Guardando...' : 'Completar lección'}
          </button>
        ) : (
          <button
            onClick={siguiente}
            className="flex items-center gap-2 px-4 py-2 rounded-lg bg-[#3498DB] hover:bg-[#2980B9] text-white transition-colors"
          >
            Siguiente
            <ChevronRight size={18} />
          </button>
        )}
      </div>
    </div>
  );
}

function EscenaVisual({ escena, hablando }: { escena: Escena; hablando: boolean }) {
  return (
    <div className="bg-[#25254A] rounded-2xl p-8 border border-white/5 min-h-[400px] flex flex-col">
      {/* Avatar + título */}
      <div className="flex items-center gap-4 mb-6">
        <div className={`w-14 h-14 rounded-full bg-gradient-to-br from-[#3498DB] to-[#9B59B6] flex items-center justify-center text-xl font-bold flex-shrink-0 ${hablando ? 'animate-pulse' : ''}`}>
          A
        </div>
        <div>
          <h2 className="text-lg font-semibold">{escena.tituloEscena}</h2>
          <p className="text-xs text-gray-500">Aria · Instructora IA</p>
        </div>
      </div>

      {/* Contenido visual según tipo */}
      <div className="flex-1">
        {escena.tipoContenidoVisual === 'Texto' && (
          <div className="prose prose-invert max-w-none text-gray-300 leading-relaxed whitespace-pre-line">
            {escena.contenidoVisual}
          </div>
        )}

        {escena.tipoContenidoVisual === 'ListaDePuntos' && (
          <ul className="space-y-3">
            {escena.contenidoVisual.split('\n').filter(Boolean).map((punto, i) => (
              <li key={i} className="flex items-start gap-3">
                <span className="w-6 h-6 rounded-full bg-[#3498DB]/20 text-[#3498DB] flex items-center justify-center text-xs flex-shrink-0 mt-0.5">
                  {i + 1}
                </span>
                <span className="text-gray-300">{punto.replace(/^[-•*]\s*/, '')}</span>
              </li>
            ))}
          </ul>
        )}

        {escena.tipoContenidoVisual === 'Diagrama' && (
          <div className="bg-[#1A1A2E] rounded-xl p-6 border border-white/5">
            <pre className="text-gray-300 text-sm whitespace-pre-wrap font-mono">{escena.contenidoVisual}</pre>
          </div>
        )}

        {escena.tipoContenidoVisual === 'Comparacion' && (
          <div className="grid grid-cols-2 gap-4">
            {escena.contenidoVisual.split('|||').map((lado, i) => (
              <div key={i} className={`rounded-xl p-5 border ${i === 0 ? 'bg-red-500/5 border-red-500/20' : 'bg-green-500/5 border-green-500/20'}`}>
                <p className={`text-xs font-medium mb-2 ${i === 0 ? 'text-red-400' : 'text-green-400'}`}>
                  {i === 0 ? 'Incorrecto' : 'Correcto'}
                </p>
                <p className="text-gray-300 text-sm whitespace-pre-line">{lado.trim()}</p>
              </div>
            ))}
          </div>
        )}

        {escena.tipoContenidoVisual === 'Imagen' && (
          <div className="flex items-center justify-center">
            <img src={escena.contenidoVisual} alt={escena.tituloEscena} className="max-h-64 rounded-xl" />
          </div>
        )}

        {(escena.tipoContenidoVisual === 'Vacio' || escena.tipoContenidoVisual === 'SoloAvatar') && (
          <div className="flex items-center justify-center h-full">
            <p className="text-gray-400 text-center text-lg italic max-w-md">
              {escena.guionAria}
            </p>
          </div>
        )}
      </div>

      {/* Pausa reflexiva */}
      {escena.esPausaReflexiva && (
        <div className="mt-6 bg-[#3498DB]/5 border border-[#3498DB]/20 rounded-xl p-4 text-center">
          <p className="text-[#3498DB] text-sm">💭 Momento de reflexión — Tómate {escena.segundosPausa}s para pensar</p>
        </div>
      )}
    </div>
  );
}
