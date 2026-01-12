USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'UserManagementDB')
BEGIN
    CREATE DATABASE UserManagementDB;
END
GO

USE UserManagementDB;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Users](
        [Id]            INT IDENTITY(1,1) NOT NULL,
        [Code]          VARCHAR(50) NOT NULL,
        [FullName]      NVARCHAR(100) NOT NULL,
        [DateOfBirth]   DATE NULL,
        [Email]         VARCHAR(100) NULL,
        [PhoneNumber]   VARCHAR(20) NULL,
        [Address]       NVARCHAR(500) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [UK_Users_Code] UNIQUE ([Code]),
        CONSTRAINT [UK_Users_Email] UNIQUE ([Email])
    );
END
GO

IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[Users])
BEGIN
    INSERT INTO [dbo].[Users] (Code, FullName, DateOfBirth, Email, PhoneNumber, Address)
    VALUES 
    ('NV001', N'Nguyễn Văn A', '1995-01-15', 'vana@example.com', '0901234567', N'123 Đường Láng, Hà Nội'),
    ('NV002', N'Trần Thị B', '1998-11-20', 'tranthib@example.com', '0912345678', N'456 Lê Lợi, Quận 1, TP.HCM');
END
GO

