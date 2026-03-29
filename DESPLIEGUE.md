# Despliegue — Servidor Rivendel

> Documento de referencia para desplegar cualquier app en el servidor `rivendel`.
> Ultima actualizacion: 2026-03-26
>
> **IMPORTANTE:** Despues de cada despliegue, actualizar este fichero Y la copia en el servidor:
> `scp -i ~/.ssh/deploy_key DESPLIEGUE.md ubuntu@79.72.56.98:/home/ubuntu/DESPLIEGUE.md`

---

## 1. Servidor de Produccion — `rivendel`

| Dato | Valor |
|------|-------|
| **Hostname** | `rivendel` |
| **IP publica** | `79.72.56.98` |
| **SO** | Ubuntu 24.04.3 LTS (Noble Numbat) |
| **Arquitectura** | `aarch64` (ARM64) |
| **Usuario SSH** | `ubuntu` |
| **SSH key (Windows/Git Bash)** | `~/.ssh/deploy_key` |
| **PostgreSQL** | 16 (local) |
| **Redis** | Activo (redis-server) |
| **Nginx** | Reverse proxy para todas las apps |
| **SSL** | Let's Encrypt (certbot) por dominio |
| **Disco** | 193 GB total, ~17 GB usados (9%) |

---

## 2. Conexion SSH

```bash
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98
```

---

## 3. Apps Desplegadas

| App | Dominio | Puerto | Servicio systemd | Directorio | BD PostgreSQL | User BD |
|-----|---------|--------|-------------------|------------|---------------|---------|
| **WakeUp** | `wakeup.felcos.es` | 5200 | `wakeup.service` | `/var/www/wakeup/` | `wakeup_db` | `wakeup_app` |
| **SkillUp Academy** | `skillupacademy.felcos.es` | 5100 | `skillupacademy.service` | `/var/www/skillupacademy/` | `skillup_academy` | `skillup_app` |
| **AgenteNews** | `news.felcos.es` | 5000 | `anews.service` | — | `anews_prod` | `anews` |
| **InfoWeb** | `info.felcos.es` | 5001 | `infoweb.service` | — | `infoweb` | `infoweb` |
| **NexusAI** | `nexusai.felcos.es` | — | — | `/var/www/nexusai-felcos/` | — | — |
| **MNEMOS** | `mnemos.felcos.es` | — | — | `/var/www/mnemos-felcos/` | — | — |
| **Felcos (web principal)** | `felcos.es` | — | — | `/var/www/felcos/` | — | — |
| **n8n** | `n8n.felcos.es` | 5678 | — | — | `n8n_ai` | `n8n_user` |

### Puertos ocupados

| Puerto | App |
|--------|-----|
| 5000 | AgenteNews |
| 5001 | InfoWeb |
| 5100 | SkillUp Academy |
| 5200 | WakeUp |
| 5432 | PostgreSQL 16 |
| 5678 | n8n |
| 6379 | Redis |

### Sitios estaticos (solo HTML, sin servicio systemd)

- `felcos.es` → `/var/www/felcos/` (index.html + proyectos/)
- `mnemos.felcos.es` → `/var/www/mnemos-felcos/` (presentacion/docs)
- `nexusai.felcos.es` → `/var/www/nexusai-felcos/` (landing page)

### Estructura de felcos.es

```
/var/www/felcos/
  index.html                     # Web principal
  proyectos/
    mnemos.html                  # Descripcion de MNEMOS
    nexus.html                   # Descripcion de NexusAI
    wakeup.html                  # Descripcion de WakeUp
```

---

## 4. WakeUp — Despliegue

### 4.1 Stack

- .NET 10, Clean Architecture + CQRS, ASP.NET Identity
- PostgreSQL (wakeup_db), SignalR, Razor Views
- Self-contained publish para linux-arm64

### 4.2 Directorio: `/var/www/wakeup/`

```
/var/www/wakeup/
├── WakeUp.Web                   # Ejecutable principal
├── appsettings.json
├── appsettings.Production.json  # SECRETS — NUNCA sobreescribir
├── wwwroot/
│   ├── css/site.css
│   ├── js/site.js
│   └── lib/
│       ├── lightweight-charts/  # TradingView v4.2.0
│       └── microsoft/signalr/
└── [DLLs de .NET runtime]
```

### 4.3 Servicio systemd

```ini
# /etc/systemd/system/wakeup.service
[Unit]
Description=WakeUp Trading Platform
After=network.target postgresql.service

[Service]
WorkingDirectory=/var/www/wakeup
ExecStart=/var/www/wakeup/WakeUp.Web
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=wakeup
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5200

[Install]
WantedBy=multi-user.target
```

### 4.4 Nginx

```nginx
# /etc/nginx/sites-enabled/wakeup
server {
    server_name wakeup.felcos.es;
    location / {
        proxy_pass http://localhost:5200;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection $http_connection;
        proxy_set_header Host $host;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }
    # SSL managed by Certbot
}
```

### 4.5 Compilar y desplegar

```bash
# 1. Compilar en local
cd C:/repos/wakeup
dotnet test
dotnet publish src/WakeUp.Web -c Release -r linux-arm64 --self-contained -o ./publish

# 2. Subir al servidor
scp -i ~/.ssh/deploy_key -r ./publish/* ubuntu@79.72.56.98:/tmp/wakeup-deploy/

# 3. Desplegar en el servidor (PROTEGER appsettings.Production.json)
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "
  sudo systemctl stop wakeup &&
  sudo cp /var/www/wakeup/appsettings.Production.json /tmp/appsettings.Production.json.bak &&
  sudo cp -r /tmp/wakeup-deploy/* /var/www/wakeup/ &&
  sudo cp /tmp/appsettings.Production.json.bak /var/www/wakeup/appsettings.Production.json &&
  sudo chown -R www-data:www-data /var/www/wakeup &&
  sudo chmod +x /var/www/wakeup/WakeUp.Web &&
  sudo systemctl start wakeup
"

# 4. Verificar
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "
  systemctl status wakeup --no-pager &&
  curl -s -o /dev/null -w '%{http_code}' http://localhost:5200/health/live
"
```

### 4.6 Migraciones EF Core

```bash
# Generar script SQL idempotente
cd C:/repos/wakeup/src/WakeUp.Infraestructura
dotnet ef migrations script --startup-project ../WakeUp.Web --idempotent -o /tmp/wakeup-migration.sql

# Subir y aplicar
scp -i ~/.ssh/deploy_key /tmp/wakeup-migration.sql ubuntu@79.72.56.98:/tmp/
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "sudo -u postgres psql -d wakeup_db -f /tmp/wakeup-migration.sql"
```

### 4.7 Migraciones aplicadas

| Migracion | Estado | Descripcion |
|-----------|--------|-------------|
| `20260326074425_Inicial` | Aplicada | Tablas base (10 tablas) |
| `20260326084008_AgregarIdentity` | Aplicada | Tablas ASP.NET Identity (roles, claims, logins, tokens) |
| `20260326XXXXXX_AgregarProveedoresIAYOrdenesReales` | Aplicada | Tablas proveedores_ia, configuraciones_ia_usuario, campo ParDeTrading en operaciones |

### 4.8 BD: 19 tablas

`usuarios`, `roles`, `usuarios_roles`, `usuarios_claims`, `usuarios_logins`, `usuarios_tokens`, `roles_claims`, `conexiones_exchange`, `activos`, `balances_de_activo`, `operaciones`, `ordenes_abiertas`, `estrategias`, `resultados_backtesting`, `alertas`, `velas_historicas`, `proveedores_ia`, `configuraciones_ia_usuario`, `__ef_migrations_history`

---

## 5. SkillUp Academy — Despliegue

### 5.1 Compilar y desplegar

```bash
cd C:/repos/SkillUpAcademy

# Frontend React
cd client && npm ci && npx vite build && cd ..

# Publish .NET
dotnet publish src/SkillUpAcademy.Api/SkillUpAcademy.Api.csproj \
  -c Release -r linux-arm64 --self-contained -o /tmp/skillup-publish

# Copiar frontend
cp -r client/dist/* /tmp/skillup-publish/wwwroot/

# Subir (NUNCA --delete sin --exclude appsettings.Production.json)
rsync -avz --progress \
  --exclude 'appsettings.Production.json' \
  --exclude 'logs/' \
  -e "ssh -i ~/.ssh/deploy_key" \
  /tmp/skillup-publish/ \
  ubuntu@79.72.56.98:/var/www/skillupacademy/

# Reiniciar
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "
  sudo systemctl stop skillupacademy &&
  chmod +x /var/www/skillupacademy/SkillUpAcademy.Api &&
  sudo systemctl start skillupacademy
"
```

### 5.2 Re-seed (si cambio contenido educativo)

```bash
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "sudo -u postgres psql -d skillup_academy -c \"
  TRUNCATE escenas_leccion, recursos_visuales, opciones_escenario, escenarios,
           opciones_quiz, preguntas_quiz, lecciones, niveles, areas_habilidad,
           proveedores_ia, proveedores_tts CASCADE;
\""
# Al reiniciar la app, el seeder recrea todo. NO borra usuarios ni progreso.
```

---

## 6. felcos.es — Web Principal (estatica)

```bash
# Editar localmente y subir
scp -i ~/.ssh/deploy_key index.html ubuntu@79.72.56.98:/tmp/
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "
  sudo cp /tmp/index.html /var/www/felcos/index.html &&
  sudo chown www-data:www-data /var/www/felcos/index.html
"

# Para paginas de proyectos:
scp -i ~/.ssh/deploy_key archivo.html ubuntu@79.72.56.98:/tmp/
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "
  sudo cp /tmp/archivo.html /var/www/felcos/proyectos/ &&
  sudo chown www-data:www-data /var/www/felcos/proyectos/archivo.html
"
```

---

## 7. Base de Datos PostgreSQL

| BD | Owner | App |
|----|-------|-----|
| `wakeup_db` | `wakeup_app` | WakeUp |
| `skillup_academy` | `skillup_app` | SkillUp Academy |
| `anews_prod` | `anews` | AgenteNews |
| `infoweb` | `infoweb` | InfoWeb |
| `n8n_ai` | `n8n_user` | n8n |

### Backup

```bash
# Backup de una BD
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 \
  "sudo -u postgres pg_dump NOMBRE_BD > /tmp/backup_$(date +%Y%m%d).sql"

# Descargar
scp -i ~/.ssh/deploy_key ubuntu@79.72.56.98:/tmp/backup_*.sql ./
```

---

## 8. Comandos Utiles

```bash
# Ver logs de cualquier servicio
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "sudo journalctl -u SERVICIO -f -n 50"

# Ver estado de todos los servicios
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "systemctl list-units --type=service --state=running | grep -E 'wakeup|skillup|anews|infoweb'"

# Ver espacio en disco
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "df -h /"

# Listar BDs
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "sudo -u postgres psql -c '\l'"

# Ver configs nginx activas
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "ls /etc/nginx/sites-enabled/"

# Renovar SSL
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "sudo certbot renew"

# Matar proceso en un puerto
ssh -i ~/.ssh/deploy_key ubuntu@79.72.56.98 "sudo fuser -k PUERTO/tcp"
```

---

## 9. Errores Comunes

| Error | Causa | Solucion |
|-------|-------|----------|
| `Address already in use` | Proceso zombie | `sudo fuser -k PUERTO/tcp` y reiniciar servicio |
| `rsync --delete` borro appsettings.Production.json | Flag `--delete` sin excludes | NUNCA usar `--delete`. Recrear manualmente si se borro |
| App no arranca tras deploy | Sin permisos | `chmod +x EJECUTABLE` |
| 502 Bad Gateway en Nginx | App no corriendo | `systemctl status SERVICIO` |
| PostgreSQL password con `!` falla | Npgsql no escapa especiales | Usar contrasena alfanumerica |
| `activating (auto-restart)` en loop | Puerto ocupado o crash | `journalctl -u SERVICIO -n 50` |

---

## 10. Checklist General de Despliegue

- [ ] Tests pasan en local
- [ ] Backup de la BD de produccion
- [ ] Publish para linux-arm64 --self-contained
- [ ] Subir al servidor (SIN sobreescribir appsettings.Production.json)
- [ ] Aplicar migraciones pendientes (si las hay)
- [ ] Reiniciar servicio systemd
- [ ] Verificar health check / que responda 200
- [ ] Verificar logs sin errores
- [ ] Actualizar este DESPLIEGUE.md y subirlo al servidor
