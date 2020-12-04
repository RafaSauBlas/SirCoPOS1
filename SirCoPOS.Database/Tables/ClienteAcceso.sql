CREATE TABLE [dbo].[ClienteAcceso]
(
    [ClienteId] INT NOT NULL, 
    [Codigo] UNIQUEIDENTIFIER NOT NULL, 
    [FechaExpiracion] DATETIME NOT NULL, 
    CONSTRAINT [PK_ClienteAcceso] PRIMARY KEY ([ClienteId]), 
    CONSTRAINT [AK_ClienteAcceso_Codigo] UNIQUE ([Codigo]) 
)
