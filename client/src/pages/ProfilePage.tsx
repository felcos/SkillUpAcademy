import { useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { useQuery } from '@tanstack/react-query';
import { progressApi, ttsApi, type VozDisponible } from '../lib/api';
import { useConfiguracionTts, useActualizarPreferenciasTts } from '../hooks/useTts';
import { User, Mail, Calendar, Star, Flame, Volume2, Play, Loader2 } from 'lucide-react';

export default function ProfilePage() {
  const { usuario } = useAuth();

  const { data: dashboard, isLoading } = useQuery({
    queryKey: ['dashboard'],
    queryFn: progressApi.dashboard,
  });

  const { data: configTts, isLoading: cargandoTts } = useConfiguracionTts();
  const actualizarTts = useActualizarPreferenciasTts();

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  const fechaRegistro = '—';

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
      <div className="grid grid-cols-2 gap-4 mb-8">
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

      {/* Configuración de voz */}
      <h2 className="font-semibold text-lg mb-4 flex items-center gap-2">
        <Volume2 size={20} className="text-[#3498DB]" />
        Voz de Aria
      </h2>
      {cargandoTts ? (
        <div className="bg-[#25254A] rounded-2xl p-8 border border-white/5 flex items-center justify-center">
          <Loader2 size={24} className="animate-spin text-gray-400" />
        </div>
      ) : configTts ? (
        <SelectorVoz
          config={configTts}
          onCambiarProveedor={(proveedor) => actualizarTts.mutate({ proveedorPreferido: proveedor })}
          onCambiarVoz={(voz) => actualizarTts.mutate({ vozPreferida: voz })}
          onCambiarVelocidad={(velocidad) => actualizarTts.mutate({ velocidadVoz: velocidad })}
          guardando={actualizarTts.isPending}
        />
      ) : null}
    </div>
  );
}

function SelectorVoz({
  config,
  onCambiarProveedor,
  onCambiarVoz,
  onCambiarVelocidad,
  guardando,
}: {
  config: { proveedores: { tipo: string; nombreVisible: string; descripcion: string | null }[]; voces: VozDisponible[]; vozSeleccionada: string | null; velocidadVoz: number; proveedorPreferido: string };
  onCambiarProveedor: (proveedor: string) => void;
  onCambiarVoz: (voz: string) => void;
  onCambiarVelocidad: (velocidad: number) => void;
  guardando: boolean;
}) {
  const [reproduciendo, setReproduciendo] = useState<string | null>(null);

  const vocesDelProveedor = config.voces.filter(
    (v) => v.proveedor === config.proveedorPreferido
  );

  const reproducirPreview = async (voz: VozDisponible) => {
    if (reproduciendo) return;
    setReproduciendo(voz.idVoz);

    try {
      if (voz.proveedor === 'WebSpeechApi') {
        // Usar Web Speech API directamente
        const utterance = new SpeechSynthesisUtterance(
          'Hola, soy Aria, tu instructora en SkillUp Academy.'
        );
        utterance.lang = 'es-ES';
        utterance.rate = config.velocidadVoz;
        utterance.onend = () => setReproduciendo(null);
        utterance.onerror = () => setReproduciendo(null);
        window.speechSynthesis.speak(utterance);
        return;
      }

      const response = await ttsApi.previewVoz(voz.proveedor, voz.idVoz);
      const contentType = response.headers.get('content-type');

      if (contentType?.includes('audio/')) {
        const blob = await response.blob();
        const audio = new Audio(URL.createObjectURL(blob));
        audio.onended = () => setReproduciendo(null);
        audio.onerror = () => setReproduciendo(null);
        await audio.play();
      } else {
        setReproduciendo(null);
      }
    } catch {
      setReproduciendo(null);
    }
  };

  return (
    <div className="bg-[#25254A] rounded-2xl p-6 border border-white/5 space-y-6">
      {/* Selector de proveedor */}
      <div>
        <label className="text-sm text-gray-400 block mb-2">Tipo de voz</label>
        <div className="grid grid-cols-1 sm:grid-cols-3 gap-3">
          {config.proveedores.map((prov) => (
            <button
              key={prov.tipo}
              onClick={() => onCambiarProveedor(prov.tipo)}
              disabled={guardando}
              className={`p-3 rounded-xl border text-left transition-all ${
                config.proveedorPreferido === prov.tipo
                  ? 'border-[#3498DB] bg-[#3498DB]/10'
                  : 'border-white/10 hover:border-white/20'
              }`}
            >
              <p className="text-sm font-medium">{prov.nombreVisible}</p>
              {prov.descripcion && (
                <p className="text-xs text-gray-500 mt-1 line-clamp-2">{prov.descripcion}</p>
              )}
            </button>
          ))}
        </div>
      </div>

      {/* Selector de voz */}
      <div>
        <label className="text-sm text-gray-400 block mb-2">
          Voz ({vocesDelProveedor.length} disponibles)
        </label>
        <div className="space-y-2 max-h-64 overflow-y-auto pr-1">
          {vocesDelProveedor.map((voz) => (
            <div
              key={voz.idVoz}
              className={`flex items-center justify-between p-3 rounded-lg border transition-all cursor-pointer ${
                config.vozSeleccionada === voz.idVoz
                  ? 'border-[#3498DB] bg-[#3498DB]/10'
                  : 'border-white/5 hover:border-white/15'
              }`}
              onClick={() => onCambiarVoz(voz.idVoz)}
            >
              <div className="flex-1 min-w-0">
                <div className="flex items-center gap-2">
                  <span className="text-sm font-medium">{voz.nombre}</span>
                  <span className={`text-xs px-1.5 py-0.5 rounded ${
                    voz.genero === 'Femenino' ? 'bg-pink-500/20 text-pink-300' : 'bg-blue-500/20 text-blue-300'
                  }`}>
                    {voz.genero}
                  </span>
                  <span className="text-xs text-gray-500">{voz.idioma}</span>
                </div>
                {voz.descripcionPreview && (
                  <p className="text-xs text-gray-500 mt-0.5 truncate">{voz.descripcionPreview}</p>
                )}
              </div>
              <button
                onClick={(e) => { e.stopPropagation(); reproducirPreview(voz); }}
                disabled={reproduciendo !== null}
                className="flex-shrink-0 ml-3 p-2 rounded-lg bg-white/5 hover:bg-white/10 transition-colors disabled:opacity-30"
                title="Escuchar preview"
              >
                {reproduciendo === voz.idVoz ? (
                  <Loader2 size={16} className="animate-spin" />
                ) : (
                  <Play size={16} />
                )}
              </button>
            </div>
          ))}
        </div>
      </div>

      {/* Velocidad */}
      <div>
        <label className="text-sm text-gray-400 block mb-2">
          Velocidad: {config.velocidadVoz.toFixed(2)}x
        </label>
        <input
          type="range"
          min="0.5"
          max="2.0"
          step="0.05"
          value={config.velocidadVoz}
          onChange={(e) => onCambiarVelocidad(parseFloat(e.target.value))}
          className="w-full accent-[#3498DB]"
        />
        <div className="flex justify-between text-xs text-gray-500 mt-1">
          <span>0.5x (lento)</span>
          <span>1.0x</span>
          <span>2.0x (rápido)</span>
        </div>
      </div>

      {guardando && (
        <p className="text-xs text-[#3498DB] flex items-center gap-1">
          <Loader2 size={12} className="animate-spin" /> Guardando preferencias...
        </p>
      )}
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
