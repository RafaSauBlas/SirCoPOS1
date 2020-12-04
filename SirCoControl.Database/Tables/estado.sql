CREATE TABLE [dbo].[estado](
	[idestado] [int] IDENTITY(1,1) NOT NULL,
	[estado] [varchar](50) NOT NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_estados] PRIMARY KEY CLUSTERED 
(
	[idestado] ASC,
	[estado] ASC
)
)
GO


