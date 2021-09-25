
CREATE PROCEDURE OrderItemCreate (
	@orderId	int,
	@bookId		int,
	@quantity	numeric(10,2)
) 
AS 
BEGIN
	DECLARE 
		@orderItemId	int,
		@total			numeric(10,2)

	SELECT @total = @quantity * price
	FROM Books
	WHERE bookId = @bookId

	INSERT INTO OrderItems (orderId, bookId, quantity, total)
	VALUES (@orderId, @bookId, @quantity, @total)

	SELECT @orderItemId = SCOPE_IDENTITY()

	SELECT * FROM OrderItems
	WHERE orderItemId = @orderItemId
END
GRANT EXECUTE ON OrderItemCreate TO PUBLIC
GO

CREATE PROCEDURE OrderItemDelete (	
	@orderItemId	int
) 
AS 
BEGIN
	DELETE FROM OrderItems
	WHERE orderItemId = @orderItemId
END
GRANT EXECUTE ON OrderItemDelete TO PUBLIC
GO

CREATE PROCEDURE OrderItemDetail (	
	@orderItemId	int
) 
AS 
BEGIN
	SELECT *
	FROM OrderItems
	WHERE orderItemId = @orderItemId
END
GRANT EXECUTE ON OrderItemDetail TO PUBLIC
GO

CREATE PROCEDURE OrderItemList (	
	@orderId	int,
	@page		int
) 
AS 
BEGIN
	DECLARE @sizePage int = 10

	SELECT OrderItems.*
	FROM OrderItems
	JOIN Books ON OrderItems.bookId = Books.bookId
	WHERE Books.isDeleted = 0 AND orderId = @orderId
	ORDER BY Books.title
	OFFSET @sizePage * (@page-1) ROWS FETCH NEXT @sizePage ROWS ONLY
END
GRANT EXECUTE ON OrderItemList TO PUBLIC
GO

CREATE PROCEDURE OrderItemListAll
	@orderId	int
AS 
BEGIN
	SELECT OrderItems.*
	FROM OrderItems
	JOIN Books ON OrderItems.bookId = Books.bookId
	WHERE Books.isDeleted = 0 AND orderId = @orderId
	ORDER BY Books.title
END
GRANT EXECUTE ON OrderItemListAll TO PUBLIC
GO

CREATE PROCEDURE OrderItemLogicDelete (	
	@orderItemId	int
) 
AS 
BEGIN
	UPDATE OrderItems SET isDeleted = 1
	WHERE orderItemId = @orderItemId
END
GRANT EXECUTE ON OrderItemLogicDelete TO PUBLIC
GO

CREATE PROCEDURE OrderItemModify (	
	@orderItemId	int,
	@bookId			int,
	@quantity		numeric(10,2)
)
AS 
BEGIN
	DECLARE @total numeric(10,2)

	SELECT @total = @quantity * price
	FROM Books
	WHERE bookId = @bookId
	
	UPDATE OrderItems SET
		bookId = @bookId, 
		quantity = @quantity,
		total = @total
	WHERE orderItemId = @orderItemId

	SELECT * FROM OrderItems
	WHERE orderItemId = @orderItemId
END
GRANT EXECUTE ON OrderItemModify TO PUBLIC
GO
