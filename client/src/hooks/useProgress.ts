import { useQuery } from '@tanstack/react-query';
import { progressApi } from '../lib/api';

export function useDashboard() {
  return useQuery({ queryKey: ['dashboard'], queryFn: progressApi.dashboard });
}

export function useLogros() {
  return useQuery({ queryKey: ['logros'], queryFn: progressApi.logros });
}
