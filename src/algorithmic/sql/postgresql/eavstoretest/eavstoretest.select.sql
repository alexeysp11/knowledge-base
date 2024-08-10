select 
	-- Product.
	p.product_id,
	p.product_name,
	-- Attribute.
	pa.attribute_id,
	pa.attribute_name,
	pa.is_num,
	-- Value.
	pav.value_id,
	pav.value_num,
	pav.value_text
from products p
inner join product_attribute_values pav on pav.product_id = p.product_id
inner join product_attributes pa on pa.attribute_id = pav.attribute_id
order by p.product_id, pa.attribute_id, pav.value_id