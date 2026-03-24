import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest';
import { renderHook, act, waitFor } from '@testing-library/react';
import type { ReactNode } from 'react';
import { AuthProvider, useAuth } from '../AuthContext';

// Mock del módulo api
vi.mock('../../lib/api', () => ({
  authApi: {
    perfil: vi.fn(),
    login: vi.fn(),
    registrar: vi.fn(),
    logout: vi.fn(),
  },
}));

// Importar después del mock
import { authApi } from '../../lib/api';

const mockedAuthApi = vi.mocked(authApi);

function wrapper({ children }: { children: ReactNode }) {
  return <AuthProvider>{children}</AuthProvider>;
}

function crearUsuarioMock(overrides = {}) {
  return {
    id: '1',
    nombre: 'Felipe',
    apellidos: 'Costales',
    email: 'felipe@test.com',
    urlAvatar: null,
    puntosTotales: 100,
    rachaDias: 5,
    audioHabilitado: true,
    idiomaPreferido: 'es',
    roles: [] as string[],
    esAdmin: false,
    ...overrides,
  };
}

describe('useAuth', () => {
  beforeEach(() => {
    localStorage.clear();
    vi.clearAllMocks();
  });

  afterEach(() => {
    localStorage.clear();
  });

  it('debe lanzar error si se usa fuera de AuthProvider', () => {
    // Silenciamos console.error para evitar ruido en la salida del test
    const consoleSpy = vi.spyOn(console, 'error').mockImplementation(() => {});

    expect(() => {
      renderHook(() => useAuth());
    }).toThrow('useAuth debe usarse dentro de AuthProvider');

    consoleSpy.mockRestore();
  });

  it('debe tener estado inicial sin usuario y cargando true (sin token)', async () => {
    const { result } = renderHook(() => useAuth(), { wrapper });

    // Sin token, cargando se pone false de inmediato en el useEffect
    await waitFor(() => {
      expect(result.current.cargando).toBe(false);
    });

    expect(result.current.usuario).toBeNull();
    expect(result.current.token).toBeNull();
    expect(result.current.estaAutenticado).toBe(false);
  });

  it('debe hacer login correctamente y guardar sesión', async () => {
    const usuario = crearUsuarioMock();
    const respuestaLogin = {
      tokenAcceso: 'jwt-token-test',
      tokenRenovacion: 'refresh-test',
      expiraEnSegundos: 3600,
      usuario,
    };

    mockedAuthApi.login.mockResolvedValue(respuestaLogin);
    mockedAuthApi.perfil.mockResolvedValue(usuario);

    const { result } = renderHook(() => useAuth(), { wrapper });

    await waitFor(() => {
      expect(result.current.cargando).toBe(false);
    });

    await act(async () => {
      await result.current.login({ email: 'felipe@test.com', contrasena: '123456' });
    });

    expect(result.current.usuario).toEqual(usuario);
    expect(result.current.token).toBe('jwt-token-test');
    expect(result.current.estaAutenticado).toBe(true);
    expect(localStorage.getItem('token')).toBe('jwt-token-test');
    expect(localStorage.getItem('refreshToken')).toBe('refresh-test');
  });

  it('debe hacer logout y limpiar el estado', async () => {
    const usuario = crearUsuarioMock();
    const respuestaLogin = {
      tokenAcceso: 'jwt-token-test',
      tokenRenovacion: 'refresh-test',
      expiraEnSegundos: 3600,
      usuario,
    };

    mockedAuthApi.login.mockResolvedValue(respuestaLogin);
    mockedAuthApi.perfil.mockResolvedValue(usuario);
    mockedAuthApi.logout.mockResolvedValue(undefined as never);

    const { result } = renderHook(() => useAuth(), { wrapper });

    await waitFor(() => {
      expect(result.current.cargando).toBe(false);
    });

    // Login primero
    await act(async () => {
      await result.current.login({ email: 'felipe@test.com', contrasena: '123456' });
    });

    expect(result.current.estaAutenticado).toBe(true);

    // Logout
    act(() => {
      result.current.logout();
    });

    expect(result.current.usuario).toBeNull();
    expect(result.current.token).toBeNull();
    expect(result.current.estaAutenticado).toBe(false);
    expect(localStorage.getItem('token')).toBeNull();
    expect(localStorage.getItem('refreshToken')).toBeNull();
  });

  it('debe cargar perfil al iniciar si hay token en localStorage', async () => {
    localStorage.setItem('token', 'existing-token');

    const perfilUsuario = crearUsuarioMock({ puntosTotales: 50, rachaDias: 3 });

    mockedAuthApi.perfil.mockResolvedValue(perfilUsuario);

    const { result } = renderHook(() => useAuth(), { wrapper });

    await waitFor(() => {
      expect(result.current.cargando).toBe(false);
    });

    expect(result.current.usuario).toEqual(perfilUsuario);
    expect(result.current.estaAutenticado).toBe(true);
    expect(mockedAuthApi.perfil).toHaveBeenCalledOnce();
  });

  it('debe limpiar token si el perfil falla (token expirado)', async () => {
    localStorage.setItem('token', 'expired-token');

    mockedAuthApi.perfil.mockRejectedValue(new Error('Unauthorized'));

    const { result } = renderHook(() => useAuth(), { wrapper });

    await waitFor(() => {
      expect(result.current.cargando).toBe(false);
    });

    expect(result.current.usuario).toBeNull();
    expect(result.current.token).toBeNull();
    expect(result.current.estaAutenticado).toBe(false);
    expect(localStorage.getItem('token')).toBeNull();
  });
});
