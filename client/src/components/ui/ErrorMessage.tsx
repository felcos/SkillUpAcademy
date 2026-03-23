export default function ErrorMessage({ mensaje = 'Ha ocurrido un error. Intenta recargar la página.' }: { mensaje?: string }) {
  return (
    <div className="max-w-4xl mx-auto px-4 py-16 text-center">
      <p className="text-red-400">{mensaje}</p>
    </div>
  );
}
