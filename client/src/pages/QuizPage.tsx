import { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useQuery, useMutation } from '@tanstack/react-query';
import { quizApi, type ResultadoQuiz } from '../lib/api';
import { CheckCircle2, XCircle, ArrowRight, RotateCcw } from 'lucide-react';

export default function QuizPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const leccionId = Number(id);
  const [preguntaActual, setPreguntaActual] = useState(0);
  const [respuestas, setRespuestas] = useState<Record<number, number>>({});
  const [resultado, setResultado] = useState<ResultadoQuiz | null>(null);

  const { data: preguntas, isLoading } = useQuery({
    queryKey: ['quiz', leccionId],
    queryFn: () => quizApi.preguntas(leccionId),
    enabled: !!leccionId,
  });

  const enviarMut = useMutation({
    mutationFn: () => {
      const respuestasArray = Object.entries(respuestas).map(([preguntaId, opcionSeleccionadaId]) => ({
        preguntaId: Number(preguntaId),
        opcionSeleccionadaId,
      }));
      return quizApi.enviar(leccionId, respuestasArray);
    },
    onSuccess: (data) => setResultado(data),
  });

  if (isLoading || !preguntas) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  // Resultado
  if (resultado) {
    const aprobado = resultado.aprobado;
    return (
      <div className="max-w-2xl mx-auto px-4 py-16 text-center">
        <div className={`w-20 h-20 rounded-full mx-auto mb-6 flex items-center justify-center ${aprobado ? 'bg-green-500/10' : 'bg-red-500/10'}`}>
          {aprobado ? <CheckCircle2 size={40} className="text-green-400" /> : <XCircle size={40} className="text-red-400" />}
        </div>
        <h1 className="text-3xl font-bold mb-2">
          {aprobado ? '¡Excelente trabajo!' : 'Sigue practicando'}
        </h1>
        <p className="text-gray-400 mb-8">
          {resultado.respuestasCorrectas}/{resultado.preguntasTotales} respuestas correctas · {resultado.puntuacion}%
        </p>

        <div className="bg-[#25254A] rounded-2xl p-6 mb-8 inline-block">
          <div className="text-4xl font-bold mb-1" style={{ color: aprobado ? '#27AE60' : '#E74C3C' }}>
            +{resultado.puntosObtenidos} pts
          </div>
          <p className="text-xs text-gray-500">Puntos obtenidos</p>
        </div>

        <div className="flex justify-center gap-4">
          {!aprobado && (
            <button
              onClick={() => { setResultado(null); setPreguntaActual(0); setRespuestas({}); }}
              className="flex items-center gap-2 px-6 py-2.5 rounded-lg bg-white/5 hover:bg-white/10 transition-colors"
            >
              <RotateCcw size={18} />
              Reintentar
            </button>
          )}
          <button
            onClick={() => navigate(-1)}
            className="px-6 py-2.5 rounded-lg bg-[#3498DB] hover:bg-[#2980B9] text-white transition-colors"
          >
            Volver
          </button>
        </div>
      </div>
    );
  }

  const pregunta = preguntas[preguntaActual];
  const seleccionada = respuestas[pregunta.id];
  const todasRespondidas = preguntas.every((p) => respuestas[p.id] != null);
  const esUltima = preguntaActual === preguntas.length - 1;

  return (
    <div className="max-w-3xl mx-auto px-4 py-10">
      {/* Progreso */}
      <div className="flex gap-1 mb-8">
        {preguntas.map((_, i) => (
          <div
            key={i}
            className={`h-1 flex-1 rounded-full transition-all ${
              respuestas[preguntas[i].id] != null ? 'bg-[#3498DB]' : i === preguntaActual ? 'bg-[#3498DB]/40' : 'bg-white/10'
            }`}
          />
        ))}
      </div>

      <p className="text-sm text-gray-500 mb-2">Pregunta {preguntaActual + 1} de {preguntas.length}</p>
      <h2 className="text-xl font-semibold mb-8">{pregunta.textoPregunta}</h2>

      <div className="space-y-3 mb-8">
        {pregunta.opciones.map((opcion) => (
          <button
            key={opcion.id}
            onClick={() => setRespuestas({ ...respuestas, [pregunta.id]: opcion.id })}
            className={`w-full text-left p-4 rounded-xl border transition-all ${
              seleccionada === opcion.id
                ? 'bg-[#3498DB]/10 border-[#3498DB]/40 text-white'
                : 'bg-[#25254A] border-white/5 hover:border-white/15 text-gray-300'
            }`}
          >
            {opcion.textoOpcion}
          </button>
        ))}
      </div>

      <div className="flex justify-between">
        <button
          onClick={() => setPreguntaActual(Math.max(0, preguntaActual - 1))}
          disabled={preguntaActual === 0}
          className="px-4 py-2 rounded-lg bg-white/5 hover:bg-white/10 disabled:opacity-30 transition-colors"
        >
          Anterior
        </button>

        {esUltima && todasRespondidas ? (
          <button
            onClick={() => enviarMut.mutate()}
            disabled={enviarMut.isPending}
            className="flex items-center gap-2 px-6 py-2 rounded-lg bg-green-600 hover:bg-green-700 text-white font-medium transition-colors"
          >
            {enviarMut.isPending ? 'Enviando...' : 'Enviar respuestas'}
          </button>
        ) : (
          <button
            onClick={() => setPreguntaActual(Math.min(preguntas.length - 1, preguntaActual + 1))}
            disabled={seleccionada == null}
            className="flex items-center gap-2 px-4 py-2 rounded-lg bg-[#3498DB] hover:bg-[#2980B9] text-white disabled:opacity-30 transition-colors"
          >
            Siguiente
            <ArrowRight size={18} />
          </button>
        )}
      </div>
    </div>
  );
}
