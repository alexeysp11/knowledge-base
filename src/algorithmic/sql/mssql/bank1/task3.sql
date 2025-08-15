-- Предполагается, что таблица #Country уже существует

-- Создаем таблицу #Country (если ее нет)
IF OBJECT_ID('tempdb..#Country') IS NULL
BEGIN
    CREATE TABLE #Country (
        CountryID numeric(15,0) PRIMARY KEY,
        Name varchar(160),
        CodeISO varchar(10)
    );

    -- Заполняем таблицу данными (для тестирования)
    INSERT INTO #Country (CountryID, Name, CodeISO)
    VALUES
    (3364, 'COSTA RICA', '188'),
    (3362, 'ANTIGUA AND BARBUDA', '028'),
    (3255, 'LATVIA', '428'),
    (3235, 'MOLDOVA, REPUBLIC OF', '498'),
    (3232, 'REUNION', '638'),
    (3237, 'GUATEMALA', '320');
END

-- Создаем хранимую процедуру Log_Add (если ее нет)
IF OBJECT_ID('dbo.Log_Add') IS NULL  -- Используем dbo, чтобы избежать проблем с контекстом выполнения
BEGIN
    EXEC('CREATE PROCEDURE dbo.Log_Add @CountryID numeric(15,0), @RetVal int OUTPUT AS BEGIN SET @RetVal = 0;  -- Просто заглушка для примера END');
END;
GO -- Обязательно нужно GO после CREATE PROCEDURE

-- Создаем хранимую процедуру Country_Change (если ее нет)
IF OBJECT_ID('dbo.Country_Change') IS NULL
BEGIN
    EXEC('CREATE PROCEDURE dbo.Country_Change @CountryID numeric(15,0), @Name varchar(160), @CodeISO varchar(10), @RetVal int OUTPUT AS BEGIN SET @RetVal = 0; END');
END;
GO -- Обязательно нужно GO после CREATE PROCEDURE

-- Создаем хранимую процедуру Country_Edit
CREATE PROCEDURE dbo.Country_Edit
AS
BEGIN
    SET NOCOUNT ON;  -- Улучшает производительность, отключая сообщения о количестве обработанных строк

    DECLARE @CountryID numeric(15,0);
    DECLARE @RetVal int;
    DECLARE @ErrorOccurred bit = 0;

    -- Объявляем курсор
    DECLARE CountryCursor CURSOR FOR
    SELECT CountryID
    FROM #Country;

    -- Открываем курсор
    OPEN CountryCursor;

    -- Читаем первую запись
    FETCH NEXT FROM CountryCursor INTO @CountryID;

    -- Цикл по всем записям в курсоре
    WHILE @@FETCH_STATUS = 0
    BEGIN
        BEGIN TRY
            -- 1. Логируем до изменения
            EXEC dbo.Log_Add @CountryID = @CountryID, @RetVal = @RetVal OUTPUT;

            IF @RetVal <> 0
            BEGIN
                RAISERROR('Log_Add failed', 16, 1);  -- Вызываем ошибку, чтобы перейти в блок CATCH
            END

            -- 2. Изменяем данные
            EXEC dbo.Country_Change
                 @CountryID = @CountryID,
                 @Name = 'Россия',
                 @CodeISO = '643',
                 @RetVal = @RetVal OUTPUT;

            IF @RetVal <> 0
            BEGIN
                RAISERROR('Country_Change failed', 16, 1);  -- Вызываем ошибку, чтобы перейти в блок CATCH
            END
        END TRY
        BEGIN CATCH
            -- Обработка ошибок
            SET @ErrorOccurred = 1;
            PRINT ERROR_MESSAGE();

            -- Логируем ошибку
            EXEC dbo.Log_Add @CountryID = @CountryID, @RetVal = @RetVal OUTPUT; -- Попытка логирования ошибки

            -- Если логирование ошибки тоже не удалось, можно предпринять дополнительные действия
            IF @RetVal <> 0
            BEGIN
                PRINT 'Failed to log error for CountryID: ' + CAST(@CountryID AS VARCHAR(20));
            END

        END CATCH

        -- Читаем следующую запись
        FETCH NEXT FROM CountryCursor INTO @CountryID;
    END

    -- Закрываем и удаляем курсор
    CLOSE CountryCursor;
    DEALLOCATE CountryCursor;

    IF @ErrorOccurred = 1
    BEGIN
        RAISERROR('Country_Edit completed with errors.', 16, 1);
        RETURN 1; -- Указываем, что процедура завершилась с ошибками
    END
    ELSE
    BEGIN
        PRINT 'Country_Edit completed successfully.';
        RETURN 0; -- Указываем, что процедура завершилась успешно
    END
END
GO

-- Пример выполнения
--EXEC dbo.Country_Edit;
