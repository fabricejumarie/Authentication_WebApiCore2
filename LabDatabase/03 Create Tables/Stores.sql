﻿CREATE TABLE [dbo].[Stores]
(
	[StoreId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NULL, 
    [Adress] VARCHAR(MAX) NULL, 
    [City] VARCHAR(100) NULL
)
