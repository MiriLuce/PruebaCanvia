
CREATE PROCEDURE BookCreate (
	@authorId		int,
	@title			varchar(200),
	@genre			varchar(200),
	@stock			numeric(10,2),
	@price			numeric(10,2),
	@publicationDate	date
) 
AS 
BEGIN
	DECLARE @bookId	int

	INSERT INTO Books (authorId, title, genre, stock, price, publicationDate)
	VALUES (@authorId, @title, @genre, @stock, @price, @publicationDate)

	SELECT @bookId = SCOPE_IDENTITY()

	SELECT * FROM Books
	WHERE bookId = @bookId
END
GRANT EXECUTE ON BookCreate TO PUBLIC
GO

CREATE PROCEDURE BookDelete (	
	@bookId	int
) 
AS 
BEGIN
	DELETE FROM Books
	WHERE bookId = @bookId
END
GRANT EXECUTE ON BookDelete TO PUBLIC
GO

CREATE PROCEDURE BookDetail (	
	@bookId		int
) 
AS 
BEGIN
	SELECT *
	FROM Books
	WHERE bookId = @bookId
END
GRANT EXECUTE ON BookDetail TO PUBLIC
GO

CREATE PROCEDURE BookList (	
	@page	int
) 
AS 
BEGIN
	DECLARE @sizePage int = 10

	SELECT *
	FROM Books
	WHERE isDeleted = 0
	ORDER BY title
	OFFSET @sizePage * (@page-1) ROWS FETCH NEXT @sizePage ROWS ONLY
END
GRANT EXECUTE ON BookList TO PUBLIC
GO

CREATE PROCEDURE BookListAll
AS 
BEGIN
	SELECT * FROM Books
	WHERE isDeleted = 0
	ORDER BY title
END
GRANT EXECUTE ON BookListAll TO PUBLIC
GO

CREATE PROCEDURE BookLogicDelete (	
	@bookId	int
) 
AS 
BEGIN
	UPDATE Books SET isDeleted = 1
	WHERE bookId = @bookId
END
GRANT EXECUTE ON BookLogicDelete TO PUBLIC
GO

CREATE PROCEDURE BookModify (	
	@bookId			int,
	@authorId		int,
	@title			varchar(200),
	@genre			varchar(200),
	@stock			numeric(10,2),
	@price			numeric(10,2),
	@publicationDate	date
)
AS 
BEGIN
	UPDATE Books SET
		authorId = @authorId, 
		title = @title, 
		genre = @genre,
		stock = @stock,
		price = @price,
		publicationDate = @publicationDate
	WHERE bookId = @bookId

	SELECT * FROM Books
	WHERE bookId = @bookId
END
GRANT EXECUTE ON BookModify TO PUBLIC
GO
