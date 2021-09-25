
CREATE PROCEDURE AuthorCreate (	
	@firstName		varchar(200),
	@lastName		varchar(200),
	@birthdate		date,
	@deathDate		date,
	@country		varchar(200)
) 
AS 
BEGIN
	DECLARE @authorId	int

	INSERT INTO Authors (firstName, lastName, birthdate, deathDate, country)
	VALUES (@firstName, @lastName, @birthdate, @deathDate, @country)

	SELECT @authorId = SCOPE_IDENTITY()

	SELECT * FROM Authors
	WHERE authorId = @authorId
END
GRANT EXECUTE ON AuthorCreate TO PUBLIC
GO

CREATE PROCEDURE AuthorDelete (	
	@authorId	int
) 
AS 
BEGIN
	DELETE FROM Authors
	WHERE authorId = @authorId
END
GRANT EXECUTE ON AuthorDelete TO PUBLIC
GO

CREATE PROCEDURE AuthorDetail (	
	@authorId		int
) 
AS 
BEGIN
	SELECT *
	FROM Authors
	WHERE authorId = @authorId
END
GRANT EXECUTE ON AuthorDetail TO PUBLIC
GO

CREATE PROCEDURE AuthorList (	
	@page	int
) 
AS 
BEGIN
	DECLARE @sizePage int = 10

	SELECT *
	FROM Authors
	WHERE isDeleted = 0
	ORDER BY lastName + ' ' + firstName
	OFFSET @sizePage * (@page-1) ROWS FETCH NEXT @sizePage ROWS ONLY
END
GRANT EXECUTE ON AuthorList TO PUBLIC
GO

CREATE PROCEDURE AuthorListAll
AS 
BEGIN
	SELECT * FROM Authors
	WHERE isDeleted = 0
END
GRANT EXECUTE ON AuthorListAll TO PUBLIC
GO

CREATE PROCEDURE AuthorLogicDelete (	
	@authorId	int
) 
AS 
BEGIN
	UPDATE Authors SET isDeleted = 1
	WHERE authorId = @authorId
END
GRANT EXECUTE ON AuthorLogicDelete TO PUBLIC
GO

CREATE PROCEDURE AuthorModify (	
	@authorId		int,
	@firstName		varchar(200),
	@lastName		varchar(200),
	@birthdate		date,
	@deathDate		date,
	@country		varchar(200)
)
AS 
BEGIN
	UPDATE Authors SET
		firstName = @firstName, 
		lastName = @lastName, 
		birthdate = @birthdate,
		deathDate = @deathDate,
		country = @country
	WHERE authorId = @authorId

	SELECT * FROM Authors
	WHERE authorId = @authorId
END
GRANT EXECUTE ON AuthorModify TO PUBLIC
GO
