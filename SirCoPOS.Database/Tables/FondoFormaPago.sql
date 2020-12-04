CREATE TABLE [dbo].[FondoFormaPago]
(
	[Id] INT NOT NULL IDENTITY, 
    [FondoId] INT NOT NULL, 
    [Entrada] BIT NOT NULL, 
    [FormaPago] INT NOT NULL,     
    [Cantidad] INT NOT NULL,     
    [UsuarioId] INT NULL, 
    [Monto] MONEY NOT NULL, 
    [Fecha] DATETIME NOT NULL, 
    [Referencia] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_FondoFormaPago] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_FondoFormaPago_Fondo] FOREIGN KEY ([FondoId]) REFERENCES [Fondo]([Id]) 
)
