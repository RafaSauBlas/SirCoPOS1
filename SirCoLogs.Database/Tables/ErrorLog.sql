CREATE TABLE [dbo].[ErrorLog]
(
	[Id] INT NOT NULL IDENTITY, 
    [Date] DATETIME NOT NULL, 
    [Application] VARCHAR(50) NOT NULL,
	[MachineName] VARCHAR(50) NOT NULL, 	
    [Level] VARCHAR(50) NOT NULL, 
	[Logger] VARCHAR(250) NULL, 
	[Type] VARCHAR(250) NULL,
	[Module] VARCHAR(50) NULL,
    [Callsite] VARCHAR(MAX) NULL, 
	[Message] VARCHAR(MAX) NULL, 
    [Exception] VARCHAR(MAX) NULL,         
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY ([Id]) 
)
