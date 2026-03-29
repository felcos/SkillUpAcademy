import { type LogroDto } from '../lib/api';
import { useLogros } from '../hooks/useProgress';
import { Trophy, Lock } from 'lucide-react';

export default function AchievementsPage() {
  const { data: logros, isLoading } = useLogros();

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  const desbloqueados = logros?.filter((l) => l.desbloqueado).length ?? 0;
  const total = logros?.length ?? 0;

  return (
    <div className="max-w-5xl mx-auto px-4 py-10">
      <div className="flex items-center gap-3 mb-2">
        <Trophy size={28} className="text-yellow-400" />
        <h1 className="text-3xl font-bold">Logros</h1>
      </div>
      <p className="text-gray-400 mb-10">{desbloqueados} de {total} desbloqueados</p>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {logros?.map((logro: LogroDto, index: number) => (
          <div
            key={logro.id}
            className={`rounded-2xl p-6 border transition-all duration-200 hover:shadow-lg hover:shadow-yellow-500/5 stagger-item ${
              logro.desbloqueado
                ? 'bg-[#25254A] border-yellow-500/20'
                : 'bg-[#25254A]/50 border-white/5 opacity-50 grayscale'
            }`}
            style={{ animationDelay: `${index * 40}ms` }}
          >
            <div className="flex items-start justify-between mb-3">
              <span className="text-3xl">{logro.icono}</span>
              {!logro.desbloqueado && <Lock size={16} className="text-gray-600" />}
            </div>
            <h3 className="font-semibold mb-1">{logro.titulo}</h3>
            <p className="text-gray-400 text-sm">{logro.descripcion}</p>
            {logro.desbloqueado && logro.fechaDesbloqueo && (
              <p className="text-xs text-yellow-500/60 mt-3">
                Desbloqueado el {new Date(logro.fechaDesbloqueo).toLocaleDateString('es-ES')}
              </p>
            )}
          </div>
        ))}
      </div>
    </div>
  );
}
