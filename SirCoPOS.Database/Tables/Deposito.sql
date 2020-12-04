CREATE TABLE [dbo].[Deposito]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UsuarioId] INT NOT NULL, 
    [Importe] MONEY NOT NULL, 
    [Banco] VARCHAR(50) NOT NULL, 
    [Folio] VARCHAR(50) NOT NULL, 
    [Fecha] DATETIME NOT NULL, 
    [Referencia] UNIQUEIDENTIFIER NOT NULL
)
