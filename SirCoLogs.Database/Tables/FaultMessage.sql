CREATE TABLE [dbo].[FaultMessage]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,     
    [StartDate] DATETIME NOT NULL, 
    [Action] VARCHAR(250) NOT NULL, 
    [Request] XML NOT NULL, 
    [EndDate] DATETIME NULL, 
    [Response] XML NULL,     
    CONSTRAINT [PK_FaultMessage] PRIMARY KEY ([Id]) 
)
