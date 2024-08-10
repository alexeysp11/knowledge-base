SELECT 
	-- User.
	u.FirstName, 
	u.LastName, 
	-- Address.
	a.Street, 
	a.City, 
	a.State, 
	a.ZipCode, 
	-- Product information.
	pc.CategoryName, 
	p.ProductName, 
	p.Price, 
	-- Order details.
	o.OrderDate, 
	od.Quantity, 
	od.Price as TotalPrice, 
	os.OrderStatusName, 
	-- Payment details.
	py.PaymentDate, 
	py.PaymentAmount, 
	pm.PaymentMethodName
FROM FinancialTransactions_Users u
JOIN FinancialTransactions_Addresses a ON u.UserId = a.UserId
JOIN FinancialTransactions_Orders o ON u.UserId = o.UserId
JOIN FinancialTransactions_OrderDetails od ON o.OrderId = od.OrderId
JOIN FinancialTransactions_Products p ON od.ProductId = p.ProductId
JOIN FinancialTransactions_ProductCategories pc ON p.CategoryId = pc.CategoryId
JOIN FinancialTransactions_OrderStatuses os ON o.OrderId = os.OrderStatusId
JOIN FinancialTransactions_Payments py ON u.UserId = py.UserId
JOIN FinancialTransactions_PaymentHistory ph ON py.PaymentId = ph.PaymentId
JOIN FinancialTransactions_PaymentMethods pm ON ph.PaymentMethodId = pm.PaymentMethodId
WHERE u.UserId = 1
-- 	AND a.State = 'California'
-- 	AND pc.CategoryName = 'Electronics'
 	AND p.Price > 200
-- 	AND od.Quantity >= 2
-- 	AND os.OrderStatusName = 'Completed'
-- 	AND py.PaymentAmount > 500
ORDER BY o.OrderDate DESC;
