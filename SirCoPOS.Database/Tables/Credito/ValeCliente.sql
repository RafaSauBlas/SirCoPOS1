CREATE TABLE [Credito].[ValeCliente]
(
	[vale] VARCHAR(14) NOT NULL,
	[iddistrib] INT NOT NULL,	
	[idcliente] INT NOT NULL, 
    [cantidad] MONEY NOT NULL, 
    [electronica] BIT NOT NULL, 
    [date] DATETIME NOT NULL, 
    [idusuario] INT NOT NULL, 
    CONSTRAINT [PK_Vale] PRIMARY KEY ([vale]) 
)
