-- Создаем таблицу #A (если ее нет)
IF OBJECT_ID('tempdb..#A') IS NULL
BEGIN
    CREATE TABLE #A (
        DealID numeric(15,0) PRIMARY KEY,
        ParentCnt int,
        ChildCnt int
    );

    -- Заполняем таблицу данными (для тестирования)
    INSERT INTO #A (DealID, ParentCnt, ChildCnt)
    VALUES
    (106, 0, 0),
    (107, 0, 0),
    (108, 0, 0),
    (109, 0, 0);
END

-- Создаем таблицу tDealRelation (если ее нет)
IF OBJECT_ID('dbo.tDealRelation') IS NULL
BEGIN
    CREATE TABLE dbo.tDealRelation (
        RelType tinyint,
        ParentID numeric(15,0),
        ChildID numeric(15,0)
    );

    -- Заполняем таблицу данными (для тестирования)
    INSERT INTO dbo.tDealRelation (RelType, ParentID, ChildID)
    VALUES
    (2, 101, 103),
    (2, 103, 106),
    (2, 104, 107),
    (2, 107, 111),
    (2, 102, 105),
    (2, 105, 108),
    (2, 106, 110),
    (2, 108, 109),
    (2, 111, 112);


END

-- Функция для подсчета предков
CREATE FUNCTION dbo.GetParentCount (@DealID numeric(15,0))
RETURNS int
AS
BEGIN
    DECLARE @ParentCount int;

    WITH ParentCTE AS (
        SELECT ParentID, ChildID
        FROM dbo.tDealRelation
        WHERE ChildID = @DealID
        AND RelType = 2

        UNION ALL

        SELECT dr.ParentID, dr.ChildID
        FROM dbo.tDealRelation dr
        INNER JOIN ParentCTE pc ON dr.ChildID = pc.ParentID
        WHERE dr.RelType = 2
    )

    SELECT @ParentCount = COUNT(*)
    FROM ParentCTE;

    RETURN ISNULL(@ParentCount, 0);
END
GO

-- Функция для подсчета потомков
CREATE FUNCTION dbo.GetChildCount (@DealID numeric(15,0))
RETURNS int
AS
BEGIN
    DECLARE @ChildCount int;

    WITH ChildCTE AS (
        SELECT ParentID, ChildID
        FROM dbo.tDealRelation
        WHERE ParentID = @DealID
        AND RelType = 2

        UNION ALL

        SELECT dr.ParentID, dr.ChildID
        FROM dbo.tDealRelation dr
        INNER JOIN ChildCTE cc ON dr.ParentID = cc.ChildID
        WHERE dr.RelType = 2
    )

    SELECT @ChildCount = COUNT(*)
    FROM ChildCTE;

    RETURN ISNULL(@ChildCount, 0);
END
GO

-- Скрипт для обновления таблицы #A
UPDATE #A
SET ParentCnt = dbo.GetParentCount(DealID),
    ChildCnt = dbo.GetChildCount(DealID);

-- Вывод результата
SELECT * FROM #A;

-- Удаление функций (опционально)
DROP FUNCTION dbo.GetParentCount
DROP FUNCTION dbo.GetChildCount

-- Удаление таблиц (опционально, если это временные таблицы)
--DROP TABLE #A
--DROP TABLE tDealRelation
