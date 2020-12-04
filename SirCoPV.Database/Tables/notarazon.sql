CREATE TABLE [dbo].[notarazon] -- nueva tabla
(
	[idnotarazon] INT NOT NULL , 
    [descripcion] VARCHAR(200) NOT NULL, 
    [comentarios] BIT NOT NULL, 
    CONSTRAINT [PK_notarazon] PRIMARY KEY ([idnotarazon])
)
