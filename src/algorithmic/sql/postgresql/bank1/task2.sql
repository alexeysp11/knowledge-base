-- Создаем таблицу A (если ее нет)
DROP TABLE IF EXISTS A;
CREATE TABLE A (
    DealID numeric(15,0) PRIMARY KEY,
    ParentCnt int,
    ChildCnt int
);

-- Заполняем таблицу данными (для тестирования)
INSERT INTO A (DealID, ParentCnt, ChildCnt)
VALUES
(106, 0, 0),
(107, 0, 0),
(108, 0, 0),
(109, 0, 0);

-- Создаем таблицу DealRelation (если ее нет)
DROP TABLE IF EXISTS DealRelation;
CREATE TABLE DealRelation (
    RelType smallint,  -- Use smallint for tinyint equivalent
    ParentID numeric(15,0),
    ChildID numeric(15,0)
);

-- Заполняем таблицу данными (для тестирования)
INSERT INTO DealRelation (RelType, ParentID, ChildID)
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


-- Функция для подсчета предков
CREATE OR REPLACE FUNCTION GetParentCount (p_DealID numeric(15,0))
RETURNS int AS $$
DECLARE
    ParentCount int;
BEGIN
    WITH RECURSIVE ParentCTE AS (
        SELECT ParentID, ChildID
        FROM DealRelation
        WHERE ChildID = p_DealID
        AND RelType = 2

        UNION ALL

        SELECT dr.ParentID, dr.ChildID
        FROM DealRelation dr
        INNER JOIN ParentCTE pc ON dr.ChildID = pc.ParentID
        WHERE dr.RelType = 2
    )

    SELECT COUNT(*) INTO ParentCount
    FROM ParentCTE;

    RETURN COALESCE(ParentCount, 0);
END;
$$ LANGUAGE plpgsql;

-- Функция для подсчета потомков
CREATE OR REPLACE FUNCTION GetChildCount (p_DealID numeric(15,0))
RETURNS int AS $$
DECLARE
    ChildCount int;
BEGIN
    WITH RECURSIVE ChildCTE AS (
        SELECT ParentID, ChildID
        FROM DealRelation
        WHERE ParentID = p_DealID
        AND RelType = 2

        UNION ALL

        SELECT dr.ParentID, dr.ChildID
        FROM DealRelation dr
        INNER JOIN ChildCTE cc ON dr.ParentID = cc.ChildID
        WHERE dr.RelType = 2
    )

    SELECT COUNT(*) INTO ChildCount
    FROM ChildCTE;

    RETURN COALESCE(ChildCount, 0);
END;
$$ LANGUAGE plpgsql;

-- Скрипт для обновления таблицы A
UPDATE A
SET ParentCnt = GetParentCount(DealID),
    ChildCnt = GetChildCount(DealID);

-- Вывод результата
SELECT * FROM A;

-- Удаление функций (опционально)
DROP FUNCTION IF EXISTS GetParentCount(numeric);
DROP FUNCTION IF EXISTS GetChildCount(numeric);

-- Удаление таблиц (опционально)
--DROP TABLE IF EXISTS A;
--DROP TABLE IF EXISTS DealRelation;
