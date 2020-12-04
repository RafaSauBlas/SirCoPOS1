CREATE TABLE [dbo].[FondoMovimiento]
(
	[Id] INT NOT NULL IDENTITY, 
    [FondoId] INT NOT NULL, 
    [Importe] MONEY NOT NULL, 
    [UsuarioId] INT NULL, 
    [Entrada] BIT NOT NULL, 
    [Fecha] DATETIME NOT NULL, 
    [Referencia] UNIQUEIDENTIFIER NOT NULL, 
    [Tipo] VARCHAR(20) NULL, 
    CONSTRAINT [PK_FondoMovimiento] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_FondoMovimiento_Fondo] FOREIGN KEY ([FondoId]) REFERENCES [Fondo]([Id]) 
)

GO

CREATE INDEX [IX_FondoMovimiento_Referencia] ON [dbo].[FondoMovimiento] ([Referencia])
