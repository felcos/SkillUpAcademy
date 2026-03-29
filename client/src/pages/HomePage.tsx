import { Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { BookOpen, Brain, Target, Sparkles } from 'lucide-react';

const areas = [
  { icono: '💬', titulo: 'Comunicación Efectiva', color: '#0F4C81', desc: 'Domina el arte de transmitir ideas' },
  { icono: '👑', titulo: 'Liderazgo', color: '#8E44AD', desc: 'Inspira y guía a tu equipo' },
  { icono: '🤝', titulo: 'Trabajo en Equipo', color: '#27AE60', desc: 'Colabora y alcanza metas juntos' },
  { icono: '🧠', titulo: 'Inteligencia Emocional', color: '#E74C3C', desc: 'Gestiona emociones con maestría' },
  { icono: '🌐', titulo: 'Networking', color: '#F39C12', desc: 'Construye relaciones estratégicas' },
  { icono: '🎯', titulo: 'Persuasión', color: '#16A085', desc: 'Influye con ética y argumentos' },
  { icono: '⚖️', titulo: 'Resolución de Conflictos', color: '#2C3E50', desc: 'Transforma desacuerdos en oportunidades' },
  { icono: '🔍', titulo: 'Pensamiento Crítico', color: '#8E44AD', desc: 'Analiza, evalúa y decide con rigor' },
  { icono: '⏱️', titulo: 'Gestión del Tiempo', color: '#D35400', desc: 'Prioriza y multiplica tu productividad' },
  { icono: '🤝', titulo: 'Negociación', color: '#1A5276', desc: 'Acuerdos win-win con estrategia' },
  { icono: '🎤', titulo: 'Presentaciones en Público', color: '#C0392B', desc: 'Cautiva audiencias con confianza' },
  { icono: '🌱', titulo: 'Adaptabilidad y Resiliencia', color: '#196F3D', desc: 'Crece ante el cambio y la adversidad' },
];

export default function HomePage() {
  const { estaAutenticado } = useAuth();

  return (
    <div>
      {/* Hero */}
      <section className="relative overflow-hidden py-20 px-4">
        <div className="absolute inset-0 bg-gradient-to-b from-[#3498DB]/10 to-transparent" />
        <div className="max-w-4xl mx-auto text-center relative">
          <div className="inline-flex items-center gap-2 bg-[#3498DB]/10 border border-[#3498DB]/20 rounded-full px-4 py-1.5 text-sm text-[#3498DB] mb-6">
            <Sparkles size={16} />
            Aprende con IA
          </div>
          <h1 className="text-5xl md:text-6xl font-bold mb-6 leading-tight">
            Las habilidades que{' '}
            <span className="bg-gradient-to-r from-[#3498DB] to-[#9B59B6] bg-clip-text text-transparent">
              realmente importan
            </span>
          </h1>
          <p className="text-xl text-gray-400 mb-10 max-w-2xl mx-auto">
            Desarrolla habilidades blandas profesionales con Aria, tu instructora IA.
            Lecciones interactivas, escenarios realistas y feedback personalizado.
          </p>
          <div className="flex justify-center gap-4">
            {estaAutenticado ? (
              <Link to="/areas" className="bg-[#3498DB] hover:bg-[#2980B9] text-white font-medium px-8 py-3 rounded-xl transition-all active:scale-[0.97] text-lg">
                Ir a mis áreas
              </Link>
            ) : (
              <>
                <Link to="/registro" className="bg-[#3498DB] hover:bg-[#2980B9] text-white font-medium px-8 py-3 rounded-xl transition-all active:scale-[0.97] text-lg">
                  Empezar gratis
                </Link>
                <Link to="/login" className="bg-white/5 hover:bg-white/10 border border-white/10 text-white font-medium px-8 py-3 rounded-xl transition-all active:scale-[0.97] text-lg">
                  Iniciar sesión
                </Link>
              </>
            )}
          </div>
        </div>
      </section>

      {/* Áreas */}
      <section className="max-w-6xl mx-auto px-4 py-16">
        <h2 className="text-3xl font-bold text-center mb-12">12 áreas de habilidades profesionales</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          {areas.map((area, index) => (
            <div key={area.titulo} className="bg-[#25254A] rounded-2xl p-6 border border-white/5 hover:border-white/15 transition-all duration-200 hover:-translate-y-1 hover:shadow-lg hover:shadow-[#3498DB]/5 stagger-item" style={{ animationDelay: `${index * 50}ms` }}>
              <div className="text-4xl mb-4">{area.icono}</div>
              <h3 className="text-lg font-semibold mb-2">{area.titulo}</h3>
              <p className="text-gray-400 text-sm">{area.desc}</p>
              <div className="mt-4 h-1 rounded-full bg-white/5">
                <div className="h-full rounded-full" style={{ width: '0%', backgroundColor: area.color }} />
              </div>
            </div>
          ))}
        </div>
      </section>

      {/* Features */}
      <section className="max-w-5xl mx-auto px-4 py-16">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div className="text-center stagger-item" style={{ animationDelay: '0ms' }}>
            <div className="w-14 h-14 rounded-2xl bg-[#3498DB]/10 flex items-center justify-center mx-auto mb-4">
              <BookOpen className="text-[#3498DB]" size={28} />
            </div>
            <h3 className="font-semibold mb-2">Lecciones con Aria</h3>
            <p className="text-gray-400 text-sm">Avatar IA que enseña con presentaciones visuales, diagramas y ejemplos prácticos</p>
          </div>
          <div className="text-center stagger-item" style={{ animationDelay: '60ms' }}>
            <div className="w-14 h-14 rounded-2xl bg-[#9B59B6]/10 flex items-center justify-center mx-auto mb-4">
              <Target className="text-[#9B59B6]" size={28} />
            </div>
            <h3 className="font-semibold mb-2">Escenarios reales</h3>
            <p className="text-gray-400 text-sm">Practica decisiones en situaciones laborales realistas con feedback inmediato</p>
          </div>
          <div className="text-center stagger-item" style={{ animationDelay: '120ms' }}>
            <div className="w-14 h-14 rounded-2xl bg-[#27AE60]/10 flex items-center justify-center mx-auto mb-4">
              <Brain className="text-[#27AE60]" size={28} />
            </div>
            <h3 className="font-semibold mb-2">Chat con IA</h3>
            <p className="text-gray-400 text-sm">Roleplay y consultas libres con Aria para profundizar en cualquier tema</p>
          </div>
        </div>
      </section>
    </div>
  );
}
