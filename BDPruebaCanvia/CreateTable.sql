USE BDPruebaCanvia;

CREATE TABLE Customers (
	customerId		int not null IDENTITY(1,1) PRIMARY KEY,
	firstName		varchar(200) not null,
	lastName		varchar(200) not null,
	phone			varchar(50) null,
	email			varchar(100) null,
	addressStreet	varchar(200) null,
	isDeleted		bit not null default 0
)

CREATE TABLE Authors (
	authorId		int not null IDENTITY(1,1) PRIMARY KEY,
	firstName		varchar(200) not null,
	lastName		varchar(200) not null,
	birthdate		date not null,
	deathDate		date null,
	country			varchar(200) not null,
	isDeleted		bit not null default 0
)

CREATE TABLE Books (
	bookId		int not null IDENTITY(1,1) PRIMARY KEY,
	authorId		int not null,
	title			varchar(200) not null,
	genre			varchar(200) not null,
	stock			numeric(10,2) not null,
	price			numeric(10,2) not null,
	publicationDate date not null,
	isDeleted		bit not null default 0
	FOREIGN KEY (authorId) REFERENCES  Authors(authorId)
)

CREATE TABLE Orders (
	orderId			int not null IDENTITY(1,1) PRIMARY KEY,
	customerId		int not null,
	orderStatus		varchar(20) not null,
	requiredDate	date not null,
	shippedDate		date null,
	subtotalPrice	numeric(10,2) not null,
	taxes			numeric(10,2) not null,
	totalPrice		numeric(10,2) not null,
	isDeleted		bit not null default 0,
	FOREIGN KEY (customerId) REFERENCES  Customers(customerId)
)

CREATE TABLE OrderItems (
	orderItemId		int not null IDENTITY(1,1) PRIMARY KEY,
	orderId			int not null,
	bookId			int not null,
	quantity		numeric(10,2) not null,
	total			numeric(10,2) not null,
	isDeleted		bit not null default 0,
	FOREIGN KEY (orderId) REFERENCES  Orders(orderId),
	FOREIGN KEY (bookId) REFERENCES  Books(bookId)
)

CREATE LOGIN Consultant WITH PASSWORD = '123';
CREATE USER Consultant FOR LOGIN Consultant;

