import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { lessonsApi, quizApi, scenarioApi } from '../lib/api';

export function useLeccion(id: number) {
  return useQuery({
    queryKey: ['leccion', id],
    queryFn: () => lessonsApi.detalle(id),
    enabled: !!id,
  });
}

export function useEscenas(leccionId: number) {
  return useQuery({
    queryKey: ['escenas', leccionId],
    queryFn: () => lessonsApi.escenas(leccionId),
    enabled: !!leccionId,
  });
}

export function useIniciarLeccion() {
  return useMutation({ mutationFn: (id: number) => lessonsApi.iniciar(id) });
}

export function useCompletarLeccion() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => lessonsApi.completar(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['dashboard'] });
      queryClient.invalidateQueries({ queryKey: ['areas'] });
      queryClient.invalidateQueries({ queryKey: ['nivel'] });
    },
  });
}

export function usePreguntas(leccionId: number) {
  return useQuery({
    queryKey: ['quiz', leccionId],
    queryFn: () => quizApi.preguntas(leccionId),
    enabled: !!leccionId,
  });
}

export function useEnviarQuiz(leccionId: number) {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (respuestas: { preguntaId: number; opcionSeleccionadaId: number }[]) =>
      quizApi.enviar(leccionId, respuestas),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['dashboard'] });
      queryClient.invalidateQueries({ queryKey: ['areas'] });
      queryClient.invalidateQueries({ queryKey: ['nivel'] });
    },
  });
}

export function useEscenario(leccionId: number) {
  return useQuery({
    queryKey: ['escenario', leccionId],
    queryFn: () => scenarioApi.obtener(leccionId),
    enabled: !!leccionId,
  });
}

export function useElegirEscenario(leccionId: number) {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ escenarioId, opcionId }: { escenarioId: number; opcionId: number }) =>
      scenarioApi.elegir(leccionId, escenarioId, opcionId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['dashboard'] });
      queryClient.invalidateQueries({ queryKey: ['areas'] });
      queryClient.invalidateQueries({ queryKey: ['nivel'] });
      queryClient.invalidateQueries({ queryKey: ['escenario', leccionId] });
    },
  });
}
