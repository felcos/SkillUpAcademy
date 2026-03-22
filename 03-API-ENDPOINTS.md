# SkillUp Academy — API Endpoints

## Base URL: `/api/v1`

## Autenticación: JWT Bearer Token en header `Authorization: Bearer {token}`

---

## 1. Auth — `/api/v1/auth`

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| POST | `/register` | Registro de nuevo usuario | No |
| POST | `/login` | Login, devuelve JWT + refresh token | No |
| POST | `/refresh` | Renovar JWT con refresh token | No |
| POST | `/logout` | Invalidar refresh token | Sí |
| POST | `/forgot-password` | Enviar email de reset | No |
| POST | `/reset-password` | Cambiar contraseña con token | No |
| GET | `/me` | Perfil del usuario autenticado | Sí |
| PUT | `/me` | Actualizar perfil | Sí |
| PUT | `/me/password` | Cambiar contraseña | Sí |
| DELETE | `/me` | Desactivar cuenta | Sí |

### POST `/register` — Request Body
```json
{
  "email": "usuario@email.com",
  "password": "MinP@ss123",
  "firstName": "Carlos",
  "lastName": "García"
}
```

### POST `/login` — Response
```json
{
  "accessToken": "eyJ...",
  "refreshToken": "abc123...",
  "expiresIn": 3600,
  "user": {
    "id": "guid",
    "email": "usuario@email.com",
    "firstName": "Carlos",
    "totalPoints": 150,
    "avatarUrl": null
  }
}
```

---

## 2. Skill Areas — `/api/v1/skills`

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/` | Listar todas las áreas (con progreso del usuario si autenticado) | Opcional |
| GET | `/{slug}` | Detalle de un área con sus niveles | Opcional |

### GET `/` — Response
```json
[
  {
    "id": 1,
    "slug": "comunicacion",
    "title": "Comunicación",
    "subtitle": "Expresa ideas con claridad e impacto",
    "icon": "💬",
    "colorPrimary": "#0F4C81",
    "userProgress": {
      "completedLessons": 5,
      "totalLessons": 15,
      "percentage": 33,
      "currentLevel": 2
    }
  }
]
```

---

## 3. Levels — `/api/v1/skills/{skillSlug}/levels`

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/` | Listar niveles de un área | Sí |
| GET | `/{levelNumber}` | Detalle de nivel con sus lecciones | Sí |

### GET `/{levelNumber}` — Response
```json
{
  "id": 3,
  "levelNumber": 1,
  "name": "Fundamentos",
  "description": "Escucha activa y claridad verbal",
  "isUnlocked": true,
  "lessons": [
    {
      "id": 10,
      "title": "Escucha Activa",
      "lessonType": "theory",
      "durationMinutes": 5,
      "pointsReward": 10,
      "status": "completed",
      "score": 10
    },
    {
      "id": 11,
      "title": "Test: Escucha Activa",
      "lessonType": "quiz",
      "status": "not_started"
    }
  ]
}
```

---

## 4. Lessons — `/api/v1/lessons`

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/{id}` | Contenido completo de una lección | Sí |
| POST | `/{id}/start` | Marcar lección como iniciada | Sí |
| POST | `/{id}/complete` | Marcar lección como completada | Sí |

### GET `/{id}` — Response (tipo theory)
```json
{
  "id": 10,
  "lessonType": "theory",
  "title": "Escucha Activa",
  "content": "La escucha activa es más que oír...",
  "keyPoints": ["Mantén contacto visual", "Parafrasea", "No interrumpas"],
  "audioScript": "Bienvenido a esta lección sobre escucha activa...",
  "pointsReward": 10,
  "durationMinutes": 5
}
```

---

## 5. Quizzes — `/api/v1/lessons/{lessonId}/quiz`

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/` | Obtener preguntas del quiz (sin respuestas correctas) | Sí |
| POST | `/answer` | Enviar respuesta a una pregunta | Sí |
| POST | `/submit` | Enviar quiz completo y obtener resultado | Sí |

### POST `/answer` — Request
```json
{
  "questionId": 5,
  "selectedOptionId": 12
}
```

### POST `/answer` — Response
```json
{
  "isCorrect": true,
  "feedback": "¡Exacto! Parafrasear demuestra que escuchaste...",
  "correctOptionId": 12,
  "explanation": "La escucha activa requiere participación verbal..."
}
```

### POST `/submit` — Response
```json
{
  "totalQuestions": 4,
  "correctAnswers": 3,
  "score": 75,
  "pointsEarned": 8,
  "passed": true,
  "minimumScore": 60
}
```

---

## 6. Scenarios — `/api/v1/lessons/{lessonId}/scenario`

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/` | Obtener escenario con situación y opciones | Sí |
| POST | `/choose` | Enviar elección del usuario | Sí |

### POST `/choose` — Request
```json
{
  "scenarioId": 3,
  "choiceId": 7
}
```

### POST `/choose` — Response
```json
{
  "resultType": "positive",
  "feedbackText": "Excelente decisión. Agradecer y pedir datos...",
  "pointsAwarded": 10,
  "nextScenario": null,
  "aiNarration": "Has demostrado una gran madurez emocional..."
}
```

---

## 7. AI Chat — `/api/v1/ai`

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| POST | `/session/start` | Crear nueva sesión IA | Sí |
| POST | `/session/{sessionId}/message` | Enviar mensaje al asistente IA | Sí |
| GET | `/session/{sessionId}/history` | Historial de mensajes | Sí |
| POST | `/session/{sessionId}/end` | Cerrar sesión | Sí |

### POST `/session/start` — Request
```json
{
  "sessionType": "roleplay",
  "lessonId": 15
}
```

### POST `/session/{sessionId}/message` — Request
```json
{
  "message": "Hola, necesito practicar una conversación de ascensor"
}
```

### POST `/session/{sessionId}/message` — Response
```json
{
  "reply": "¡Perfecto! Imaginemos que estás en el ascensor con el director financiero...",
  "audioUrl": "/api/v1/ai/audio/{messageId}",
  "wasFlagged": false,
  "tokensUsed": 245,
  "suggestions": [
    "Iniciar con un comentario sobre el clima",
    "Mencionar un logro reciente del equipo",
    "Hacer una pregunta abierta sobre el negocio"
  ]
}
```

### Endpoint de audio
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/audio/{messageId}` | Stream de audio TTS del mensaje | Sí |
| POST | `/tts` | Generar audio de un texto arbitrario (para lecciones) | Sí |

---

## 8. User Progress & Stats — `/api/v1/progress`

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/dashboard` | Resumen general del progreso del usuario | Sí |
| GET | `/skill/{skillSlug}` | Progreso detallado en un área | Sí |
| GET | `/achievements` | Logros desbloqueados y pendientes | Sí |
| GET | `/history` | Historial de actividad reciente | Sí |

### GET `/dashboard` — Response
```json
{
  "totalPoints": 450,
  "completedLessons": 28,
  "totalLessons": 90,
  "currentStreak": 5,
  "skillSummary": [
    {
      "slug": "comunicacion",
      "title": "Comunicación",
      "percentage": 60,
      "currentLevel": 2
    }
  ],
  "recentAchievements": [
    {
      "title": "Primer Paso",
      "icon": "🎯",
      "unlockedAt": "2026-03-20T10:30:00Z"
    }
  ],
  "nextRecommendedLesson": {
    "id": 29,
    "title": "Elevator Pitch de 30 segundos",
    "skillArea": "Small Talk & Networking"
  }
}
```

---

## 9. Health & Admin

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/health` | Health check | No |
| GET | `/health/ready` | Readiness (DB + servicios externos) | No |

---

## Reglas Generales de la API

### Paginación
```
GET /api/v1/resource?page=1&pageSize=20
```
Response incluye:
```json
{
  "data": [...],
  "pagination": {
    "page": 1,
    "pageSize": 20,
    "totalItems": 90,
    "totalPages": 5
  }
}
```

### Errores — Formato estándar
```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "El email ya está registrado",
    "details": [
      { "field": "email", "message": "Este email ya existe en el sistema" }
    ]
  }
}
```

### Códigos HTTP utilizados
- `200` OK
- `201` Created
- `400` Bad Request (validación)
- `401` Unauthorized
- `403` Forbidden
- `404` Not Found
- `429` Too Many Requests (rate limit)
- `500` Internal Server Error

### Rate Limiting
- Endpoints generales: 100 req/min por usuario
- Endpoints IA: 20 req/min por usuario
- Endpoint TTS: 30 req/min por usuario
- Headers de respuesta: `X-RateLimit-Limit`, `X-RateLimit-Remaining`, `X-RateLimit-Reset`
