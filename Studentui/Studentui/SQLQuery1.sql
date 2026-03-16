USE master;
Go

if not exists (select name from sys.databases where name='StudentDB')
begin
	create database StudentDB;
end 
go

use StudentDB;
Go 

if object_id('dbo.Students','U') is not null
	drop table dbo.Students;
Go

create table dbo.Students(
StudentId INT NOT NULL IDENTITY(1,1) Primary Key,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
Email NVARCHAR(50) NOT NULL UNIQUE,
Phone NVARCHAR(20) NULL,
DateOfBirth DATE NULL,
Course NVARCHAR(100) Not NULL,
EnrollDate Date Not Null Default GetDate(),
IsActive BIT Not NULL Default 1
);
Go

If Object_ID('dbo.sp_GetAllStudents','P') is not null
	drop Procedure dbo.sp_GetAllStudents;
Go

create procedure dbo.sp_GetAllStudents
as
begin
	set nocount on;
	select StudentId,FirstName,LastName,Email,Phone,DateOfBirth,Course,EnrollDate,IsActive
	from dbo.Students
	order by LastName,FirstName;
end
Go

if object_id('dbo.sp_GetStudentById','P') is not null
	drop procedure dbo.sp_GetStudentById;
Go

create procedure dbo.sp_GetStudentById
	@StudentId INT
as
begin 
	set nocount on;
	select StudentId,FirstName,LastName,Email,Phone,DateOfBirth,Course,EnrollDate,IsActive
	from dbo.Students
	where StudentId=@StudentId;
end
Go

if object_id('dbo.sp_CreateStudent','P')is not null
	drop procedure dbo.sp_CreateStudent;
Go
create procedure dbo.sp_CreateStudent
	@FirstName   NVARCHAR(50),
    @LastName    NVARCHAR(50),
    @Email       NVARCHAR(100),
    @Phone       NVARCHAR(20)   = NULL,
    @DateOfBirth DATE           = NULL,
    @Course      NVARCHAR(100),
    @EnrollDate  DATE           = NULL
as
begin 
	set nocount on;
	if @EnrollDate is null set @EnrollDate=cast(GetDate() as Date);
	insert into dbo.Students (FirstName,LastName,Email,Phone,DateOfBirth,Course,EnrollDate,IsActive)
	values (@FirstName,@LastName,@Email,@Phone,@DateOfBirth,@Course,@EnrollDate,1);
	select SCOPE_IDENTITY() as NewStudentId;
end
Go

if object_id('dbo.sp_UpdateStudent','P') is not null
	drop procedure dbo.sp_UpdateStudent;
go

create procedure dbo.sp_UpdateStudent
	@StudentId   INT,
    @FirstName   NVARCHAR(50),
    @LastName    NVARCHAR(50),
    @Email       NVARCHAR(100),
    @Phone       NVARCHAR(20)  = NULL,
    @DateOfBirth DATE          = NULL,
    @Course      NVARCHAR(100),
    @EnrollDate  DATE,
    @IsActive    BIT
as
begin
	set nocount on;
	update dbo.Students
	set FirstName=@FirstName,
		LastName=@LastName,
		Email       = @Email,
        Phone       = @Phone,
        DateOfBirth = @DateOfBirth,
        Course      = @Course,
        EnrollDate  = @EnrollDate,
        IsActive    = @IsActive
    WHERE StudentId = @StudentId;
end
Go

if object_id('dbo.sp_DeleteStudent','P') is not null
	drop procedure dbo.sp_DeleteStudent;
go

create procedure dbo.sp_DeleteStudent
	@StudentId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Students WHERE StudentId = @StudentId;
END
GO

INSERT INTO dbo.Students (FirstName, LastName, Email, Phone, DateOfBirth, Course, EnrollDate, IsActive)
VALUES
    ('Aarav',   'Sharma',   'aarav.sharma@example.com',   '9876543210', '2002-03-15', 'Computer Science',  '2023-07-01', 1),
    ('Priya',   'Verma',    'priya.verma@example.com',    '9123456780', '2001-11-22', 'Information Tech',  '2022-07-01', 1),
    ('Rohan',   'Singh',    'rohan.singh@example.com',    NULL,         '2003-05-08', 'Electronics',       '2023-07-01', 1),
    ('Sneha',   'Patel',    'sneha.patel@example.com',    '9988776655', '2000-08-30', 'Mechanical Engg',   '2021-07-01', 0),
    ('Karan',   'Mehta',    'karan.mehta@example.com',    '9012345678', '2002-01-19', 'Civil Engineering', '2023-07-01', 1);
GO

PRINT 'Database setup complete. 5 stored procedures created. 5 sample students inserted.';

EXEC sp_GetAllStudents;