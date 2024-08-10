CREATE OR REPLACE FUNCTION f_AddProduct_UseDbgLog
(
    aCategoryId integer, 
    aProductName character varying(50),
    aPrice numeric(10,2)
) 
RETURNS VOID 
AS $$
DECLARE 
	lProductsTableName TEXT;
	lTransactionName TEXT;
	lInputParametersString TEXT;
	lInsertProductStatus TEXT;
	lErrCode TEXT;
	lMsgText TEXT;
	lExcContext TEXT;
BEGIN
	lTransactionName = 'f_AddProduct_UseDbgLog';
	lProductsTableName = 'FinancialTransactions_Products';
	lInputParametersString = 'Parameters: aCategoryId: ' || aCategoryId 
		|| ', aProductName: ' || aProductName
		|| ', aPrice: ' || aPrice;

    INSERT INTO FinancialTransactions_DbgLogs (TransactionName, TransactionDetails, TransactionStatus)
    VALUES (lTransactionName, 'Begin', 'Started');

	BEGIN
		IF (SELECT COUNT(*) FROM FinancialTransactions_Products WHERE categoryid = aCategoryId AND productname = aProductName AND price = aPrice) = 0
		THEN
			INSERT INTO FinancialTransactions_Products (categoryid, productname, price)
			VALUES (aCategoryId, aProductName, aPrice);
		ELSE
			RAISE EXCEPTION 'The record with the specified parameters already exists in table %. %', 
				lProductsTableName, lInputParametersString;
		END IF;

        lInsertProductStatus = 'Record inserted. Table: ' || lProductsTableName || '. ' || lInputParametersString;
	EXCEPTION WHEN OTHERS 
	THEN
		GET STACKED DIAGNOSTICS
		lErrCode = RETURNED_SQLSTATE,
		lMsgText = MESSAGE_TEXT,
		lExcContext = PG_CONTEXT;

		lInsertProductStatus = 'ERROR CODE: ' || lErrCode || ' MESSAGE TEXT: ' || lMsgText || ' CONTEXT: ' lExcContext;

   		RAISE NOTICE 'ERROR CODE: % MESSAGE TEXT: % CONTEXT: %', lErrCode, lMsgText, lExcContext;
  	END;

    INSERT INTO FinancialTransactions_DbgLogs (TransactionName, TransactionDetails, TransactionStatus)
    VALUES (lTransactionName, lInsertProductStatus, 'Active');

    INSERT INTO FinancialTransactions_DbgLogs (TransactionName, TransactionDetails, TransactionStatus)
    VALUES (lTransactionName, 'End', 'Finished');
END;
$$ LANGUAGE plpgsql;

-- select f_AddProduct_UseDbgLog(1, 'Cap', 10);
