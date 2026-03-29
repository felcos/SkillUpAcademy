/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        primary: {
          50: '#EBF5FF',
          100: '#D6EBFF',
          200: '#ADD6FF',
          300: '#85C2FF',
          400: '#5CADFF',
          500: '#0F4C81',
          600: '#0D3F6B',
          700: '#0A3256',
          800: '#082640',
          900: '#05192B',
        },
        accent: '#3498DB',
        surface: {
          DEFAULT: '#1A1A2E',
          light: '#25254A',
          lighter: '#2D2D5E',
        },
      },
      fontFamily: {
        sans: ['Inter', 'system-ui', 'sans-serif'],
      },
      animation: {
        'fade-in': 'fadeIn 0.3s cubic-bezier(0.23, 1, 0.32, 1)',
        'slide-left': 'slideLeft 0.3s cubic-bezier(0.23, 1, 0.32, 1)',
        'slide-up': 'slideUp 0.3s cubic-bezier(0.23, 1, 0.32, 1)',
        'zoom-in': 'zoomIn 0.25s cubic-bezier(0.23, 1, 0.32, 1)',
        'pulse-slow': 'pulse 3s infinite',
        'spin-fast': 'spin 0.6s linear infinite',
      },
      keyframes: {
        fadeIn: {
          '0%': { opacity: '0' },
          '100%': { opacity: '1' },
        },
        slideLeft: {
          '0%': { transform: 'translateX(30px)', opacity: '0' },
          '100%': { transform: 'translateX(0)', opacity: '1' },
        },
        slideUp: {
          '0%': { transform: 'translateY(12px)', opacity: '0' },
          '100%': { transform: 'translateY(0)', opacity: '1' },
        },
        zoomIn: {
          '0%': { transform: 'scale(0.95)', opacity: '0' },
          '100%': { transform: 'scale(1)', opacity: '1' },
        },
      },
    },
  },
  plugins: [],
};
