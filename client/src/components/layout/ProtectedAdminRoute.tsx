import { Navigate } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';

export default function ProtectedAdminRoute({ children }: { children: React.ReactNode }) {
  const { estaAutenticado, esAdmin, cargando } = useAuth();

  if (cargando) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-10 h-10 border-4 border-[#3498DB] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  if (!estaAutenticado) {
    return <Navigate to="/login" replace />;
  }

  if (!esAdmin) {
    return <Navigate to="/dashboard" replace />;
  }

  return <>{children}</>;
}
