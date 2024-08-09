INSERT INTO products (product_name)
SELECT 'Product ' || i
FROM generate_series(1, 10) AS i;

INSERT INTO product_attributes (attribute_name)
VALUES ('Color'), ('Size'), ('Weight');

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
