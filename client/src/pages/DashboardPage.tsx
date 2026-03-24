import { useDashboard } from '../hooks/useProgress';
import { useAuth } from '../contexts/AuthContext';
import { Flame, Star, BookOpen, TrendingUp } from 'lucide-react';

export default function DashboardPage() {
  const { usuario } = useAuth();

  const { data: dashboard, isLoading } = useDashboard();

  if (isLoading || !dashboard) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  const porcentajeGeneral = dashboard.leccionesTotales > 0
    ? Math.round((dashboard.leccionesCompletadas / dashboard.leccionesTotales) * 100)
    : 0;

  return (
    <div className="max-w-5xl mx-auto px-4 py-10">
      {/* Saludo */}
      <div className="mb-10">
        <h1 className="text-3xl font-bold mb-1">Hola, {usuario?.nombre} 👋</h1>
        <p className="text-gray-400">Tu progreso de aprendizaje</p>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-2 md:grid-cols-3 gap-4 mb-10">
        <StatCard icono={<Star size={22} className="text-yellow-400" />} valor={dashboard.puntosTotales} etiqueta="Puntos totales" />
        <StatCard icono={<Flame size={22} className="text-orange-400" />} valor={dashboard.rachaDias} etiqueta="Días de racha" />
        <StatCard icono={<BookOpen size={22} className="text-[#3498DB]" />} valor={dashboard.leccionesCompletadas} etiqueta="Lecciones" />
      </div>

      {/* Progreso general */}
      <div className="bg-[#25254A] rounded-2xl p-6 border border-white/5 mb-8">
        <div className="flex items-center justify-between mb-4">
          <h2 className="font-semibold flex items-center gap-2">
            <TrendingUp size={18} className="text-[#3498DB]" />
            Progreso general
          </h2>
          <span className="text-2xl font-bold text-[#3498DB]">{porcentajeGeneral}%</span>
        </div>
        <div className="h-3 rounded-full bg-white/5">
          <div
            className="h-full rounded-full bg-gradient-to-r from-[#3498DB] to-[#9B59B6] transition-all duration-700"
            style={{ width: `${porcentajeGeneral}%` }}
          />
        </div>
        <p className="text-xs text-gray-500 mt-2">
          {dashboard.leccionesCompletadas} de {dashboard.leccionesTotales} lecciones completadas
        </p>
      </div>

      {/* Progreso por área */}
      <h2 className="font-semibold mb-4">Progreso por área</h2>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {dashboard.resumenAreas.map((area) => (
          <div key={area.slug} className="bg-[#25254A] rounded-xl p-5 border border-white/5">
            <div className="flex items-center gap-3 mb-3">
              <span className="text-2xl">{area.icono}</span>
              <div className="flex-1">
                <h3 className="font-medium text-sm">{area.titulo}</h3>
                <p className="text-xs text-gray-500">Nivel {area.nivelActual}</p>
              </div>
              <span className="text-sm font-semibold text-[#3498DB]">{area.porcentaje}%</span>
            </div>
            <div className="h-1.5 rounded-full bg-white/5">
              <div
                className="h-full rounded-full bg-[#3498DB] transition-all duration-500"
                style={{ width: `${area.porcentaje}%` }}
              />
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

function StatCard({ icono, valor, etiqueta }: { icono: React.ReactNode; valor: number; etiqueta: string }) {
  return (
    <div className="bg-[#25254A] rounded-xl p-4 border border-white/5">
      <div className="mb-2">{icono}</div>
      <p className="text-2xl font-bold">{valor.toLocaleString()}</p>
      <p className="text-xs text-gray-500">{etiqueta}</p>
    </div>
  );
}
