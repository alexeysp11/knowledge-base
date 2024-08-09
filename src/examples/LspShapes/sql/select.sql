SELECT 
    p.name AS product_name, 
    c.name AS category_name
FROM [LspShapesDb].[dbo].[product] p 
LEFT JOIN [LspShapesDb].[dbo].[product_category] pc ON pc.product_id = p.product_id 
LEFT JOIN [LspShapesDb].[dbo].[category] c ON c.category_id = pc.category_id
