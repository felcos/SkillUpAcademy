import { Link } from 'react-router-dom';
import { useResumenAdmin, useEstadisticasContenido } from '../hooks/useAdmin';
import { Users, MessageSquare, BookOpen, BarChart3, Brain, CheckCircle, Volume2, UserCog } from 'lucide-react';

export default function AdminDashboardPage() {
  const { data: resumen, isLoading: cargandoResumen } = useResumenAdmin();
  const { data: estadisticas, isLoading: cargandoEstadisticas } = useEstadisticasContenido();

  if (cargandoResumen || cargandoEstadisticas) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  if (!resumen || !estadisticas) {
    return (
      <div className="max-w-6xl mx-auto px-4 py-10">
        <p className="text-gray-400">No se pudieron cargar los datos del panel de administracion.</p>
      </div>
    );
  }

  const maxActividad = Math.max(
    ...resumen.actividadUltimos30Dias.map(d => d.usuariosActivos + d.leccionesCompletadas + d.mensajesIA),
    1
  );

  return (
    <div className="max-w-6xl mx-auto px-4 py-10">
      <div className="mb-10">
        <h1 className="text-3xl font-bold mb-1">Panel de Administracion</h1>
        <p className="text-gray-400">Resumen general de la plataforma</p>
        <div className="flex gap-3 mt-4">
          <Link to="/admin/usuarios" className="flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-white/5 hover:bg-white/10 text-sm text-gray-300 transition-colors">
            <UserCog size={16} /> Usuarios
          </Link>
          <Link to="/admin/tts" className="flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-white/5 hover:bg-white/10 text-sm text-gray-300 transition-colors">
            <Volume2 size={16} /> Configurar Voz
          </Link>
          <Link to="/admin/ia" className="flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-white/5 hover:bg-white/10 text-sm text-gray-300 transition-colors">
            <Brain size={16} /> Proveedores IA
          </Link>
        </div>
      </div>

      {/* Metricas principales */}
      <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-4 mb-10">
        <TarjetaMetrica
          icono={<Users size={20} className="text-[#3498DB]" />}
          valor={resumen.totalUsuarios}
          etiqueta="Usuarios"
        />
        <TarjetaMetrica
          icono={<Users size={20} className="text-green-400" />}
          valor={resumen.usuariosActivos7Dias}
          etiqueta="Activos (7d)"
        />
        <TarjetaMetrica
          icono={<Brain size={20} className="text-purple-400" />}
          valor={resumen.totalSesionesIA}
          etiqueta="Sesiones IA"
        />
        <TarjetaMetrica
          icono={<MessageSquare size={20} className="text-cyan-400" />}
          valor={resumen.totalMensajesIA}
          etiqueta="Mensajes IA"
        />
        <TarjetaMetrica
          icono={<CheckCircle size={20} className="text-emerald-400" />}
          valor={resumen.totalLeccionesCompletadas}
          etiqueta="Completadas"
        />
        <TarjetaMetrica
          icono={<BarChart3 size={20} className="text-yellow-400" />}
          valor={`${resumen.promedioProgreso}%`}
          etiqueta="Progreso prom."
        />
      </div>

      {/* Grafico de actividad */}
      <div className="bg-[#25254A] rounded-2xl p-6 border border-white/5 mb-10">
        <h2 className="font-semibold mb-6 flex items-center gap-2">
          <BarChart3 size={18} className="text-[#3498DB]" />
          Actividad ultimos 30 dias
        </h2>
        <div className="flex items-end gap-1 h-40">
          {resumen.actividadUltimos30Dias.map((dia) => {
            const total = dia.usuariosActivos + dia.leccionesCompletadas + dia.mensajesIA;
            const altura = maxActividad > 0 ? (total / maxActividad) * 100 : 0;
            const fechaCorta = dia.fecha.slice(5);
            return (
              <div
                key={dia.fecha}
                className="flex-1 flex flex-col items-center group relative"
              >
                <div
                  className="w-full rounded-t bg-gradient-to-t from-[#3498DB] to-[#9B59B6] transition-all duration-300 hover:opacity-80 min-h-[2px]"
                  style={{ height: `${Math.max(altura, 2)}%` }}
                  title={`${fechaCorta}: ${dia.usuariosActivos} activos, ${dia.leccionesCompletadas} lecciones, ${dia.mensajesIA} mensajes`}
                />
                <div className="absolute -top-16 left-1/2 -translate-x-1/2 bg-[#1A1A2E] border border-white/10 rounded-lg px-2 py-1 text-xs whitespace-nowrap opacity-0 group-hover:opacity-100 transition-opacity pointer-events-none z-10">
                  <p className="font-medium">{fechaCorta}</p>
                  <p className="text-[#3498DB]">{dia.usuariosActivos} activos</p>
                  <p className="text-emerald-400">{dia.leccionesCompletadas} lecciones</p>
                  <p className="text-purple-400">{dia.mensajesIA} mensajes</p>
                </div>
              </div>
            );
          })}
        </div>
        <div className="flex justify-between mt-2 text-xs text-gray-500">
          <span>{resumen.actividadUltimos30Dias[0]?.fecha.slice(5) ?? ''}</span>
          <span>{resumen.actividadUltimos30Dias[resumen.actividadUltimos30Dias.length - 1]?.fecha.slice(5) ?? ''}</span>
        </div>
      </div>

      {/* Estadisticas de contenido */}
      <div className="bg-[#25254A] rounded-2xl p-6 border border-white/5 mb-10">
        <h2 className="font-semibold mb-4 flex items-center gap-2">
          <BookOpen size={18} className="text-[#3498DB]" />
          Contenido educativo
        </h2>
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-4 mb-6">
          <MiniMetrica valor={estadisticas.totalAreas} etiqueta="Areas" />
          <MiniMetrica valor={estadisticas.totalNiveles} etiqueta="Niveles" />
          <MiniMetrica valor={estadisticas.totalLecciones} etiqueta="Lecciones" />
          <MiniMetrica valor={estadisticas.totalPreguntas} etiqueta="Preguntas" />
          <MiniMetrica valor={estadisticas.totalEscenarios} etiqueta="Escenarios" />
          <MiniMetrica valor={estadisticas.totalEscenas} etiqueta="Escenas" />
        </div>

        {estadisticas.areasPorCompletados.length > 0 && (
          <>
            <h3 className="text-sm font-medium text-gray-400 mb-3">Estadisticas por area</h3>
            <div className="overflow-x-auto">
              <table className="w-full text-sm">
                <thead>
                  <tr className="border-b border-white/10">
                    <th className="text-left py-2 px-3 text-gray-400 font-medium">Area</th>
                    <th className="text-right py-2 px-3 text-gray-400 font-medium">Completados</th>
                    <th className="text-right py-2 px-3 text-gray-400 font-medium">Promedio calif.</th>
                  </tr>
                </thead>
                <tbody>
                  {estadisticas.areasPorCompletados.map((area) => (
                    <tr key={area.nombreArea} className="border-b border-white/5 hover:bg-white/5 transition-colors">
                      <td className="py-2 px-3">{area.nombreArea}</td>
                      <td className="py-2 px-3 text-right">{area.vecesCompletada}</td>
                      <td className="py-2 px-3 text-right">{area.promedioCalificacion.toFixed(1)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </>
        )}
      </div>
    </div>
  );
}

function TarjetaMetrica({ icono, valor, etiqueta }: { icono: React.ReactNode; valor: number | string; etiqueta: string }) {
  return (
    <div className="bg-[#25254A] rounded-xl p-4 border border-white/5">
      <div className="mb-2">{icono}</div>
      <p className="text-xl font-bold">{typeof valor === 'number' ? valor.toLocaleString() : valor}</p>
      <p className="text-xs text-gray-500">{etiqueta}</p>
    </div>
  );
}

function MiniMetrica({ valor, etiqueta }: { valor: number; etiqueta: string }) {
  return (
    <div className="text-center">
      <p className="text-lg font-bold text-[#3498DB]">{valor}</p>
      <p className="text-xs text-gray-500">{etiqueta}</p>
    </div>
  );
}
