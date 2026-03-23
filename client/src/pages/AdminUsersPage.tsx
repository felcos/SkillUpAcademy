import { useState } from 'react';
import { useUsuariosAdmin, useAlternarBloqueoIA } from '../hooks/useAdmin';
import { Users, ChevronLeft, ChevronRight, Shield, ShieldOff } from 'lucide-react';

export default function AdminUsersPage() {
  const [pagina, setPagina] = useState(1);
  const tamano = 20;
  const { data, isLoading } = useUsuariosAdmin(pagina, tamano);
  const alternarBloqueo = useAlternarBloqueoIA();

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  if (!data) {
    return (
      <div className="max-w-6xl mx-auto px-4 py-10">
        <p className="text-gray-400">No se pudieron cargar los usuarios.</p>
      </div>
    );
  }

  const totalPaginas = Math.ceil(data.total / tamano);

  const formatearFecha = (fecha: string | null) => {
    if (!fecha) return '-';
    return new Date(fecha).toLocaleDateString('es-ES', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
    });
  };

  return (
    <div className="max-w-6xl mx-auto px-4 py-10">
      <div className="mb-8">
        <h1 className="text-3xl font-bold mb-1 flex items-center gap-3">
          <Users size={28} className="text-[#3498DB]" />
          Gestion de Usuarios
        </h1>
        <p className="text-gray-400">{data.total} usuarios registrados</p>
      </div>

      <div className="bg-[#25254A] rounded-2xl border border-white/5 overflow-hidden">
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-white/10 bg-white/5">
                <th className="text-left py-3 px-4 text-gray-400 font-medium">Email</th>
                <th className="text-left py-3 px-4 text-gray-400 font-medium">Nombre</th>
                <th className="text-center py-3 px-4 text-gray-400 font-medium">Registro</th>
                <th className="text-center py-3 px-4 text-gray-400 font-medium">Lecciones</th>
                <th className="text-center py-3 px-4 text-gray-400 font-medium">Logros</th>
                <th className="text-center py-3 px-4 text-gray-400 font-medium">Progreso</th>
                <th className="text-center py-3 px-4 text-gray-400 font-medium">IA</th>
              </tr>
            </thead>
            <tbody>
              {data.usuarios.map((usuario) => (
                <tr key={usuario.id} className="border-b border-white/5 hover:bg-white/5 transition-colors">
                  <td className="py-3 px-4 text-xs">{usuario.email}</td>
                  <td className="py-3 px-4">{usuario.nombreCompleto}</td>
                  <td className="py-3 px-4 text-center text-gray-400 text-xs">{formatearFecha(usuario.fechaRegistro)}</td>
                  <td className="py-3 px-4 text-center">{usuario.leccionesCompletadas}</td>
                  <td className="py-3 px-4 text-center">{usuario.logrosDesbloqueados}</td>
                  <td className="py-3 px-4 text-center">
                    <div className="flex items-center justify-center gap-2">
                      <div className="w-16 h-1.5 rounded-full bg-white/10">
                        <div
                          className="h-full rounded-full bg-[#3498DB]"
                          style={{ width: `${usuario.progresoGeneral}%` }}
                        />
                      </div>
                      <span className="text-xs text-gray-400">{usuario.progresoGeneral}%</span>
                    </div>
                  </td>
                  <td className="py-3 px-4 text-center">
                    <button
                      onClick={() => alternarBloqueo.mutate(usuario.id)}
                      disabled={alternarBloqueo.isPending}
                      className={`inline-flex items-center gap-1 px-2 py-1 rounded-lg text-xs font-medium transition-colors ${
                        usuario.estaBloqueadoIA
                          ? 'bg-red-500/20 text-red-400 hover:bg-red-500/30'
                          : 'bg-emerald-500/20 text-emerald-400 hover:bg-emerald-500/30'
                      }`}
                      title={usuario.estaBloqueadoIA ? 'Desbloquear acceso IA' : 'Bloquear acceso IA'}
                    >
                      {usuario.estaBloqueadoIA ? (
                        <><ShieldOff size={14} /> Bloqueado</>
                      ) : (
                        <><Shield size={14} /> Activo</>
                      )}
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        {/* Paginacion */}
        {totalPaginas > 1 && (
          <div className="flex items-center justify-between px-4 py-3 border-t border-white/10">
            <p className="text-xs text-gray-500">
              Pagina {pagina} de {totalPaginas}
            </p>
            <div className="flex gap-2">
              <button
                onClick={() => setPagina(p => Math.max(1, p - 1))}
                disabled={pagina === 1}
                className="p-1.5 rounded-lg bg-white/5 hover:bg-white/10 disabled:opacity-30 disabled:cursor-not-allowed transition-colors"
              >
                <ChevronLeft size={16} />
              </button>
              <button
                onClick={() => setPagina(p => Math.min(totalPaginas, p + 1))}
                disabled={pagina === totalPaginas}
                className="p-1.5 rounded-lg bg-white/5 hover:bg-white/10 disabled:opacity-30 disabled:cursor-not-allowed transition-colors"
              >
                <ChevronRight size={16} />
              </button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
