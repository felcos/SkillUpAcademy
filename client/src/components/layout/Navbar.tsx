import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';
import { BookOpen, LayoutDashboard, Trophy, LogOut, User, Shield } from 'lucide-react';

export default function Navbar() {
  const { usuario, estaAutenticado, esAdmin, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <nav className="bg-[#25254A] border-b border-[#3498DB]/20 sticky top-0 z-50">
      <div className="max-w-7xl mx-auto px-4 h-16 flex items-center justify-between">
        <Link to="/" className="flex items-center gap-2 text-xl font-bold">
          <span className="text-2xl">🚀</span>
          <span className="bg-gradient-to-r from-[#3498DB] to-[#9B59B6] bg-clip-text text-transparent">
            SkillUp Academy
          </span>
        </Link>

        {estaAutenticado ? (
          <div className="flex items-center gap-6">
            <Link to="/areas" className="flex items-center gap-1.5 text-gray-300 hover:text-white transition-colors text-sm">
              <BookOpen size={18} />
              <span className="hidden sm:inline">Áreas</span>
            </Link>
            <Link to="/dashboard" className="flex items-center gap-1.5 text-gray-300 hover:text-white transition-colors text-sm">
              <LayoutDashboard size={18} />
              <span className="hidden sm:inline">Dashboard</span>
            </Link>
            <Link to="/logros" className="flex items-center gap-1.5 text-gray-300 hover:text-white transition-colors text-sm">
              <Trophy size={18} />
              <span className="hidden sm:inline">Logros</span>
            </Link>
            {esAdmin && (
              <Link to="/admin" className="flex items-center gap-1.5 text-amber-400 hover:text-amber-300 transition-colors text-sm">
                <Shield size={18} />
                <span className="hidden sm:inline">Admin</span>
              </Link>
            )}

            <div className="flex items-center gap-3 ml-4 pl-4 border-l border-white/10">
              <div className="hidden sm:block text-right">
                <p className="text-sm font-medium">{usuario?.nombre}</p>
                <p className="text-xs text-gray-400">{usuario?.puntosTotales} pts · {usuario?.rachaDias} 🔥</p>
              </div>
              <div className="w-8 h-8 rounded-full bg-gradient-to-br from-[#3498DB] to-[#9B59B6] flex items-center justify-center text-sm font-bold">
                {usuario?.nombre?.[0]}
              </div>
              <button onClick={handleLogout} className="text-gray-400 hover:text-red-400 transition-colors" title="Cerrar sesión">
                <LogOut size={18} />
              </button>
            </div>
          </div>
        ) : (
          <div className="flex items-center gap-3">
            <Link to="/login" className="text-gray-300 hover:text-white transition-colors text-sm px-3 py-1.5">
              Entrar
            </Link>
            <Link to="/registro" className="bg-[#3498DB] hover:bg-[#2980B9] text-white text-sm px-4 py-1.5 rounded-lg transition-colors">
              Registrarse
            </Link>
          </div>
        )}
      </div>
    </nav>
  );
}
