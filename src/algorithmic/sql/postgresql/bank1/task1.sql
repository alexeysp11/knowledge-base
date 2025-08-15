-- PostgreSQL Solution

-- Создаем временную таблицу, если ее еще нет (для тестирования)
DROP TABLE IF EXISTS OperPart; -- Убеждаемся, что таблица не существует
CREATE TEMP TABLE OperPart (  --Используем именно временную таблицу, а не переменную.
    OperationID numeric(15,0),
    CharType int,
    OperDate timestamp,
    Qty numeric(28,10),
    Indatetime timestamp,
    Rest numeric(28,10)
);

-- Заполняем таблицу данными (для тестирования)
INSERT INTO OperPart (OperationID, CharType, OperDate, Qty, Indatetime, Rest)
VALUES
(33581974000, 1, '2021-08-20', 1000011.0000000000, '2021-08-20 13:10:25.700', 0),
(33582024800, -1, '2021-08-20', 1000011.1100000000, '2021-08-20 13:51:08.903', 0),
(33582208700, 1, '2021-08-20', 825000.1500000000, '2021-08-20 15:33:38.776', 0),
(33582407110, -1, '2021-08-20', 825000.0000000000, '2021-08-20 16:29:39.980', 0),
(33582408610, 1, '2021-08-20', 1000011.0000000000, '2021-08-20 16:34:53.183', 0),
(33582419620, -1, '2021-08-20', 1000011.0000000000, '2021-08-20 17:09:24.250', 0),
(33582519290, 1, '2021-08-20', 15000.0000000000, '2021-08-20 19:57:20.546', 0);


DO $$
DECLARE
    current_rest numeric(28,10) := 1000868.31;
    operation_record record;
BEGIN
    FOR operation_record IN SELECT OperationID, CharType, Qty, Indatetime FROM OperPart ORDER BY Indatetime LOOP
        IF operation_record.CharType = -1 THEN
            current_rest := current_rest + operation_record.Qty;
        ELSE
            current_rest := current_rest - operation_record.Qty;
        END IF;

        UPDATE OperPart SET Rest = current_rest WHERE OperationID = operation_record.OperationID;
    END LOOP;
END $$;

SELECT * FROM OperPart;

-- DROP TABLE OperPart; -- Удалить таблицу, если не нужна.
