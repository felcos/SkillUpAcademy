import { createContext, useContext, useState, useEffect, type ReactNode } from 'react';
import { authApi, type PerfilUsuario, type PeticionLogin, type PeticionRegistro, type RespuestaLogin } from '../lib/api';

interface AuthContextType {
  usuario: PerfilUsuario | null;
  token: string | null;
  cargando: boolean;
  login: (datos: PeticionLogin) => Promise<void>;
  registrar: (datos: PeticionRegistro) => Promise<void>;
  logout: () => void;
  estaAutenticado: boolean;
}

const AuthContext = createContext<AuthContextType | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [usuario, setUsuario] = useState<PerfilUsuario | null>(null);
  const [token, setToken] = useState<string | null>(() => localStorage.getItem('token'));
  const [cargando, setCargando] = useState(true);

  useEffect(() => {
    if (token) {
      authApi.perfil()
        .then(setUsuario)
        .catch(() => {
          localStorage.removeItem('token');
          setToken(null);
        })
        .finally(() => setCargando(false));
    } else {
      setCargando(false);
    }
  }, [token]);

  const guardarSesion = (respuesta: RespuestaLogin) => {
    localStorage.setItem('token', respuesta.token);
    localStorage.setItem('refreshToken', respuesta.refreshToken);
    setToken(respuesta.token);
    setUsuario(respuesta.usuario);
  };

  const login = async (datos: PeticionLogin) => {
    const respuesta = await authApi.login(datos);
    guardarSesion(respuesta);
  };

  const registrar = async (datos: PeticionRegistro) => {
    const respuesta = await authApi.registrar(datos);
    guardarSesion(respuesta);
  };

  const logout = () => {
    authApi.logout().catch(() => {});
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    setToken(null);
    setUsuario(null);
  };

  return (
    <AuthContext.Provider value={{
      usuario,
      token,
      cargando,
      login,
      registrar,
      logout,
      estaAutenticado: !!token && !!usuario,
    }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth debe usarse dentro de AuthProvider');
  return context;
}
