# SkillUp Academy — Estructura del Proyecto y Arquitectura

## 1. Estructura de Carpetas

```
SkillUpAcademy/
│
├── SkillUpAcademy.sln
│
├── src/
│   ├── SkillUpAcademy.Api/                    # ASP.NET Core Web API
│   │   ├── Controllers/
│   │   │   ├── AuthController.cs
│   │   │   ├── SkillsController.cs
│   │   │   ├── LevelsController.cs
│   │   │   ├── LessonsController.cs
│   │   │   ├── QuizController.cs
│   │   │   ├── ScenarioController.cs
│   │   │   ├── AiChatController.cs
│   │   │   ├── ProgressController.cs
│   │   │   └── HealthController.cs
│   │   ├── Middleware/
│   │   │   ├── ExceptionHandlingMiddleware.cs
│   │   │   ├── RateLimitingMiddleware.cs
│   │   │   └── SecurityHeadersMiddleware.cs
│   │   ├── Filters/
│   │   │   └── ValidateModelFilter.cs
│   │   ├── Extensions/
│   │   │   ├── ServiceCollectionExtensions.cs
│   │   │   └── ApplicationBuilderExtensions.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── Dockerfile
│   │
│   ├── SkillUpAcademy.Core/                   # Dominio / Lógica de negocio
│   │   ├── Entities/
│   │   │   ├── AppUser.cs
│   │   │   ├── SkillArea.cs
│   │   │   ├── Level.cs
│   │   │   ├── Lesson.cs
│   │   │   ├── QuizQuestion.cs
│   │   │   ├── QuizOption.cs
│   │   │   ├── Scenario.cs
│   │   │   ├── ScenarioChoice.cs
│   │   │   ├── UserProgress.cs
│   │   │   ├── UserQuizAnswer.cs
│   │   │   ├── UserScenarioChoice.cs
│   │   │   ├── AiChatSession.cs
│   │   │   ├── AiChatMessage.cs
│   │   │   ├── Achievement.cs
│   │   │   ├── UserAchievement.cs
│   │   │   └── AbuseLog.cs
│   │   ├── Interfaces/
│   │   │   ├── Repositories/
│   │   │   │   ├── ISkillAreaRepository.cs
│   │   │   │   ├── ILessonRepository.cs
│   │   │   │   ├── IProgressRepository.cs
│   │   │   │   ├── IAiChatRepository.cs
│   │   │   │   └── IAchievementRepository.cs
│   │   │   ├── Services/
│   │   │   │   ├── IAuthService.cs
│   │   │   │   ├── ISkillService.cs
│   │   │   │   ├── ILessonService.cs
│   │   │   │   ├── IQuizService.cs
│   │   │   │   ├── IScenarioService.cs
│   │   │   │   ├── IAiChatService.cs
│   │   │   │   ├── IAiSafetyService.cs
│   │   │   │   ├── ITtsService.cs
│   │   │   │   ├── IProgressService.cs
│   │   │   │   └── IAchievementService.cs
│   │   │   └── IUnitOfWork.cs
│   │   ├── DTOs/
│   │   │   ├── Auth/
│   │   │   │   ├── RegisterRequest.cs
│   │   │   │   ├── LoginRequest.cs
│   │   │   │   ├── LoginResponse.cs
│   │   │   │   ├── RefreshTokenRequest.cs
│   │   │   │   └── UserProfileDto.cs
│   │   │   ├── Skills/
│   │   │   │   ├── SkillAreaDto.cs
│   │   │   │   ├── LevelDto.cs
│   │   │   │   └── LessonDto.cs
│   │   │   ├── Quiz/
│   │   │   │   ├── QuizQuestionDto.cs
│   │   │   │   ├── QuizAnswerRequest.cs
│   │   │   │   ├── QuizAnswerResponse.cs
│   │   │   │   └── QuizResultDto.cs
│   │   │   ├── Scenario/
│   │   │   │   ├── ScenarioDto.cs
│   │   │   │   ├── ScenarioChoiceRequest.cs
│   │   │   │   └── ScenarioChoiceResponse.cs
│   │   │   ├── AI/
│   │   │   │   ├── AiSessionRequest.cs
│   │   │   │   ├── AiMessageRequest.cs
│   │   │   │   ├── AiMessageResponse.cs
│   │   │   │   └── AiSessionDto.cs
│   │   │   └── Progress/
│   │   │       ├── DashboardDto.cs
│   │   │       ├── SkillProgressDto.cs
│   │   │       └── AchievementDto.cs
│   │   ├── Enums/
│   │   │   ├── LessonType.cs          // Theory, Quiz, Scenario, Roleplay
│   │   │   ├── ProgressStatus.cs       // NotStarted, InProgress, Completed
│   │   │   ├── ScenarioResultType.cs   // Positive, Neutral, Negative
│   │   │   ├── AiSessionType.cs        // LessonGuide, Roleplay, FreePractice
│   │   │   ├── ViolationType.cs        // OffTopic, Inappropriate, PromptInjection, RateLimit
│   │   │   └── ActionTaken.cs          // Warned, Blocked, SessionEnded
│   │   └── Exceptions/
│   │       ├── NotFoundException.cs
│   │       ├── UnauthorizedException.cs
│   │       ├── ValidationException.cs
│   │       └── AbuseDetectedException.cs
│   │
│   ├── SkillUpAcademy.Infrastructure/          # Implementaciones / Datos
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs
│   │   │   ├── Configurations/                 # EF Core Fluent API configs
│   │   │   │   ├── AppUserConfiguration.cs
│   │   │   │   ├── SkillAreaConfiguration.cs
│   │   │   │   ├── LevelConfiguration.cs
│   │   │   │   ├── LessonConfiguration.cs
│   │   │   │   ├── QuizQuestionConfiguration.cs
│   │   │   │   ├── ScenarioConfiguration.cs
│   │   │   │   └── ... (una por entidad)
│   │   │   ├── Migrations/
│   │   │   └── Seeders/
│   │   │       ├── DbSeeder.cs                 # Orquestador
│   │   │       ├── SkillAreaSeeder.cs
│   │   │       ├── ComunicacionSeeder.cs
│   │   │       ├── LiderazgoSeeder.cs
│   │   │       ├── TrabajoEnEquipoSeeder.cs
│   │   │       ├── InteligenciaEmocionalSeeder.cs
│   │   │       ├── SmallTalkSeeder.cs
│   │   │       ├── PersuasionSeeder.cs
│   │   │       └── AchievementSeeder.cs
│   │   ├── Repositories/
│   │   │   ├── SkillAreaRepository.cs
│   │   │   ├── LessonRepository.cs
│   │   │   ├── ProgressRepository.cs
│   │   │   ├── AiChatRepository.cs
│   │   │   ├── AchievementRepository.cs
│   │   │   └── UnitOfWork.cs
│   │   └── Services/
│   │       ├── AuthService.cs
│   │       ├── SkillService.cs
│   │       ├── LessonService.cs
│   │       ├── QuizService.cs
│   │       ├── ScenarioService.cs
│   │       ├── AiChatService.cs
│   │       ├── AiSafetyService.cs              # Filtros anti-abuso
│   │       ├── TtsService.cs                   # Text-to-Speech
│   │       ├── ProgressService.cs
│   │       ├── AchievementService.cs
│   │       └── AnthropicClient.cs              # Cliente HTTP para Claude API
│   │
│   └── SkillUpAcademy.Web/                     # Frontend (React o Blazor)
│       ├── public/
│       │   ├── index.html
│       │   └── favicon.ico
│       ├── src/
│       │   ├── api/                            # Clientes API
│       │   │   ├── axiosClient.ts
│       │   │   ├── authApi.ts
│       │   │   ├── skillsApi.ts
│       │   │   ├── lessonsApi.ts
│       │   │   ├── quizApi.ts
│       │   │   ├── scenarioApi.ts
│       │   │   ├── aiChatApi.ts
│       │   │   └── progressApi.ts
│       │   ├── components/
│       │   │   ├── layout/
│       │   │   │   ├── Header.tsx
│       │   │   │   ├── Sidebar.tsx
│       │   │   │   ├── BottomNav.tsx
│       │   │   │   └── Layout.tsx
│       │   │   ├── common/
│       │   │   │   ├── ProgressBar.tsx
│       │   │   │   ├── Badge.tsx
│       │   │   │   ├── Modal.tsx
│       │   │   │   ├── Toast.tsx
│       │   │   │   ├── LoadingSpinner.tsx
│       │   │   │   ├── EmptyState.tsx
│       │   │   │   └── AudioPlayer.tsx
│       │   │   ├── skills/
│       │   │   │   ├── SkillCard.tsx
│       │   │   │   ├── LevelCard.tsx
│       │   │   │   └── LessonCard.tsx
│       │   │   ├── lessons/
│       │   │   │   ├── TheoryView.tsx
│       │   │   │   ├── QuizQuestion.tsx
│       │   │   │   ├── FeedbackBanner.tsx
│       │   │   │   ├── ScenarioCard.tsx
│       │   │   │   └── ScenarioChoice.tsx
│       │   │   ├── ai/
│       │   │   │   ├── ChatBubble.tsx
│       │   │   │   ├── ChatInput.tsx
│       │   │   │   ├── TypingIndicator.tsx
│       │   │   │   └── AiChat.tsx
│       │   │   └── achievements/
│       │   │       ├── AchievementCard.tsx
│       │   │       └── AchievementToast.tsx
│       │   ├── pages/
│       │   │   ├── LandingPage.tsx
│       │   │   ├── LoginPage.tsx
│       │   │   ├── RegisterPage.tsx
│       │   │   ├── DashboardPage.tsx
│       │   │   ├── SkillsPage.tsx
│       │   │   ├── SkillDetailPage.tsx
│       │   │   ├── LessonPage.tsx
│       │   │   ├── PracticePage.tsx
│       │   │   ├── AchievementsPage.tsx
│       │   │   └── ProfilePage.tsx
│       │   ├── hooks/
│       │   │   ├── useAuth.ts
│       │   │   ├── useProgress.ts
│       │   │   └── useAudio.ts
│       │   ├── store/
│       │   │   ├── authStore.ts
│       │   │   └── progressStore.ts
│       │   ├── types/
│       │   │   └── index.ts
│       │   ├── utils/
│       │   │   ├── formatters.ts
│       │   │   └── constants.ts
│       │   ├── styles/
│       │   │   └── globals.css
│       │   ├── App.tsx
│       │   └── main.tsx
│       ├── package.json
│       ├── tsconfig.json
│       ├── tailwind.config.js
│       └── vite.config.ts
│
├── tests/
│   ├── SkillUpAcademy.UnitTests/
│   │   ├── Services/
│   │   │   ├── AuthServiceTests.cs
│   │   │   ├── QuizServiceTests.cs
│   │   │   ├── AiSafetyServiceTests.cs
│   │   │   └── ProgressServiceTests.cs
│   │   └── SkillUpAcademy.UnitTests.csproj
│   └── SkillUpAcademy.IntegrationTests/
│       ├── Controllers/
│       │   ├── AuthControllerTests.cs
│       │   └── SkillsControllerTests.cs
│       └── SkillUpAcademy.IntegrationTests.csproj
│
├── docker-compose.yml
├── .gitignore
├── .editorconfig
└── README.md
```

---

## 2. Patrón Arquitectónico: Clean Architecture

```
┌─────────────────────────────────────────────────┐
│                  API Layer                       │
│           (Controllers, Middleware)              │
├─────────────────────────────────────────────────┤
│              Core / Domain Layer                 │
│     (Entities, Interfaces, DTOs, Enums)         │
├─────────────────────────────────────────────────┤
│           Infrastructure Layer                   │
│  (EF Core, Repositories, Services, External)    │
└─────────────────────────────────────────────────┘
```

### Reglas de dependencia:
- **Api** depende de **Core** e **Infrastructure**
- **Infrastructure** depende de **Core**
- **Core** no depende de nadie (es el centro)

---

## 3. Configuración — appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SkillUpAcademy;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "Jwt": {
    "Secret": "GENERATE_A_STRONG_SECRET_KEY_MIN_32_CHARS",
    "Issuer": "SkillUpAcademy",
    "Audience": "SkillUpAcademy",
    "AccessTokenExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  },
  "Anthropic": {
    "ApiKey": "sk-ant-XXXXXXXX",
    "Model": "claude-sonnet-4-20250514",
    "MaxTokens": 1000,
    "Temperature": 0.7
  },
  "AzureSpeech": {
    "SubscriptionKey": "AZURE_SPEECH_KEY",
    "Region": "westeurope",
    "VoiceName": "es-ES-ElviraNeural"
  },
  "RateLimiting": {
    "GeneralRequestsPerMinute": 100,
    "AiRequestsPerMinute": 20,
    "TtsRequestsPerMinute": 30
  },
  "Security": {
    "MaxLoginAttempts": 5,
    "LockoutMinutes": 15,
    "MaxStrikesPerSession": 3,
    "CooldownAfterFlagMinutes": 5,
    "AiBlockAfterStrikesHours": 1
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

---

## 4. Docker Compose

```yaml
version: '3.8'
services:
  api:
    build:
      context: .
      dockerfile: src/SkillUpAcademy.Api/Dockerfile
    ports:
      - "5000:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=db;Database=SkillUpAcademy;User=sa;Password=YourStr0ng!Pass;TrustServerCertificate=true;
    depends_on:
      - db

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStr0ng!Pass
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql

volumes:
  sqldata:
```

---

## 5. NuGet Packages Necesarios

### SkillUpAcademy.Api
```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" />
<PackageReference Include="Swashbuckle.AspNetCore" />          <!-- Swagger -->
<PackageReference Include="Serilog.AspNetCore" />
<PackageReference Include="AspNetCoreRateLimit" />
<PackageReference Include="FluentValidation.AspNetCore" />
```

### SkillUpAcademy.Core
```xml
<!-- Sin dependencias externas pesadas, solo abstracciones -->
<PackageReference Include="FluentValidation" />
```

### SkillUpAcademy.Infrastructure
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" />
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" />
<PackageReference Include="Anthropic.SDK" />                    <!-- o HttpClient manual -->
<PackageReference Include="Microsoft.CognitiveServices.Speech" />  <!-- Azure TTS -->
```

### Frontend (package.json)
```json
{
  "dependencies": {
    "react": "^18.3",
    "react-dom": "^18.3",
    "react-router-dom": "^6.20",
    "axios": "^1.6",
    "zustand": "^4.4",
    "framer-motion": "^11.0",
    "lucide-react": "^0.383",
    "tailwindcss": "^3.4",
    "@tailwindcss/typography": "^0.5"
  },
  "devDependencies": {
    "typescript": "^5.3",
    "vite": "^5.0",
    "@types/react": "^18.3"
  }
}
```

---

## 6. Instrucciones para Claude Code

### Orden de construcción recomendado:

1. **Crear solución y proyectos** (.sln + 4 proyectos .csproj)
2. **Entidades y Enums** (Core/Entities, Core/Enums)
3. **DbContext y Configuraciones** (Infrastructure/Data)
4. **Migrations** (Initial migration)
5. **Seeders** — CONTENIDO COMPLETO del Nivel 1 de las 6 áreas
6. **Interfaces** (Core/Interfaces)
7. **DTOs** (Core/DTOs)
8. **Repositories** (Infrastructure/Repositories)
9. **Services** — AuthService, SkillService, LessonService, QuizService, ScenarioService
10. **Controllers** — Auth, Skills, Levels, Lessons, Quiz, Scenario, Progress
11. **Middleware** — Exception handling, Rate limiting, Security headers
12. **AI Service** — AnthropicClient, AiChatService, AiSafetyService
13. **TTS Service** — TtsService con fallback a Web Speech API
14. **Frontend** — React project con todas las páginas y componentes
15. **Docker** — Dockerfile + docker-compose
16. **Tests** — Unit + Integration
17. **README** con instrucciones de setup

### Principios a seguir:
- Código limpio, bien comentado en español
- Nombres de variables y clases en inglés (convención .NET)
- Comentarios y documentación en español
- Validación en todos los inputs (FluentValidation)
- Manejo de errores consistente
- Logs estructurados con Serilog
- Async/await en toda la cadena
- Inyección de dependencias en todo
