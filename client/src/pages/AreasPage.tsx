import { Link } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import { skillsApi, type AreaHabilidad } from '../lib/api';
import { BookOpen } from 'lucide-react';

export default function AreasPage() {
  const { data: areas, isLoading, error } = useQuery({
    queryKey: ['areas'],
    queryFn: skillsApi.listar,
  });

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  if (error) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-16 text-center">
        <p className="text-red-400">Error al cargar las áreas. Intenta recargar la página.</p>
      </div>
    );
  }

  return (
    <div className="max-w-6xl mx-auto px-4 py-10">
      <div className="mb-10">
        <h1 className="text-3xl font-bold mb-2">Áreas de Habilidades</h1>
        <p className="text-gray-400">Elige un área para comenzar tu aprendizaje</p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {areas?.map((area: AreaHabilidad) => (
          <Link
            key={area.id}
            to={`/areas/${area.slug}`}
            className="group bg-[#25254A] rounded-2xl p-6 border border-white/5 hover:border-white/15 transition-all hover:-translate-y-1"
          >
            <div className="flex items-start justify-between mb-4">
              <span className="text-4xl">{area.icono}</span>
              <BookOpen size={20} className="text-gray-500 group-hover:text-gray-300 transition-colors" />
            </div>
            <h3 className="text-lg font-semibold mb-1">{area.titulo}</h3>
            <p className="text-gray-400 text-sm mb-4">{area.subtitulo}</p>

            {area.progreso && (
              <div>
                <div className="flex justify-between text-xs text-gray-500 mb-1.5">
                  <span>{area.progreso.leccionesCompletadas}/{area.progreso.leccionesTotales} lecciones</span>
                  <span>{area.progreso.porcentaje}%</span>
                </div>
                <div className="h-1.5 rounded-full bg-white/5">
                  <div
                    className="h-full rounded-full transition-all duration-500"
                    style={{ width: `${area.progreso.porcentaje}%`, backgroundColor: area.colorPrimario }}
                  />
                </div>
              </div>
            )}

            {!area.progreso && (
              <div className="h-1.5 rounded-full bg-white/5">
                <div className="h-full rounded-full w-0" style={{ backgroundColor: area.colorPrimario }} />
              </div>
            )}
          </Link>
        ))}
      </div>
    </div>
  );
}
