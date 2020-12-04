CREATE TABLE [dbo].[Nota]
(
	[Id] INT NOT NULL IDENTITY, 
    [Date] DATETIME NOT NULL, 
    [Sucursal] VARCHAR(2) NOT NULL, 
    [Venta] VARCHAR(6) NULL, 
    [CajeroId] INT NOT NULL, 
    [VendedorId] INT NULL, 
    [Data] XML NOT NULL, 
    [Multiple] BIT NOT NULL, 
    CONSTRAINT [PK_Nota] PRIMARY KEY ([Id]) 
)
