import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';

export default function Layout() {
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      <main className="flex-1">
        <Outlet />
      </main>
      <footer className="bg-[#25254A] border-t border-white/5 py-6 text-center text-gray-500 text-sm">
        <p>SkillUp Academy &copy; {new Date().getFullYear()} — Aprende habilidades que importan</p>
      </footer>
    </div>
  );
}
