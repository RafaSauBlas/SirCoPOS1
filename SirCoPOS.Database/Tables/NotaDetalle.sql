CREATE TABLE [dbo].[NotaDetalle]
(
	[NotaId] INT NOT NULL , 
    [Serie] VARCHAR(50) NOT NULL, 
    [Amount] MONEY NOT NULL, 
    [Coments] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_NotaDetalle_Nota] FOREIGN KEY ([NotaId]) REFERENCES [Nota]([Id]), 
    CONSTRAINT [PK_NotaDetalle] PRIMARY KEY ([NotaId], [Serie])
)
