import { useEffect } from 'react';
import { Trophy, BookOpen, Flame, X } from 'lucide-react';
import type { NotificacionApp } from '../hooks/useNotificaciones';

interface Props {
  notificaciones: NotificacionApp[];
  onDescartar: (id: string) => void;
}

const ICONOS_TIPO = {
  logro: Trophy,
  leccion: BookOpen,
  racha: Flame,
} as const;

const COLORES_TIPO = {
  logro: 'border-yellow-500/30 bg-yellow-500/10',
  leccion: 'border-[#3498DB]/30 bg-[#3498DB]/10',
  racha: 'border-orange-500/30 bg-orange-500/10',
} as const;

const COLORES_ICONO = {
  logro: 'text-yellow-400',
  leccion: 'text-[#3498DB]',
  racha: 'text-orange-400',
} as const;

/**
 * Muestra notificaciones tipo toast en la esquina superior derecha.
 * Se auto-descarta después de 5 segundos.
 */
export default function NotificacionToast({ notificaciones, onDescartar }: Props) {
  return (
    <div className="fixed top-4 right-4 z-50 flex flex-col gap-3 pointer-events-none">
      {notificaciones.map((notif) => (
        <ToastItem key={notif.id} notificacion={notif} onDescartar={onDescartar} />
      ))}
    </div>
  );
}

function ToastItem({ notificacion, onDescartar }: { notificacion: NotificacionApp; onDescartar: (id: string) => void }) {
  const Icono = ICONOS_TIPO[notificacion.tipo];

  useEffect(() => {
    const timer = setTimeout(() => onDescartar(notificacion.id), 5000);
    return () => clearTimeout(timer);
  }, [notificacion.id, onDescartar]);

  return (
    <div
      className={`pointer-events-auto flex items-start gap-3 px-4 py-3 rounded-xl border ${COLORES_TIPO[notificacion.tipo]} backdrop-blur-md shadow-lg max-w-sm animate-slide-in-right`}
    >
      <div className={`flex-shrink-0 mt-0.5 ${COLORES_ICONO[notificacion.tipo]}`}>
        {notificacion.icono ? (
          <span className="text-xl">{notificacion.icono}</span>
        ) : (
          <Icono size={20} />
        )}
      </div>

      <div className="flex-1 min-w-0">
        <p className="text-sm font-medium text-white truncate">{notificacion.titulo}</p>
        {notificacion.descripcion && (
          <p className="text-xs text-gray-400 mt-0.5">{notificacion.descripcion}</p>
        )}
      </div>

      <button
        onClick={() => onDescartar(notificacion.id)}
        className="flex-shrink-0 text-gray-500 hover:text-white transition-colors active:scale-[0.9]"
      >
        <X size={14} />
      </button>
    </div>
  );
}
