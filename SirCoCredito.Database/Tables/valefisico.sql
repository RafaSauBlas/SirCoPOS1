CREATE TABLE [dbo].[valefisico]
(
	[vale] CHAR(10) NOT NULL, 
    [negocio] CHAR(2) NOT NULL, 
    [limite] MONEY NOT NULL, 
    [disponible] MONEY NOT NULL, 
    [iddistrib] INT NOT NULL, 
    [fecha] DATETIME NOT NULL, 
    CONSTRAINT [PK_valefisico] PRIMARY KEY ([vale], [negocio]) 
)
