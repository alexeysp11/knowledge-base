begin transaction isolation level read committed;
do
$$
declare 
	lMaxProductId integer;
begin
	select max(p.productid) into lMaxProductId from FinancialTransactions_Products p;
	update FinancialTransactions_Products
	set price = price + 1
	where productid = lMaxProductId;
end;
$$;
commit;