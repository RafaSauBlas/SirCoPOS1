CREATE TABLE [dbo].[NotaPago]
(
	[Id] INT NOT NULL IDENTITY, 
    [NotaId] INT NOT NULL,
    [FormaPagoId] INT NOT NULL, 
    [Amount] MONEY NOT NULL, 
    CONSTRAINT [PK_NotaPago] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_NotaPago_Nota] FOREIGN KEY ([NotaId]) REFERENCES [Nota]([Id]),     
)
