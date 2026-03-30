import { useEffect, useRef, useState, useCallback } from 'react';
import { HubConnectionBuilder, HubConnection, LogLevel } from '@microsoft/signalr';

export interface NotificacionApp {
  id: string;
  tipo: 'logro' | 'leccion' | 'racha';
  titulo: string;
  icono?: string;
  descripcion?: string;
  fecha: string;
}

/**
 * Hook que conecta al hub de SignalR y expone notificaciones en tiempo real.
 * Se conecta automáticamente cuando hay token JWT.
 */
export function useNotificaciones() {
  const [notificaciones, setNotificaciones] = useState<NotificacionApp[]>([]);
  const [conectado, setConectado] = useState(false);
  const connectionRef = useRef<HubConnection | null>(null);

  const descartar = useCallback((id: string) => {
    setNotificaciones((prev) => prev.filter((n) => n.id !== id));
  }, []);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (!token) return;

    const connection = new HubConnectionBuilder()
      .withUrl('/hubs/notificaciones', {
        accessTokenFactory: () => localStorage.getItem('token') ?? '',
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Warning)
      .build();

    connectionRef.current = connection;

    const generarId = () => `${Date.now()}-${Math.random().toString(36).slice(2, 8)}`;

    connection.on('LogroDesbloqueado', (data: { titulo: string; icono: string; descripcion: string }) => {
      setNotificaciones((prev) => [
        ...prev,
        {
          id: generarId(),
          tipo: 'logro',
          titulo: data.titulo,
          icono: data.icono,
          descripcion: data.descripcion,
          fecha: new Date().toISOString(),
        },
      ]);
    });

    connection.on('LeccionCompletada', (data: { tituloLeccion: string; puntosObtenidos: number }) => {
      setNotificaciones((prev) => [
        ...prev,
        {
          id: generarId(),
          tipo: 'leccion',
          titulo: data.tituloLeccion,
          descripcion: `+${data.puntosObtenidos} puntos`,
          fecha: new Date().toISOString(),
        },
      ]);
    });

    connection.on('RachaActualizada', (data: { nuevaRacha: number }) => {
      setNotificaciones((prev) => [
        ...prev,
        {
          id: generarId(),
          tipo: 'racha',
          titulo: `Racha de ${data.nuevaRacha} días`,
          icono: '🔥',
          fecha: new Date().toISOString(),
        },
      ]);
    });

    connection
      .start()
      .then(() => setConectado(true))
      .catch(() => {
        // Silenciar — el usuario puede no estar autenticado
      });

    return () => {
      connection.stop();
      connectionRef.current = null;
      setConectado(false);
    };
  }, []);

  return { notificaciones, conectado, descartar };
}
