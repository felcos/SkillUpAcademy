import { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { type ResultadoEscenario } from '../lib/api';
import { useEscenario, useElegirEscenario } from '../hooks/useLessons';
import { ArrowLeft, CheckCircle2, AlertTriangle, XCircle } from 'lucide-react';

const iconoResultado: Record<string, typeof CheckCircle2> = {
  Optimo: CheckCircle2,
  Aceptable: AlertTriangle,
  Inadecuado: XCircle,
};

const colorResultado: Record<string, string> = {
  Optimo: 'text-green-400 bg-green-500/10 border-green-500/20',
  Aceptable: 'text-yellow-400 bg-yellow-500/10 border-yellow-500/20',
  Inadecuado: 'text-red-400 bg-red-500/10 border-red-500/20',
};

export default function ScenarioPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const leccionId = Number(id);
  const [resultado, setResultado] = useState<ResultadoEscenario | null>(null);

  const { data: escenario, isLoading } = useEscenario(leccionId);

  const elegirMut = useElegirEscenario(leccionId);

  if (isLoading || !escenario) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  const IconoRes = resultado ? iconoResultado[resultado.tipoResultado] || CheckCircle2 : null;

  return (
    <div className="max-w-3xl mx-auto px-4 py-10">
      <button
        onClick={() => navigate(-1)}
        className="inline-flex items-center gap-1.5 text-gray-400 hover:text-white transition-colors text-sm mb-6"
      >
        <ArrowLeft size={16} />
        Volver
      </button>

      {/* Situación */}
      <div className="bg-[#25254A] rounded-2xl p-8 border border-white/5 mb-8">
        <p className="text-xs text-[#3498DB] font-medium mb-3 uppercase tracking-wider">Escenario</p>
        <h1 className="text-xl font-semibold mb-4">{escenario.textoSituacion}</h1>
        {escenario.contexto && (
          <p className="text-gray-400 text-sm leading-relaxed">{escenario.contexto}</p>
        )}
      </div>

      {/* Resultado */}
      {resultado && IconoRes && (
        <div className={`rounded-2xl p-6 border mb-8 ${colorResultado[resultado.tipoResultado] || ''}`}>
          <div className="flex items-start gap-3">
            <IconoRes size={24} className="flex-shrink-0 mt-0.5" />
            <div>
              <p className="font-semibold mb-2">
                {resultado.tipoResultado === 'Optimo' ? '¡Decisión óptima!' :
                 resultado.tipoResultado === 'Aceptable' ? 'Decisión aceptable' : 'Decisión inadecuada'}
              </p>
              <p className="text-sm opacity-80">{resultado.textoRetroalimentacion}</p>
              <p className="text-sm mt-3 font-medium">+{resultado.puntosOtorgados} puntos</p>
            </div>
          </div>
        </div>
      )}

      {/* Opciones */}
      {!resultado && (
        <>
          <p className="text-sm text-gray-400 mb-4">¿Qué harías en esta situación?</p>
          <div className="space-y-3">
            {escenario.opciones.map((opcion) => (
              <button
                key={opcion.id}
                onClick={() => elegirMut.mutate(opcion.id, { onSuccess: (data) => setResultado(data) })}
                disabled={elegirMut.isPending}
                className="w-full text-left p-5 rounded-xl bg-[#25254A] border border-white/5 hover:border-[#3498DB]/40 hover:bg-[#3498DB]/5 transition-all disabled:opacity-50"
              >
                <p className="text-gray-200">{opcion.textoOpcion}</p>
              </button>
            ))}
          </div>
        </>
      )}

      {resultado && (
        <div className="flex justify-center">
          <button
            onClick={() => navigate(-1)}
            className="px-6 py-2.5 rounded-lg bg-[#3498DB] hover:bg-[#2980B9] text-white transition-colors"
          >
            Continuar
          </button>
        </div>
      )}
    </div>
  );
}
