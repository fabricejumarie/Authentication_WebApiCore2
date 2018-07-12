USE [LabDatabase]
GO

/****** Object:  User [LabSQLUser]    Script Date: 2018/07/12 15:08:59 ******/
CREATE USER [LabSQLUser] FOR LOGIN [LabSQLUser] WITH DEFAULT_SCHEMA=[dbo]
GO

ALTER ROLE db_datareader ADD MEMBER [LabSQLUser]
GO

ALTER ROLE db_datawriter ADD MEMBER [LabSQLUser]
GO