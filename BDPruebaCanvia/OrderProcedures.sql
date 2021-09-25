
CREATE PROCEDURE OrderCreate (
	@customerId		int,
	@shippedDate	date
) 
AS 
BEGIN
	DECLARE 
		@orderId		int,
		@requiredDate	date,
		@orderStatus	varchar(20),
		@subtotalPrice	numeric(10,2),
		@taxes			numeric(10,2),
		@totalPrice		numeric(10,2)

	SELECT
		@requiredDate = GETDATE(),
		@orderStatus = 'REGISTRADO',
		@subtotalPrice = 0,
		@taxes = 0,
		@totalPrice = 0

	INSERT INTO Orders (customerId, orderStatus, requiredDate, shippedDate, subtotalPrice, taxes, totalPrice)
	VALUES (@customerId, @orderStatus, @requiredDate, @shippedDate, @subtotalPrice, @taxes, @totalPrice)

	SELECT @orderId = SCOPE_IDENTITY()

	SELECT * FROM Orders
	WHERE orderId = @orderId
END
GRANT EXECUTE ON OrderCreate TO PUBLIC
GO

CREATE PROCEDURE OrderDelete (	
	@orderId	int
) 
AS 
BEGIN
	DELETE FROM Orders
	WHERE orderId = @orderId
END
GRANT EXECUTE ON OrderDelete TO PUBLIC
GO

CREATE PROCEDURE OrderDetail (	
	@orderId		int
) 
AS 
BEGIN
	SELECT *
	FROM Orders
	WHERE orderId = @orderId
END
GRANT EXECUTE ON OrderDetail TO PUBLIC
GO

CREATE PROCEDURE OrderList (	
	@page	int
) 
AS 
BEGIN
	DECLARE @sizePage int = 10

	SELECT *
	FROM Orders
	WHERE isDeleted = 0
	ORDER BY shippedDate
	OFFSET @sizePage * (@page-1) ROWS FETCH NEXT @sizePage ROWS ONLY
END
GRANT EXECUTE ON OrderList TO PUBLIC
GO

CREATE PROCEDURE OrderListAll
AS 
BEGIN
	SELECT * FROM Orders
	WHERE isDeleted = 0
	ORDER BY shippedDate
END
GRANT EXECUTE ON OrderListAll TO PUBLIC
GO

CREATE PROCEDURE OrderLogicDelete (	
	@orderId	int
) 
AS 
BEGIN
	UPDATE Orders SET isDeleted = 1
	WHERE orderId = @orderId
END
GRANT EXECUTE ON OrderLogicDelete TO PUBLIC
GO

CREATE PROCEDURE OrderValidated (	
	@orderId		int,
	@customerId		int,
	@shippedDate	date
)
AS 
BEGIN
	DECLARE @orderStatus varchar(20) = 'VALIDADO'

	UPDATE Orders SET
		customerId = @customerId, 
		orderStatus = @orderStatus,
		shippedDate = @shippedDate
	WHERE orderId = @orderId

	SELECT * FROM Orders
	WHERE orderId = @orderId
END
GRANT EXECUTE ON OrderValidated TO PUBLIC
GO

CREATE PROCEDURE OrderCalculated (	
	@orderId		int
)
AS 
BEGIN
	DECLARE
		@subtotalPrice	numeric(10,2),
		@taxes			numeric(10,2),
		@totalPrice		numeric(10,2)
	
	SELECT @totalPrice = SUM(total)
	FROM OrderItems
	WHERE orderId = @orderId
	
	SET @subtotalPrice = ROUND(@subtotalPrice / 1.18, 2)
	SET @taxes = ROUND(@subtotalPrice * 0.18, 2)

	UPDATE Orders SET
		subtotalPrice = @subtotalPrice,
		taxes = @taxes,
		totalPrice = @totalPrice
	WHERE orderId = @orderId

	SELECT * FROM Orders
	WHERE orderId = @orderId
END
GRANT EXECUTE ON OrderCalculated TO PUBLIC
GO
