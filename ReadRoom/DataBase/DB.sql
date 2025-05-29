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
    PrequelBookTitle NVARCHAR(255) DEFAULT '���', 
    [Status] NVARCHAR(50) DEFAULT '��������',
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
('����� � ���', '���', '�������', '���', 1225, '�����', 1869, 500.00, 800.00, '���', '���', '��������', '1869-01-01'),
('���� ��������', '���', '�������', '���', 864, '�����', 1877, 450.00, 750.00, '���', '���', '��������', '1877-01-01'),
('451 ������ �� ����������', '���', '��������', '������', 192, '����������', 1953, 200.00, 350.00, '���', '���', '��������', '1953-01-01'),
('����', '�����', '�������', '�����', 688, '����������', 1965, 600.00, 950.00, '���', '���', '��������', '1965-01-01'),
('����� ������ � ����������� ������', '�����', '�������', '������', 320, '�������', 1997, 350.00, 500.00, '���', '���', '��������', '1997-01-01'),
('����� ������ � ������ �������', '�����', '�������', '������', 352, '�������', 1998, 350.00, 500.00, '��', '����� ������ � ����������� ������', '��������', '1998-01-01'),
('����� ������ � ����� ��������', '�����', '�������', '������', 435, '�������', 1999, 400.00, 550.00, '��', '����� ������ � ������ �������', '��������', '1999-01-01'),
('������ � ���������', '������', '��������', '�����', 480, '�����', 1967, 300.00, 450.00, '���', '���', '��������', '1967-01-01'),
('������������ � ���������', 'Ը���', '�����������', '���', 672, '�����', 1866, 400.00, 600.00, '���', '���', '��������', '1866-01-01'),
('�����', 'Ը���', '�����������', '�����', 832, '�����', 1869, 450.00, 650.00, '���', '���', '��������', '1869-01-01'),
('���� �����-������', '���������', '����', '�����', 1276, '�����������', 1844, 550.00, 850.00, '���', '���', '��������', '1844-01-01'),
('���� � ����', '����', '��������', '������', 320, '�����', 1862, 250.00, 400.00, '���', NULL, '��������', '1862-01-01'),
('��������� �����: �������� ������', '���� ������� ����', '������', '���', 479, '�������', 1954, 500.00, 700.00, '���', '���', '��������', '1954-01-01'),
('��������� �����: ��� ��������', '���� ������� ����', '������', '���', 415, '�������', 1954, 500.00, 700.00, '��', '��������� �����: �������� ������', '��������', '1954-01-01'),
('��������� �����: ����������� ������', '���� ������� ����', '������', '���', 347, '�������', 1955, 500.00, 700.00, '��', '��������� �����: ��� ��������', '��������', '1955-01-01'),
('��� ��������� �� ���', '������ �����', '���������', '�����', 277, '�����', 1951, 300.00, 450.00, '���', '���', '��������', '1951-01-01');
GO





SELECT * FROM Books;
GO


Select * from Sales
Select * from Promotions

select * from Sales

select * from Users