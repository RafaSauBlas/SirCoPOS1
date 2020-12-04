CREATE TABLE [dbo].[FondoArqueo]
(
	[Id] INT NOT NULL IDENTITY, 
    [FondoId] INT NOT NULL,
    [AuditorId] INT NOT NULL, 
    [Fecha] DATETIME NOT NULL, 
    [Importe] MONEY NOT NULL,
    [Reportado] MONEY NOT NULL, 
    [Corte] BIT NOT NULL, 
    [Faltante] AS ([Importe] - [Reportado])
    CONSTRAINT [PK_FondoArqueo] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_FondoArqueo_Fondo] FOREIGN KEY ([FondoId]) REFERENCES [Fondo]([Id])    
)
