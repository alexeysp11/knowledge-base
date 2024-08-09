CREATE OR REPLACE FUNCTION f_AddUser_CatchException
(
	aFirstName TEXT, 
	aLastName TEXT, 
	aEmail TEXT, 
	aPassword TEXT
) 
RETURNS VOID
LANGUAGE plpgsql
AS $$
DECLARE 
	lErrCode TEXT;
	lMsgText TEXT;
	lExcContext TEXT;
BEGIN
	BEGIN
		INSERT INTO FinancialTransactions_Users (FirstName, LastName, Email, Password) VALUES (aFirstName, aLastName, aEmail, aPassword);
		COMMIT;
	EXCEPTION WHEN OTHERS 
	THEN
		GET STACKED DIAGNOSTICS
		lErrCode = RETURNED_SQLSTATE,
		lMsgText = MESSAGE_TEXT,
		lExcContext = PG_CONTEXT;

   		RAISE NOTICE 'ERROR CODE: % MESSAGE TEXT: % CONTEXT: %', lErrCode, lMsgText, lExcContext;
  	END;
END;
$$;

DO $$
BEGIN
	PERFORM f_AddUser_CatchException('John', 'Doe', 'johndoe@example.com', 'password');
END;
$$;