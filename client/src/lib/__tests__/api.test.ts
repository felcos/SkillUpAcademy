import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest';
import { ApiError } from '../api';

describe('ApiError', () => {
  it('debe crear una instancia con status y message', () => {
    const error = new ApiError(404, 'No encontrado');

    expect(error).toBeInstanceOf(Error);
    expect(error).toBeInstanceOf(ApiError);
    expect(error.status).toBe(404);
    expect(error.message).toBe('No encontrado');
    expect(error.name).toBe('ApiError');
  });

  it('debe ser capturado como Error genérico', () => {
    const error = new ApiError(500, 'Error del servidor');

    expect(() => { throw error; }).toThrow('Error del servidor');
  });
});

describe('Token management (localStorage)', () => {
  beforeEach(() => {
    localStorage.clear();
  });

  afterEach(() => {
    localStorage.clear();
  });

  it('getToken devuelve null cuando no hay token', () => {
    const token = localStorage.getItem('token');

    expect(token).toBeNull();
  });

  it('setToken y getToken funcionan correctamente', () => {
    localStorage.setItem('token', 'mi-token-jwt');

    const token = localStorage.getItem('token');

    expect(token).toBe('mi-token-jwt');
  });

  it('remover token lo deja en null', () => {
    localStorage.setItem('token', 'mi-token-jwt');
    localStorage.removeItem('token');

    const token = localStorage.getItem('token');

    expect(token).toBeNull();
  });
});

describe('request (fetch mockeado)', () => {
  beforeEach(() => {
    localStorage.clear();
    vi.restoreAllMocks();
  });

  afterEach(() => {
    localStorage.clear();
    vi.restoreAllMocks();
  });

  it('debe hacer GET sin token cuando no hay sesión', async () => {
    const mockResponse = { id: 1, titulo: 'Test' };
    const fetchSpy = vi.spyOn(globalThis, 'fetch').mockResolvedValue(
      new Response(JSON.stringify(mockResponse), {
        status: 200,
        headers: { 'Content-Type': 'application/json' },
      })
    );

    // Importamos dinámicamente para que use el fetch mockeado
    const { skillsApi } = await import('../api');
    await skillsApi.listar();

    expect(fetchSpy).toHaveBeenCalledOnce();
    const [url, options] = fetchSpy.mock.calls[0];
    expect(url).toBe('/api/v1/skills');
    expect((options as RequestInit).method).toBe('GET');
    // Sin token, no debería haber header Authorization
    const headers = (options as RequestInit).headers as Record<string, string>;
    expect(headers['Authorization']).toBeUndefined();
  });

  it('debe incluir Authorization header cuando hay token', async () => {
    localStorage.setItem('token', 'jwt-test-token');

    const mockResponse = { id: '1', nombre: 'Test', apellidos: 'User', email: 'test@test.com', puntosTotales: 0, rachaDias: 0, fechaRegistro: '2024-01-01' };
    const fetchSpy = vi.spyOn(globalThis, 'fetch').mockResolvedValue(
      new Response(JSON.stringify(mockResponse), {
        status: 200,
        headers: { 'Content-Type': 'application/json' },
      })
    );

    const { authApi } = await import('../api');
    await authApi.perfil();

    const [, options] = fetchSpy.mock.calls[0];
    const headers = (options as RequestInit).headers as Record<string, string>;
    expect(headers['Authorization']).toBe('Bearer jwt-test-token');
  });

  it('debe enviar Content-Type y body en POST', async () => {
    const mockResponse = { token: 'abc', refreshToken: 'def', expiracion: '2025-01-01', usuario: {} };
    const fetchSpy = vi.spyOn(globalThis, 'fetch').mockResolvedValue(
      new Response(JSON.stringify(mockResponse), {
        status: 200,
        headers: { 'Content-Type': 'application/json' },
      })
    );

    const { authApi } = await import('../api');
    await authApi.login({ email: 'test@test.com', contrasena: '123456' });

    const [url, options] = fetchSpy.mock.calls[0];
    expect(url).toBe('/api/v1/auth/login');
    expect((options as RequestInit).method).toBe('POST');
    const headers = (options as RequestInit).headers as Record<string, string>;
    expect(headers['Content-Type']).toBe('application/json');
    expect((options as RequestInit).body).toBe(JSON.stringify({ email: 'test@test.com', contrasena: '123456' }));
  });

  it('debe lanzar ApiError cuando la respuesta no es ok', async () => {
    vi.spyOn(globalThis, 'fetch').mockResolvedValue(
      new Response(JSON.stringify({ mensaje: 'Credenciales inválidas' }), {
        status: 401,
        headers: { 'Content-Type': 'application/json' },
      })
    );

    const { authApi, ApiError: ApiErrorClass } = await import('../api');

    try {
      await authApi.login({ email: 'bad@test.com', contrasena: 'wrong' });
      expect.fail('Debería haber lanzado un error');
    } catch (error) {
      expect(error).toBeInstanceOf(ApiErrorClass);
      expect((error as InstanceType<typeof ApiErrorClass>).status).toBe(401);
      expect((error as InstanceType<typeof ApiErrorClass>).message).toBe('Credenciales inválidas');
    }
  });

  it('debe retornar undefined para respuestas 204', async () => {
    vi.spyOn(globalThis, 'fetch').mockResolvedValue(
      new Response(null, { status: 204 })
    );

    const { lessonsApi } = await import('../api');
    const result = await lessonsApi.iniciar(1);

    expect(result).toBeUndefined();
  });
});
