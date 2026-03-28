import { Link, useParams } from 'react-router-dom';
import { useAreaDetalle, useNivel } from '../hooks/useSkills';
import { ArrowLeft, Lock, CheckCircle2, Play, MessageSquare, HelpCircle, Theater, Brain, BookOpen, Target, Lightbulb, ClipboardCheck, Award } from 'lucide-react';

const iconosTipo: Record<string, typeof Play> = {
  Teoria: Play,
  Quiz: HelpCircle,
  Escenario: Theater,
  Roleplay: MessageSquare,
  Autoevaluacion: Brain,
  CasoEstudio: BookOpen,
  PracticaGuiada: Target,
  Reflexion: Lightbulb,
  PlanAccion: ClipboardCheck,
  Capstone: Award,
};

const colorEstado: Record<string, string> = {
  Completado: 'text-green-400',
  EnProgreso: 'text-yellow-400',
  NoIniciado: 'text-gray-500',
};

export default function AreaDetailPage() {
  const { slug } = useParams<{ slug: string }>();

  const { data: area, isLoading: cargandoArea } = useAreaDetalle(slug);

  const nivelActivo = area?.niveles?.[0]?.numeroNivel ?? 1;

  const { data: nivel, isLoading: cargandoNivel } = useNivel(slug, nivelActivo);

  if (cargandoArea || cargandoNivel) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  if (!area) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-16 text-center">
        <p className="text-red-400">Área no encontrada</p>
      </div>
    );
  }

  return (
    <div className="max-w-5xl mx-auto px-4 py-10">
      {/* Header */}
      <Link to="/areas" className="inline-flex items-center gap-1.5 text-gray-400 hover:text-white transition-colors text-sm mb-6">
        <ArrowLeft size={16} />
        Volver a áreas
      </Link>

      <div className="flex items-center gap-4 mb-8">
        <span className="text-5xl">{area.icono}</span>
        <div>
          <h1 className="text-3xl font-bold">{area.titulo}</h1>
          <p className="text-gray-400">{area.descripcion || area.subtitulo}</p>
        </div>
      </div>

      {/* Niveles */}
      <div className="flex gap-3 mb-8 overflow-x-auto pb-2">
        {area.niveles?.map((n) => (
          <div
            key={n.id}
            className={`flex-shrink-0 px-5 py-3 rounded-xl border transition-all ${
              n.numeroNivel === nivelActivo
                ? 'bg-[#3498DB]/10 border-[#3498DB]/40 text-white'
                : n.desbloqueado
                ? 'bg-[#25254A] border-white/5 text-gray-300 cursor-pointer hover:border-white/15'
                : 'bg-[#25254A]/50 border-white/5 text-gray-600'
            }`}
          >
            <div className="flex items-center gap-2">
              {!n.desbloqueado && <Lock size={14} />}
              <span className="font-medium text-sm">Nivel {n.numeroNivel}</span>
            </div>
            <p className="text-xs mt-0.5 opacity-70">{n.nombre}</p>
            {n.desbloqueado && (
              <p className="text-xs mt-1 opacity-50">{n.leccionesCompletadas}/{n.leccionesTotales}</p>
            )}
          </div>
        ))}
      </div>

      {/* Lecciones */}
      <div className="space-y-3">
        {nivel?.lecciones?.map((leccion, index) => {
          const Icono = iconosTipo[leccion.tipoLeccion] || Play;
          const tiposIA = ['Roleplay', 'Autoevaluacion', 'CasoEstudio', 'PracticaGuiada', 'Reflexion', 'PlanAccion', 'Capstone'];
          const rutaLeccion = leccion.tipoLeccion === 'Quiz'
            ? `/leccion/${leccion.id}/quiz`
            : leccion.tipoLeccion === 'Escenario'
            ? `/leccion/${leccion.id}/escenario`
            : tiposIA.includes(leccion.tipoLeccion)
            ? `/chat?leccion=${leccion.id}&tipo=${leccion.tipoLeccion}`
            : `/leccion/${leccion.id}`;

          return (
            <Link
              key={leccion.id}
              to={rutaLeccion}
              className="flex items-center gap-4 bg-[#25254A] rounded-xl p-4 border border-white/5 hover:border-white/15 transition-all group"
            >
              <div
                className="w-10 h-10 rounded-lg flex items-center justify-center flex-shrink-0"
                style={{ backgroundColor: `${area.colorPrimario}15` }}
              >
                {leccion.estado === 'Completado' ? (
                  <CheckCircle2 size={20} className="text-green-400" />
                ) : (
                  <Icono size={20} style={{ color: area.colorPrimario }} />
                )}
              </div>

              <div className="flex-1 min-w-0">
                <div className="flex items-center gap-2">
                  <span className="text-xs text-gray-500">{index + 1}.</span>
                  <h3 className="font-medium text-sm truncate group-hover:text-white transition-colors">
                    {leccion.titulo}
                  </h3>
                </div>
                <div className="flex items-center gap-3 mt-0.5">
                  <span className="text-xs text-gray-500">{leccion.tipoLeccion}</span>
                  <span className="text-xs text-gray-600">·</span>
                  <span className="text-xs text-gray-500">{leccion.duracionMinutos} min</span>
                  <span className="text-xs text-gray-600">·</span>
                  <span className="text-xs text-gray-500">{leccion.puntosRecompensa} pts</span>
                </div>
              </div>

              <div className="flex-shrink-0">
                <span className={`text-xs font-medium ${colorEstado[leccion.estado] || 'text-gray-500'}`}>
                  {leccion.estado === 'Completado' && leccion.puntuacion != null
                    ? `${leccion.puntuacion}%`
                    : leccion.estado === 'EnProgreso'
                    ? 'En progreso'
                    : ''}
                </span>
              </div>
            </Link>
          );
        })}
      </div>
    </div>
  );
}
