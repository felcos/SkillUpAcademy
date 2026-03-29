import { useState, type FormEvent } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { ApiError } from '../lib/api';
import { Eye, EyeOff } from 'lucide-react';

export default function RegisterPage() {
  const [nombre, setNombre] = useState('');
  const [apellidos, setApellidos] = useState('');
  const [email, setEmail] = useState('');
  const [contrasena, setContrasena] = useState('');
  const [confirmarContrasena, setConfirmarContrasena] = useState('');
  const [mostrarContrasena, setMostrarContrasena] = useState(false);
  const [error, setError] = useState('');
  const [cargando, setCargando] = useState(false);
  const { registrar } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError('');

    if (contrasena !== confirmarContrasena) {
      setError('Las contraseñas no coinciden');
      return;
    }

    setCargando(true);
    try {
      await registrar({ nombre, apellidos, email, contrasena, confirmarContrasena });
      navigate('/areas');
    } catch (err) {
      if (err instanceof ApiError) {
        setError(err.message);
      } else {
        setError('Error al registrarse');
      }
    } finally {
      setCargando(false);
    }
  };

  return (
    <div className="min-h-[80vh] flex items-center justify-center px-4 py-8">
      <div className="w-full max-w-md">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold mb-2">Crea tu cuenta</h1>
          <p className="text-gray-400">Empieza a desarrollar tus habilidades profesionales</p>
        </div>

        <form onSubmit={handleSubmit} className="bg-[#25254A] rounded-2xl p-8 space-y-4">
          {error && (
            <div className="bg-red-500/10 border border-red-500/30 text-red-400 px-4 py-3 rounded-lg text-sm animate-slide-up">
              {error}
            </div>
          )}

          <div className="grid grid-cols-2 gap-3">
            <div>
              <label className="block text-sm text-gray-300 mb-1.5">Nombre</label>
              <input type="text" value={nombre} onChange={(e) => setNombre(e.target.value)}
                className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-4 py-2.5 text-white focus:border-[#3498DB] focus:outline-none focus:ring-2 focus:ring-[#3498DB]/20 transition-all" required />
            </div>
            <div>
              <label className="block text-sm text-gray-300 mb-1.5">Apellidos</label>
              <input type="text" value={apellidos} onChange={(e) => setApellidos(e.target.value)}
                className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-4 py-2.5 text-white focus:border-[#3498DB] focus:outline-none focus:ring-2 focus:ring-[#3498DB]/20 transition-all" required />
            </div>
          </div>

          <div>
            <label className="block text-sm text-gray-300 mb-1.5">Email</label>
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)}
              className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-4 py-2.5 text-white focus:border-[#3498DB] focus:outline-none focus:ring-2 focus:ring-[#3498DB]/20 transition-all" required />
          </div>

          <div>
            <label className="block text-sm text-gray-300 mb-1.5">Contraseña</label>
            <div className="relative">
              <input
                type={mostrarContrasena ? 'text' : 'password'}
                value={contrasena}
                onChange={(e) => setContrasena(e.target.value)}
                className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-4 py-2.5 pr-11 text-white focus:border-[#3498DB] focus:outline-none focus:ring-2 focus:ring-[#3498DB]/20 transition-all"
                placeholder="Mínimo 8 caracteres, mayúscula, número y símbolo"
                required minLength={8}
              />
              <button
                type="button"
                onClick={() => setMostrarContrasena(!mostrarContrasena)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-white transition-colors"
                tabIndex={-1}
              >
                {mostrarContrasena ? <EyeOff size={18} /> : <Eye size={18} />}
              </button>
            </div>
            <p className="text-xs text-gray-500 mt-1">Debe incluir mayúscula, minúscula, número y carácter especial</p>
          </div>

          <div>
            <label className="block text-sm text-gray-300 mb-1.5">Confirmar contraseña</label>
            <div className="relative">
              <input
                type={mostrarContrasena ? 'text' : 'password'}
                value={confirmarContrasena}
                onChange={(e) => setConfirmarContrasena(e.target.value)}
                className="w-full bg-[#1A1A2E] border border-white/10 rounded-lg px-4 py-2.5 pr-11 text-white focus:border-[#3498DB] focus:outline-none focus:ring-2 focus:ring-[#3498DB]/20 transition-all"
                required
              />
              <button
                type="button"
                onClick={() => setMostrarContrasena(!mostrarContrasena)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-white transition-colors"
                tabIndex={-1}
              >
                {mostrarContrasena ? <EyeOff size={18} /> : <Eye size={18} />}
              </button>
            </div>
          </div>

          <button type="submit" disabled={cargando}
            className="w-full bg-[#3498DB] hover:bg-[#2980B9] disabled:opacity-50 text-white font-medium py-2.5 rounded-lg transition-all active:scale-[0.97]">
            {cargando ? 'Creando cuenta...' : 'Crear cuenta'}
          </button>

          <p className="text-center text-sm text-gray-400">
            ¿Ya tienes cuenta?{' '}
            <Link to="/login" className="text-[#3498DB] hover:underline">Inicia sesión</Link>
          </p>
        </form>
      </div>
    </div>
  );
}
