-- Создаем временную таблицу, если ее еще нет (для тестирования)
IF OBJECT_ID('tempdb..#OperPart') IS NULL
BEGIN
    CREATE TABLE #OperPart (
        OperationID numeric(15,0),
        CharType int,
        OperDate datetime,
        Qty numeric(28,10),
        Indatetime datetime,
        Rest numeric(28,10)
    );

    -- Заполняем таблицу данными (для тестирования)
    INSERT INTO #OperPart (OperationID, CharType, OperDate, Qty, Indatetime, Rest)
    VALUES
    (33581974000, 1, '2021-08-20', 1000011.0000000000, '2021-08-20 13:10:25.700', 0),
    (33582024800, -1, '2021-08-20', 1000011.1100000000, '2021-08-20 13:51:08.903', 0),
    (33582208700, 1, '2021-08-20', 825000.1500000000, '2021-08-20 15:33:38.776', 0),
    (33582407110, -1, '2021-08-20', 825000.0000000000, '2021-08-20 16:29:39.980', 0),
    (33582408610, 1, '2021-08-20', 1000011.0000000000, '2021-08-20 16:34:53.183', 0),
    (33582419620, -1, '2021-08-20', 1000011.0000000000, '2021-08-20 17:09:24.250', 0),
    (33582519290, 1, '2021-08-20', 15000.0000000000, '2021-08-20 19:57:20.546', 0);
END;

-- Объявляем переменную для хранения текущего остатка
DECLARE @CurrentRest numeric(28,10);

-- Инициализируем остаток на начало дня
SET @CurrentRest = 1000868.31;

-- Создаем курсор для перебора записей в таблице
DECLARE OperationCursor CURSOR FOR
SELECT OperationID, CharType, Qty
FROM #OperPart
ORDER BY Indatetime; -- Очень важно сортировать по Indatetime

-- Объявляем переменные для хранения данных из курсора
DECLARE @OperationID numeric(15,0), @CharType int, @Qty numeric(28,10);

-- Открываем курсор
OPEN OperationCursor;

-- Читаем первую запись из курсора
FETCH NEXT FROM OperationCursor INTO @OperationID, @CharType, @Qty;

-- Перебираем записи в цикле
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Обновляем остаток в зависимости от типа операции
    IF @CharType = -1
        SET @CurrentRest = @CurrentRest + @Qty;  -- Пополнение
    ELSE
        SET @CurrentRest = @CurrentRest - @Qty;  -- Списание

    -- Обновляем поле Rest в таблице
    UPDATE #OperPart
    SET Rest = @CurrentRest
    WHERE OperationID = @OperationID;

    -- Читаем следующую запись из курсора
    FETCH NEXT FROM OperationCursor INTO @OperationID, @CharType, @Qty;
END

-- Закрываем и удаляем курсор
CLOSE OperationCursor;
DEALLOCATE OperationCursor;

-- Выводим результат (для проверки)
SELECT * FROM #OperPart;

-- DROP TABLE #OperPart -- если не нужно сохранять таблицу, раскомментируйте эту строку
