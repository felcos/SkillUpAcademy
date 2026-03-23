export default function AvatarAria({ size = 56, hablando = false }: { size?: number; hablando?: boolean }) {
  return (
    <svg
      width={size}
      height={size}
      viewBox="0 0 56 56"
      className={hablando ? 'animate-pulse' : ''}
    >
      <defs>
        <linearGradient id="aria-gradient" x1="0%" y1="0%" x2="100%" y2="100%">
          <stop offset="0%" stopColor="#3498DB" />
          <stop offset="100%" stopColor="#9B59B6" />
        </linearGradient>
      </defs>
      <circle cx="28" cy="28" r="28" fill="url(#aria-gradient)" />
      <text
        x="28"
        y="28"
        textAnchor="middle"
        dominantBaseline="central"
        fill="white"
        fontSize="24"
        fontWeight="bold"
        fontFamily="sans-serif"
      >
        A
      </text>
    </svg>
  );
}
