import { useMutation } from '@tanstack/react-query';
import { aiApi, type EventoStreamChat } from '../lib/api';

export function useIniciarSesionIA() {
  return useMutation({
    mutationFn: ({ tipo, leccionId }: { tipo: string; leccionId?: number }) =>
      aiApi.iniciarSesion(tipo, leccionId),
  });
}

export function useEnviarMensajeIA(sesionId: string | null) {
  return useMutation({
    mutationFn: (mensaje: string) => aiApi.enviarMensaje(sesionId!, mensaje),
  });
}

/** Hook para enviar mensaje con streaming SSE. */
export function useEnviarMensajeStreamIA(sesionId: string | null) {
  return useMutation({
    mutationFn: ({ mensaje, onEvento, signal }: {
      mensaje: string;
      onEvento: (evento: EventoStreamChat) => void;
      signal?: AbortSignal;
    }) => aiApi.enviarMensajeStream(sesionId!, mensaje, onEvento, signal),
  });
}
