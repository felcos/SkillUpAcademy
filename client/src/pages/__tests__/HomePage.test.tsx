import { describe, it, expect, vi, beforeEach } from 'vitest';
import { screen } from '@testing-library/react';
import { renderWithProviders } from '../../test/utils';
import HomePage from '../HomePage';

// Mock de useAuth para controlar el estado de autenticación
vi.mock('../../contexts/AuthContext', () => ({
  useAuth: vi.fn(),
}));

import { useAuth } from '../../contexts/AuthContext';

const mockedUseAuth = vi.mocked(useAuth);

describe('HomePage', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it('debe renderizar el texto hero principal', () => {
    mockedUseAuth.mockReturnValue({
      usuario: null,
      token: null,
      cargando: false,
      login: vi.fn(),
      registrar: vi.fn(),
      logout: vi.fn(),
      estaAutenticado: false,
      esAdmin: false,
    });

    renderWithProviders(<HomePage />);

    expect(screen.getByText(/las habilidades que/i)).toBeInTheDocument();
    expect(screen.getByText(/realmente importan/i)).toBeInTheDocument();
    expect(screen.getByText(/Aprende con IA/i)).toBeInTheDocument();
  });

  it('debe mostrar "Empezar gratis" cuando no está autenticado', () => {
    mockedUseAuth.mockReturnValue({
      usuario: null,
      token: null,
      cargando: false,
      login: vi.fn(),
      registrar: vi.fn(),
      logout: vi.fn(),
      estaAutenticado: false,
      esAdmin: false,
    });

    renderWithProviders(<HomePage />);

    const linkEmpezar = screen.getByRole('link', { name: /empezar gratis/i });
    expect(linkEmpezar).toBeInTheDocument();
    expect(linkEmpezar).toHaveAttribute('href', '/registro');

    const linkLogin = screen.getByRole('link', { name: /iniciar sesión/i });
    expect(linkLogin).toBeInTheDocument();
    expect(linkLogin).toHaveAttribute('href', '/login');

    expect(screen.queryByRole('link', { name: /ir a mis áreas/i })).not.toBeInTheDocument();
  });

  it('debe mostrar "Ir a mis áreas" cuando está autenticado', () => {
    mockedUseAuth.mockReturnValue({
      usuario: {
        id: '1',
        nombre: 'Felipe',
        apellidos: 'Costales',
        email: 'felipe@test.com',
        urlAvatar: null,
        puntosTotales: 100,
        rachaDias: 5,
        audioHabilitado: true,
        idiomaPreferido: 'es',
        roles: [],
        esAdmin: false,
      },
      token: 'jwt-token',
      cargando: false,
      login: vi.fn(),
      registrar: vi.fn(),
      logout: vi.fn(),
      estaAutenticado: true,
      esAdmin: false,
    });

    renderWithProviders(<HomePage />);

    const linkAreas = screen.getByRole('link', { name: /ir a mis áreas/i });
    expect(linkAreas).toBeInTheDocument();
    expect(linkAreas).toHaveAttribute('href', '/areas');

    expect(screen.queryByRole('link', { name: /empezar gratis/i })).not.toBeInTheDocument();
    expect(screen.queryByRole('link', { name: /iniciar sesión/i })).not.toBeInTheDocument();
  });

  it('debe mostrar las 6 áreas de habilidades', () => {
    mockedUseAuth.mockReturnValue({
      usuario: null,
      token: null,
      cargando: false,
      login: vi.fn(),
      registrar: vi.fn(),
      logout: vi.fn(),
      estaAutenticado: false,
      esAdmin: false,
    });

    renderWithProviders(<HomePage />);

    expect(screen.getByText('Comunicación Efectiva')).toBeInTheDocument();
    expect(screen.getByText('Liderazgo')).toBeInTheDocument();
    expect(screen.getByText('Trabajo en Equipo')).toBeInTheDocument();
    expect(screen.getByText('Inteligencia Emocional')).toBeInTheDocument();
    expect(screen.getByText('Networking')).toBeInTheDocument();
    expect(screen.getByText('Persuasión')).toBeInTheDocument();
  });

  it('debe mostrar las secciones de features', () => {
    mockedUseAuth.mockReturnValue({
      usuario: null,
      token: null,
      cargando: false,
      login: vi.fn(),
      registrar: vi.fn(),
      logout: vi.fn(),
      estaAutenticado: false,
      esAdmin: false,
    });

    renderWithProviders(<HomePage />);

    expect(screen.getByText('Lecciones con Aria')).toBeInTheDocument();
    expect(screen.getByText('Escenarios reales')).toBeInTheDocument();
    expect(screen.getByText('Chat con IA')).toBeInTheDocument();
  });
});
