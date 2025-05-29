--create database ReadRoom

use ReadRoom

--drop database ReadRoom

CREATE TABLE Books (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    AuthorName NVARCHAR(255) NOT NULL,
    AuthorSurname NVARCHAR(255) NOT NULL,
    Publisher NVARCHAR(255),
    Pages INT,
    Genre NVARCHAR(100),
    PublicationYear INT,
    CostPrice MONEY NOT NULL,
    SalePrice MONEY NOT NULL,
    IsContinuation VARCHAR(3) NOT NULL,  
    PrequelBookTitle NVARCHAR(255) DEFAULT 'Нет', 
    [Status] NVARCHAR(50) DEFAULT 'Доступно',
    PublicationDate DATETIME
);
GO


CREATE TABLE Sales (
    SaleId INT PRIMARY KEY IDENTITY(1,1),
    BookId INT NOT NULL,
    SaleDate DATETIME NOT NULL,
    Quantity INT NOT NULL,
    TotalPrice MONEY NOT NULL,
    UserLogin NVARCHAR(50) NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(Id),
    FOREIGN KEY (UserLogin) REFERENCES Users(Login)
);
GO


CREATE TABLE Reservations (
    ReservationId INT PRIMARY KEY IDENTITY(1,1),
    BookId INT NOT NULL,
    ReservationDate DATETIME NOT NULL,
    UserLogin NVARCHAR(50) NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(Id),
    FOREIGN KEY (UserLogin) REFERENCES Users(Login)
);
GO


CREATE TABLE Promotions (
    PromotionId INT PRIMARY KEY IDENTITY(1,1),
    PromotionName NVARCHAR(255) NOT NULL,
	BookId INT NOT NULL,
    DiscountPercent DECIMAL(5, 2),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
	FOREIGN KEY (BookId) REFERENCES Books(Id)
);
GO

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Login] NVARCHAR(50) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE
);
GO


INSERT INTO Users ([Login], [Password], Email) 
VALUES ('admin', 'admin', 'admin@example.com');
GO


INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Pages, Genre, PublicationYear, CostPrice, SalePrice, IsContinuation, PrequelBookTitle, [Status], PublicationDate)
VALUES 
('Война и мир', 'Лев', 'Толстой', 'АСТ', 1225, 'Роман', 1869, 500.00, 800.00, 'Нет', 'Нет', 'Доступно', '1869-01-01'),
('Анна Каренина', 'Лев', 'Толстой', 'АСТ', 864, 'Роман', 1877, 450.00, 750.00, 'Нет', 'Нет', 'Доступно', '1877-01-01'),
('451 градус по Фаренгейту', 'Рэй', 'Бредбери', 'Азбука', 192, 'Фантастика', 1953, 200.00, 350.00, 'Нет', 'Нет', 'Доступно', '1953-01-01'),
('Дюна', 'Фрэнк', 'Херберт', 'Эксмо', 688, 'Фантастика', 1965, 600.00, 950.00, 'Нет', 'Нет', 'Доступно', '1965-01-01'),
('Гарри Поттер и философский камень', 'Джоан', 'Роулинг', 'Росмэн', 320, 'Фэнтези', 1997, 350.00, 500.00, 'Нет', 'Нет', 'Доступно', '1997-01-01'),
('Гарри Поттер и Тайная комната', 'Джоан', 'Роулинг', 'Росмэн', 352, 'Фэнтези', 1998, 350.00, 500.00, 'Да', 'Гарри Поттер и философский камень', 'Доступно', '1998-01-01'),
('Гарри Поттер и узник Азкабана', 'Джоан', 'Роулинг', 'Росмэн', 435, 'Фэнтези', 1999, 400.00, 550.00, 'Да', 'Гарри Поттер и Тайная комната', 'Доступно', '1999-01-01'),
('Мастер и Маргарита', 'Михаил', 'Булгаков', 'Эксмо', 480, 'Роман', 1967, 300.00, 450.00, 'Нет', 'Нет', 'Доступно', '1967-01-01'),
('Преступление и наказание', 'Фёдор', 'Достоевский', 'АСТ', 672, 'Роман', 1866, 400.00, 600.00, 'Нет', 'Нет', 'Доступно', '1866-01-01'),
('Идиот', 'Фёдор', 'Достоевский', 'Эксмо', 832, 'Роман', 1869, 450.00, 650.00, 'Нет', 'Нет', 'Доступно', '1869-01-01'),
('Граф Монте-Кристо', 'Александр', 'Дюма', 'Эксмо', 1276, 'Приключения', 1844, 550.00, 850.00, 'Нет', 'Нет', 'Доступно', '1844-01-01'),
('Отцы и дети', 'Иван', 'Тургенев', 'Азбука', 320, 'Роман', 1862, 250.00, 400.00, 'Нет', NULL, 'Доступно', '1862-01-01'),
('Властелин колец: Братство Кольца', 'Джон Рональд Руэл', 'Толкин', 'АСТ', 479, 'Фэнтези', 1954, 500.00, 700.00, 'Нет', 'Нет', 'Доступно', '1954-01-01'),
('Властелин колец: Две крепости', 'Джон Рональд Руэл', 'Толкин', 'АСТ', 415, 'Фэнтези', 1954, 500.00, 700.00, 'Да', 'Властелин колец: Братство Кольца', 'Доступно', '1954-01-01'),
('Властелин колец: Возвращение короля', 'Джон Рональд Руэл', 'Толкин', 'АСТ', 347, 'Фэнтези', 1955, 500.00, 700.00, 'Да', 'Властелин колец: Две крепости', 'Доступно', '1955-01-01'),
('Над пропастью во ржи', 'Джером Дэвид', 'Сэлинджер', 'Эксмо', 277, 'Роман', 1951, 300.00, 450.00, 'Нет', 'Нет', 'Доступно', '1951-01-01');
GO





SELECT * FROM Books;
GO


Select * from Sales
Select * from Promotions

select * from Sales

select * from Users