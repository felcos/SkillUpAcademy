import { Link } from 'react-router-dom';

export default function NotFoundPage() {
  return (
    <div className="flex flex-col items-center justify-center min-h-[70vh] px-4 text-center">
      <h1 className="text-9xl font-extrabold text-[#3498DB]">404</h1>
      <p className="text-2xl font-semibold text-gray-200 mt-4">Página no encontrada</p>
      <p className="text-gray-400 mt-2 max-w-md">
        La página que buscas no existe o fue movida.
      </p>
      <Link
        to="/"
        className="mt-8 px-6 py-3 bg-[#25254A] text-[#3498DB] font-medium rounded-xl border border-white/10 hover:bg-[#2f2f5a] transition-colors"
      >
        Volver al inicio
      </Link>
    </div>
  );
}
