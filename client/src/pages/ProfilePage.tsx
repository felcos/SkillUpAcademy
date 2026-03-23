import { useAuth } from '../contexts/AuthContext';
import { useQuery } from '@tanstack/react-query';
import { progressApi } from '../lib/api';
import { User, Mail, Calendar, Star, Flame } from 'lucide-react';

export default function ProfilePage() {
  const { usuario } = useAuth();

  const { data: dashboard, isLoading } = useQuery({
    queryKey: ['dashboard'],
    queryFn: progressApi.dashboard,
  });

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  const fechaRegistro = usuario?.fechaRegistro
    ? new Date(usuario.fechaRegistro).toLocaleDateString('es-ES', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      })
    : '—';

  return (
    <div className="max-w-3xl mx-auto px-4 py-10">
      {/* Cabecera del perfil */}
      <div className="bg-[#25254A] rounded-2xl p-8 border border-white/5 mb-8">
        <div className="flex items-center gap-5 mb-6">
          <div className="w-20 h-20 rounded-full bg-gradient-to-br from-[#3498DB] to-[#9B59B6] flex items-center justify-center">
            <User size={36} className="text-white" />
          </div>
          <div>
            <h1 className="text-2xl font-bold">
              {usuario?.nombre} {usuario?.apellidos}
            </h1>
            <p className="text-gray-400 text-sm">{usuario?.email}</p>
          </div>
        </div>

        {/* Info del usuario */}
        <div className="grid grid-cols-1 sm:grid-cols-3 gap-4">
          <InfoItem
            icono={<Mail size={18} className="text-[#3498DB]" />}
            etiqueta="Email"
            valor={usuario?.email ?? '—'}
          />
          <InfoItem
            icono={<Calendar size={18} className="text-purple-400" />}
            etiqueta="Miembro desde"
            valor={fechaRegistro}
          />
          <InfoItem
            icono={<User size={18} className="text-green-400" />}
            etiqueta="Nombre completo"
            valor={`${usuario?.nombre ?? ''} ${usuario?.apellidos ?? ''}`}
          />
        </div>
      </div>

      {/* Estadísticas */}
      <h2 className="font-semibold text-lg mb-4">Estadísticas</h2>
      <div className="grid grid-cols-2 gap-4">
        <StatCard
          icono={<Star size={22} className="text-yellow-400" />}
          valor={dashboard?.puntosTotales ?? usuario?.puntosTotales ?? 0}
          etiqueta="Puntos totales"
        />
        <StatCard
          icono={<Flame size={22} className="text-orange-400" />}
          valor={dashboard?.rachaDias ?? usuario?.rachaDias ?? 0}
          etiqueta="Días de racha"
        />
      </div>
    </div>
  );
}

function InfoItem({ icono, etiqueta, valor }: { icono: React.ReactNode; etiqueta: string; valor: string }) {
  return (
    <div className="flex items-start gap-3">
      <div className="mt-0.5">{icono}</div>
      <div>
        <p className="text-xs text-gray-500">{etiqueta}</p>
        <p className="text-sm text-gray-200">{valor}</p>
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
