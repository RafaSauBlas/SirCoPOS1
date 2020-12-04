CREATE TABLE [dbo].[huellaspos]
(
	[id] INT NOT NULL IDENTITY, 
    [idempleado] INT NOT NULL, 
    [template] VARBINARY(400) NOT NULL, 
    CONSTRAINT [PK_huellaspos] PRIMARY KEY ([id]) 
)
