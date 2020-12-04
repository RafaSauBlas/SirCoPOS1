CREATE TABLE [dbo].[FondoArqueoSerie]
(
	[FondoArqueoId] INT NOT NULL , 
    [Serie] VARCHAR(50) NOT NULL, 
    [Reportado] DATETIME NULL, 
    [UsuarioId] INT NULL, 
    CONSTRAINT [PK_FondoArqueoSerie] PRIMARY KEY ([FondoArqueoId], [Serie]) 
)
