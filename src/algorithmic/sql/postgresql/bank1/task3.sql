-- Предполагается, что таблица Country уже существует

-- Создаем таблицу Country (если ее нет)
DROP TABLE IF EXISTS Country;  -- Сначала удаляем, если существует

CREATE TABLE Country (
    CountryID numeric(15,0) PRIMARY KEY,
    Name varchar(160),
    CodeISO varchar(10)
);

-- Заполняем таблицу данными (для тестирования)
INSERT INTO Country (CountryID, Name, CodeISO)
VALUES
(3364, 'COSTA RICA', '188'),
(3362, 'ANTIGUA AND BARBUDA', '028'),
(3255, 'LATVIA', '428'),
(3235, 'MOLDOVA, REPUBLIC OF', '498'),
(3232, 'REUNION', '638'),
(3237, 'GUATEMALA', '320');


-- Создаем функцию Log_Add (если ее нет)
CREATE OR REPLACE FUNCTION Log_Add(p_CountryID numeric(15,0)) RETURNS integer AS $$
BEGIN
    -- Просто заглушка для примера
    RETURN 0;  -- Успех
END;
$$ LANGUAGE plpgsql;

-- Создаем функцию Country_Change (если ее нет)
CREATE OR REPLACE FUNCTION Country_Change(p_CountryID numeric(15,0), p_Name varchar(160), p_CodeISO varchar(10)) RETURNS integer AS $$
BEGIN
    -- Просто заглушка для примера
    RETURN 0;  -- Успех
END;
$$ LANGUAGE plpgsql;


-- Создаем хранимую процедуру Country_Edit
CREATE OR REPLACE FUNCTION Country_Edit() RETURNS VOID
AS $$
DECLARE
    country_record RECORD;
    ret_val INTEGER;
    error_occurred BOOLEAN := FALSE;
BEGIN
    FOR country_record IN SELECT CountryID FROM Country LOOP
        BEGIN
            -- 1. Логируем до изменения
            ret_val := Log_Add(country_record.CountryID);

            IF ret_val <> 0 THEN
                RAISE EXCEPTION 'Log_Add failed for CountryID %', country_record.CountryID;
            END IF;


            -- 2. Изменяем данные
            ret_val := Country_Change(country_record.CountryID, 'Россия', '643');

            IF ret_val <> 0 THEN
                RAISE EXCEPTION 'Country_Change failed for CountryID %', country_record.CountryID;
            END IF;


            UPDATE Country
            SET Name = 'Россия',
                CodeISO = '643'
            WHERE CountryID = country_record.CountryID;

        EXCEPTION
            WHEN OTHERS THEN
                error_occurred := TRUE;
                RAISE NOTICE 'Error occurred for CountryID %: %', country_record.CountryID, SQLERRM;

                -- Логируем ошибку
                ret_val := Log_Add(country_record.CountryID);

                IF ret_val <> 0 THEN
                    RAISE NOTICE 'Failed to log error for CountryID: %', country_record.CountryID;
                END IF;

        END;
    END LOOP;

    IF error_occurred THEN
        RAISE EXCEPTION 'Country_Edit completed with errors.';
    ELSE
        RAISE NOTICE 'Country_Edit completed successfully.';
    END IF;

END;
$$ LANGUAGE plpgsql;


-- Пример выполнения
--CALL Country_Edit();
