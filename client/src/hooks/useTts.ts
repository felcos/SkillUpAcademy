import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { ttsApi, adminTtsApi } from '../lib/api';

// ============ HOOKS USUARIO ============

export function useConfiguracionTts() {
  return useQuery({
    queryKey: ['tts', 'configuracion'],
    queryFn: ttsApi.configuracion,
  });
}

export function useVocesDisponibles() {
  return useQuery({
    queryKey: ['tts', 'voces'],
    queryFn: ttsApi.voces,
  });
}

export function useActualizarPreferenciasTts() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (datos: { vozPreferida?: string; velocidadVoz?: number; proveedorPreferido?: string }) =>
      ttsApi.actualizarPreferencias(datos),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['tts'] });
      queryClient.invalidateQueries({ queryKey: ['perfil'] });
    },
  });
}

// ============ HOOKS ADMIN ============

export function useProveedoresTtsAdmin() {
  return useQuery({
    queryKey: ['admin', 'tts', 'proveedores'],
    queryFn: adminTtsApi.obtenerProveedores,
  });
}

export function useActualizarProveedorTts() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ tipo, datos }: { tipo: string; datos: Parameters<typeof adminTtsApi.actualizarProveedor>[1] }) =>
      adminTtsApi.actualizarProveedor(tipo, datos),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'tts'] });
    },
  });
}

export function useAlternarProveedorTts() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (tipo: string) => adminTtsApi.alternarProveedor(tipo),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'tts'] });
    },
  });
}
