interface AvatarAriaProps {
  size?: number;
  estado?: 'idle' | 'hablando' | 'pensando' | 'saludando' | 'celebrando';
  className?: string;
}

export default function AvatarAria({ size = 120, estado = 'idle', className = '' }: AvatarAriaProps) {
  // ID único para evitar conflictos de gradientes si hay múltiples avatares
  const uid = 'aria';

  return (
    <div className={`inline-block ${className}`} style={{ width: size, height: size }}>
      <style>{`
        /* === PARPADEO (idle) === */
        @keyframes aria-parpadeo {
          0%, 90%, 100% { transform: scaleY(1); }
          95% { transform: scaleY(0.1); }
        }
        .aria-idle .aria-parpado {
          animation: aria-parpadeo 4s ease-in-out infinite;
          transform-origin: center;
        }
        .aria-idle .aria-parpado-der {
          animation: aria-parpadeo 4s ease-in-out infinite;
          animation-delay: 0.05s;
          transform-origin: center;
        }

        /* === RESPIRACIÓN (idle) === */
        @keyframes aria-respiracion {
          0%, 100% { transform: translateY(0); }
          50% { transform: translateY(1.5px); }
        }
        .aria-idle .aria-hombros {
          animation: aria-respiracion 3.5s ease-in-out infinite;
        }

        /* === AURA IDLE === */
        @keyframes aria-aura-idle {
          0%, 100% { opacity: 0.3; }
          50% { opacity: 0.5; }
        }
        .aria-idle .aria-aura {
          animation: aria-aura-idle 3s ease-in-out infinite;
        }

        /* === HABLANDO: boca === */
        @keyframes aria-boca-hablar {
          0%, 100% { transform: scaleY(1); }
          25% { transform: scaleY(1.8); }
          50% { transform: scaleY(0.6); }
          75% { transform: scaleY(1.5); }
        }
        .aria-hablando .aria-boca {
          animation: aria-boca-hablar 0.4s ease-in-out infinite;
          transform-origin: center;
        }

        /* === HABLANDO: aura intensa === */
        @keyframes aria-aura-hablar {
          0%, 100% { opacity: 0.5; r: 56; }
          50% { opacity: 0.8; r: 58; }
        }
        .aria-hablando .aria-aura {
          animation: aria-aura-hablar 0.8s ease-in-out infinite;
        }

        /* === HABLANDO: ojos más abiertos === */
        .aria-hablando .aria-parpado,
        .aria-hablando .aria-parpado-der {
          transform: scaleY(1);
        }

        /* === HABLANDO: respiración === */
        .aria-hablando .aria-hombros {
          animation: aria-respiracion 2s ease-in-out infinite;
        }

        /* === PENSANDO: ojos miran arriba-derecha === */
        @keyframes aria-ojos-pensar {
          0%, 100% { transform: translate(0, 0); }
          20%, 80% { transform: translate(1.5px, -1.5px); }
        }
        .aria-pensando .aria-pupilas {
          animation: aria-ojos-pensar 2.5s ease-in-out infinite;
        }

        /* === PENSANDO: aura pulsa === */
        @keyframes aria-aura-pensar {
          0%, 100% { opacity: 0.3; }
          50% { opacity: 0.7; }
        }
        .aria-pensando .aria-aura {
          animation: aria-aura-pensar 1.5s ease-in-out infinite;
        }

        /* === PENSANDO: puntos suspensivos === */
        @keyframes aria-punto {
          0%, 20%, 100% { opacity: 0; }
          40%, 80% { opacity: 1; }
        }
        .aria-pensando .aria-puntos { opacity: 1; }
        .aria-pensando .aria-punto1 { animation: aria-punto 1.5s ease-in-out infinite; }
        .aria-pensando .aria-punto2 { animation: aria-punto 1.5s ease-in-out infinite 0.3s; }
        .aria-pensando .aria-punto3 { animation: aria-punto 1.5s ease-in-out infinite 0.6s; }

        /* === PENSANDO: respiración === */
        .aria-pensando .aria-hombros {
          animation: aria-respiracion 3.5s ease-in-out infinite;
        }
        .aria-pensando .aria-parpado,
        .aria-pensando .aria-parpado-der {
          animation: aria-parpadeo 4s ease-in-out infinite;
          transform-origin: center;
        }

        /* === SALUDANDO: sonrisa amplia === */
        .aria-saludando .aria-boca-normal { display: none; }
        .aria-saludando .aria-boca-sonrisa { display: block; }

        /* === SALUDANDO: ojos entrecerrados === */
        @keyframes aria-ojos-saludo {
          0%, 100% { transform: scaleY(0.7); }
          50% { transform: scaleY(0.6); }
        }
        .aria-saludando .aria-parpado,
        .aria-saludando .aria-parpado-der {
          animation: aria-ojos-saludo 2s ease-in-out infinite;
          transform-origin: center;
        }

        /* === SALUDANDO: aura === */
        @keyframes aria-aura-saludo {
          0%, 100% { opacity: 0.4; }
          50% { opacity: 0.7; }
        }
        .aria-saludando .aria-aura {
          animation: aria-aura-saludo 1.5s ease-in-out infinite;
        }

        /* === SALUDANDO: respiración === */
        .aria-saludando .aria-hombros {
          animation: aria-respiracion 3s ease-in-out infinite;
        }

        /* === CELEBRANDO: sonrisa amplia === */
        .aria-celebrando .aria-boca-normal { display: none; }
        .aria-celebrando .aria-boca-sonrisa { display: block; }

        /* === CELEBRANDO: ojos brillantes === */
        @keyframes aria-ojos-celebrar {
          0%, 100% { transform: scaleY(0.75); }
          50% { transform: scaleY(0.6); }
        }
        .aria-celebrando .aria-parpado,
        .aria-celebrando .aria-parpado-der {
          animation: aria-ojos-celebrar 1.2s ease-in-out infinite;
          transform-origin: center;
        }

        /* === CELEBRANDO: aura intensa dorada === */
        @keyframes aria-aura-celebrar {
          0%, 100% { opacity: 0.6; r: 56; }
          50% { opacity: 0.9; r: 59; }
        }
        .aria-celebrando .aria-aura {
          animation: aria-aura-celebrar 1s ease-in-out infinite;
        }

        /* === CELEBRANDO: respiración animada === */
        .aria-celebrando .aria-hombros {
          animation: aria-respiracion 2s ease-in-out infinite;
        }

        /* === CELEBRANDO: partículas/confeti === */
        @keyframes aria-confeti-1 {
          0% { transform: translate(0, 0) rotate(0deg); opacity: 1; }
          100% { transform: translate(-12px, -20px) rotate(180deg); opacity: 0; }
        }
        @keyframes aria-confeti-2 {
          0% { transform: translate(0, 0) rotate(0deg); opacity: 1; }
          100% { transform: translate(15px, -18px) rotate(-150deg); opacity: 0; }
        }
        @keyframes aria-confeti-3 {
          0% { transform: translate(0, 0) rotate(0deg); opacity: 1; }
          100% { transform: translate(-8px, -24px) rotate(120deg); opacity: 0; }
        }
        @keyframes aria-confeti-4 {
          0% { transform: translate(0, 0) rotate(0deg); opacity: 1; }
          100% { transform: translate(10px, -22px) rotate(-200deg); opacity: 0; }
        }
        .aria-celebrando .aria-confeti-1 { animation: aria-confeti-1 1.5s ease-out infinite; }
        .aria-celebrando .aria-confeti-2 { animation: aria-confeti-2 1.5s ease-out infinite 0.2s; }
        .aria-celebrando .aria-confeti-3 { animation: aria-confeti-3 1.5s ease-out infinite 0.4s; }
        .aria-celebrando .aria-confeti-4 { animation: aria-confeti-4 1.5s ease-out infinite 0.6s; }
        .aria-celebrando .aria-confeti { opacity: 1; }

        /* === Cabello flotando (siempre sutil) === */
        @keyframes aria-cabello {
          0%, 100% { transform: translateX(0) rotate(0deg); }
          50% { transform: translateX(0.5px) rotate(0.5deg); }
        }
        .aria-cabello-anim {
          animation: aria-cabello 4s ease-in-out infinite;
          transform-origin: top center;
        }
      `}</style>

      <svg
        width={size}
        height={size}
        viewBox="0 0 120 120"
        className={`aria-${estado}`}
        role="img"
        aria-label={`Avatar de Aria - estado: ${estado}`}
      >
        <defs>
          {/* Gradiente del aura */}
          <radialGradient id={`${uid}-aura`} cx="50%" cy="45%" r="50%">
            <stop offset="0%" stopColor="#9B59B6" stopOpacity="0.6" />
            <stop offset="50%" stopColor="#3498DB" stopOpacity="0.3" />
            <stop offset="100%" stopColor="#8E44AD" stopOpacity="0" />
          </radialGradient>

          {/* Gradiente del cabello */}
          <linearGradient id={`${uid}-cabello`} x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#9B59B6" />
            <stop offset="50%" stopColor="#8E44AD" />
            <stop offset="100%" stopColor="#7D3C98" />
          </linearGradient>

          {/* Tono de piel */}
          <linearGradient id={`${uid}-piel`} x1="0%" y1="0%" x2="0%" y2="100%">
            <stop offset="0%" stopColor="#FDDCB5" />
            <stop offset="100%" stopColor="#F5C6A0" />
          </linearGradient>

          {/* Gradiente hombros/ropa */}
          <linearGradient id={`${uid}-ropa`} x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#3498DB" />
            <stop offset="100%" stopColor="#9B59B6" />
          </linearGradient>

          {/* Clip para el contenido dentro del círculo */}
          <clipPath id={`${uid}-clip`}>
            <circle cx="60" cy="60" r="55" />
          </clipPath>
        </defs>

        {/* Aura exterior */}
        <circle className="aria-aura" cx="60" cy="60" r="56" fill={`url(#${uid}-aura)`} opacity="0.3" />

        {/* Fondo circular */}
        <circle cx="60" cy="60" r="55" fill="#1A1A2E" />

        <g clipPath={`url(#${uid}-clip)`}>
          {/* === HOMBROS / CUERPO === */}
          <g className="aria-hombros">
            {/* Cuello */}
            <rect x="51" y="82" width="18" height="14" rx="4" fill={`url(#${uid}-piel)`} />

            {/* Hombros */}
            <ellipse cx="60" cy="108" rx="38" ry="22" fill={`url(#${uid}-ropa)`} />

            {/* Escote en V */}
            <path d="M52 96 L60 108 L68 96" fill="none" stroke="#1A1A2E" strokeWidth="1.5" />
          </g>

          {/* === CABELLO TRASERO (detrás de la cabeza) === */}
          <g className="aria-cabello-anim">
            {/* Masa de cabello detrás */}
            <ellipse cx="60" cy="52" rx="30" ry="32" fill={`url(#${uid}-cabello)`} />
            {/* Mechones laterales largos */}
            <path d="M32 48 Q28 65 30 82 Q32 86 35 80 Q34 65 36 50 Z" fill="#8E44AD" opacity="0.9" />
            <path d="M88 48 Q92 65 90 82 Q88 86 85 80 Q86 65 84 50 Z" fill="#7D3C98" opacity="0.9" />
          </g>

          {/* === CABEZA === */}
          <ellipse cx="60" cy="55" rx="24" ry="27" fill={`url(#${uid}-piel)`} />

          {/* === OREJAS === */}
          <ellipse cx="36.5" cy="56" rx="3.5" ry="5" fill="#F5C6A0" />
          <ellipse cx="83.5" cy="56" rx="3.5" ry="5" fill="#F5C6A0" />

          {/* === CABELLO FRONTAL === */}
          <g className="aria-cabello-anim">
            {/* Flequillo */}
            <path
              d="M36 45 Q40 28 60 25 Q80 28 84 45 Q80 35 70 33 Q60 30 50 33 Q40 35 36 45 Z"
              fill={`url(#${uid}-cabello)`}
            />
            {/* Mechones del flequillo */}
            <path d="M42 42 Q44 33 52 30 Q48 35 45 43 Z" fill="#9B59B6" opacity="0.7" />
            <path d="M55 38 Q58 29 65 28 Q62 32 58 39 Z" fill="#8E44AD" opacity="0.6" />
            {/* Raya lateral */}
            <path d="M50 28 Q55 25 60 25" fill="none" stroke="#7D3C98" strokeWidth="0.8" opacity="0.5" />
          </g>

          {/* === CEJAS === */}
          <path d="M44 44 Q48 41 53 43" fill="none" stroke="#8B6F5E" strokeWidth="1.2" strokeLinecap="round" />
          <path d="M67 43 Q72 41 76 44" fill="none" stroke="#8B6F5E" strokeWidth="1.2" strokeLinecap="round" />

          {/* === OJOS === */}
          <g className="aria-pupilas">
            {/* Ojo izquierdo */}
            <g>
              {/* Blanco del ojo */}
              <ellipse cx="49" cy="50" rx="6" ry="5.5" fill="white" />
              {/* Iris */}
              <circle cx="49.5" cy="50" r="3.2" fill="#5B3A8C" />
              {/* Pupila */}
              <circle cx="49.5" cy="50" r="1.6" fill="#1A1A2E" />
              {/* Brillo */}
              <circle cx="51" cy="48.5" r="1" fill="white" opacity="0.9" />
              {/* Párpado - se anima */}
              <ellipse className="aria-parpado" cx="49" cy="45.5" rx="6.5" ry="3" fill={`url(#${uid}-piel)`} />
            </g>

            {/* Ojo derecho */}
            <g>
              <ellipse cx="71" cy="50" rx="6" ry="5.5" fill="white" />
              <circle cx="70.5" cy="50" r="3.2" fill="#5B3A8C" />
              <circle cx="70.5" cy="50" r="1.6" fill="#1A1A2E" />
              <circle cx="72" cy="48.5" r="1" fill="white" opacity="0.9" />
              <ellipse className="aria-parpado-der" cx="71" cy="45.5" rx="6.5" ry="3" fill={`url(#${uid}-piel)`} />
            </g>
          </g>

          {/* === PESTAÑAS === */}
          <path d="M43 46 Q42 44 41 43" fill="none" stroke="#4A3560" strokeWidth="0.8" strokeLinecap="round" />
          <path d="M55 46 Q56 44 57 43" fill="none" stroke="#4A3560" strokeWidth="0.8" strokeLinecap="round" />
          <path d="M65 46 Q64 44 63 43" fill="none" stroke="#4A3560" strokeWidth="0.8" strokeLinecap="round" />
          <path d="M77 46 Q78 44 79 43" fill="none" stroke="#4A3560" strokeWidth="0.8" strokeLinecap="round" />

          {/* === NARIZ === */}
          <path d="M58 58 Q60 62 62 58" fill="none" stroke="#E8B796" strokeWidth="1" strokeLinecap="round" />

          {/* === BOCA === */}
          {/* Boca normal (se oculta en estado saludando) */}
          <g className="aria-boca-normal">
            <g className="aria-boca">
              {/* Sonrisa suave */}
              <path d="M53 67 Q56 70 60 70 Q64 70 67 67" fill="none" stroke="#D4856A" strokeWidth="1.5" strokeLinecap="round" />
            </g>
          </g>

          {/* Boca sonrisa amplia (solo visible en saludando) */}
          <g className="aria-boca-sonrisa" style={{ display: 'none' }}>
            <path d="M50 66 Q55 74 60 74 Q65 74 70 66" fill="#D4856A" stroke="#C77560" strokeWidth="0.8" />
            <path d="M53 66 Q56 68 60 68 Q64 68 67 66" fill="white" opacity="0.5" />
          </g>

          {/* === MEJILLAS (rubor) === */}
          <ellipse cx="41" cy="60" rx="4" ry="2.5" fill="#F0A0A0" opacity="0.25" />
          <ellipse cx="79" cy="60" rx="4" ry="2.5" fill="#F0A0A0" opacity="0.25" />

          {/* === PUNTOS SUSPENSIVOS (pensando) === */}
          <g className="aria-puntos" opacity="0">
            <circle className="aria-punto1" cx="52" cy="74" r="1.5" fill="#9B59B6" />
            <circle className="aria-punto2" cx="60" cy="74" r="1.5" fill="#9B59B6" />
            <circle className="aria-punto3" cx="68" cy="74" r="1.5" fill="#9B59B6" />
          </g>
        </g>

        {/* Partículas de confeti (solo visibles en celebrando) */}
        <g className="aria-confeti" opacity="0">
          <rect className="aria-confeti-1" x="40" y="25" width="4" height="4" rx="1" fill="#F1C40F" />
          <rect className="aria-confeti-2" x="72" y="28" width="3" height="5" rx="1" fill="#E74C3C" />
          <rect className="aria-confeti-3" x="50" y="20" width="4" height="3" rx="1" fill="#3498DB" />
          <rect className="aria-confeti-4" x="65" y="22" width="3" height="4" rx="1" fill="#2ECC71" />
        </g>

        {/* Borde exterior */}
        <circle cx="60" cy="60" r="55" fill="none" stroke="#9B59B6" strokeWidth="1" opacity="0.3" />
      </svg>
    </div>
  );
}
