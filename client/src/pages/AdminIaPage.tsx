import { useState } from 'react';
import { useProveedoresIAAdmin, useActualizarProveedorIA, useAlternarProveedorIA, useActivarProveedorIA } from '../hooks/useAdmin';
import { Brain, Settings, Key, ToggleLeft, ToggleRight, Save, Loader2, Zap } from 'lucide-react';
import type { ConfiguracionProveedorIA } from '../lib/api';

const iconosProveedor: Record<string, string> = {
  Anthropic: '🟠',
  OpenAI: '🟢',
  Groq: '⚡',
  Mistral: '🔵',
  Google: '🔴',
};

export default function AdminIaPage() {
  const { data: proveedores, isLoading } = useProveedoresIAAdmin();
  const alternarMut = useAlternarProveedorIA();
  const activarMut = useActivarProveedorIA();

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
        <Brain size={28} className="text-[#3498DB]" />
        <div>
          <h1 className="text-2xl font-bold">Proveedores de IA</h1>
          <p className="text-gray-400 text-sm">
            Configura los proveedores de inteligencia artificial para el chat con Aria.
          </p>
        </div>
      </div>

      <div className="bg-[#3498DB]/10 border border-[#3498DB]/20 rounded-xl p-4 mb-6">
        <p className="text-sm text-[#3498DB]">
          <strong>Proveedor activo</strong> es el que se usa para las conversaciones con Aria.
          Solo un proveedor puede estar activo a la vez. Debe estar habilitado y tener API key configurada.
        </p>
      </div>

      <div className="space-y-6">
        {proveedores?.map((prov) => (
          <ProveedorIACard
            key={prov.id}
            proveedor={prov}
            onAlternar={() => alternarMut.mutate(prov.tipo)}
            onActivar={() => activarMut.mutate(prov.tipo)}
            alternando={alternarMut.isPending}
            activando={activarMut.isPending}
          />
        ))}
      </div>
    </div>
  );
}

function ProveedorIACard({
  proveedor,
  onAlternar,
  onActivar,
  alternando,
  activando,
}: {
  proveedor: ConfiguracionProveedorIA;
  onAlternar: () => void;
  onActivar: () => void;
  alternando: boolean;
  activando: boolean;
}) {
  const [editando, setEditando] = useState(false);
  const [apiKey, setApiKey] = useState('');
  const [nombreVisible, setNombreVisible] = useState(proveedor.nombreVisible);
  const [descripcion, setDescripcion] = useState(proveedor.descripcion ?? '');
  const [urlBase, setUrlBase] = useState(proveedor.urlBase);
  const [modeloChat, setModeloChat] = useState(proveedor.modeloChat);
  const [maxTokens, setMaxTokens] = useState(proveedor.maxTokens);
  const [temperatura, setTemperatura] = useState(proveedor.temperatura);

  const actualizarMut = useActualizarProveedorIA();

  const guardar = () => {
    actualizarMut.mutate(
      {
        tipo: proveedor.tipo,
        datos: {
          nombreVisible,
          descripcion,
          habilitado: proveedor.habilitado,
          apiKey: apiKey || undefined,
          urlBase,
          modeloChat,
          maxTokens,
          temperatura,
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

  const icono = iconosProveedor[proveedor.tipo] ?? '🤖';

  return (
    <div className={`bg-[#25254A] rounded-2xl border transition-all ${
      proveedor.esActivo ? 'border-green-500/40 ring-1 ring-green-500/20' :
      proveedor.habilitado ? 'border-blue-500/30' : 'border-white/5'
    }`}>
      {/* Header */}
      <div className="flex items-center justify-between p-6 pb-0">
        <div className="flex items-center gap-3">
          <span className="text-2xl">{icono}</span>
          <div>
            <div className="flex items-center gap-2">
              <h3 className="font-semibold text-lg">{proveedor.nombreVisible}</h3>
              {proveedor.esActivo && (
                <span className="text-xs bg-green-500/20 text-green-400 px-2 py-0.5 rounded-full font-medium">
                  ACTIVO
                </span>
              )}
            </div>
            <p className="text-xs text-gray-500">{proveedor.tipo} · {proveedor.modeloChat}</p>
          </div>
        </div>
        <div className="flex items-center gap-2">
          {proveedor.habilitado && !proveedor.esActivo && (
            <button
              onClick={onActivar}
              disabled={activando}
              className="flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-green-500/10 hover:bg-green-500/20 text-green-400 text-xs font-medium transition-colors disabled:opacity-50"
              title="Establecer como proveedor activo"
            >
              <Zap size={14} />
              Activar
            </button>
          )}
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
      <div className="px-6 py-3 flex items-center gap-4 text-sm flex-wrap">
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
          <div className="grid grid-cols-2 gap-4">
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
              <label className="text-xs text-gray-400 block mb-1">Modelo</label>
              <input
                type="text"
                value={modeloChat}
                onChange={(e) => setModeloChat(e.target.value)}
                className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none font-mono"
              />
            </div>
          </div>

          <div>
            <label className="text-xs text-gray-400 block mb-1">Descripcion</label>
            <textarea
              value={descripcion}
              onChange={(e) => setDescripcion(e.target.value)}
              rows={2}
              className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none resize-none"
            />
          </div>

          <div>
            <label className="text-xs text-gray-400 block mb-1">
              API Key {proveedor.tieneApiKey && <span className="text-green-400">(ya configurada)</span>}
            </label>
            <input
              type="password"
              value={apiKey}
              onChange={(e) => setApiKey(e.target.value)}
              placeholder={proveedor.tieneApiKey ? '••••••••••••' : 'Pega aqui tu API key'}
              className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none font-mono"
            />
          </div>

          <div>
            <label className="text-xs text-gray-400 block mb-1">URL Base de la API</label>
            <input
              type="text"
              value={urlBase}
              onChange={(e) => setUrlBase(e.target.value)}
              className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none font-mono"
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="text-xs text-gray-400 block mb-1">Max Tokens</label>
              <input
                type="number"
                value={maxTokens}
                onChange={(e) => setMaxTokens(Number(e.target.value))}
                min={100}
                max={4096}
                className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-3 py-2 text-sm focus:border-[#3498DB] focus:outline-none"
              />
            </div>
            <div>
              <label className="text-xs text-gray-400 block mb-1">Temperatura ({temperatura})</label>
              <input
                type="range"
                value={temperatura}
                onChange={(e) => setTemperatura(Number(e.target.value))}
                min={0}
                max={1}
                step={0.1}
                className="w-full mt-2"
              />
            </div>
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
