-- في master: إنشاء login على مستوى السيرفر
USE [master];
GO

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'ccms_user')
BEGIN
    CREATE LOGIN [ccms_user] WITH PASSWORD = N'ChangeThisStrong@123!', CHECK_POLICY = OFF;
END
ELSE
BEGIN
    ALTER LOGIN [ccms_user] WITH PASSWORD = N'ChangeThisStrong@123!';
END
GO

-- إنشاء القاعدة لو مش موجودة
IF DB_ID('CCMS') IS NULL
BEGIN
    CREATE DATABASE CCMS;
END
GO

-- اعطاء user على قاعدة CCMS وصلاحية dbo
USE CCMS;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ccms_user')
BEGIN
    CREATE USER [ccms_user] FOR LOGIN [ccms_user];
END
ELSE
BEGIN
    ALTER USER [ccms_user] WITH LOGIN = [ccms_user];
END

EXEC sp_addrolemember N'db_owner', N'ccms_user';
GO
