# eav

### Условие задачи для использования EAV модели:

Представим, что у нас есть онлайн-площадка, где пользователи могут размещать свои собственные товары. Каждый товар может иметь различные характеристики, которые могут быть уникальными для каждого товара. Для решения данной задачи можно использовать EAV (Entity-Attribute-Value) модель, которая позволяет хранить разнородные данные в базе данных без необходимости изменения схемы таблиц.

### SQL-запросы для создания таблиц в PostgreSQL:

```sql
CREATE TABLE products (
    product_id SERIAL PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL
);

CREATE TABLE product_attributes (
    attribute_id SERIAL PRIMARY KEY,
    product_id INT REFERENCES products(product_id),
    attribute_name VARCHAR(50) NOT NULL,
    is_num INT
);

CREATE TABLE product_attribute_values (
    value_id SERIAL PRIMARY KEY,
    product_id INT REFERENCES products(product_id),
    attribute_id INT REFERENCES product_attributes(attribute_id),
    value_text TEXT,
    value_num TEXT
);
```

### SQL-запросы для инициализации таблиц случайными данными в PostgreSQL:

```sql
-- Генерация случайных продуктов
INSERT INTO products (product_name)
SELECT 'Product ' || i
FROM generate_series(1, 10) AS i;

-- Генерация случайных атрибутов
INSERT INTO product_attributes (attribute_name)
VALUES ('Color'), ('Size'), ('Weight');

-- Привязка случайных атрибутов к случайным продуктам с рандомными значениями
INSERT INTO product_attribute_values (product_id, attribute_id, value_text, value_num)
SELECT 
    p.product_id,
    a.attribute_id,
    CASE 
        WHEN a.attribute_name = 'Color' THEN (ARRAY['Red', 'Blue', 'Green', 'Yellow'])[floor(random()*4)+1]
        WHEN a.attribute_name = 'Size' THEN (ARRAY['Small', 'Medium', 'Large'])[floor(random()*3)+1]
        WHEN a.attribute_name = 'Weight' THEN NULL
    END,
    CASE 
        WHEN a.attribute_name = 'Weight' THEN (random()*1000)
        ELSE NULL
    END
FROM products p
CROSS JOIN product_attributes a;
```

Эти запросы создадут таблицы для хранения продуктов, атрибутов и их значений, а также заполнят их случайными данными для демонстрации работы EAV модели.
