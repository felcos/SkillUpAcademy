# ============================================================
# Dockerfile para SkillUp Academy
# Multi-stage: frontend (Node) → backend (dotnet) → runtime
# ============================================================

# Stage 0: Compilar frontend React
FROM node:20-alpine AS frontend
WORKDIR /app/client
COPY client/package.json client/package-lock.json ./
RUN npm ci
COPY client/ .
RUN npx vite build

# Stage 1: Restaurar dependencias .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS restore
WORKDIR /src

# Copiar archivos de proyecto primero (para cache de capas Docker)
COPY SkillUpAcademy.slnx .
COPY src/SkillUpAcademy.Api/SkillUpAcademy.Api.csproj src/SkillUpAcademy.Api/
COPY src/SkillUpAcademy.Core/SkillUpAcademy.Core.csproj src/SkillUpAcademy.Core/
COPY src/SkillUpAcademy.Infrastructure/SkillUpAcademy.Infrastructure.csproj src/SkillUpAcademy.Infrastructure/

# Restaurar NuGet packages
RUN dotnet restore src/SkillUpAcademy.Api/SkillUpAcademy.Api.csproj

# Stage 2: Compilar y publicar
FROM restore AS build
WORKDIR /src

# Copiar todo el código fuente
COPY src/ src/

# Compilar en modo Release
RUN dotnet publish src/SkillUpAcademy.Api/SkillUpAcademy.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 3: Imagen de runtime (mínima, sin SDK)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

# Configuración de seguridad: usuario no-root
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

# Copiar la aplicación publicada
COPY --from=build /app/publish .

# Copiar el frontend compilado a wwwroot para servir como SPA
COPY --from=frontend /app/client/dist ./wwwroot/

# Variables de entorno por defecto
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Exponer puerto
EXPOSE 8080

# Cambiar a usuario no-root
USER appuser

# Health check
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:8080/api/v1/health || exit 1

# Punto de entrada
ENTRYPOINT ["dotnet", "SkillUpAcademy.Api.dll"]
