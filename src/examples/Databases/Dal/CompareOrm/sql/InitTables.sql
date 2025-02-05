
-- Initialize database for testing ORM performance.

----------------------------

CREATE TABLE IF NOT EXISTS CompareOrmDefault (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS CompareOrmDA (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS CompareOrmDapper (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255)
);

----------------------------

CREATE OR REPLACE FUNCTION FillCompareOrmObjectType(table_name TEXT, rows_count INT)
RETURNS void AS $$
DECLARE
    i INT;
BEGIN
    FOR i IN 1..rows_count LOOP
        EXECUTE format('INSERT INTO %I (Name) VALUES (%L)', lower(table_name), 'Test Name ' || i);
    END LOOP;
END;
$$ LANGUAGE plpgsql;

SELECT FillCompareOrmObjectType('CompareOrmDefault', 2000);
SELECT FillCompareOrmObjectType('CompareOrmDA', 2000);
SELECT FillCompareOrmObjectType('CompareOrmDapper', 2000);
