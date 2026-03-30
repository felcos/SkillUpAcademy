import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';
import NotificacionToast from '../NotificacionToast';
import { useNotificaciones } from '../../hooks/useNotificaciones';

export default function Layout() {
  const { notificaciones, descartar } = useNotificaciones();

  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      <main className="flex-1">
        <Outlet />
      </main>
      <footer className="bg-[#25254A] border-t border-white/5 py-6 text-center text-gray-500 text-sm">
        <p>SkillUp Academy &copy; {new Date().getFullYear()} — Aprende habilidades que importan</p>
      </footer>
      <NotificacionToast notificaciones={notificaciones} onDescartar={descartar} />
    </div>
  );
}
