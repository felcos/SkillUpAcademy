import { useMutation } from '@tanstack/react-query';
import { aiApi } from '../lib/api';

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
