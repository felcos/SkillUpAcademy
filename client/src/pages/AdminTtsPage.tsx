import { useState } from 'react';
import { useProveedoresTtsAdmin, useActualizarProveedorTts, useAlternarProveedorTts } from '../hooks/useTts';
import { Volume2, Settings, Key, ToggleLeft, ToggleRight, Save, Loader2 } from 'lucide-react';
import type { ConfiguracionProveedorTts } from '../lib/api';

export default function AdminTtsPage() {
  const { data: proveedores, isLoading } = useProveedoresTtsAdmin();
  const alternarMut = useAlternarProveedorTts();

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  return (
    <div className="max-w-4xl mx-auto px-4 py-10">
      <div className="flex items-center gap-3 mb-8">
        <Volume2 size={28} className="text-[#3498DB]" />
        <div>
          <h1 className="text-2xl font-bold">Configuración de Voz (TTS)</h1>
          <p className="text-gray-400 text-sm">
            Gestiona los proveedores de Text-to-Speech. Los usuarios verán solo los proveedores habilitados.
          </p>
        </div>
      </div>

      {/* Info */}
      <div className="bg-[#3498DB]/10 border border-[#3498DB]/20 rounded-xl p-4 mb-6">
        <p className="text-sm text-[#3498DB]">
          <strong>Web Speech API</strong> (voz del sistema) siempre está disponible como fallback.
          Habilita Azure y/o ElevenLabs para ofrecer voces de mayor calidad a los usuarios.
        </p>
      </div>

      <div className="space-y-6">
        {proveedores?.map((prov) => (
          <ProveedorCard
            key={prov.id}
            proveedor={prov}
            onAlternar={() => alternarMut.mutate(prov.tipo)}
            alternando={alternarMut.isPending}
          />
        ))}
      </div>
    </div>
  );
}

function ProveedorCard({
  proveedor,
  onAlternar,
  alternando,
}: {
  proveedor: ConfiguracionProveedorTts;
  onAlternar: () => void;
  alternando: boolean;
}) {
  const [editando, setEditando] = useState(false);
  const [apiKey, setApiKey] = useState('');
  const [region, setRegion] = useState(proveedor.region ?? '');
  const [nombreVisible, setNombreVisible] = useState(proveedor.nombreVisible);
  const [descripcion, setDescripcion] = useState(proveedor.descripcion ?? '');
  const [vozPorDefecto, setVozPorDefecto] = useState(proveedor.vozPorDefecto);

  const actualizarMut = useActualizarProveedorTts();

  const guardar = () => {
    actualizarMut.mutate(
      {
        tipo: proveedor.tipo,
        datos: {
          nombreVisible,
          descripcion,
          habilitado: proveedor.habilitado,
          apiKey: apiKey || undefined,
          region: proveedor.tipo === 'AzureSpeech' ? region : undefined,
          vozPorDefecto,
        },
      },
      {
        onSuccess: () => {
          setEditando(false);
          setApiKey('');
        },
      }
    );
  };

  const iconoProveedor = proveedor.tipo === 'AzureSpeech' ? '🔷' : '🟣';

  return (
    <div className={`bg-[#25254A] rounded-2xl border transition-all ${
      proveedor.habilitado ? 'border-green-500/30' : 'border-white/5'
    }`}>
      {/* Header */}
      <div className="flex items-center justify-between p-6 pb-0">
        <div className="flex items-center gap-3">
          <span className="text-2xl">{iconoProveedor}</span>
          <div>
            <h3 className="font-semibold text-lg">{proveedor.nombreVisible}</h3>
            <p className="text-xs text-gray-500">{proveedor.tipo}</p>
          </div>
        </div>
        <div className="flex items-center gap-3">
          <button
            onClick={onAlternar}
            disabled={alternando}
            className="flex items-center gap-2 transition-colors"
            title={proveedor.habilitado ? 'Deshabilitar' : 'Habilitar'}
          >
            {proveedor.habilitado ? (
              <ToggleRight size={32} className="text-green-400" />
            ) : (
              <ToggleLeft size={32} className="text-gray-500" />
            )}
          </button>
          <button
            onClick={() => setEditando(!editando)}
            className="p-2 rounded-lg bg-white/5 hover:bg-white/10 transition-colors"
            title="Configurar"
          >
            <Settings size={18} />
          </button>
        </div>
      </div>

      {/* Status */}
      <div className="px-6 py-3 flex items-center gap-4 text-sm">
        <span className={`flex items-center gap-1.5 ${proveedor.habilitado ? 'text-green-400' : 'text-gray-500'}`}>
          <span className={`w-2 h-2 rounded-full ${proveedor.habilitado ? 'bg-green-400' : 'bg-gray-500'}`} />
          {proveedor.habilitado ? 'Habilitado' : 'Deshabilitado'}
        </span>
        <span className={`flex items-center gap-1.5 ${proveedor.tieneApiKey ? 'text-blue-400' : 'text-yellow-400'}`}>
          <Key size={14} />
          {proveedor.tieneApiKey ? 'API Key configurada' : 'Sin API Key'}
        </span>
        {proveedor.descripcion && (
          <span className="text-gray-500 truncate">{proveedor.descripcion}</span>
        )}
      </div>

      {/* Editor */}
      {editando && (
        <div className="px-6 pb-6 pt-2 border-t border-white/5 space-y-4">
          <div>
            <label className="text-xs text-gray-400 block mb-1">Nombre visible</label>
            <input
              type="text"
              value={nombreVisible}
              onChange={(e) => setNombreVisible(e.target.value)}
              className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none"
            />
          </div>

          <div>
            <label className="text-xs text-gray-400 block mb-1">Descripción</label>
            <textarea
              value={descripcion}
              onChange={(e) => setDescripcion(e.target.value)}
              rows={2}
              className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none resize-none"
            />
          </div>

          <div>
            <label className="text-xs text-gray-400 block mb-1">
              API Key {proveedor.tieneApiKey && <span className="text-green-400">(ya configurada — dejar vacío para mantener)</span>}
            </label>
            <input
              type="password"
              value={apiKey}
              onChange={(e) => setApiKey(e.target.value)}
              placeholder={proveedor.tieneApiKey ? '••••••••••••' : 'Pega aquí tu API key'}
              className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none font-mono"
            />
          </div>

          {proveedor.tipo === 'AzureSpeech' && (
            <div>
              <label className="text-xs text-gray-400 block mb-1">Región Azure</label>
              <select
                value={region}
                onChange={(e) => setRegion(e.target.value)}
                className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none"
              >
                <option value="westeurope">West Europe</option>
                <option value="northeurope">North Europe</option>
                <option value="eastus">East US</option>
                <option value="westus">West US</option>
                <option value="southcentralus">South Central US</option>
              </select>
            </div>
          )}

          <div>
            <label className="text-xs text-gray-400 block mb-1">Voz por defecto</label>
            <input
              type="text"
              value={vozPorDefecto}
              onChange={(e) => setVozPorDefecto(e.target.value)}
              className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none font-mono"
            />
          </div>

          <div className="flex justify-end gap-3 pt-2">
            <button
              onClick={() => setEditando(false)}
              className="px-4 py-2 rounded-lg bg-white/5 hover:bg-white/10 text-sm transition-colors"
            >
              Cancelar
            </button>
            <button
              onClick={guardar}
              disabled={actualizarMut.isPending}
              className="flex items-center gap-2 px-4 py-2 rounded-lg bg-[#3498DB] hover:bg-[#2980B9] text-white text-sm font-medium transition-colors disabled:opacity-50"
            >
              {actualizarMut.isPending ? (
                <Loader2 size={16} className="animate-spin" />
              ) : (
                <Save size={16} />
              )}
              Guardar
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
