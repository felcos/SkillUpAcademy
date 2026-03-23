import { useQuery } from '@tanstack/react-query';
import { skillsApi } from '../lib/api';

export function useAreas() {
  return useQuery({ queryKey: ['areas'], queryFn: skillsApi.listar });
}

export function useAreaDetalle(slug: string | undefined) {
  return useQuery({
    queryKey: ['area', slug],
    queryFn: () => skillsApi.detalle(slug!),
    enabled: !!slug,
  });
}

export function useNivel(slug: string | undefined, numero: number) {
  return useQuery({
    queryKey: ['nivel', slug, numero],
    queryFn: () => skillsApi.nivel(slug!, numero),
    enabled: !!slug,
  });
}
