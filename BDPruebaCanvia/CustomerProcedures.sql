
CREATE PROCEDURE CustomerCreate (	
	@firstName		varchar(200),
	@lastName		varchar(200),
	@phone			varchar(50),
	@email			varchar(100),
	@addressStreet	varchar(200)
) 
AS 
BEGIN
	DECLARE @customerId	int

	INSERT INTO Customers (firstName, lastName, phone, email, addressStreet)
	VALUES (@firstName, @lastName, @phone, @email, @addressStreet)

	SELECT @customerId = SCOPE_IDENTITY()

	SELECT * FROM Customers
	WHERE CustomerId = @customerId
END
GRANT EXECUTE ON CustomerCreate TO PUBLIC
GO

CREATE PROCEDURE CustomerDelete (	
	@customerId	int
) 
AS 
BEGIN
	DELETE FROM Customers
	WHERE CustomerId = @customerId
END
GRANT EXECUTE ON CustomerDelete TO PUBLIC
GO

CREATE PROCEDURE CustomerDetail (	
	@customerId		int
) 
AS 
BEGIN
	SELECT *
	FROM Customers
	WHERE CustomerId = @customerId
END
GRANT EXECUTE ON CustomerDetail TO PUBLIC
GO

CREATE PROCEDURE CustomerList (	
	@page	int
) 
AS 
BEGIN
	DECLARE @sizePage int = 10

	SELECT *
	FROM Customers
	WHERE IsDeleted = 0
	ORDER BY lastName + ' ' + firstName
	OFFSET @sizePage * (@page-1) ROWS FETCH NEXT @sizePage ROWS ONLY
END
GRANT EXECUTE ON CustomerList TO PUBLIC
GO

CREATE PROCEDURE CustomerListAll
AS 
BEGIN
	SELECT * FROM Customers
	WHERE IsDeleted = 0
END
GRANT EXECUTE ON CustomerListAll TO PUBLIC
GO

CREATE PROCEDURE CustomerLogicDelete (	
	@customerId	int
) 
AS 
BEGIN
	UPDATE Customers SET IsDeleted = 1
	WHERE CustomerId = @customerId
END
GRANT EXECUTE ON CustomerLogicDelete TO PUBLIC
GO

CREATE PROCEDURE CustomerModify (	
	@customerId		int,
	@firstName		varchar(200),
	@lastName		varchar(200),
	@phone			varchar(50),
	@email			varchar(100),
	@addressStreet	varchar(200)
) 
AS 
BEGIN
	UPDATE Customers SET
		firstName = @firstName, 
		lastName = @lastName, 
		phone = @phone,
		email = @email, 
		AddressStreet = @addressStreet
	WHERE CustomerId = @customerId

	SELECT * FROM Customers
	WHERE CustomerId = @customerId
END
GRANT EXECUTE ON CustomerModify TO PUBLIC
GO