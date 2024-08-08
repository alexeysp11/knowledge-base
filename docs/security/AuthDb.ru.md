# Авторизация с использование базы данных

SQL-код для создания таблиц для авторизации и управления доступами внутри приложения:
```SQL
-- Таблица пользователей.
CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(255) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL, -- Хешированный пароль.
    Email VARCHAR(255),
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedAt TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
    UpdatedAt TIMESTAMP WITHOUT TIME ZONE
);

-- Таблица приложений.
CREATE TABLE Applications (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) UNIQUE NOT NULL,
    Description TEXT,
    CreatedAt TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
    UpdatedAt TIMESTAMP WITHOUT TIME ZONE
);

-- Таблица ролей.
CREATE TABLE Roles (
    Id SERIAL PRIMARY KEY,
    UserId INTEGER REFERENCES Users(Id) ON DELETE CASCADE,
    ApplicationId INTEGER REFERENCES Applications(Id) ON DELETE CASCADE,
    RoleName VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
    UpdatedAt TIMESTAMP WITHOUT TIME ZONE
);

-- Индекс для быстрой выборки пользователей по роли.
CREATE INDEX user_roles_idx ON Roles (UserId, RoleName);
```

Пример запроса для проверки наличия у пользователя роли "Администратор" в приложении "MyApplication":
```SQL
SELECT 1 
FROM Users u
JOIN Roles r ON u.Id = r.UserId
JOIN Applications a ON r.ApplicationId = a.Id
WHERE u.Username = 'testuser'
AND a.Name = 'MyApplication'
AND r.RoleName = 'Администратор';
```
