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