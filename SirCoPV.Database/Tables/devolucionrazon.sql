CREATE TABLE [dbo].[devolucionrazon] -- nueva tabla
(
	[iddevolucionrazon] INT NOT NULL , 
    [descripcion] VARCHAR(200) NOT NULL, 
    [comentarios] BIT NOT NULL, 
    CONSTRAINT [PK_devolucionrazon] PRIMARY KEY ([iddevolucionrazon])
)
