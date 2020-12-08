CREATE TABLE [dbo].[Caja]
(
    [Sucursal] VARCHAR(2) NOT NULL, 
	[Numero] TINYINT NOT NULL, 
    [Disponible] DECIMAL(18, 2) NOT NULL, 
    [Tipo] TINYINT NOT NULL, 
    [ResponsableId] INT NULL, 
    CONSTRAINT [PK_Caja] PRIMARY KEY ([Sucursal], [Numero])
)
