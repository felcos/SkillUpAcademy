import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { adminApi } from '../lib/api';

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
