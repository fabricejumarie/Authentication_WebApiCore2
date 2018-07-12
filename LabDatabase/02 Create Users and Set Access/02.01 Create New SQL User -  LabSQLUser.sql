USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [LabSQLAccount]    Script Date: 2018/07/12 14:27:05 ******/
CREATE LOGIN [LabSQLUser] WITH PASSWORD=N'29gLf7T+2Hhmqv0a35VDUAqPaLkB90vMkaAIB9pttBk=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
-- Note the password is Test123! for this example