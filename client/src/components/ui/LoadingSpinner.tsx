export default function LoadingSpinner({ size = 'md' }: { size?: 'sm' | 'md' | 'lg' }) {
  const sizes = { sm: 'w-6 h-6 border-2', md: 'w-10 h-10 border-4', lg: 'w-14 h-14 border-4' };
  return (
    <div className="flex items-center justify-center min-h-[60vh]">
      <div className={`${sizes[size]} border-[#3498DB] border-t-transparent rounded-full animate-spin`} />
    </div>
  );
}
