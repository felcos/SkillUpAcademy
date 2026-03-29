# Despliegue — SkillUp Academy

> Documento de referencia para desplegar esta app y cualquier otra en el servidor `rivendel`.
> Última actualización: 2026-03-25

---

## 1. Servidor de Producción — `rivendel`

| Dato | Valor |
|------|-------|
| **Hostname** | `rivendel` |
| **IP pública** | `79.72.56.98` |
| **SO** | Ubuntu 24.04.3 LTS (Noble Numbat) |
| **Arquitectura** | `aarch64` (ARM64) |
| **Usuario SSH** | `ubuntu` |
| **SSH key** | `~/Downloads/ssh-key-2026-01-16.key` |
| **PostgreSQL** | 16 (en el mismo servidor) |
| **Redis** | Activo (redis-server) |
| **Nginx** | Reverse proxy para todas las apps |
| **SSL** | Let's Encrypt (certbot) por dominio |
| **Disco** | 193 GB total, ~17 GB usados (9%) |

---

## 2. Conexión SSH

```bash
# Conexión directa (sin túnel)
ssh -i ~/Downloads/ssh-key-2026-01-16.key ubuntu@79.72.56.98

# Túnel SSH (para reenviar puerto local 10022 → servidor SSH)
ssh -i ~/Downloads/ssh-key-2026-01-16.key -L 0.0.0.0:10022:127.0.0.1:22 ubuntu@79.72.56.98 -N
```

> **Nota:** En Windows/Git Bash la key está en `/c/Users/felipe.costales/Downloads/ssh-key-2026-01-16.key`

---

## 3. Apps en el Servidor (NO TOCAR sin saber qué hacen)

| App | Dominio | Puerto | Servicio systemd | Directorio | BD |
|-----|---------|--------|-------------------|------------|-----|
| **SkillUp Academy** | `skillupacademy.felcos.es` | 5100 | `skillupacademy.service` | `/var/www/skillupacademy/` | `skillup_academy` (user: `skillup_app`) |
| **AgenteNews** | `news.felcos.es` | 5000 | `anews.service` | — | `anews_prod` (user: `anews`) |
| **InfoWeb** | `info.felcos.es` | 5001 | `infoweb.service` | — | `infoweb` (user: `infoweb`) |
| **n8n** | `n8n.websoftware.es` | 5678 | — | — | `n8n_ai` (user: `n8n_user`) |
| **NexusAI** | `nexusai.felcos.es` | — | — | — | — |
| **WebSoftware** | `websoftware.es` | — | — | — | — |
| **Felcos** | `felcos.es` | — | — | — | — |
| **Mnemos** | `mnemos.felcos.es` | — | — | — | — |

### Puertos ocupados conocidos
- **5000** — AgenteNews
- **5001** — InfoWeb
- **5100** — SkillUp Academy
- **5432** — PostgreSQL 16
- **5678** — n8n
- **6379** — Redis

---

## 4. SkillUp Academy — Configuración en Producción

### 4.1 Directorio: `/var/www/skillupacademy/`

```
/var/www/skillupacademy/
├── SkillUpAcademy.Api          # Ejecutable principal (linux-arm64, self-contained)
├── SkillUpAcademy.Api.dll      # DLL principal
├── SkillUpAcademy.Core.dll
├── SkillUpAcademy.Infrastructure.dll
├── appsettings.json            # Config base (viene del publish)
├── appsettings.Production.json # ⚠️ SECRETS REALES — NUNCA sobreescribir con rsync
├── wwwroot/                    # Frontend React compilado
│   ├── assets/
│   ├── fonts/
│   ├── favicon.svg
│   ├── icons.svg
│   └── index.html
└── [DLLs de .NET runtime]      # ~115 MB total (self-contained)
```

### 4.2 Servicio systemd

```ini
# /etc/systemd/system/skillupacademy.service
[Unit]
Description=SkillUp Academy API
After=network.target postgresql.service

[Service]
WorkingDirectory=/var/www/skillupacademy
ExecStart=/var/www/skillupacademy/SkillUpAcademy.Api
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=skillupacademy
User=ubuntu
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5100
Environment=DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

[Install]
WantedBy=multi-user.target
```

### 4.3 Nginx

```nginx
# /etc/nginx/sites-available/skillupacademy
server {
    server_name skillupacademy.felcos.es;

    location / {
        proxy_pass http://localhost:5100;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;

        # SSE streaming support (chat IA)
        proxy_buffering off;
        proxy_read_timeout 300s;
    }

    listen [::]:443 ssl; # managed by Certbot
    listen 443 ssl; # managed by Certbot
    ssl_certificate /etc/letsencrypt/live/skillupacademy.felcos.es/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/skillupacademy.felcos.es/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf;
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;
}

server {
    if ($host = skillupacademy.felcos.es) {
        return 301 https://$host$request_uri;
    }
    listen 80;
    listen [::]:80;
    server_name skillupacademy.felcos.es;
    return 404;
}
```

### 4.4 appsettings.Production.json (en el servidor)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=127.0.0.1;Port=5432;Database=skillup_academy;Username=skillup_app;Password=SkillUpPr0d2026Secure"
  },
  "Jwt": {
    "Secret": "SkillUpAcademy2026ProductionSecretKeyMustBe64CharsLongAtLeastXXXX"
  },
  "Anthropic": { "ApiKey": "" },
  "Tts": { "AzureSpeechKey": "", "AzureSpeechRegion": "" },
  "Cors": {
    "OrigenesPermitidos": ["https://skillupacademy.felcos.es"]
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Warning",
      "Override": {
        "Microsoft.AspNetCore": "Warning",
        "Microsoft.EntityFrameworkCore": "Warning",
        "SkillUpAcademy": "Information"
      }
    },
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "/var/log/skillupacademy/skillup-academy-.log",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 30,
          "fileSizeLimitBytes": 10485760
        }
      }
    ]
  }
}
```

### 4.5 Logs

```bash
# Logs del servicio (systemd/journal)
sudo journalctl -u skillupacademy.service -f

# Logs de Serilog (archivo)
ls /var/log/skillupacademy/
```

---

## 5. Proceso de Despliegue SkillUp Academy

### 5.1 Compilar en local (Windows / Git Bash)

```bash
cd /c/repos/SkillUpAcademy

# 1. Compilar frontend React
cd client && npm ci && npx vite build && cd ..

# 2. Publish .NET self-contained para linux-arm64
dotnet publish src/SkillUpAcademy.Api/SkillUpAcademy.Api.csproj \
  -c Release \
  -r linux-arm64 \
  --self-contained true \
  -o /tmp/skillup-publish

# 3. Copiar frontend compilado a wwwroot
cp -r client/dist/* /tmp/skillup-publish/wwwroot/
```

### 5.2 Subir al servidor

```bash
SSH_KEY=~/Downloads/ssh-key-2026-01-16.key
SERVER=ubuntu@79.72.56.98
APP_DIR=/var/www/skillupacademy

# rsync — SIEMPRE excluir appsettings.Production.json y logs
rsync -avz --progress \
  --exclude 'appsettings.Production.json' \
  --exclude 'logs/' \
  -e "ssh -i $SSH_KEY" \
  /tmp/skillup-publish/ \
  $SERVER:$APP_DIR/
```

> **NUNCA usar `--delete`** sin `--exclude appsettings.Production.json`. En sesión 6 se borró este archivo por error y hubo que recrearlo manualmente.

### 5.3 Aplicar migraciones (si hay nuevas)

```bash
# Generar script SQL en local
cd /c/repos/SkillUpAcademy
dotnet ef migrations script MIGRACION_ANTERIOR MIGRACION_NUEVA \
  --project src/SkillUpAcademy.Infrastructure \
  --startup-project src/SkillUpAcademy.Api \
  -o /tmp/migration.sql

# Copiar al servidor
scp -i $SSH_KEY /tmp/migration.sql $SERVER:/tmp/

# Ejecutar en el servidor
ssh -i $SSH_KEY $SERVER 'sudo -u postgres psql -d skillup_academy -f /tmp/migration.sql'
```

### 5.4 Re-seed (si cambió contenido educativo)

```bash
# El seeder solo corre si las tablas están vacías.
# Para forzar re-seed, truncar las tablas de contenido:
ssh -i $SSH_KEY $SERVER "sudo -u postgres psql -d skillup_academy -c \"
  TRUNCATE escenas_leccion, recursos_visuales, opciones_escenario, escenarios,
           opciones_quiz, preguntas_quiz, lecciones, niveles, areas_habilidad,
           proveedores_ia, proveedores_tts CASCADE;
\""
# Al reiniciar la app, el seeder recreará todo.
# ⚠️ Esto NO borra usuarios, progreso, ni chat — solo contenido educativo.
```

### 5.5 Reiniciar y verificar

```bash
# Parar servicio actual
ssh -i $SSH_KEY $SERVER 'sudo systemctl stop skillupacademy.service'

# Dar permisos de ejecución (por si acaso)
ssh -i $SSH_KEY $SERVER 'chmod +x /var/www/skillupacademy/SkillUpAcademy.Api'

# Arrancar
ssh -i $SSH_KEY $SERVER 'sudo systemctl start skillupacademy.service'

# Verificar estado
ssh -i $SSH_KEY $SERVER 'systemctl status skillupacademy.service --no-pager'

# Health check
curl -s https://skillupacademy.felcos.es/api/v1/health

# Verificar SPA
curl -s -o /dev/null -w "%{http_code}" https://skillupacademy.felcos.es/
```

---

## 6. Base de Datos PostgreSQL

| BD | Owner | Descripción |
|----|-------|-------------|
| `skillup_academy` | `skillup_app` | SkillUp Academy |
| `anews_prod` | `anews` | AgenteNews |
| `infoweb` | `infoweb` | InfoWeb |
| `n8n_ai` | `n8n_user` | n8n workflows |

### Backup

```bash
# Backup de skillup_academy
ssh -i $SSH_KEY $SERVER 'sudo -u postgres pg_dump skillup_academy > /tmp/skillup_backup_$(date +%Y%m%d).sql'

# Descargar backup
scp -i $SSH_KEY $SERVER:/tmp/skillup_backup_*.sql /tmp/
```

---

## 7. Checklist de Despliegue

- [ ] Tests pasan en local (`dotnet test` + `cd client && npm test`)
- [ ] Código pusheado a master
- [ ] Backup de la BD de producción
- [ ] Compilar frontend (`npm ci && npx vite build`)
- [ ] Publish .NET (`dotnet publish -r linux-arm64 --self-contained`)
- [ ] Copiar wwwroot al publish
- [ ] rsync al servidor (con `--exclude appsettings.Production.json`)
- [ ] Aplicar migraciones pendientes (si las hay)
- [ ] Re-seed (si cambió contenido educativo)
- [ ] Reiniciar servicio systemd
- [ ] Verificar health check (`/api/v1/health`)
- [ ] Verificar SPA carga (200 en `/`)
- [ ] Verificar logs (`journalctl -u skillupacademy.service -f`)

---

## 8. Errores Comunes y Soluciones

| Error | Causa | Solución |
|-------|-------|----------|
| `Address already in use` (port 5100) | Proceso zombie del deploy anterior | `sudo fuser -k 5100/tcp` y reiniciar servicio |
| `rsync --delete` borró appsettings.Production.json | Flag `--delete` sin excludes | NUNCA usar `--delete`. Si se borró, recrear manualmente |
| PostgreSQL password con `!` falla | Npgsql no escapa caracteres especiales | Usar contraseña alfanumérica |
| App no arranca tras deploy | Sin permisos de ejecución | `chmod +x SkillUpAcademy.Api` |
| 502 Bad Gateway en Nginx | App no corriendo o puerto mal | `systemctl status skillupacademy.service` |
| Seed no se ejecuta | BD ya tiene datos | Truncar tablas de contenido (ver sección 5.4) |
| `activating (auto-restart)` en loop | Puerto ocupado o crash al iniciar | Ver `journalctl -u skillupacademy.service -n 50` |

---

## 9. Migraciones — Historial

| Migración | Estado Prod | Descripción |
|-----------|-------------|-------------|
| Todas hasta `AgregarEstaBloqueadoIA` | ✅ Aplicada | Tablas base + bloqueo IA |
| `20260324232236_AgregarProveedoresIA` | ⚠️ Pendiente | Tabla `proveedores_ia` |
