CREATE TABLE Table1 (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);
CREATE TABLE Table2 (
    Id SERIAL PRIMARY KEY,
    Value INT NOT NULL
);
CREATE TABLE ProcessedData (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Value INT NOT NULL
);

INSERT INTO Table1 (Name) VALUES ('Data1');
INSERT INTO Table1 (Name) VALUES ('Data2');
INSERT INTO Table1 (Name) VALUES ('Data3');
INSERT INTO Table1 (Name) VALUES ('Data4');
INSERT INTO Table1 (Name) VALUES ('Data5');
INSERT INTO Table1 (Name) VALUES ('Data6');
INSERT INTO Table1 (Name) VALUES ('Data7');
INSERT INTO Table1 (Name) VALUES ('Data8');
INSERT INTO Table1 (Name) VALUES ('Data9');
INSERT INTO Table1 (Name) VALUES ('Data10');
INSERT INTO Table2 (Value) VALUES (0);
INSERT INTO Table2 (Value) VALUES (20);
INSERT INTO Table2 (Value) VALUES (14);
INSERT INTO Table2 (Value) VALUES (10);
INSERT INTO Table2 (Value) VALUES (1);
INSERT INTO Table2 (Value) VALUES (15);
INSERT INTO Table2 (Value) VALUES (11);
INSERT INTO Table2 (Value) VALUES (19);
INSERT INTO Table2 (Value) VALUES (20);
INSERT INTO Table2 (Value) VALUES (25);

CREATE OR REPLACE FUNCTION ProcessData(
    aDeleteFromProcessedData INT DEFAULT 1)
RETURNS VOID AS $$
BEGIN
    IF aDeleteFromProcessedData = 1 THEN
        DELETE FROM ProcessedData;
    END IF;
    
    INSERT INTO ProcessedData (Name, Value)
    SELECT t1.Name, t2.Value
    FROM Table1 t1
    JOIN Table2 t2 ON t1.Id = t2.Id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION ProcessAndReturnResult(
    aDeleteFromProcessedData INT DEFAULT 1,
    aInsertProcessedData INT DEFAULT 1)
RETURNS TABLE (Result BIGINT) AS $$
BEGIN
    IF aDeleteFromProcessedData = 1 THEN
        DELETE FROM ProcessedData;
    END IF;

    IF aInsertProcessedData = 1 THEN
        INSERT INTO ProcessedData (Name, Value)
        SELECT t1.Name, t2.Value
        FROM Table1 t1
        JOIN Table2 t2 ON t1.Id = t2.Id;
    END IF;

    RETURN QUERY SELECT SUM(Value) AS Result FROM ProcessedData;
END;
$$ LANGUAGE plpgsql;
