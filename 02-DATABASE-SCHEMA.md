# SkillUp Academy — Esquema de Base de Datos

## Motor: SQL Server (o PostgreSQL) con Entity Framework Core 8

## Diagrama de Entidades

```
┌──────────────┐     ┌──────────────────┐     ┌──────────────┐
│   AppUser     │────▶│  UserProgress    │◀────│   Lesson     │
│ (Identity)    │     │                  │     │              │
└──────┬───────┘     └──────────────────┘     └──────┬───────┘
       │                                             │
       │             ┌──────────────────┐     ┌──────┴───────┐
       ├────────────▶│  UserQuizAnswer  │◀────│   Quiz       │
       │             └──────────────────┘     │  Question    │
       │                                      └──────────────┘
       │             ┌──────────────────┐     ┌──────────────┐
       ├────────────▶│  UserScenario    │◀────│  Scenario    │
       │             │  Choice          │     │              │
       │             └──────────────────┘     └──────────────┘
       │
       │             ┌──────────────────┐
       ├────────────▶│  AIChatSession   │
       │             └───────┬──────────┘
       │                     │
       │             ┌───────▼──────────┐
       └────────────▶│  AIChatMessage   │
                     └──────────────────┘
```

---

## Tablas Detalladas

### 1. AspNetUsers (extendida por AppUser)

ASP.NET Core Identity genera las tablas base. Extendemos `IdentityUser`:

```csharp
public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }          // max 100
    public string LastName { get; set; }           // max 100
    public string? AvatarUrl { get; set; }         // max 500
    public DateTime CreatedAt { get; set; }        // default: UTC now
    public DateTime LastLoginAt { get; set; }
    public int TotalPoints { get; set; }           // default: 0
    public string PreferredLanguage { get; set; }  // default: "es"
    public bool IsActive { get; set; }             // default: true
    public bool AudioEnabled { get; set; }         // default: true
}
```

### 2. SkillArea

```sql
CREATE TABLE SkillAreas (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    Slug            NVARCHAR(50) NOT NULL UNIQUE,       -- "comunicacion", "liderazgo"...
    Title           NVARCHAR(100) NOT NULL,
    Subtitle        NVARCHAR(200),
    Description     NVARCHAR(1000),
    Icon            NVARCHAR(10),                       -- emoji
    ColorPrimary    NVARCHAR(7),                        -- "#0F4C81"
    ColorAccent     NVARCHAR(7),
    SortOrder       INT NOT NULL DEFAULT 0,
    IsActive        BIT NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

### 3. Level

```sql
CREATE TABLE Levels (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    SkillAreaId     INT NOT NULL REFERENCES SkillAreas(Id),
    LevelNumber     INT NOT NULL,                       -- 1, 2, 3
    Name            NVARCHAR(100) NOT NULL,             -- "Fundamentos", "Práctica", "Dominio"
    Description     NVARCHAR(500),
    UnlockPoints    INT NOT NULL DEFAULT 0,             -- puntos mínimos para desbloquear
    IsActive        BIT NOT NULL DEFAULT 1,
    UNIQUE(SkillAreaId, LevelNumber)
);
```

### 4. Lesson

```sql
CREATE TABLE Lessons (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    LevelId         INT NOT NULL REFERENCES Levels(Id),
    LessonType      NVARCHAR(20) NOT NULL,              -- 'theory', 'quiz', 'scenario', 'roleplay'
    Title           NVARCHAR(200) NOT NULL,
    Description     NVARCHAR(500),
    Content         NVARCHAR(MAX),                      -- contenido principal (markdown/HTML)
    KeyPoints       NVARCHAR(MAX),                      -- JSON array de tips clave
    AudioScript     NVARCHAR(MAX),                      -- texto para TTS de la IA narradora
    PointsReward    INT NOT NULL DEFAULT 10,
    SortOrder       INT NOT NULL DEFAULT 0,
    DurationMinutes INT DEFAULT 5,
    IsActive        BIT NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

### 5. QuizQuestion

```sql
CREATE TABLE QuizQuestions (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    LessonId        INT NOT NULL REFERENCES Lessons(Id),
    QuestionText    NVARCHAR(1000) NOT NULL,
    Explanation     NVARCHAR(1000),                     -- explicación general
    SortOrder       INT NOT NULL DEFAULT 0
);
```

### 6. QuizOption

```sql
CREATE TABLE QuizOptions (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    QuizQuestionId  INT NOT NULL REFERENCES QuizQuestions(Id),
    OptionText      NVARCHAR(500) NOT NULL,
    IsCorrect       BIT NOT NULL DEFAULT 0,
    Feedback        NVARCHAR(500),                      -- feedback específico de esta opción
    SortOrder       INT NOT NULL DEFAULT 0
);
```

### 7. Scenario

```sql
CREATE TABLE Scenarios (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    LessonId        INT NOT NULL REFERENCES Lessons(Id),
    SituationText   NVARCHAR(2000) NOT NULL,            -- descripción de la situación
    Context         NVARCHAR(1000),                     -- contexto adicional
    AudioScript     NVARCHAR(MAX)                       -- narración IA
);
```

### 8. ScenarioChoice

```sql
CREATE TABLE ScenarioChoices (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    ScenarioId      INT NOT NULL REFERENCES Scenarios(Id),
    ChoiceText      NVARCHAR(500) NOT NULL,
    ResultType      NVARCHAR(20) NOT NULL,              -- 'positive', 'neutral', 'negative'
    FeedbackText    NVARCHAR(1000) NOT NULL,
    PointsAwarded   INT NOT NULL DEFAULT 0,             -- positivo=10, neutral=5, negativo=0
    NextScenarioId  INT NULL REFERENCES Scenarios(Id),  -- encadenamiento opcional
    SortOrder       INT NOT NULL DEFAULT 0
);
```

### 9. UserProgress

```sql
CREATE TABLE UserProgress (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES AspNetUsers(Id),
    LessonId        INT NOT NULL REFERENCES Lessons(Id),
    Status          NVARCHAR(20) NOT NULL DEFAULT 'not_started',  -- 'not_started','in_progress','completed'
    Score           INT DEFAULT 0,                       -- puntuación obtenida
    Attempts        INT DEFAULT 0,
    CompletedAt     DATETIME2 NULL,
    LastAccessedAt  DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UNIQUE(UserId, LessonId)
);
```

### 10. UserQuizAnswer

```sql
CREATE TABLE UserQuizAnswers (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES AspNetUsers(Id),
    QuizQuestionId  INT NOT NULL REFERENCES QuizQuestions(Id),
    SelectedOptionId INT NOT NULL REFERENCES QuizOptions(Id),
    IsCorrect       BIT NOT NULL,
    AnsweredAt      DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

### 11. UserScenarioChoice

```sql
CREATE TABLE UserScenarioChoices (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES AspNetUsers(Id),
    ScenarioId      INT NOT NULL REFERENCES Scenarios(Id),
    ChoiceId        INT NOT NULL REFERENCES ScenarioChoices(Id),
    ChosenAt        DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

### 12. AIChatSession

```sql
CREATE TABLE AIChatSessions (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES AspNetUsers(Id),
    LessonId        INT NULL REFERENCES Lessons(Id),    -- NULL = práctica libre
    SessionType     NVARCHAR(20) NOT NULL,              -- 'lesson_guide', 'roleplay', 'free_practice'
    StartedAt       DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    EndedAt         DATETIME2 NULL,
    MessageCount    INT NOT NULL DEFAULT 0,
    WasFlagged      BIT NOT NULL DEFAULT 0,             -- si se detectó abuso
    IsActive        BIT NOT NULL DEFAULT 1
);
```

### 13. AIChatMessage

```sql
CREATE TABLE AIChatMessages (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    SessionId       UNIQUEIDENTIFIER NOT NULL REFERENCES AIChatSessions(Id),
    Role            NVARCHAR(20) NOT NULL,              -- 'user', 'assistant', 'system'
    Content         NVARCHAR(MAX) NOT NULL,
    WasFlagged      BIT NOT NULL DEFAULT 0,
    FlagReason      NVARCHAR(200) NULL,
    TokensUsed      INT DEFAULT 0,
    SentAt          DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

### 14. UserAchievement (gamificación)

```sql
CREATE TABLE Achievements (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    Slug            NVARCHAR(50) NOT NULL UNIQUE,
    Title           NVARCHAR(100) NOT NULL,
    Description     NVARCHAR(300),
    Icon            NVARCHAR(10),
    PointsRequired  INT NOT NULL DEFAULT 0,
    Condition       NVARCHAR(200)                       -- descripción de la condición
);

CREATE TABLE UserAchievements (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES AspNetUsers(Id),
    AchievementId   INT NOT NULL REFERENCES Achievements(Id),
    UnlockedAt      DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UNIQUE(UserId, AchievementId)
);
```

### 15. AbuseLog (seguridad)

```sql
CREATE TABLE AbuseLogs (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES AspNetUsers(Id),
    SessionId       UNIQUEIDENTIFIER NULL REFERENCES AIChatSessions(Id),
    ViolationType   NVARCHAR(50) NOT NULL,              -- 'off_topic', 'inappropriate', 'prompt_injection', 'rate_limit'
    OriginalMessage NVARCHAR(MAX),
    DetectionMethod NVARCHAR(50),                       -- 'keyword', 'ai_classifier', 'rate_limit'
    ActionTaken     NVARCHAR(50),                       -- 'warned', 'blocked', 'session_ended'
    CreatedAt       DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

---

## Índices Recomendados

```sql
CREATE INDEX IX_UserProgress_UserId ON UserProgress(UserId);
CREATE INDEX IX_UserProgress_LessonId ON UserProgress(LessonId);
CREATE INDEX IX_Lessons_LevelId ON Lessons(LevelId);
CREATE INDEX IX_Levels_SkillAreaId ON Levels(SkillAreaId);
CREATE INDEX IX_AIChatMessages_SessionId ON AIChatMessages(SessionId);
CREATE INDEX IX_AIChatSessions_UserId ON AIChatSessions(UserId);
CREATE INDEX IX_AbuseLogs_UserId ON AbuseLogs(UserId);
CREATE INDEX IX_UserQuizAnswers_UserId ON UserQuizAnswers(UserId);
```

## Seed Data

El proyecto debe incluir un `DbSeeder` que pueble:
- Las 6 áreas de skills
- 3 niveles por área (18 niveles)
- Al menos 4 lecciones completas por nivel 1 de cada área (24 lecciones con quizzes y escenarios)
- Al menos 2 lecciones por nivel 2 y 3 (24 lecciones adicionales como estructura base)
- Logros base (10-15 achievements)
- Un usuario admin de prueba
