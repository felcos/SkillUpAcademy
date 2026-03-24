import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { adminApi, adminIaApi } from '../lib/api';

export function useResumenAdmin() {
  return useQuery({ queryKey: ['admin', 'resumen'], queryFn: adminApi.obtenerResumen });
}

export function useUsuariosAdmin(pagina: number, tamano: number) {
  return useQuery({
    queryKey: ['admin', 'usuarios', pagina, tamano],
    queryFn: () => adminApi.obtenerUsuarios(pagina, tamano),
  });
}

export function useEstadisticasContenido() {
  return useQuery({
    queryKey: ['admin', 'estadisticas-contenido'],
    queryFn: adminApi.obtenerEstadisticasContenido,
  });
}

export function useAlternarBloqueoIA() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => adminApi.alternarBloqueoIA(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'usuarios'] });
    },
  });
}

// ============ ADMIN IA ============

export function useProveedoresIAAdmin() {
  return useQuery({
    queryKey: ['admin', 'ia', 'proveedores'],
    queryFn: adminIaApi.obtenerProveedores,
  });
}

export function useActualizarProveedorIA() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ tipo, datos }: { tipo: string; datos: Parameters<typeof adminIaApi.actualizarProveedor>[1] }) =>
      adminIaApi.actualizarProveedor(tipo, datos),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'ia', 'proveedores'] });
    },
  });
}

export function useAlternarProveedorIA() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (tipo: string) => adminIaApi.alternarProveedor(tipo),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'ia', 'proveedores'] });
    },
  });
}

export function useActivarProveedorIA() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (tipo: string) => adminIaApi.activarProveedor(tipo),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'ia', 'proveedores'] });
    },
  });
}
