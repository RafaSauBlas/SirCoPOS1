CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL IDENTITY, 
    [Date] DATETIME NOT NULL, 
    [Action] VARCHAR(250) NOT NULL, 
    [Data] XML NOT NULL, 
    [GID] UNIQUEIDENTIFIER NOT NULL, 
    [IsRequest] BIT NOT NULL, 
    [UserId] INT NULL, 
    [Sucursal] VARCHAR(2) NULL, 
    [Machine] VARCHAR(50) NULL, 
    CONSTRAINT [PK_Message] PRIMARY KEY ([Id]) 
)
