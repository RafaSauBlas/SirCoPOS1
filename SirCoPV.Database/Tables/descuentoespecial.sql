CREATE TABLE [dbo].[descuentoespecial] --nueva tabla
(
	[iddescuentoespecial] INT NOT NULL, 
    [razon] VARCHAR(200) NOT NULL, 
    [descuento] TINYINT NOT NULL, 
    [devolucion] BIT NOT NULL, 
    CONSTRAINT [PK_descuentoespecial] PRIMARY KEY ([iddescuentoespecial]) 
)
