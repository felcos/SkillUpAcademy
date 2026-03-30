/** Limpia caracteres markdown para que TTS no los pronuncie. */
export function limpiarParaTts(texto: string): string {
  return texto
    .replace(/#{1,6}\s?/g, '')       // encabezados: # ## ###
    .replace(/\*{1,3}([^*]+)\*{1,3}/g, '$1') // **negrita**, *cursiva*, ***ambas***
    .replace(/_{1,3}([^_]+)_{1,3}/g, '$1')   // __negrita__, _cursiva_
    .replace(/~~([^~]+)~~/g, '$1')   // ~~tachado~~
    .replace(/`{1,3}[^`]*`{1,3}/g, '') // `código` y ```bloques```
    .replace(/^\s*[-*+]\s/gm, '')     // bullets: - * +
    .replace(/^\s*\d+\.\s/gm, '')     // listas numeradas: 1. 2.
    .replace(/\[([^\]]+)\]\([^)]+\)/g, '$1') // [links](url)
    .replace(/[>|]/g, '')             // blockquotes y tablas
    .replace(/\n{2,}/g, '. ')         // dobles saltos → pausa
    .replace(/\n/g, ' ')             // saltos simples → espacio
    .trim();
}
